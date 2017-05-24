﻿using System;
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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Net.Http;
using System.Net;

namespace AutoDoc.Controllers
{
    [Route("api/Document")]
    [EnableCors("EnableCors")]
    public class DocumentController : DefaultController
    {
        private IHostingEnvironment _hostingEnvironment;
        private IDocumentService _documentService;
        private IBookmarkService _bookmarkService;
        public DocumentMapper _documentMapper;
        public BookmarkMapper _bookmarkMapper;

        public DocumentController(IHostingEnvironment hostingEnvironment,
            IDocumentService documentService,
            IBookmarkService bookmarkService,
            DocumentMapper documentMapper,
            BookmarkMapper bookmarkMapper)
        {
            _hostingEnvironment = hostingEnvironment;
            _documentService = documentService;
            _bookmarkService = bookmarkService;
            _documentMapper = documentMapper;
            _bookmarkMapper = bookmarkMapper;
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<DocumentJsonModel> UploadFile(IFormFile file)
        {
            if (file == null) throw new Exception("File is null");
            if (file.Length == 0) throw new Exception("File is empty");

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "AppData");
            var filePath = Path.Combine(uploads, file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var doc = DocumentCore.OpenDocument(filePath);
            var bookmarksList = WordBookmarkParser.FindAllBookmarks(doc);

            var bookmarks = _bookmarkMapper.ToBookmarks(bookmarksList);
            var document = _documentMapper.ToDocument(file.FileName, filePath, bookmarks);
            _documentService.CreateDocument(document);

            var documentLast = _documentService.GetAll().LastOrDefault();
            var documentJsonModel = _documentMapper.ToDocumentJsonModel(documentLast);

            DocumentCore.CloseDocument(doc);

            return documentJsonModel;
        }

        [HttpPost]
        [Route("ReplaceBookmarks")]
        public int ReplaceBookmarks([FromBody]DocumentJsonModel documentJsonModel)
        {
            var document = _documentService.GetDocument(documentJsonModel.Id);

            WordprocessingDocument doc = DocumentCore.OpenDocument(document.Path);
            var bookMarks = WordBookmarkParser.FindBookmarks(doc.MainDocumentPart.Document);

            foreach (var value in documentJsonModel.Bookmarks)
            {
                WordBookmarkParser.ReplaceBookmarkSecondMethod(bookMarks, value.Name, value.Message);
            }
            DocumentCore.CloseDocument(doc);

            return document.Id;
        }

        [HttpGet]
        [Route("DownloadDocument/{documentId}")]
        public FileContentResult DownloadDocument(int documentId)
        {
            var document = _documentService.GetDocument(documentId);

            var fileByteArray = System.IO.File.ReadAllBytes(document.Path);
            FileContentResult file = new FileContentResult(fileByteArray, "application/x-msdownload; " + document.Name)
            {
                FileDownloadName = WebUtility.UrlEncode(document.Name)
            };

            return file;
        }
    }
}