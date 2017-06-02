using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoDoc.BL.Core;
using AutoDoc.BL.Parsers;
using AutoDoc.DAL.Entities;
using AutoDoc.DAL.Services;
using AutoDoc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using AutoMapper;
using AutoDoc.BL.ModelsUtilities;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutoDoc.Controllers
{
    [Route("api/Bookmark")]
    public class BookmarkController : Controller
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

        public BookmarkController (
            IHostingEnvironment hostingEnvironment,
            IBookmarkService bookmarkService, 
            IDocumentService documentService, 
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

        [HttpGet]
        [Route("GetBookmarks")]
        public async Task<List<BookmarksJsonModel>> GetBookmarks(int id)
        {
            var bookmarksEntities = _bookmarkService.GetAllBookmarksByDocument(id);
            List<BookmarksJsonModel> responceBookmarksJsonModels = new List<BookmarksJsonModel>();

            foreach (var bookmartEntity in bookmarksEntities)
            {
                switch(bookmartEntity.Type)
                {
                    case 1:
                        var bookmark = _mapper.Map<Bookmark, BookmarksJsonModel>(bookmartEntity);
                        bookmark.Message = bookmartEntity.MessageJson;
                        responceBookmarksJsonModels.Add(bookmark);
                        break;
                    case 2:
                        var bookmarkTable = _mapper.Map<Bookmark, BookmarksJsonModel>(bookmartEntity);
                        bookmarkTable.Message = JObject.Parse(bookmartEntity.MessageJson) as JObject;
                        responceBookmarksJsonModels.Add(bookmarkTable);
                        break;
                    case 3:
                        var bookmarkPic = _mapper.Map<Bookmark, BookmarksJsonModel>(bookmartEntity);
                        bookmarkPic.Message = bookmartEntity.MessageJson;
                        responceBookmarksJsonModels.Add(bookmarkPic);
                        break;
                    case 4:
                        var bookmarkExtTable = _mapper.Map<Bookmark, BookmarksJsonModel>(bookmartEntity);
                        bookmarkExtTable.Message = JObject.Parse(bookmartEntity.MessageJson) as JObject;
                        responceBookmarksJsonModels.Add(bookmarkExtTable);
                        break;
                    default: break;
                }
                
            }

            return responceBookmarksJsonModels;
        }

        [HttpPost]
        [Route("PostBookmarks")]
        public async Task<Boolean> PostBookmarks([FromBody] List<BookmarksJsonModel> bookmarks)
        {
            try
            {
                var currentBookmark = _bookmarkService.GetBookmark(bookmarks.First().Id);
                var documentPath = _documentService.GetDocument(currentBookmark.DocumentId).Path;

                var docFile = _documentCore.OpenDocument(documentPath);
                var bookmarkNames = _bookmarkParser.FindBookmarks(docFile);

                foreach (var bookmark in bookmarks)
                {
                    switch (bookmark.Type)
                    {
                        case 1:
                            var bookmarkDb = _mapper.Map<BookmarksJsonModel, Bookmark>(bookmark);

                            if (bookmark.Message.GetType() != typeof(string)) throw new Exception("Not my type!");
                            bookmarkDb.MessageJson = bookmark.Message;

                            _bookmarkService.EditBookmark(bookmarkDb);
                            _bookmarkParser.ReplaceBookmark(bookmarkNames.Find(name => name.BookmarkData.Key == bookmark.Name).BookmarkData, _textUtil.GetText(bookmark.Message.ToString()), docFile.MainDocumentPart);
                            break;
                        case 2:
                            var bookmarkDbTable = _mapper.Map<BookmarksJsonModel, Bookmark>(bookmark);

                            bookmarkDbTable.MessageJson = bookmark.Message.ToString();

                            _bookmarkService.EditBookmark(bookmarkDbTable);
                            _bookmarkParser.ReplaceBookmark(bookmarkNames.Find(name => name.BookmarkData.Key == bookmark.Name).BookmarkData, _tableUtil.GetTable(bookmark.Message.ToString()), docFile.MainDocumentPart); 
                            break;
                        case 3:
                            var bookmarkDbPic = _mapper.Map<BookmarksJsonModel, Bookmark>(bookmark);

                            if (bookmark.Message.GetType() != typeof(string)) throw new Exception("Not my type!");

                            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "AppData");
                            var filePath = Path.Combine(uploads, bookmark.Message);

                            bookmarkDbPic.MessageJson = bookmark.Message;

                            _bookmarkService.EditBookmark(bookmarkDbPic);
                            _bookmarkParser.ReplaceBookmark(bookmarkNames.Find(name => name.BookmarkData.Key == bookmark.Name).BookmarkData, _imageUtil.GetImage(filePath, docFile), docFile.MainDocumentPart);
                            break;
                        case 4:
                            var bookmarkDbExtTable = _mapper.Map<BookmarksJsonModel, Bookmark>(bookmark);

                            bookmarkDbExtTable.MessageJson = bookmark.Message.ToString();

                            _bookmarkService.EditBookmark(bookmarkDbExtTable);
                            _bookmarkParser.ExpandTableBookmark(bookmarkNames.Find(name => name.BookmarkData.Key == bookmark.Name).BookmarkData, _tableUtil.GetTable(bookmark.Message.ToString()), docFile.MainDocumentPart);
                            break;
                        default: break;
                    }
                }
                docFile.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        [Route("PostBookmarkPictures")]
        public async Task<string> UploadPic(IFormFile file)
        {
            if (file == null) throw new Exception("File is null");
            if (file.Length == 0) throw new Exception("File is empty");

            var fileHashName = file.GetHashCode().ToString();
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "AppData");
            var filePath = Path.Combine(uploads, fileHashName + file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileHashName + file.FileName;
        }

        [HttpGet]
        [Route("GetBookmarkPictures")]
        public FileContentResult DownloadFiles(string name)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "AppData");
            var filePath = Path.Combine(uploads, name);

            var fileByteArray = System.IO.File.ReadAllBytes(filePath);
            FileContentResult file = new FileContentResult(fileByteArray, "application/x-msdownload; " + name)
            {
                FileDownloadName = WebUtility.UrlEncode(name)
            };
            return file;
        }
    }
}
