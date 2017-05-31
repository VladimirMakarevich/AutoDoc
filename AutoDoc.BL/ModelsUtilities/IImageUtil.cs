using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AutoDoc.BL.ModelsUtilities
{
    public interface IImageUtil
    {
        Drawing GetPicture(WordprocessingDocument doc, string path);
        Drawing AddImageToBody(WordprocessingDocument wordDoc, string relationshipId);
    }
}