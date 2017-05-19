using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using AutoDoc.BL.Core;
using AutoDoc.BL.Parsers;
using Microsoft.AspNetCore.Cors;
using AutoDoc.Models;
using AutoDoc.BL.ModelsUtilities;
using AutoDoc.DAL.Repository;
using AutoDoc.DAL.Entities;
using AutoDoc.Mappers;
using AutoDoc.DAL.Services;

namespace AutoDoc.Controllers
{
    [Route("api/Document")]
    [EnableCors("EnableCors")]
    public class DocumentController : DefaultController
    {
        private IHostingEnvironment _hostingEnvironment;
        private IDocumentService _documentService;
        public DocumentMapper _documentMapper = new DocumentMapper();
        public BookmarkMapper _bookmarkMapper = new BookmarkMapper();

        public DocumentController(IHostingEnvironment hostingEnvironment,
            IDocumentService documentService)
        {
            _hostingEnvironment = hostingEnvironment;
            _documentService = documentService;
            //_documentMapper = documentMapper;
            //_bookmarkMapper = bookmarkMapper;
        }

        [HttpPost]
        [Route("UploadFiles")]
        public async Task<BookmarksListJsonModel> UploadFiles(IFormFile file)
        {
            if (file == null) throw new Exception("File is null");
            if (file.Length == 0) throw new Exception("File is empty");

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "AppData");
            var filePath = Path.Combine(uploads, file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var document = _documentMapper.GetDocument(file.FileName, filePath);
            _documentService.CreateDocument(document);

            var doc = DocumentCore.OpenDocument(filePath);
            var bookmarksList = WordBookmarkParser.FindAllBookmarks(doc);

            var bookmarksListJsonModel = _bookmarkMapper.GetBookmarksListJsonModel(bookmarksList);

            return bookmarksListJsonModel;
        }

        [HttpPost]
        [Route("ReplaceBookmarks")]
        public int ReplaceBookmarks(BookmarksListJsonModel bookmarkList)
        {
            var document = _documentService.GetDocument(bookmarkList.DocumentId);
            var doc = DocumentCore.OpenDocument(document.Path);

            foreach (var bookmarks in bookmarkList.Bookmarks)
            {
                var wordprocessingText = TextUtil.GetText(bookmarks.Message);
                WordBookmarkParser.ReplaceBookmark(doc, bookmarks.Name, wordprocessingText);
            }

            //TODO: replace It
            return document.Id;
        }
    }
}