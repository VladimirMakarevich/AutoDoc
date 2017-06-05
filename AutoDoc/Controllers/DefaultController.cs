using Microsoft.AspNetCore.Mvc;
using AutoDoc.DAL.Services;
using AutoDoc.BL.Parsers;
using AutoDoc.Mappers;
using AutoDoc.BL.ModelsUtilities;
using Microsoft.AspNetCore.Hosting;
using AutoDoc.BL.Core;

namespace AutoDoc.Controllers
{
    public class DefaultController : Controller
    {
        public IHostingEnvironment _hostingEnvironment;
        public IDocumentService _documentService;
        public IBookmarkService _bookmarkService;
        public IDocumentCore _documentCore;
        public IWordBookmarkParser _bookmarkParser;
        public DocumentMapper _documentMapper;
        public BookmarkMapper _bookmarkMapper;
        public ITableUtil _tableUtil;
        public ITextUtil _textUtil;

    }
}
