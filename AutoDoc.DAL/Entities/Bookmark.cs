using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDoc.DAL.Entities
{
    public class Bookmark : BaseEntity
    {
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }

        public virtual Document Document { get; set; }
    }
}
