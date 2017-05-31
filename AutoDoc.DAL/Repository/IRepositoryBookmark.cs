using System.Collections.Generic;
using AutoDoc.DAL.Entities;

namespace AutoDoc.DAL.Repository
{
    public interface IRepositoryBookmark<T> where T : Bookmark
    {
        void Add(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Update(Bookmark bookmark);
    }
}
