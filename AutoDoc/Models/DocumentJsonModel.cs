using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoDoc.Models
{
    public class DocumentJsonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookmarksJsonModel> Bookmarks { get; set; }
    }
}
