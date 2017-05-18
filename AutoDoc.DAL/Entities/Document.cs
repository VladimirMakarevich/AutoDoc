using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDoc.DAL.Entities
{
    public class Document : BaseEntity
    {
        public Document()
        {
            Bookmarks = new List<Bookmark>();
        }

        public string Name { get; set; }
        public string Path { get; set; }

        public virtual ICollection<Bookmark> Bookmarks { get; set; }
    }
}
