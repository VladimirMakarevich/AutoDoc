using System.Collections.Generic;
using AutoDoc.DAL.Entities;

namespace AutoDoc.DAL.Services
{
    public interface IDocumentService
    {
        IEnumerable<Document> GetAll();
        Document GetDocument(int id);
        int CreateDocument(Document document);
    }
}
