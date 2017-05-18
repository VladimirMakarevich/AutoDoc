using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using AutoDoc.DocumentFormat;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.AspNetCore.Hosting;
using AutoDoc.BL.Core;
using AutoDoc.BL.Parsers;
using AutoDoc.DAL.Context;
using AutoDoc.DAL.Entities;
using AutoDoc.DAL.Repository;
using AutoDoc.DAL.Services;
using Microsoft.AspNetCore.Http.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutoDoc.Controllers
{
    [Route("api/Document")]
    public class DocumentController
    {
        IHostingEnvironment _hostingEnvironment;
        private IDocumentService _documentService;

        public DocumentController(IHostingEnvironment hostingEnvironment, IDocumentService documentService)
        {
            _hostingEnvironment = hostingEnvironment;
            _documentService = documentService;
        }

        [HttpPost]
        [Route("UploadFiles")]
        public async Task<int> UploadFiles(IFormFile file)
        {
            if (file == null) throw new Exception("File is null");
            if (file.Length == 0) throw new Exception("File is empty");

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "AppData");
            var filePath = Path.Combine(uploads, file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            //var doc = DocumentCore.OpenDocument(filePath);
            //var bookmarksList = WordBookmarkParser.FindAllBookmarks(doc);

            //return bookmarksList;

            int id = _documentService.CreateDocument(new Document
            {
                Name = file.FileName,
                Path = filePath
            });

            return id;
        }

        [HttpGet]
        [Route("DownloadFiles")]
        public IActionResult DownloadFiles(int id)
        {
            Document doc = _documentService.GetDocument(id);

            using (var stream = new FileStream(doc.Path, FileMode.Open))
            {
                return new FileStreamResult(stream, "application/x-msdownload");
            }
        }
    }
}
