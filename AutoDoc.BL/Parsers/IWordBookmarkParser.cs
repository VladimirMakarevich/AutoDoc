﻿using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;
using AutoDoc.BL.Models;

namespace AutoDoc.BL.Parsers
{
    public interface IWordBookmarkParser
    {
        void ReplaceBookmark<T>(KeyValuePair<string, BookmarkStart> bookMark, T element, MainDocumentPart doc) where T : OpenXmlElement;
        //Dictionary<string, BookmarkEnd> FindBookmarks(OpenXmlElement documentPart, Dictionary<string, BookmarkEnd> results = null, Dictionary<string, string> unmatched = null);
        List<WordBookmark> FindBookmarks(WordprocessingDocument doc);

        void ExpandTableBookmark<T>(KeyValuePair<string, BookmarkStart> bookMark, T element,
            MainDocumentPart doc, string mode) where T : OpenXmlElement;
    }
}
