using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDoc.BL.Models
{
    public class TableSettings
    {
        public List<Column> Columns;
        public string Mode;
    }

    public class Column
    {
        public string Title;
        public bool Sort;
        public bool Filter;
    }
}
