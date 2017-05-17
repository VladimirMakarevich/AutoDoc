using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;

namespace AutoDoc.DocumentFormat
{
    public class Bookmarks
    {
        private static Dictionary<string, BookmarkEnd> FindBookmarks(OpenXmlElement documentPart, 
            Dictionary<string, BookmarkEnd> results = null, 
            Dictionary<string, string> unmatched = null)
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

                FindBookmarks(child, results, unmatched);
            }

            return results;
        }

        public static void GetBookmarks(WordprocessingDocument doc)
        {
            var bookMarks = FindBookmarks(doc.MainDocumentPart.Document);

            foreach (var end in bookMarks)
            {
                var textElement = new Text("qwerty23");
                var runElement = new Run(textElement);

                end.Value.InsertAfterSelf(runElement);
            }
        }
    }
}
