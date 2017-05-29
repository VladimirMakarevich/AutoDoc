using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDoc.DAL.Models
{
    public class Table
    {
        public Settings Settings { get; set; }
        public dynamic Data { get; set; }
    }

    public class Settings
    {
        public String Mode { get; set; }
        public List<Header> Columns { get; set; }
    }

    public class Header
    {
        public string Title { get; set; }
        public string Filter { get; set; }
    }
}
