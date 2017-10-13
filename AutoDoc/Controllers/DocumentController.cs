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
using System.Net;

namespace AutoDoc.Controllers
{
    [Route("api/Document")]
    [EnableCors("EnableCors")]
    public class DocumentController : DefaultController
    {
        private int ParentId = 0;

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
            var filePath = await SaveFileDataAsync(file);

            var openDocument = _documentCore.OpenDocument(filePath);

            var customProps = _documentCore.CheckCustomProperty(openDocument.CustomFilePropertiesPart, ParentId);

            var document = _documentMapper.ToDocument(file.FileName, filePath, ParentId);
            var documentId = _documentService.CreateDocument(document);

            if (documentId != ParentId)
            {
                _documentCore.CheckIfDocumentExist(openDocument, documentId);
            }

            var bookmarkNames = _bookmarkParser.FindBookmarks(openDocument);
            var bookmarksEntitiesCheck = _bookmarkService.GetAllBookmarksByDocument(documentId).ToList();

            if (!bookmarksEntitiesCheck.Any() && bookmarkNames != null)
            {
                foreach (var bookmarkName in bookmarkNames)
                {
                    var bookmark = _bookmarkMapper.ToBookmarkFromName(bookmarkName, documentId);
                    _bookmarkService.CreateBookmark(bookmark);
                }
            }

            var bookmarksEntities = _bookmarkService.GetAllBookmarksByDocument(documentId).ToList();

            DocumentJsonModel documentJsonModel = _documentMapper.ToDocumentJsonModelByType(bookmarksEntities, documentId, document);

            _documentCore.CloseDocument(openDocument);

            return documentJsonModel;
        }

        [HttpPost]
        [Route("ReplaceBookmarks")]
        public int ReplaceBookmarks([FromBody]DocumentJsonModel documentJsonModel)
        {
            var document = _documentService.GetDocument(documentJsonModel.Id);
            var openDocument = _documentCore.OpenDocument(document.Path);
            var bookmarkNames = _bookmarkParser.FindBookmarks(openDocument);

            foreach (var bookmark in documentJsonModel.Bookmarks)
            {
                var bookmarkDb = _bookmarkMapper.ToBookmark(bookmark);

                switch (bookmark.Type)
                {
                    case 1:
                        bookmarkDb.MessageJson = bookmark.Message;

                        _bookmarkService.EditBookmark(bookmarkDb);
                        _bookmarkParser.ReplaceBookmark(
                            bookmarkNames.Find(name => name.BookmarkData.Key == bookmark.Name).BookmarkData,
                            _textUtil.GetText(bookmark.Message.ToString()), 
                            openDocument.MainDocumentPart);
                        break;

                    case 2:
                        bookmarkDb.MessageJson = bookmark.Message.ToString();

                        _bookmarkService.EditBookmark(bookmarkDb);
                        _bookmarkParser.ReplaceBookmark(
                            bookmarkNames.Find(name => name.BookmarkData.Key == bookmark.Name).BookmarkData,
                            _tableUtil.GetTable(bookmark.Message.ToString()), 
                            openDocument.MainDocumentPart);
                        break;

                    default:
                        break;
                }
            }

            _documentCore.CloseDocument(openDocument);

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


        private async Task<string> SaveFileDataAsync(IFormFile file)
        {
            var fileHashName = file.GetHashCode().ToString();
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "AppData");
            var filePath = Path.Combine(uploads, fileHashName + ".docx");

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }
    }
}