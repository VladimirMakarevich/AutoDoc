using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoDoc.DAL.Entities;
using AutoDoc.DAL.Repository;

namespace AutoDoc.DAL.Services
{
    public class DocumentService : IDocumentService, IDisposable
    {
        private readonly IRepositoryBase<Document> _baseRepository;

        public DocumentService(IRepositoryBase<Document> baseRepository)
        {
            this._baseRepository = baseRepository;
        }

        public Document GetDocument(string name)
        {
            return _baseRepository.GetAll().First(c => c.Name == name);
        }

        public Document GetDocument(int id)
        {
            var document = _baseRepository.GetById(id);
            return document;
        }

        public List<Document> GetAllDocuments()
        {
            return _baseRepository.GetAll().ToList();
        }

        public int CreateDocument(Document document)
        {
            _baseRepository.Add(document);
            _baseRepository.Commit();

            return _baseRepository.Get(d => d.Name == document.Name && d.Path == document.Path).Id;
        }

        public void EditDocument(Document document)
        {
            _baseRepository.Update(document);
            _baseRepository.Commit();
        }

        public void DeleteDocument(int id)
        {
            _baseRepository.Delete(_baseRepository.GetById(id));
            _baseRepository.Commit();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _baseRepository.Dispose();
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
