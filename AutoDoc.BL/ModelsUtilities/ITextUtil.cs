using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AutoDoc.BL.ModelsUtilities
{
    public interface ITextUtil
    {
        Text GetText(string text);
    }
}
