using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoDoc.DAL.Entities;
using AutoDoc.DAL.Repository;

namespace AutoDoc.DAL.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepositoryBase<Document> _baseRepository;

        public DocumentService(IRepositoryBase<Document> baseRepository)
        {
            this._baseRepository = baseRepository;
        }

        public Document GetDocument(int id)
        {
            var document = _baseRepository.GetById(id);
            return document;
        }

        public void CreateDocument(Document document)
        {
            _baseRepository.Add(document);
        }
    }
}
