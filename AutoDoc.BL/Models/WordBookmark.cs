using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AutoDoc.BL.Models
{
    public class WordBookmark
    {
        public KeyValuePair<string, BookmarkStart> BookmarkData;
        public int BookmarkType;
    }
}
