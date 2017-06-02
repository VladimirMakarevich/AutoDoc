using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Hosting;
using AutoDoc.BL.Core;
using AutoDoc.BL.Parsers;
using AutoDoc.DAL.Context;
using AutoDoc.DAL.Entities;
using AutoDoc.DAL.Repository;
using AutoDoc.DAL.Services;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Cors;
using AutoDoc.Models;
using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using AutoDoc.BL.ModelsUtilities;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.VariantTypes;

namespace AutoDoc.Controllers
{
    [Route("api/Document")]
    public class DocumentController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        private IBookmarkService _bookmarkService;
        private IDocumentService _documentService;
        private IMapper _mapper;
        private ITableUtil _tableUtil;
        private ITextUtil _textUtil;
        private IImageUtil _imageUtil;
        private IDocumentCore _documentCore;
        private IWordBookmarkParser _bookmarkParser;
        private IWordTagParser _tagParser;

        public DocumentController(
            IHostingEnvironment hostingEnvironment, 
            IDocumentService documentService, 
            IBookmarkService bookmarkService, 
            IMapper mapper,
            ITableUtil tableUtil,
            ITextUtil textUtil,
            IImageUtil imageUtil,
            IDocumentCore documentCore,
            IWordBookmarkParser bookmarkParser,
            IWordTagParser tagParser)
        {
            _hostingEnvironment = hostingEnvironment;
            _bookmarkService = bookmarkService;
            _documentService = documentService;
            _mapper = mapper;
            _tableUtil = tableUtil;
            _textUtil = textUtil;
            _imageUtil = imageUtil;
            _documentCore = documentCore;
            _bookmarkParser = bookmarkParser;
            _tagParser = tagParser;
        }

        [HttpPost]
        [Route("UploadFiles")]
        public async Task<DocumentJsonModel> UploadFiles(IFormFile file)
        {
            if (file == null) throw new Exception("File is null");
            if (file.Length == 0) throw new Exception("File is empty");

            var fileHashName = file.GetHashCode().ToString();
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "AppData");
            var filePath = Path.Combine(uploads, fileHashName + ".docx");
            int ParentId = 0;

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var docFile = _documentCore.OpenDocument(filePath);


            var customProps = docFile.CustomFilePropertiesPart;
            if (customProps != null)
            {
                var props = customProps.Properties;
                if (props != null)
                {
                    var prop = props.FirstOrDefault(p => ((CustomDocumentProperty)p).Name.Value == "ParentId");
                    if (prop != null) Int32.TryParse(((CustomDocumentProperty)prop).InnerText, out ParentId); //Int32.TryParse(prop.InnerText, out ParentId);
                }
            }


            var doc = new DAL.Entities.Document
            {
                Id = ParentId,
                Name = file.FileName,
                Path = filePath
            };
            int id = _documentService.CreateDocument(doc);
            if (id != ParentId)
            {
                var customPropsAdd = docFile.CustomFilePropertiesPart;
                if (customPropsAdd == null)
                {
                    var customFilePropPart = docFile.AddCustomFilePropertiesPart();

                    customFilePropPart.Properties = new DocumentFormat.OpenXml.CustomProperties.Properties();
                    var customProp = new CustomDocumentProperty();
                    customProp.Name = "ParentId";
                    customProp.FormatId = "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}";
                    customProp.VTLPWSTR = new VTLPWSTR(id.ToString());

                    customFilePropPart.Properties.AppendChild(customProp);
                    int pid = 2;
                    foreach (CustomDocumentProperty item in customFilePropPart.Properties)
                    {
                        item.PropertyId = pid++;
                    }
                    customFilePropPart.Properties.Save();

                    //customFilePropPart.Properties.ToList().Add(customProp);
                    //customFilePropPart.Properties.Save();

                }
            }

            var bookmarkNames = _bookmarkParser.FindBookmarks(docFile);

            if (bookmarkNames != null)
            {
                foreach (var bookmarkName in bookmarkNames)
                {
                    Bookmark bookmarkEntity = new Bookmark
                    {
                        Name = bookmarkName.BookmarkData.Key,
                        MessageJson = bookmarkName.Message,
                        DocumentId = id,
                        Type = bookmarkName.BookmarkType
                    };
                    int bookmarkId = _bookmarkService.CreateBookmark(bookmarkEntity);
                }
            }

            var docJson = new DocumentJsonModel
            {
                Name = doc.Name,
                Path = doc.Path,
                Id = id
            };

            _documentCore.CloseDocument(docFile);

            return docJson;
        }

        [HttpGet]
        [Route("DownloadFiles")]
        public FileContentResult DownloadFiles(int id)
        {
            DAL.Entities.Document doc = _documentService.GetDocument(id);

            var fileByteArray = System.IO.File.ReadAllBytes(doc.Path);
            doc.Name = doc.Name.Replace(" ", "_");
            FileContentResult file = new FileContentResult(fileByteArray, "application/x-msdownload; " + doc.Name)
            {              
                FileDownloadName = WebUtility.UrlEncode(doc.Name)
            };
            return file;
        }
    }
}
