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
        public string title;
        public bool sort;
        public bool filter;
    }
}
