using System;
using System.Collections.Generic;
using System.Text;
using AutoDoc.DAL.Entities;

namespace AutoDoc.DAL.Services
{
    public interface IDocumentService
    {
        IEnumerable<Document> GetAll();
        Document GetDocument(int id);
        void CreateDocument(Document document);
    }
}
