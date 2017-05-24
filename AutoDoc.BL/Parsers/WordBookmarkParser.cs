using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using AutoDoc.BL.Core;

namespace AutoDoc.BL.Parsers
{
    public static class WordBookmarkParser
    {
        public static List<string> FindAllBookmarks(WordprocessingDocument doc)
        {
            List<string> bookmarkNames = new List<string>();

            foreach (BookmarkStart bookmarkStart in doc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
            {
                if (bookmarkStart.Name != "_GoBack")
                {
                    bookmarkNames.Add(bookmarkStart.Name);
                }
            }

            return bookmarkNames;
        }

        public static IDictionary<String, BookmarkStart> FindAllBookmarksSecond(WordprocessingDocument doc)
        {
            IDictionary<String, BookmarkStart> bookmarkMap = new Dictionary<String, BookmarkStart>();

            foreach (BookmarkStart bookmarkStart in doc.MainDocumentPart.RootElement.Descendants<BookmarkStart>())
            {
                bookmarkMap[bookmarkStart.Name] = bookmarkStart;
            }

            return bookmarkMap;
        }

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

        public static void ReplaceAllBookmarks<T>(WordprocessingDocument doc, Dictionary<string, T> newValues) where T : OpenXmlElement
        {
            var bookmarkMap = FindAllBookmarksSecond(doc);

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
        }

        public static void ReplaceBookmarkSecondMethod(Dictionary<string, BookmarkEnd> bookMarks, string name, string message)
        {
            var bookmark = bookMarks[name];
            Run bookmarkEl = bookmark.NextSibling<Run>();
            if (bookmarkEl != null)
            {
                //bookmarkText.GetFirstChild<Text>().Text = value.Value;
                var newValue = new Text(message);
                var table = CreateTableMain();
                //bookmarkEl.GetFirstChild<OpenXmlElement>().InsertAfterSelf(newValue);
                //bookmarkStart.Parent.InsertAfter<Run>(new Run(newElement), bookmarkStart);
                //bookmarkEl
                //ReplaceTable(bookmarkEl);
                bookmarkEl.GetFirstChild<OpenXmlElement>().InsertAfter<Run>(new Run(table), bookmarkEl);
                //doc.MainDocumentPart.Document.Body.Append(table);
            }
        }

        public static void ReplaceTable(BookmarkEnd bmend, BookmarkStart bmstart, 
            string bookmarkName, string text, Table table)
        {
            string idBm = bmstart.Id;

            OpenXmlElement sliblingElement = bmstart.Parent.PreviousSibling();
            //Table should be replaced
            //Replace table with paragraph
            Paragraph parentPara = (Paragraph)bmend.Parent.ElementsBefore()
                .Where(e => e.GetType() == typeof(Paragraph))
                .LastOrDefault();

            bmend.Parent.Remove();
            sliblingElement = parentPara;

            //Set new start of bookmark
            BookmarkStart nBmStart = new BookmarkStart()
            {
                Name = bookmarkName,
                Id = idBm
            };
            //New paragraph
            Paragraph nPara = new Paragraph();
            if (parentPara.Descendants<ParagraphProperties>().Select(z => z) != null)
            {
                foreach (var props in parentPara.Descendants<ParagraphProperties>())
                {
                    nPara.AppendChild<ParagraphProperties>((ParagraphProperties)props.Clone());
                }
            }
            nPara.Append(nBmStart);
            //New Run
            Run nRun = new Run();
            //New text
            //Text nText = new Text()
            //{
            //    Text = text
            //};

            //nRun.Append(nText);
            nRun.Append(table);
            nPara.Append(nRun);
            //New end of bookmark
            BookmarkEnd nBmEnd = new BookmarkEnd()
            {
                Id = idBm
            };
            nPara.Append(nBmEnd);

            sliblingElement.InsertAfterSelf(nPara);
        }

        public static Table CreateTableMain()
        {
            // Create an empty table.
            Table table = new Table();

            // Create a TableProperties object and specify its border information.
            TableProperties tblProp = new TableProperties(
                new TableBorders(
                    new TopBorder()
                    {
                        Val =
                        new EnumValue<BorderValues>(BorderValues.Dashed),
                        Size = 10
                    },
                    new BottomBorder()
                    {
                        Val =
                        new EnumValue<BorderValues>(BorderValues.Dashed),
                        Size = 10
                    },
                    new LeftBorder()
                    {
                        Val =
                        new EnumValue<BorderValues>(BorderValues.Dashed),
                        Size = 24
                    },
                    new RightBorder()
                    {
                        Val =
                        new EnumValue<BorderValues>(BorderValues.Dashed),
                        Size = 24
                    },
                    new InsideHorizontalBorder()
                    {
                        Val =
                        new EnumValue<BorderValues>(BorderValues.Dashed),
                        Size = 24
                    },
                    new InsideVerticalBorder()
                    {
                        Val =
                        new EnumValue<BorderValues>(BorderValues.Dashed),
                        Size = 24
                    }
                )
            );

            // Append the TableProperties object to the empty table.
            table.AppendChild<TableProperties>(tblProp);

            // Create a row.
            TableRow tr = new TableRow();

            // Create a cell.
            TableCell tc1 = new TableCell();

            // Specify the width property of the table cell.
            tc1.Append(new TableCellProperties(
                new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));

            // Specify the table cell content.
            tc1.Append(new Paragraph(new Run(new Text("some text"))));

            // Append the table cell to the table row.
            tr.Append(tc1);

            // Create a second table cell by copying the OuterXml value of the first table cell.
            TableCell tc2 = new TableCell(tc1.OuterXml);

            // Append the table cell to the table row.
            tr.Append(tc2);

            // Append the table row to the table.
            table.Append(tr);

            // Append the table to the document.
            return table;
        }

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
            }
        }


        public static Dictionary<string, BookmarkEnd> FindBookmarks(OpenXmlElement documentPart, Dictionary<string, BookmarkEnd> results = null, Dictionary<string, string> unmatched = null)
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

        public static void ReplaceBookmarkSecondMethod(WordprocessingDocument doc)
        {
            var bookMarks = FindBookmarks(doc.MainDocumentPart.Document);

            foreach (var end in bookMarks)
            {
                var textElement = new Text("asdfasdf");
                var runElement = new Run(textElement);

                end.Value.InsertAfterSelf(runElement);
            }
        }
    }
}
