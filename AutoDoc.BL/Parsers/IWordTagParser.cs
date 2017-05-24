using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AutoDoc.BL.Parsers
{
    public interface IWordTagParser
    {
        void ReplaceParagraphParts<T>(OpenXmlElement element, Regex regex, T newElement) where T : OpenXmlElement;
        void ReplaceTags<T>(WordprocessingDocument doc, T newElement, string tag) where T : OpenXmlElement;
    }
}
