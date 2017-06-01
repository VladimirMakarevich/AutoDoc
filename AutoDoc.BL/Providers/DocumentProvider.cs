using AutoDoc.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDoc.BL.Providers
{
    public class DocumentProvider : IDocumentProvider
    { 
        void IDocumentProvider.UploadDocument(string fileHashName, string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
