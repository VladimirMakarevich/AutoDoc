using System;
using System.Collections.Generic;
using System.Text;
using AutoDoc.DAL.Entities;

namespace AutoDoc.DAL.Services
{
    public interface IBookmarkService : IDisposable
    {
        List<Bookmark> GetAllBookmarks();
        List<Bookmark> GetAllBookmarksByDocument(int id);
        Bookmark GetBookmark(string name);
        Bookmark GetBookmark(int id);
        void DeleteBookmark(int id);
        int CreateBookmark(Bookmark bookmark);
        void EditBookmark(Bookmark bookmark);
    }
}
