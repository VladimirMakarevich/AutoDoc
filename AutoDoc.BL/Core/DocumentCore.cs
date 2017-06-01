using System;
using DocumentFormat.OpenXml.Packaging;

namespace AutoDoc.BL.Core
{
    public class DocumentCore : IDocumentCore
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
                throw ex;
            }
        }

        public void CloseDocument(WordprocessingDocument doc)
        {
            doc.Save();
            doc.Close();
        }
    }
}