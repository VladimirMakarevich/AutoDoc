using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoDoc.DAL.Entities;
using AutoDoc.DAL.Repository;

namespace AutoDoc.DAL.Services
{
    public class BookmarkService : IBookmarkService, IDisposable
    {
        private readonly IRepositoryBase<Bookmark> _baseRepository;

        public BookmarkService(IRepositoryBase<Bookmark> baseRepository)
        {
            this._baseRepository = baseRepository;
        }

        public List<Bookmark> GetAllBookmarksByDocument(int id)
        {
            return _baseRepository.GetAll().Where(c => c.DocumentId == id).ToList();
        }

        public Bookmark GetBookmark(string name)
        {
            return _baseRepository.GetAll().First(c => c.Name == name);
        }

        public Bookmark GetBookmark(int id)
        {
            var bookmark = _baseRepository.GetById(id);
            return bookmark;
        }

        public List<Bookmark> GetAllBookmarks()
        {
            return _baseRepository.GetAll().ToList();
        }

        public int CreateBookmark(Bookmark bookmark)
        {
            _baseRepository.Add(bookmark);
            _baseRepository.Commit();

            return _baseRepository.Get(b => b.DocumentId == bookmark.DocumentId && b.Name == b.Name).Id;
        }

        public void EditBookmark(Bookmark bookmark)
        {
            _baseRepository.Update(bookmark);
            _baseRepository.Commit();
        }

        public void DeleteBookmark(int id)
        {
            _baseRepository.Delete(_baseRepository.GetById(id));
            _baseRepository.Commit();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _baseRepository.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
