using AutoDoc.DAL.Entities;

namespace AutoDoc.DAL.Services
{
    public interface IBookmarkService
    {
        Bookmark GetBookmark(int id);
        void CreateBookmark(Bookmark bookmark);
    }
}
