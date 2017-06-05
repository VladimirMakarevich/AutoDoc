using System.Collections.Generic;
using AutoDoc.DAL.Entities;
using AutoMapper;
using AutoDoc.Models;
using Newtonsoft.Json.Linq;

namespace AutoDoc.Mappers
{
    public class DocumentMapper
    {
        private readonly IMapper _mapper;
        private BookmarkMapper _bookmarkMapper;

        public DocumentMapper(IMapper mapper, BookmarkMapper bookmarkMapper)
        {
            _mapper = mapper;
            _bookmarkMapper = bookmarkMapper;
        }

        public Document ToDocument(string fileName, string filePath, int parentId)
        {
            return new Document() { Id = parentId, Name = fileName, Path = filePath };
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

        internal DocumentJsonModel ToDocumentJsonModelByType(List<Bookmark> bookmarksEntities, int documentId, Document document)
        {
            var responseBookmarksJsonModels = new List<BookmarkJsonModel>();

            foreach (var bookmartEntity in bookmarksEntities)
            {
                switch (bookmartEntity.Type)
                {
                    case 1:
                        var bookmark = _bookmarkMapper.ToBookmarkJsonModel(bookmartEntity);
                        bookmark.Message = bookmartEntity.MessageJson;

                        responseBookmarksJsonModels.Add(bookmark);
                        break;

                    case 2:
                        var bookmarkTable = _bookmarkMapper.ToBookmarkJsonModel(bookmartEntity);
                        bookmarkTable.Message = JObject.Parse(bookmartEntity.MessageJson) as JObject;

                        responseBookmarksJsonModels.Add(bookmarkTable);
                        break;
                    case 3:
                        var bookmarkPic = _bookmarkMapper.ToBookmarkJsonModel(bookmartEntity);
                        bookmarkPic.Message = bookmartEntity.MessageJson;

                        responseBookmarksJsonModels.Add(bookmarkPic);
                        break;
                    default: break;
                }
            }

            return new DocumentJsonModel
            {
                Name = document.Name,
                Path = document.Path,
                Id = documentId,
                Bookmarks = responseBookmarksJsonModels
            };
        }
    }
}
