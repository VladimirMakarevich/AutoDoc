using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Packaging;

namespace AutoDoc.BL.Core
{
    public static class DocumentCore
    {
        public static WordprocessingDocument OpenDocument(string path)
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

        public static void CloseDocument(WordprocessingDocument doc)
        {
            doc.Save();
            doc.Close();
        }
    }
}
