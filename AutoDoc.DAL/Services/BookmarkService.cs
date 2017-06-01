using System;
using System.Collections.Generic;
using AutoDoc.DAL.Entities;
using AutoDoc.DAL.Repository;
using System.Linq;

namespace AutoDoc.DAL.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IRepositoryBookmark<Bookmark> _bookmarkRepository;

        public BookmarkService(IRepositoryBookmark<Bookmark> bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        public Bookmark GetBookmark(int id)
        {
            var bookmark = _bookmarkRepository.GetById(id);
            return bookmark;
        }

        public void CreateBookmark(Bookmark bookmark)
        {
            _bookmarkRepository.Add(bookmark);
        }

        public void EditBookmark(Bookmark bookmark)
        {
            var buf = _bookmarkRepository.GetById(bookmark.Id);
            buf.MessageJson = bookmark.MessageJson;
            buf.Type = bookmark.Type;

            _bookmarkRepository.Update(buf);
        }

        public IEnumerable<Bookmark> GetAllBookmarksByDocument(int id)
        {
            var bookmarks = _bookmarkRepository.GetAll();

            return bookmarks.Where(c => c.DocumentId == id).ToList();
        }
    }
}
