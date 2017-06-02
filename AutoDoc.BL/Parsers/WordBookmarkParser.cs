using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoDoc.BL.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AutoDoc.BL.Parsers
{
    public class WordBookmarkParser : IWordBookmarkParser
    {
        /*public static List<string> FindAllBookmarks(WordprocessingDocument doc)
        {
            List<string> bookmarkNames = new List<string>();

            foreach (BookmarkStart bookmarkStart in doc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
            {
                if (bookmarkStart.Name != "_GoBack") bookmarkNames.Add(bookmarkStart.Name);
            }

            doc.Close();
            return bookmarkNames;
        }*/

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

        /*public static void ReplaceBookmark<T>(WordprocessingDocument doc, string bookmarkName, T newElement) where T : OpenXmlElement
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

            doc.Close();
        }*/

        /*public Dictionary<string, BookmarkEnd> FindBookmarks(OpenXmlElement documentPart, Dictionary<string, BookmarkEnd> results = null, Dictionary<string, string> unmatched = null)
        {
            try
            {
                results = results ?? new Dictionary<string, BookmarkEnd>();
                unmatched = unmatched ?? new Dictionary<string, string>();

                foreach (var child in documentPart.Elements())
                {
                    if (child is BookmarkStart)
                    {
                        var bStart = child as BookmarkStart;
                        if (bStart.Name != "_GoBack") unmatched.Add(bStart.Id, bStart.Name);
                    }

                    if (child is BookmarkEnd)
                    {
                        var bEnd = child as BookmarkEnd;
                        foreach (var orphanName in unmatched)
                        {
                            if (bEnd.Id == orphanName.Key && orphanName.Value != "_GoBack")
                                results.Add(orphanName.Value, bEnd);
                        }
                    }

                    FindBookmarks(child, results, unmatched);
                }

                return results;
            }
            catch (Exception ex)
            {
                return null;
            }
        }*/

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

        /*public void ReplaceBookmark<T>(Dictionary<string, BookmarkEnd> bookMarks, string name, T message) where T: OpenXmlElement
        {
            var bookmark = bookMarks[name];
            Run bookmarkEl = bookmark.NextSibling<Run>();
            if (bookmarkEl != null)
            {
                bookmarkEl.GetFirstChild<OpenXmlElement>().InsertAfterSelf(message);
            }

        }*/

        public void ExpandTableBookmark<T>(KeyValuePair<string, BookmarkStart> bookMark, T element,
            MainDocumentPart doc) where T : OpenXmlElement 
        {
            
        }


        public void ReplaceBookmark<T>(KeyValuePair<string, BookmarkStart> bookMark, T element,
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
                if (
                    valueElementStart.Parent.ChildElements.First(
                        el => el.IsAfter(valueElementStart) && el.IsBefore(valueElementEnd)) != null)
                {
                    //valueElementStart.Parent.ChildElements.First(el => el.IsAfter(valueElementStart)).OuterXml.Remove(0);
                    //valueElementStart.Parent.ChildElements.First(el => el.IsAfter(valueElementStart)).OuterXml.Insert(0, runElement.OuterXml);
                    valueElementStart.Parent.ChildElements.First(el => el.IsAfter(valueElementStart)).InnerXml =
                        runElement.InnerXml;
                }
                else valueElementStart.InsertAfterSelf(runElement);
            }
            catch (Exception ex)
            {
                valueElementStart.InsertAfterSelf(runElement);
            }
 
            //if (valueElementStart.FirstChild != null) valueElementStart.FirstChild.InnerXml = runElement.InnerXml;
            //else valueElementStart.InsertAfterSelf(runElement);

            //if (valueElementStart.FirstChild != null) valueElementStart.ReplaceChild(runElement, valueElementStart.FirstChild);
            //else valueElementStart.InsertAfterSelf(runElement);


            /*var bookmark = bookMarks[name];
            Run bookmarkEl = bookmark.NextSibling<Run>();

            if (bookmarkEl != null)
            {
                var bmstart = (from b in doc.Document.Body.Descendants<BookmarkStart>()
                               where b.Name.ToString().StartsWith(name)
                               select b).FirstOrDefault();

                BookmarkEnd bmend = null;
                string idBm = bmstart.Id;

                bmend = (from b in doc.RootElement.Descendants<BookmarkEnd>()
                         where b.Id == idBm
                         select b).FirstOrDefault();

                OpenXmlElement sliblingElement = bookmark.Parent.NextSibling<OpenXmlElement>();

                BookmarkStart nBmStart = new BookmarkStart()
                {
                    Name = name,
                    Id = idBm
                };

                Paragraph nPara = new Paragraph();

                nPara.Append(nBmStart);

                Run nRun = new Run();

                nRun.Append(element);
                nPara.Append(nRun);

                BookmarkEnd nBmEnd = new BookmarkEnd()
                {
                    Id = idBm
                };

                nPara.Append(nBmEnd);

                sliblingElement.InsertAfterSelf(nPara);*/
        }
    }
}
