using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDoc.BL.Interfaces
{
    public interface IDocumentProvider
    {
        void UploadDocument(string fileHashName, string filePath);
    }
}
