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
using DocumentFormat.OpenXml.Wordprocessing;
using System.Net;
using Newtonsoft.Json;

namespace AutoDoc.Controllers
{
    [Route("api/Document")]
    [EnableCors("EnableCors")]
    public class DocumentController : DefaultController
    {
        private IHostingEnvironment _hostingEnvironment;
        private IDocumentService _documentService;
        private IBookmarkService _bookmarkService;
        public DocumentMapper _documentMapper;
        public BookmarkMapper _bookmarkMapper;

        public DocumentController(IHostingEnvironment hostingEnvironment,
            IDocumentService documentService,
            IBookmarkService bookmarkService,
            DocumentMapper documentMapper,
            BookmarkMapper bookmarkMapper)
        {
            _hostingEnvironment = hostingEnvironment;
            _documentService = documentService;
            _bookmarkService = bookmarkService;
            _documentMapper = documentMapper;
            _bookmarkMapper = bookmarkMapper;
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<DocumentJsonModel> UploadFile(IFormFile file)
        {
            if (file == null) throw new Exception("File is null");
            if (file.Length == 0) throw new Exception("File is empty");

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "AppData");
            var fileHashName = file.GetHashCode().ToString();
            var filePath = Path.Combine(uploads, fileHashName + ".docx");
            int ParentId = 0;

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var doc = DocumentCore.OpenDocument(filePath);
            var bookmarksList = WordBookmarkParser.FindAllBookmarks(doc);

            var bookmarks = _bookmarkMapper.ToBookmarks(bookmarksList);
            var document = _documentMapper.ToDocument(file.FileName, filePath, bookmarks);
            _documentService.CreateDocument(document);

            var documentLast = _documentService.GetAll().LastOrDefault();
            var documentJsonModel = _documentMapper.ToDocumentJsonModel(documentLast);

            DocumentCore.CloseDocument(doc);

            return documentJsonModel;
        }

        [HttpPost]
        [Route("UploadFiles")]
        public async Task<DocumentJsonModel> UploadFiles(IFormFile file)
        {
            //if (file == null) throw new Exception("File is null");
            //if (file.Length == 0) throw new Exception("File is empty");

            //var fileHashName = file.GetHashCode().ToString();
            //var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "AppData");
            //var filePath = Path.Combine(uploads, fileHashName + ".docx");
            //int ParentId = 0;

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

            var bookmarkNames = new Dictionary<string, BookmarkEnd>();

            bookmarkNames = _bookmarkParser.FindBookmarks(docFile.MainDocumentPart.Document);

            if (bookmarkNames != null)
            {
                foreach (var bookmarkName in bookmarkNames.Keys)
                {
                    Bookmark bookmarkEntity = new Bookmark
                    {
                        Name = bookmarkName,
                        MessageJson = string.Empty,
                        DocumentId = id
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

        [HttpPost]
        [Route("ReplaceBookmarks")]
        public int ReplaceBookmarks([FromBody]DocumentJsonModel documentJsonModel)
        {
            var document = _documentService.GetDocument(documentJsonModel.Id);

            WordprocessingDocument doc = DocumentCore.OpenDocument(document.Path);
            var bookMarks = WordBookmarkParser.FindBookmarks(doc.MainDocumentPart.Document);

            foreach (var bookmark in documentJsonModel.Bookmarks)
            {
                switch (bookmark.Type)
                {
                    case 1:
                        if (bookmark.Message.GetType() != typeof(string)) throw new Exception("Not my type!");

                        Text text = TextUtil.GetText(bookmark.Message);
                        WordBookmarkParser.ReplaceBookmarkSecondMethod(bookMarks, bookmark.Name, text, doc.MainDocumentPart);

                        //bookmarkDb.MessageJson = bookmark.Message;

                        //_bookmarkService.EditBookmark(bookmarkDb);
                        //_bookmarkParser.ReplaceBookmark(bookmarkNames, bookmark.Name, new TextUtil().GetText(bookmark.Message.ToString()), docFile.MainDocumentPart);
                        break;
                    case 2:
                        var dnmk = bookmark.Message;

                        //var tableData = JsonConvert.SerializeObject(bookmark.Message);
                        var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                        var serializeObject = JsonConvert.SerializeObject(bookmark.Message, settings);

                        Table table = WordBookmarkParser.CreateTableMain();
                        WordBookmarkParser.ReplaceBookmarkSecondMethod(bookMarks, bookmark.Name, table, doc.MainDocumentPart);

                        //AutoDoc.DAL.Models.Table table = JsonConvert.DeserializeObject<AutoDoc.DAL.Models.Table>(bookmark.Message);
                        //bookmarkDbTable.MessageJson = JsonConvert.SerializeObject(table);

                        //_bookmarkService.EditBookmark(bookmarkDbTable);
                        //_bookmarkParser.ReplaceBookmark(bookmarkNames, bookmark.Name, new TableUtil().GetTable(table), docFile.MainDocumentPart); //TODO table
                        break;
                    default: break;
                }
            }

            foreach (var value in documentJsonModel.Bookmarks)
            {
                Table table = WordBookmarkParser.CreateTableMain();
                var text = TextUtil.GetText(value.Message);

                //WordBookmarkParser.ReplaceBookmarkSecondMethod(bookMarks, value.Name, text, doc.MainDocumentPart);

                WordBookmarkParser.ReplaceBookmarkSecondMethod(bookMarks, value.Name, table, doc.MainDocumentPart);
            }
            DocumentCore.CloseDocument(doc);

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