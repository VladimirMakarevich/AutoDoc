using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AutoDoc.BL.Parsers
{
    public static class WordBookmarkParser
    {
        public static List<string> FindAllBookmarks(WordprocessingDocument doc)
        {
            List<string> bookmarkNames = new List<string>();

            foreach (BookmarkStart bookmarkStart in doc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
            {
                bookmarkNames.Add(bookmarkStart.Name);
            }

            return bookmarkNames;
        }

        /*public static IDictionary<String, BookmarkStart> FindAllBookmarks(WordprocessingDocument doc)
        {
            IDictionary<String, BookmarkStart> bookmarkMap = new Dictionary<String, BookmarkStart>();

            foreach (BookmarkStart bookmarkStart in doc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
            {
                bookmarkMap[bookmarkStart.Name] = bookmarkStart;
            }

            return bookmarkMap;
        }*/

        /*public static IDictionary<string, BookmarkEnd> FindAllBookmarksRecursive(OpenXmlElement documentPart, Dictionary<string, BookmarkEnd> results = null, Dictionary<string, string> unmatched = null)
        {
            results = results ?? new Dictionary<string, BookmarkEnd>();
            unmatched = unmatched ?? new Dictionary<string, string>();

            foreach (var child in documentPart.Elements())
            {
                if (child is BookmarkStart)
                {
                    var bStart = child as BookmarkStart;
                    unmatched.Add(bStart.Id, bStart.Name);
                }

                if (child is BookmarkEnd)
                {
                    var bEnd = child as BookmarkEnd;
                    foreach (var orphanName in unmatched)
                    {
                        if (bEnd.Id == orphanName.Key)
                            results.Add(orphanName.Value, bEnd);
                    }
                }

                FindAllBookmarksRecursive(child, results, unmatched);
            }

            return results;
        }*/

        /*public static void ReplaceAllBookmarksRecursive<T>(WordprocessingDocument doc, IDictionary<string, T> newValues) where T: OpenXmlElement
        {
            var bookmarkMap = FindAllBookmarksRecursive(doc.MainDocumentPart.Document);

            foreach (var value in newValues)
            {
                //var textElement = new Text(value.Value);
                var runElement = new Run(value.Value);

                bookmarkMap[value.Key].InsertAfterSelf(runElement);
            }
        }*/

        /*public static void ReplaceAllBookmarks<T>(WordprocessingDocument doc, IDictionary<string, T> newValues) where T: OpenXmlElement
        {
            var bookmarkMap = FindAllBookmarks(doc);

            foreach (var value in newValues)
            {
                var bookmark = bookmarkMap[value.Key];
                Run bookmarkEl = bookmark.NextSibling<Run>();
                if (bookmarkEl != null)
                {
                    //bookmarkText.GetFirstChild<Text>().Text = value.Value;
                    bookmarkEl.GetFirstChild<T>().InsertAfterSelf(value.Value);
                }
            }
        }*/

        /*public static void InsertIntoBookmark<T>(WordprocessingDocument doc, string bookmarkName, T newElement) where T: OpenXmlElement
        {
            MainDocumentPart mainPart = doc.MainDocumentPart;
            Body body = mainPart.Document.GetFirstChild<Body>();
            var bookmarkStart = body
                                .Descendants<BookmarkStart>()
                                .FirstOrDefault(o => o.Name == bookmarkName);

            OpenXmlElement elem = bookmarkStart.NextSibling();

            while (elem != null && !(elem is BookmarkEnd))
            {
                OpenXmlElement nextElem = elem.NextSibling();
                elem.Remove();
                elem = nextElem;
            }

            //bookmarkStart.Parent.InsertAfter<Run>(new Run(new Text(text)), bookmarkStart);
            bookmarkStart.Parent.InsertAfter<Run>(new Run(newElement), bookmarkStart);
            mainPart.Document.Save();
        }*/

        public static void ReplaceBookmark<T>(WordprocessingDocument doc, string bookmarkName, T newElement) where T : OpenXmlElement
        {
            MainDocumentPart mainPart = doc.MainDocumentPart;
            Body body = mainPart.Document.GetFirstChild<Body>();

            var bookmark = body
                                .Descendants<BookmarkStart>()
                                .FirstOrDefault(o => o.Name == bookmarkName);
            var parent = bookmark.Parent; //bookmark's parent element

            if (newElement != null)
            {
                parent.InsertAfterSelf(newElement);
                parent.Remove();
            }
        }
    }
}
