using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Text.RegularExpressions;

namespace AutoDoc.BL.Parsers
{
    public class WordTagParser : IWordTagParser
    {
        public void ReplaceTags<T>(WordprocessingDocument doc, T newElement, string tag) where T: OpenXmlElement
        {
            //Regex regex = new Regex("<!(.)*?!>", RegexOptions.Compiled);
            Regex regex = new Regex("<" + tag + ">", RegexOptions.Compiled);

            //grab the header parts and replace tags there
            foreach (HeaderPart headerPart in doc.MainDocumentPart.HeaderParts)
            {
                ReplaceParagraphParts<T>(headerPart.Header, regex, newElement);
            }
            //now do the document
            ReplaceParagraphParts<T>(doc.MainDocumentPart.Document, regex, newElement);
            //now replace the footer parts
            foreach (FooterPart footerPart in doc.MainDocumentPart.FooterParts)
            {
                ReplaceParagraphParts<T>(footerPart.Footer, regex, newElement);
            }

        }

        public void ReplaceParagraphParts<T>(OpenXmlElement element, Regex regex, T newElement)where T :OpenXmlElement
        {
            foreach (var paragraph in element.Descendants<Paragraph>())
            {
                Match match = regex.Match(paragraph.InnerText);
                if (match.Success)
                {
                    //create a new run and set its value to the correct text
                    //this must be done before the child runs are removed otherwise
                    //paragraph.InnerText will be empty
                    Run newRun = new Run();
                    //newRun.AppendChild(new Text(paragraph.InnerText.Replace(match.Value, "some new value")));
                    newRun.AppendChild(newElement);
                    //remove any child runs
                    paragraph.RemoveAllChildren<Run>();
                    //add the newly created run
                    paragraph.AppendChild(newRun);
                }
            }
        }

    }
}
