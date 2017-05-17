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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutoDoc.Controllers
{
    [Route("api/Document")]
    public class DocumentController : DefaultController
    {
        IHostingEnvironment _hostingEnvironment;

        public DocumentController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        [Route("UploadFiles")]
        public async Task UploadFiles(IFormFile file)
        {
            if (file == null) throw new Exception("File is null");
            if (file.Length == 0) throw new Exception("File is empty");

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "AppData");

            var filePath = Path.Combine(uploads, file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var doc = WordprocessingDocument.Open(filePath, true);

            Bookmarks.GetBookmarks(doc);
        }
    }
}
