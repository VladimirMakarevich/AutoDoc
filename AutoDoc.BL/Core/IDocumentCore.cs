using DocumentFormat.OpenXml.Packaging;

namespace AutoDoc.BL.Core
{
    public interface IDocumentCore
    {
        WordprocessingDocument OpenDocument(string path);
        void CloseDocument(WordprocessingDocument doc);
        CustomFilePropertiesPart CheckCustomProperty(CustomFilePropertiesPart customFilePropertiesPart, int ParentId);
        void CheckIfDocumentExist(WordprocessingDocument doc, int id);
    }
}
