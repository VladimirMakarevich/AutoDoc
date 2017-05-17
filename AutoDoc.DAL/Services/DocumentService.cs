﻿using System;
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

        public void CreateDocument(Document document)
        {
            _baseRepository.Add(document);
            _baseRepository.Commit();
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
    }
}