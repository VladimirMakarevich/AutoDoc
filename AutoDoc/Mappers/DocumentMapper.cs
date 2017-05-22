using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutoDoc.DAL.Entities;
using AutoMapper;
using AutoDoc.Models;

namespace AutoDoc.Mappers
{
    public class DocumentMapper
    {
        private readonly IMapper _mapper;

        public DocumentMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Document ToDocument(string fileName, string filePath, List<Bookmark> bookmarks)
        {
            return new Document() { Name = fileName, Path = filePath, Bookmarks = bookmarks };
        }

        public DocumentJsonModel ToDocumentJsonModel(List<BookmarkJsonModel> bookmarksJsonModel, int documentId)
        {
            return new DocumentJsonModel
            {
                Bookmarks = bookmarksJsonModel,
                Id = documentId
            };
        }

        public DocumentJsonModel ToDocumentJsonModel(Document documentLast)
        {
            return _mapper.Map<Document, DocumentJsonModel>(documentLast);
        }
    }
}
