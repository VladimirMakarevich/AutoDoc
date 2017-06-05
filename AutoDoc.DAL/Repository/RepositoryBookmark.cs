using System.Collections.Generic;
using System.Linq;
using AutoDoc.DAL.Context;
using AutoDoc.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoDoc.DAL.Repository
{
    public class RepositoryBookmark : IRepositoryBookmark<Bookmark>
    {
        private AutoDocContext _dataContext;

        public RepositoryBookmark(AutoDocContext dbCont)
        {
            _dataContext = dbCont;
        }

        public virtual void Add(Bookmark bookmark)
        {
            _dataContext.Entry(bookmark).State = EntityState.Added;
            _dataContext.SaveChanges();
        }

        public IEnumerable<Bookmark> GetAll()
        {
            return _dataContext.Bookmarks.OrderBy(m => m.Id);
        }

        public virtual Bookmark GetById(int id)
        {
            return _dataContext.Bookmarks.Find(id);
        }

        public void Update(Bookmark bookmark)
        {
            _dataContext.Attach(bookmark);
            _dataContext.Entry(bookmark).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }
    }
}
