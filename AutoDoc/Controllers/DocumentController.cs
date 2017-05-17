using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using AutoDoc.DocumentFormat;
using DocumentFormat.OpenXml.Packaging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutoDoc.Controllers
{
    [Route("api/Document")]
    public class DocumentController : DefaultController
    {
        public DocumentController()
        {

        }

        [HttpPost]
        [Route("UploadFiles")]
        public void UploadFiles(IFormFile file)
        {
            if (file == null) throw new Exception("File is null");
            if (file.Length == 0) throw new Exception("File is empty");

            using (Stream stream = file.OpenReadStream())
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var fileContent = binaryReader.ReadBytes((int)file.Length);
                }
            }

            //var doc = WordprocessingDocument.Open(filePath, true);

            Bookmarks.GetBookmarks(doc);
        }
    }
}
