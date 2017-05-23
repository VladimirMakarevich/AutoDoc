using System.Collections.Generic;
using System.Linq;
using AutoDoc.DAL.Context;
using AutoDoc.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoDoc.DAL.Repository
{
    public class RepositoryDocument : IRepositoryDocument<Document>
    {
        private AutoDocContext _dataContext;

        public RepositoryDocument(AutoDocContext dbCont)
        {
            _dataContext = dbCont;
        }

        public virtual void Add(Document entity)
        {
            _dataContext.Add(entity);
            _dataContext.SaveChanges();
        }

        public IEnumerable<Document> GetAll()
        {
            return _dataContext.Documents.OrderBy(m => m.Id);
        }

        public virtual Document GetById(int id)
        {
            return _dataContext.Documents.Find(id);
        }
    }
}
