using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AutoDoc.BL.ModelsUtilities
{
    public static class TextUtil
    {
        public static Text GetText(string text)
        {
            return new Text(text);
        }
    }
}
