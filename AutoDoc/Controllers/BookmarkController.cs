using System;
using System.Collections.Generic;
using System.Linq;
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
using Newtonsoft.Json;

namespace AutoDoc.Controllers
{
    [Route("api/Bookmark")]
    public class BookmarkController : Controller
    {
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
                /*switch(bookmartEntity.Type)
                {
                    case 1:
                        var bookmarkJSONText = _mapper.Map<Bookmark, BookmarksJsonModel>(bookmartEntity);
                        bookmarkJSONText.MessageText = bookmartEntity.Message;
                        responceBookmarksJsonModels.Add(bookmarkJSONText);
                        break;

                    case 2:
                        var bookmarkJSONTable = _mapper.Map<Bookmark, BookmarksJsonModel>(bookmartEntity);
                        bookmarkJSONTable.MessageTable = JsonConvert.DeserializeObject<Models.Table>(bookmartEntity.Message);
                        responceBookmarksJsonModels.Add(bookmarkJSONTable);
                        break;
                }*/
                var bookmark = _mapper.Map<Bookmark, BookmarksJsonModel>(bookmartEntity);
                bookmark.Message = JsonConvert.DeserializeObject(bookmartEntity.MessageJson);
                responceBookmarksJsonModels.Add(bookmark);
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
                var bookmarkNames = _bookmarkParser.FindBookmarks(docFile.MainDocumentPart.Document);

                foreach (var bookmark in bookmarks)
                {
                    /*switch (bookmark.Type)
                    {
                        case 1:
                            var bookmarkDBText = _mapper.Map<BookmarksJsonModel, Bookmark>(bookmark);
                            bookmarkDBText.Message = bookmark.MessageText;
                            _bookmarkService.EditBookmark(bookmarkDBText);
                            _bookmarkParser.ReplaceBookmark(bookmarkNames, bookmark.Name, new TextUtil().GetText(bookmark.MessageText));
                            break;
                        case 2:
                            var bookmarkDBTable = _mapper.Map<BookmarksJsonModel, Bookmark>(bookmark);
                            bookmarkDBTable.Message = JsonConvert.SerializeObject(bookmark.MessageTable);
                            _bookmarkService.EditBookmark(bookmarkDBTable);
                            _bookmarkParser.ReplaceBookmark(bookmarkNames, bookmark.Name, new TextUtil().GetText(bookmark.MessageText)); //TODO table
                            break;
                        default: break;
                    }*/

                    var bookmarkDb = _mapper.Map<BookmarksJsonModel, Bookmark>(bookmark);
                    bookmarkDb.MessageJson = JsonConvert.SerializeObject(bookmark.Message);
                    _bookmarkService.EditBookmark(bookmarkDb);
                    _bookmarkParser.ReplaceBookmark(bookmarkNames, bookmark.Name, new TextUtil().GetText(bookmark.Message)); //TODO table
                }
                docFile.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
