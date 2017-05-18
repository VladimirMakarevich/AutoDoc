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
        T GetById(int id);
    }
}
