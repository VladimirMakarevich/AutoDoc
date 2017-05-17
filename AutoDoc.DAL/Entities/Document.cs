using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDoc.DAL.Entities
{
    public class Document : BaseEntity
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public ICollection<Bookmark> Bookmarks { get; set; }
    }
}
