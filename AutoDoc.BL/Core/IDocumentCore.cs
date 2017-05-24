using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDoc.BL.Core
{
    public interface IDocumentCore
    {
        WordprocessingDocument OpenDocument(string path);
        void CloseDocument(WordprocessingDocument doc);
    }
}
