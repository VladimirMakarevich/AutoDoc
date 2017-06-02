using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDoc.BL.Parsers
{
    public interface IWordBookmarkParser
    {
        void ReplaceBookmark<T>(KeyValuePair<string, BookmarkStart> bookMark, T element, MainDocumentPart doc) where T : OpenXmlElement;
        //Dictionary<string, BookmarkEnd> FindBookmarks(OpenXmlElement documentPart, Dictionary<string, BookmarkEnd> results = null, Dictionary<string, string> unmatched = null);
        List<KeyValuePair<string, BookmarkStart>> FindBookmarks(WordprocessingDocument doc);
    }
}
