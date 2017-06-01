using AutoDoc.DAL.Entities;
using System.Collections.Generic;

namespace AutoDoc.DAL.Services
{
    public interface IBookmarkService
    {
        Bookmark GetBookmark(int id);
        void CreateBookmark(Bookmark bookmark);
        void EditBookmark(Bookmark bookmark);
        IEnumerable<Bookmark> GetAllBookmarksByDocument(int id);
    }
}
