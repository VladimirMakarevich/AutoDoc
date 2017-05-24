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
    public class RepositoryBase<T> : IRepositoryBase<T>, IDisposable where T : BaseEntity
    {
        private AutoDocContext _dataContext;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(AutoDocContext dbCont)
        {
            _dataContext = dbCont;
            if (_dataContext != null) _dbSet = _dataContext.Set<T>();
        }

        public virtual int Add(T entity)
        {
            _dataContext.Entry(entity).State = EntityState.Added;
            _dbSet.Add(entity);

            return entity.Id;
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _dataContext.Entry(entity).State = EntityState.Deleted;
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault<T>();
        }

        public virtual void Commit()
        {
            _dataContext.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dataContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
