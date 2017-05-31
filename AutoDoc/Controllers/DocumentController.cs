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
using DocumentFormat.OpenXml.CustomProperties;
using DocumentFormat.OpenXml.VariantTypes;

namespace AutoDoc.Controllers
{
    [Route("api/Document")]
    [EnableCors("EnableCors")]
    public class DocumentController : DefaultController
    {
        private IHostingEnvironment _hostingEnvironment;
        private IDocumentService _documentService;
        private IBookmarkService _bookmarkService;
        private IDocumentCore _documentCore;
        public IWordBookmarkParser _bookmarkParser;
        public DocumentMapper _documentMapper;
        public BookmarkMapper _bookmarkMapper;
        private int parentId = 0;


        public DocumentController(IHostingEnvironment hostingEnvironment,
            IDocumentService documentService,
            IBookmarkService bookmarkService,
            DocumentMapper documentMapper,
            BookmarkMapper bookmarkMapper,
            IDocumentCore documentCore,
            IWordBookmarkParser bookmarkParser)
        {
            _hostingEnvironment = hostingEnvironment;
            _documentService = documentService;
            _bookmarkService = bookmarkService;
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

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "AppData");
            var fileHashName = file.GetHashCode().ToString();
            var filePath = Path.Combine(uploads, fileHashName + ".docx");

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var doc = _documentCore.OpenDocument(filePath);

            try
            {
                var customProps = doc.CustomFilePropertiesPart;
                if (customProps != null)
                {
                    var props = customProps.Properties;
                    if (props != null)
                    {
                        var prop = props.Where(p => ((CustomDocumentProperty)p).Name.Value == "parentId").FirstOrDefault();
                        if (prop != null) Int32.TryParse(((CustomDocumentProperty)prop).InnerText, out parentId);
                    }
                }

                var bookmarksList = _bookmarkParser.FindAllBookmarks(doc);
                //_documentCore.CloseDocument(doc);
                var bookmarks = _bookmarkMapper.ToBookmarks(bookmarksList);
                var document = _documentMapper.ToDocument(file.FileName, filePath, bookmarks);
                var id = _documentService.CreateDocument(document);
                //_documentCore.CloseDocument(doc);
                if (id != parentId)
                {
                    //_documentCore.CloseDocument(doc);
                    //var doc = _documentCore.OpenDocument(filePath);
                    var customPropsAdd = doc.CustomFilePropertiesPart;
                    if (customPropsAdd == null)
                    {
                        CustomFilePropertiesPart customFilePropPart = doc.AddCustomFilePropertiesPart();

                        customFilePropPart.Properties = new DocumentFormat.OpenXml.CustomProperties.Properties();

                        var customProp = new CustomDocumentProperty();
                        customProp.Name = "parentId";
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
            }
            catch (Exception ex)
            {
                throw ex;
            }

            _documentCore.CloseDocument(doc);

            var documentLast = _documentService.GetAll().LastOrDefault();
            var documentJsonModel = _documentMapper.ToDocumentJsonModel(documentLast);

            return documentJsonModel;
        }

        [HttpPost]
        [Route("ReplaceBookmarks")]
        public int ReplaceBookmarks([FromBody]DocumentJsonModel documentJsonModel)
        {
            var document = _documentService.GetDocument(documentJsonModel.Id);

            WordprocessingDocument doc = _documentCore.OpenDocument(document.Path);
            var bookmarkNames = _bookmarkParser.FindBookmarks(doc.MainDocumentPart.Document);

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
                        _bookmarkParser.ReplaceBookmark(bookmarkNames, bookmark.Name, new TextUtil().GetText(bookmark.Message.ToString()), doc.MainDocumentPart);
                        break;

                    //if (bookmark.Message.GetType() != typeof(string)) throw new Exception("Not my type!");

                    //Text text = TextUtil.GetText(bookmark.Message);
                    //WordBookmarkParser.ReplaceBookmarkSecondMethod(bookMarks, bookmark.Name, text, doc.MainDocumentPart);

                    ////bookmarkDb.MessageJson = bookmark.Message;

                    ////_bookmarkService.EditBookmark(bookmarkDb);
                    ////_bookmarkParser.ReplaceBookmark(bookmarkNames, bookmark.Name, new TextUtil().GetText(bookmark.Message.ToString()), docFile.MainDocumentPart);
                    //break;
                    case 2:
                        var bookmarkDbTable = _bookmarkMapper.ToBookmark(bookmark);

                        bookmarkDbTable.MessageJson = bookmark.Message.ToString();

                        _bookmarkService.EditBookmark(bookmarkDbTable);
                        _bookmarkParser.ReplaceBookmark(bookmarkNames, bookmark.Name, new TableUtil().GetTable(bookmark.Message.ToString()), doc.MainDocumentPart);
                        break;

                    //var dnmk = bookmark.Message;

                    ////var tableData = JsonConvert.SerializeObject(bookmark.Message);
                    //var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                    //var serializeObject = JsonConvert.SerializeObject(bookmark.Message, settings);

                    //Table table = WordBookmarkParser.CreateTableMain();
                    //WordBookmarkParser.ReplaceBookmarkSecondMethod(bookMarks, bookmark.Name, table, doc.MainDocumentPart);

                    ////AutoDoc.DAL.Models.Table table = JsonConvert.DeserializeObject<AutoDoc.DAL.Models.Table>(bookmark.Message);
                    ////bookmarkDbTable.MessageJson = JsonConvert.SerializeObject(table);

                    ////_bookmarkService.EditBookmark(bookmarkDbTable);
                    ////_bookmarkParser.ReplaceBookmark(bookmarkNames, bookmark.Name, new TableUtil().GetTable(table), docFile.MainDocumentPart); //TODO table
                    //break;
                    default: break;
                }
            }

            //foreach (var value in documentJsonModel.Bookmarks)
            //{
            //    Table table = WordBookmarkParser.CreateTableMain();
            //    var text = TextUtil.GetText(value.Message);

            //    WordBookmarkParser.ReplaceBookmarkSecondMethod(bookMarks, value.Name, table, doc.MainDocumentPart);
            //}

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