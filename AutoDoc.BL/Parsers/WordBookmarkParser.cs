using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using AutoDoc.BL.Models;

namespace AutoDoc.BL.Parsers
{
    public class WordBookmarkParser : IWordBookmarkParser
    {
        private WordBookmark WhatTypeBookmark(BookmarkStart bookmarkStart)
        {
            var bookmarkInfo = new WordBookmark();
            bookmarkInfo.BookmarkData = new KeyValuePair<string, BookmarkStart>(bookmarkStart.Name, bookmarkStart);
            bookmarkInfo.BookmarkType = 1;
            bookmarkInfo.Message = string.Empty;


            return bookmarkInfo;
        }

        public List<WordBookmark> FindBookmarks(WordprocessingDocument doc)
        {
            List<WordBookmark> bookmarkNames = new List<WordBookmark>();

            foreach (BookmarkStart bookmarkStart in doc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
            {
                if (bookmarkStart.Name != "_GoBack")
                {
                    var bookmarkInfo = WhatTypeBookmark(bookmarkStart);
                    bookmarkNames.Add(bookmarkInfo);

                }
            }

            return bookmarkNames;
        }

        public void ReplaceBookmark<T>(KeyValuePair<string, BookmarkStart> bookMark, 
            T element, 
            MainDocumentPart doc) where T : OpenXmlElement
        {
            var valueElementStart = bookMark.Value;

            string idvalueElementEnd = valueElementStart.Id;

            var valueElementEnd = (from b in doc.RootElement.Descendants<BookmarkEnd>()
                                   where b.Id == idvalueElementEnd
                                   select b).FirstOrDefault();

            var runElement = new Run(element);

            try
            {
                if (valueElementStart.Parent.ChildElements.First(
                        el => el.IsAfter(valueElementStart) && el.IsBefore(valueElementEnd)) != null)
                {
                    valueElementStart.Parent.ChildElements.First(el => el.IsAfter(valueElementStart)).InnerXml =
                        runElement.InnerXml;
                }
                else valueElementStart.InsertAfterSelf(runElement);
            }
            catch (Exception)
            {
                valueElementStart.InsertAfterSelf(runElement);
            }
        }
    }
}