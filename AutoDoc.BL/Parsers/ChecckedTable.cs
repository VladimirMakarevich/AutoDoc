using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using System.Linq;

namespace AutoDoc.BL.Parsers
{
    enum DocumentPart
    {
        Main,
        Headers,
        Footers
    }

    class ChecckedTable
    {
        DocumentFormat.OpenXml.Packaging.WordprocessingDocument _activeWordDocument = null;
        /// <summary>
        /// Suchen book in whole active document and replace content with text
        /// </summary>
        public bool ReplaceBookmarkContent(string bookmarkName, string text)
        {
            bool success = true;
            if (_activeWordDocument != null)
            {
                //Get body part of document
                MainDocumentPart main = _activeWordDocument.MainDocumentPart;
                //Function to find and replace the bookmark with text
                FindBookmarksAndReplaceWithText(bookmarkName, text, DocumentPart.Main, main);
                success = true;
                //Get header areas of document
                if (_activeWordDocument.MainDocumentPart.HeaderParts != null)
                {
                    foreach (var header in _activeWordDocument.MainDocumentPart.HeaderParts)
                    {
                        FindBookmarksAndReplaceWithText(bookmarkName, text, DocumentPart.Headers, header);
                        success = true;
                    }
                }
                //Get footer areas of document
                if (_activeWordDocument.MainDocumentPart.FooterParts != null)
                {
                    foreach (var footer in _activeWordDocument.MainDocumentPart.FooterParts)
                    {
                        FindBookmarksAndReplaceWithText(bookmarkName, text, DocumentPart.Footers, footer);
                        success = true;
                        footer.Footer.Save(footer);
                    }
                }
            }

            return success;
        }

        private void FindBookmarksAndReplaceWithText(string bookmarkName, string text, DocumentPart partType, object part)
        {
            MainDocumentPart main = null;
            BookmarkStart bmstart = null;
            HeaderPart header = null;
            FooterPart footer = null;
            //Check, which document part is the target 
            //and find the bookmark's start in this document area
            switch (partType)
            {
                case DocumentPart.Main:
                    {
                        main = (MainDocumentPart)part;
                        bmstart = (from b in main.Document.Body.Descendants<BookmarkStart>()
                                   where b.Name.ToString().StartsWith(bookmarkName)
                                   select b).FirstOrDefault();
                        break;
                    }
                case DocumentPart.Headers:
                    {
                        header = (HeaderPart)part;
                        bmstart = (from b in header.RootElement.Descendants<BookmarkStart>()
                                   where b.Name.ToString().StartsWith(bookmarkName)
                                   select b).FirstOrDefault();
                        break;
                    }
                case DocumentPart.Footers:
                    {
                        footer = (FooterPart)part;
                        bmstart = (from b in footer.RootElement.Descendants<BookmarkStart>()
                                   where b.Name.ToString().StartsWith(bookmarkName)
                                   select b).FirstOrDefault();
                        bmstart = footer.RootElement.Descendants<BookmarkStart>()
                            .Where(b => b.Name.ToString()
                        .StartsWith(bookmarkName)).FirstOrDefault();
                        break;
                    }
            }
            if (bmstart != null) //start of bookmark was found
            {
                BookmarkEnd bmend = null;
                string idBm = bmstart.Id;

                //Find end of bookmark
                switch (partType)
                {
                    case DocumentPart.Main:
                        {
                            bmend = (from b in main.Document.Body.Descendants<BookmarkEnd>()
                                     where b.Id == idBm
                                     select b).FirstOrDefault();
                            break;
                        }
                    case DocumentPart.Headers:
                        {
                            bmend = (from b in header.RootElement.Descendants<BookmarkEnd>()
                                     where b.Id == idBm
                                     select b).FirstOrDefault();
                            break;
                        }
                    case DocumentPart.Footers:
                        {
                            bmend = (from b in footer.RootElement.Descendants<BookmarkEnd>()
                                     where b.Id == idBm
                                     select b).FirstOrDefault();
                            break;
                        }
                }
                if (bmend != null) //End of bookmark was found
                {
                    //Check, if new text should be append
                    OpenXmlElement sliblingElement = bmstart.Parent.PreviousSibling();
                    if (sliblingElement == null)
                    {
                        sliblingElement = bmstart.Parent.Parent;
                    }
                    RunProperties rProp = null;
                    if (bmstart.Parent.Descendants<Run>() != null)
                    {
                        rProp = (from rp in bmstart.Parent.Descendants<Run>()
                                 where rp.RunProperties != null
                                 select rp.RunProperties).FirstOrDefault();
                    }
                    //Check, if bookmark is extended over more the one paragraph and check, if
                    //end of bookmark is embedded into a table (if yes -> replace the table with the text)

                    if (bmend.Parent.GetType() == typeof(Table))
                    {
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
                        Text nText = new Text()
                        {
                            Text = text
                        };
                        nRun.Append(nText);
                        nPara.Append(nRun);
                        //New end of bookmark
                        BookmarkEnd nBmEnd = new BookmarkEnd()
                        {
                            Id = idBm
                        };
                        nPara.Append(nBmEnd);

                        sliblingElement.InsertAfterSelf(nPara);
                    }
                    else
                    {
                        //End of bookmark was not embedded into a table
                        //Check, if bookmark range contains more paragraphs 
                        if (bmstart.Parent != bmend.Parent)
                        {
                            //Bookmark contains more paragraphs
                            //Copy the paragraph styles of the first paragraph, which will be used for the inserted text
                            ParagraphProperties paraProp = (ParagraphProperties)((Paragraph)bmstart.Parent).ParagraphProperties.Clone();
                            //Get all paragraphs of bookmark
                            var list = (from p in bmstart.Parent.ElementsAfter()
                                        where
                                        p.IsBefore(bmend.Parent)
                                        ||
                                        p == bmend.Parent
                                        select p).ToList();
                            //Remove all paragraphs
                            for (int n = list.Count(); n > 0; n--)
                            {
                                list[n - 1].Remove();
                            }
                            //Remove start paragraph
                            bmstart.Parent.Remove();
                            //Check, if new text contains line feed and carriage return = more lines
                            if (string.IsNullOrEmpty(text) || (!text.Contains("\r") && !text.Contains("\n")))
                            {
                                //New text has only one paragraph
                                //New start of bookmark
                                BookmarkStart nBmStart = new BookmarkStart()
                                {
                                    Name = bookmarkName,
                                    Id = idBm
                                };
                                //New paragarpah
                                Paragraph nPara = new Paragraph()
                                {
                                    ParagraphProperties = (ParagraphProperties)paraProp.Clone()
                                };
                                nPara.Append(nBmStart);
                                //New run
                                Run nRun = new Run();
                                //New text
                                Text nText = new Text()
                                {
                                    Text = text
                                };
                                nRun.Append(nText);
                                nPara.Append(nRun);
                                //New end of bookmark
                                BookmarkEnd nBmEnd = new BookmarkEnd()
                                {
                                    Id = idBm
                                };
                                nPara.Append(nBmEnd);

                                sliblingElement.InsertAfterSelf(nPara);
                            }
                            else
                            {
                                //Inserting text with more lines
                                InsertMultiLineText(bookmarkName, text, bmstart, sliblingElement, paraProp, rProp);
                            }
                        }
                        else
                        {
                            //Existing bookmark range contains only one paragraph
                            if (string.IsNullOrEmpty(text) || (!text.Contains("\r") && !text.Contains("\n")))
                            {
                                //New text contains only one paragraph  
                                if (bmstart.PreviousSibling<Run>() == null
                                    &&
                                    bmend.ElementsAfter() != null
                                    &&
                                    bmend.ElementsAfter().Where(e => e.GetType() == typeof(Run)).Count() == 0)
                                {

                                    bmstart.Parent.RemoveAllChildren<Run>();
                                }
                                else
                                {
                                    //removing runs of paragraphs, which are part of the bookmark range
                                    var list = (from r in bmstart.ElementsAfter()
                                                where
                                                r.IsBefore(bmend)
                                                select r).ToList();
                                    RunProperties trRun = (from rp in list
                                                           where rp.GetType() == typeof(Run)
                                                           &&
                                                           ((Run)rp).RunProperties != null
                                                           select ((Run)rp).RunProperties).FirstOrDefault();
                                    if (trRun != null)
                                    {
                                        rProp = (RunProperties)trRun.Clone();
                                    }
                                    for (int n = list.Count(); n > 0; n--)
                                    {
                                        list[n - 1].Remove();
                                    }
                                }
                                //New run with the style of the first run, which was removed
                                Run nRun = new Run();
                                if (rProp != null)
                                {
                                    nRun.RunProperties = (RunProperties)rProp.Clone();
                                }
                                //New text
                                Text nText = new Text()
                                {
                                    Text = text
                                };
                                nRun.Append(nText);
                                bmstart.InsertAfterSelf(nRun);
                            }
                            else
                            {
                                //New text contains more lines
                                //Copy the style of the first paragraph
                                ParagraphProperties paraProp = (ParagraphProperties)((Paragraph)bmstart.Parent).ParagraphProperties.Clone();
                                //remove paragraph
                                bmstart.Parent.Remove();

                                //Inserting multiline new text with the style of the bookmark start paragraph
                                InsertMultiLineText(bookmarkName, text, bmstart, sliblingElement, paraProp, rProp);

                            }
                        }
                    }

                }

            }
        }

        private void InsertMultiLineText(string bookmarkName,
                    string text,
                    BookmarkStart bmstart,
                    OpenXmlElement sliblingElement,
                    ParagraphProperties paraProp,
                    RunProperties rProp)
        {
            OpenXmlElement insertElement = sliblingElement;
            if (text.Contains("\r\n"))
            {
                string[] textLines = text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                //Recreation
                int cnt = 0;
                BookmarkStart nbmStart = null;
                foreach (string textLine in textLines)
                {
                    Paragraph np = new Paragraph()
                    {
                        ParagraphProperties = (ParagraphProperties)paraProp.Clone()
                    };
                    if (cnt == 0)
                    {
                        //Insert new bookmark start
                        nbmStart = new BookmarkStart();
                        nbmStart.Name = bookmarkName;
                        nbmStart.Id = bmstart.Id;
                        np.AppendChild<BookmarkStart>(nbmStart);
                    }
                    //Insert new runs with text
                    Run nr = new Run();
                    if (rProp != null)
                    {
                        nr.RunProperties = (RunProperties)rProp.Clone();
                    }
                    nr.AppendChild<Text>(new Text(textLine));
                    np.AppendChild<Run>(nr);
                    if (cnt == textLines.Count() - 1)
                    {
                        //Insert new bookmark end
                        BookmarkEnd nbmEnd = new BookmarkEnd()
                        {
                            Id = nbmStart.Id
                        };
                        np.AppendChild<BookmarkEnd>(nbmEnd);
                    }
                    //Append paragraph
                    if (insertElement.Parent != null)
                    {
                        insertElement.InsertAfterSelf(np);
                    }
                    else
                    {
                        insertElement.Append(np);
                    }

                    insertElement = np;
                    cnt += 1;
                }
            }
        }
    }
}