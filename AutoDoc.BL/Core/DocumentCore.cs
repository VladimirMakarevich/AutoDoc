using System;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.CustomProperties;
using System.Linq;
using DocumentFormat.OpenXml.VariantTypes;

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

        public CustomFilePropertiesPart CheckCustomProperty(CustomFilePropertiesPart customFilePropertiesPart, int ParentId)
        {
            if (customFilePropertiesPart != null)
            {
                var props = customFilePropertiesPart.Properties;
                if (props != null)
                {
                    var prop = props.Where(p => ((CustomDocumentProperty)p).Name.Value == "ParentId").FirstOrDefault();
                    if (prop != null) Int32.TryParse(((CustomDocumentProperty)prop).InnerText, out ParentId);
                }
            }

            return customFilePropertiesPart;
        }

        public void CheckIfDocumentExist(WordprocessingDocument doc, int id)
        {
            var customPropsAdd = doc.CustomFilePropertiesPart;
            if (customPropsAdd == null)
            {
                var customFilePropPart = doc.AddCustomFilePropertiesPart();

                customFilePropPart.Properties = new DocumentFormat.OpenXml.CustomProperties.Properties();
                var customProp = new CustomDocumentProperty();
                customProp.Name = "ParentId";
                customProp.FormatId = "{D5CDD505-2E9C-101B-9397-08002B2CF9AE}";
                customProp.VTLPWSTR = new VTLPWSTR(id.ToString());

                customFilePropPart.Properties.AppendChild(customProp);
                int pid = 2;

                foreach (CustomDocumentProperty item in customFilePropPart.Properties)
                {
                    item.PropertyId = pid++;
                }

                customFilePropPart.Properties.Save();
            }
        }
    }
}