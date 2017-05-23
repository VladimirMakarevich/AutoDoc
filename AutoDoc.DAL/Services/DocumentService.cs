using System.Collections.Generic;
using AutoDoc.DAL.Entities;
using AutoDoc.DAL.Repository;

namespace AutoDoc.DAL.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IRepositoryDocument<Document> _documentRepository;

        public DocumentService(IRepositoryDocument<Document> documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public Document GetDocument(int id)
        {
            var document = _documentRepository.GetById(id);
            return document;
        }

        public void CreateDocument(Document document)
        {
            _documentRepository.Add(document);
        }

        public IEnumerable<Document> GetAll()
        {
            return _documentRepository.GetAll();
        }
    }
}
