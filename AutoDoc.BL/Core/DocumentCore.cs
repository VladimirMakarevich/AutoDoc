using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Packaging;

namespace AutoDoc.BL.Core
{
    class DocumentCore
    {
        public WordprocessingDocument OpenDocument(string path)
        {
            try
            {
                var doc = WordprocessingDocument.Open(path, true);
                return doc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void CloseDocument(WordprocessingDocument doc)
        {
            doc.Close();
        }
    }
}
