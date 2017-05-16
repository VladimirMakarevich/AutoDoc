using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDoc.DAL.Entities
{
    public class Bookmark : BaseEntity
    {
        public int DocumentId { get; set; }
        public string Name { get; set; }

        public Document Document { get; set; }
    }
}
