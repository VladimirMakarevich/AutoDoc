using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoDoc.Models
{
    public class BookmarksJsonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public dynamic Message { get; set; }
        public int Type { get; set; }
    }

    /*public class BookmarksJsonModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Table MessageTable { get; set; }
        public string MessageText { get; set; }
        public int Type { get; set; }
    }*/
}
