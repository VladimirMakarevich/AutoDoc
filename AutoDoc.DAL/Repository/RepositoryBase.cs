using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoDoc.DAL.Context;
using AutoDoc.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoDoc.DAL.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private AutoDocContext _dataContext;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(AutoDocContext dbCont)
        {
            _dataContext = dbCont;
            if (_dataContext != null) _dbSet = _dataContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            //_dataContext.Add(entity);
            _dataContext.Entry(entity).State = EntityState.Added;
            _dbSet.Add(entity);
            _dataContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.OrderBy(m => m.Id);
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }
    }
}
