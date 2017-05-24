using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AutoDoc.BL.ModelsUtilities
{
    public class TextUtil : ITextUtil
    {
        public Text GetText(string text)
        {
            return new Text(text);
        }
    }
}
