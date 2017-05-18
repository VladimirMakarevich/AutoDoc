using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoDoc.Models
{
    public class BookmarksListJsonModel
    {
        public List<BookmarkJsonModel> Bookmarks { get; set; }
        public int DocumentId { get; set; }
    }
}
