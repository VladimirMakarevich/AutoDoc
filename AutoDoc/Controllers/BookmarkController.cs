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

namespace AutoDoc.Controllers
{
    [Route("api/Bookmark")]
    public class BookmarkController
    {
        public IBookmarkService BookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            BookmarkService = bookmarkService;
        }

        [HttpGet]
        [Route("FindBookmarks")]
        public async Task<List<BookmarksJsonModel>> FindBookmarks(int id, string path)
        {
            var doc = DocumentCore.OpenDocument(path);
            var bookmarkNames = WordBookmarkParser.FindAllBookmarks(doc);

            List<BookmarksJsonModel> responseBookmarksJsonModels = new List<BookmarksJsonModel>();

            foreach (var bookmarkName in bookmarkNames)
            {
                Bookmark bookmarkEntity = new Bookmark
                {
                    Name = bookmarkName,
                    Message = string.Empty,
                    DocumentId = id
                };
                int bookmarkId = BookmarkService.CreateBookmark(bookmarkEntity);

                BookmarksJsonModel bookmarkJson = new BookmarksJsonModel
                {
                    Id = bookmarkId,
                    Message = string.Empty,
                    Name = bookmarkName
                };
                responseBookmarksJsonModels.Add(bookmarkJson);
            }

            return responseBookmarksJsonModels;
        }

        [HttpGet]
        [Route("GetBookmarks")]
        public async Task<List<BookmarksJsonModel>> GetBookmarks(int id)
        {
            var bookmarksEntities = BookmarkService.GetAllBookmarksByDocument(id);
            List<BookmarksJsonModel> responceBookmarksJsonModels = new List<BookmarksJsonModel>();

            foreach (var bookmartEntity in bookmarksEntities)
            {
                BookmarksJsonModel bookmarkJsonModel = new BookmarksJsonModel
                {
                    Id = bookmartEntity.Id,
                    Message = bookmartEntity.Message,
                    Name = bookmartEntity.Name
                };

                responceBookmarksJsonModels.Add(bookmarkJsonModel);
            }

            return responceBookmarksJsonModels;
        }

        [HttpPost]
        [Route("PostBookmarks")]
        public async Task<Boolean> PostBookmarks(List<BookmarksJsonModel> bookmarks)
        {
            try
            {
                foreach (var bookmark in bookmarks)
                {
                    Bookmark bookmarkEntity = new Bookmark
                    {
                        Id = bookmark.Id,
                        Message = bookmark.Message,
                        Name = bookmark.Name
                    };

                    BookmarkService.EditBookmark(bookmarkEntity);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
