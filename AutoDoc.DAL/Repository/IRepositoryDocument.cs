using System.Collections.Generic;
using AutoDoc.DAL.Entities;

namespace AutoDoc.DAL.Repository
{
    public interface IRepositoryDocument<T> where T : Document
    {
        void Add(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}
