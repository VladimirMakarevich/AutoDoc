using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using AutoDoc.BL.Core;
using AutoDoc.BL.Parsers;
using Microsoft.AspNetCore.Cors;
using AutoDoc.Models;
using AutoDoc.BL.ModelsUtilities;
using AutoDoc.Mappers;
using AutoDoc.DAL.Services;
using DocumentFormat.OpenXml.Packaging;
using System.Net;
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.VariantTypes;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Wordprocessing;
using AutoDoc.DAL.Entities;
using Newtonsoft.Json.Linq;
using DocumentFormat.OpenXml;

namespace AutoDoc.Controllers
{
    [Route("api/Document")]
    [EnableCors("EnableCors")]
    public class DocumentController : DefaultController
    {
        public DocumentController(IHostingEnvironment hostingEnvironment,
            IDocumentService documentService,
            IBookmarkService bookmarkService,
            ITableUtil tableUtil,
            ITextUtil textUtil,
            DocumentMapper documentMapper,
            BookmarkMapper bookmarkMapper,
            IDocumentCore documentCore,
            IWordBookmarkParser bookmarkParser)
        {
            _hostingEnvironment = hostingEnvironment;
            _documentService = documentService;
            _bookmarkService = bookmarkService;
            _tableUtil = tableUtil;
            _textUtil = textUtil;
            _documentMapper = documentMapper;
            _bookmarkMapper = bookmarkMapper;
            _documentCore = documentCore;
            _bookmarkParser = bookmarkParser;
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<DocumentJsonModel> UploadFile(IFormFile file)
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
                    var prop = props.Where(p => ((CustomDocumentProperty)p).Name.Value == "ParentId").FirstOrDefault();
                    if (prop != null) Int32.TryParse(((CustomDocumentProperty)prop).InnerText, out ParentId);
                }
            }

            var document = _documentMapper.ToDocument(file.FileName, filePath, ParentId);
            var id = _documentService.CreateDocument(document);

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
                }
            }

            var bookmarkNames = _bookmarkParser.FindBookmarks(docFile);

            var bookmarksEntitiesCheck = _bookmarkService.GetAllBookmarksByDocument(id).ToList();

            if (!bookmarksEntitiesCheck.Any())
            {
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

                        _bookmarkService.CreateBookmark(bookmarkEntity);
                    }
                }
            }

            var bookmarksEntities = _bookmarkService.GetAllBookmarksByDocument(id).ToList();
            List<BookmarkJsonModel> responseBookmarksJsonModels = new List<BookmarkJsonModel>();

            foreach (var bookmartEntity in bookmarksEntities)
            {
                switch (bookmartEntity.Type)
                {
                    case 1:
                        var bookmark = _bookmarkMapper.ToBookmarkJsonModel(bookmartEntity);
                        bookmark.Message = bookmartEntity.MessageJson;

                        responseBookmarksJsonModels.Add(bookmark);
                        break;

                    case 2:
                        var bookmarkTable = _bookmarkMapper.ToBookmarkJsonModel(bookmartEntity);
                        bookmarkTable.Message = JObject.Parse(bookmartEntity.MessageJson) as JObject;

                        responseBookmarksJsonModels.Add(bookmarkTable);
                        break;
                    case 3:
                        var bookmarkPic = _bookmarkMapper.ToBookmarkJsonModel(bookmartEntity);
                        bookmarkPic.Message = bookmartEntity.MessageJson;

                        responseBookmarksJsonModels.Add(bookmarkPic);
                        break;
                    default: break;
                }

            }

            var documentJsonModel = new DocumentJsonModel
            {
                Name = document.Name,
                Path = document.Path,
                Id = id,
                Bookmarks = responseBookmarksJsonModels
            };

            _documentCore.CloseDocument(docFile);

            return documentJsonModel;
        }

        [HttpPost]
        [Route("ReplaceBookmarks")]
        public int ReplaceBookmarks([FromBody]DocumentJsonModel documentJsonModel)
        {
            var document = _documentService.GetDocument(documentJsonModel.Id);

            WordprocessingDocument doc = _documentCore.OpenDocument(document.Path);

            var bookmarkNames = _bookmarkParser.FindBookmarks(doc);

            foreach (var bookmark in documentJsonModel.Bookmarks)
            {
                switch (bookmark.Type)
                {
                    case 1:

                        var bookmarkDb = _bookmarkMapper.ToBookmark(bookmark);

                        if (bookmark.Message.GetType() != typeof(string))
                            throw new Exception("Not my type!");

                        bookmarkDb.MessageJson = bookmark.Message;

                        _bookmarkService.EditBookmark(bookmarkDb);
                        _bookmarkParser.ReplaceBookmark(bookmarkNames.Find(name => name.BookmarkData.Key == bookmark.Name).BookmarkData,
                            _textUtil.GetText(bookmark.Message.ToString()), doc.MainDocumentPart);
                        break;
                    case 2:

                        var bookmarkDbTable = _bookmarkMapper.ToBookmark(bookmark);

                        bookmarkDbTable.MessageJson = bookmark.Message.ToString();

                        _bookmarkService.EditBookmark(bookmarkDbTable);
                        _bookmarkParser.ReplaceBookmark(bookmarkNames.Find(name => name.BookmarkData.Key == bookmark.Name).BookmarkData,
                            _tableUtil.GetTable(bookmark.Message.ToString()), doc.MainDocumentPart);
                        break;
                    default:
                        break;
                }
            }

            _documentCore.CloseDocument(doc);

            return document.Id;
        }

        [HttpGet]
        [Route("DownloadDocument/{documentId}")]
        public FileContentResult DownloadDocument(int documentId)
        {
            var document = _documentService.GetDocument(documentId);

            var fileByteArray = System.IO.File.ReadAllBytes(document.Path);

            FileContentResult file = new FileContentResult(fileByteArray, "application/x-msdownload; " + document.Name)
            {
                FileDownloadName = WebUtility.UrlEncode(document.Name)
            };

            return file;
        }
    }
}