using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoDoc.Models
{
    public class DocumentJsonModel
    {
        public int Id { get; set; }
        public List<BookmarkJsonModel> Bookmarks { get; set; }
    }
}
