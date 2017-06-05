using System.Collections.Generic;

namespace AutoDoc.DAL.Entities
{
    public class Document
    {
        public Document()
        {
            Bookmarks = new List<Bookmark>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public virtual ICollection<Bookmark> Bookmarks { get; set; }
    }
}
