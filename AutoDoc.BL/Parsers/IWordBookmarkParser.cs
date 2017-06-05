using AutoDoc.BL.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;

namespace AutoDoc.BL.Parsers
{
    public interface IWordBookmarkParser
    {
        void ReplaceBookmark<T>(KeyValuePair<string, BookmarkStart> bookMark,
            T element, MainDocumentPart doc) where T : OpenXmlElement;

        List<WordBookmark> FindBookmarks(WordprocessingDocument doc);
    }
}