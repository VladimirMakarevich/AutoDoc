using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoDoc.DAL.Entities;
using AutoDoc.DAL.Repository;

namespace AutoDoc.DAL.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IRepositoryBase<Bookmark> _baseRepository;

        public BookmarkService(IRepositoryBase<Bookmark> baseRepository)
        {
            this._baseRepository = baseRepository;
        }

        public Bookmark GetBookmark(int id)
        {
            var bookmark = _baseRepository.GetById(id);
            return bookmark;
        }

        public void CreateBookmark(Bookmark bookmark)
        {
            _baseRepository.Add(bookmark);
        }
    }
}
