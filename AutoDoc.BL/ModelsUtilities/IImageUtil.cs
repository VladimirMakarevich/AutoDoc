using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDoc.BL.ModelsUtilities
{
    public interface IImageUtil
    {
        Drawing GetPicture(string path);
    }
}
