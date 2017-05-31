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

        public int CreateDocument(Document document)
        {
            var existing = _documentRepository.Get(d => d.Id == document.Id);

            if (existing == null)
            {
                return _documentRepository.Add(document);
            }
            else
                return existing.Id;
        }

        public IEnumerable<Document> GetAll()
        {
            return _documentRepository.GetAll();
        }
    }
}
