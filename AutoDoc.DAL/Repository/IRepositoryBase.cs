using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoDoc.DAL.Entities;

namespace AutoDoc.DAL.Repository
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);

        T GetById(int id);
        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        void Commit();
    }
}
