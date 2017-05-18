using System;
using System.Collections.Generic;
using System.Text;
using AutoDoc.DAL.Entities;

namespace AutoDoc.DAL.Services
{
    public interface IDocumentService : IDisposable
    {
        List<Document> GetAllDocuments();
        Document GetDocument(string name);
        Document GetDocument(int id);
        void DeleteDocument(int id);
        int CreateDocument(Document document);
        void EditDocument(Document document);
    }
}
