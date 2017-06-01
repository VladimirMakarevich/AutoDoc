using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;

namespace AutoDoc.BL.Parsers
{
    public interface IWordBookmarkParser
    {
        List<string> FindAllBookmarks(WordprocessingDocument doc);
        void ReplaceBookmark<T>(Dictionary<string, BookmarkEnd> bookMarks, string name, T element,
           MainDocumentPart doc) where T : OpenXmlElement;
        //Dictionary<string, BookmarkEnd> FindMainBookmarks(WordprocessingDocument documentPart);
        Dictionary<string, BookmarkEnd> FindBookmarks(OpenXmlElement documentPart, Dictionary<string, BookmarkEnd> results = null, Dictionary<string, String> unmatched = null);
    }
}