using System;
using System.Collections.Generic;
using System.Text;
using AutoDoc.DAL.Entities;

namespace AutoDoc.DAL.Services
{
    public interface IBookmarkService
    {
        List<Bookmark> GetAllBookmarks();
        List<Bookmark> GetAllBookmarksByDocument(int id);
        Bookmark GetBookmark(string name);
        Bookmark GetBookmark(int id);
        void DeleteBookmark(int id);
        void CreateBookmark(Bookmark bookmark);
        void EditBookmark(Bookmark bookmark);
    }
}
