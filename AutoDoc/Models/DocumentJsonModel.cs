using System.Collections.Generic;

namespace AutoDoc.Models
{
    public class DocumentJsonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public List<BookmarkJsonModel> Bookmarks { get; set; }
    }
}
