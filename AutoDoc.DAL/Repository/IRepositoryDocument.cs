using System.Collections.Generic;
using AutoDoc.DAL.Entities;
using System.Linq.Expressions;
using System;

namespace AutoDoc.DAL.Repository
{
    public interface IRepositoryDocument<T> where T : Document
    {
        int Add(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> expression);
    }
}
