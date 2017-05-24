using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDoc.BL.Parsers
{
    public interface IWordBookmarkParser
    {
        void ReplaceBookmark<T>(Dictionary<string, BookmarkEnd> bookMarks, string name, T message) where T : OpenXmlElement;
        Dictionary<string, BookmarkEnd> FindBookmarks(OpenXmlElement documentPart, Dictionary<string, BookmarkEnd> results = null, Dictionary<string, string> unmatched = null);
    }
}
