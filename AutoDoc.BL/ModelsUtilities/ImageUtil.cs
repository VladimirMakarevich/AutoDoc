using System;
using System.Drawing;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

namespace AutoDoc.BL.ModelsUtilities
{
    public class ImageUtil : IImageUtil
    {
        public Drawing ReplaceTextWithImage(string find, string filepath, Bitmap bitmap, int incremental)
        {
            using (WordprocessingDocument doc = WordprocessingDocument.Open(filepath, true))
            {
                MainDocumentPart mainPart = doc.MainDocumentPart;
                ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);

                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    ms.Position = 0;
                    imagePart.FeedData(ms);
                }

                var relID = mainPart.GetIdOfPart(imagePart);

                var element =
                     new Drawing(
                         new DW.Inline(
                             new DW.Extent() { Cx = 990000L * (long)(7.13 / 1.08), Cy = 792000L * (long)(8.51 / 0.87) },
                             new DW.EffectExtent()
                             {
                                 LeftEdge = 0L,
                                 TopEdge = 0L,
                                 RightEdge = 0L,
                                 BottomEdge = 0L
                             },
                             new DW.DocProperties()
                             {
                                 Id = (UInt32Value)1U,
                                 Name = "img" + incremental
                             },
                             new DW.NonVisualGraphicFrameDrawingProperties(
                                 new A.GraphicFrameLocks() { NoChangeAspect = true }),
                             new A.Graphic(
                                 new A.GraphicData(
                                     new PIC.Picture(
                                         new PIC.NonVisualPictureProperties(
                                             new PIC.NonVisualDrawingProperties()
                                             {
                                                 Id = (UInt32Value)0U,
                                                 Name = "img" + incremental + ".jpg"
                                             },
                                             new PIC.NonVisualPictureDrawingProperties()),
                                         new PIC.BlipFill(
                                             new A.Blip(
                                                 new A.BlipExtensionList(
                                                     new A.BlipExtension()
                                                     {
                                                         Uri = Guid.NewGuid().ToString()
                                                     })
                                             )
                                             {
                                                 Embed = relID,
                                                 CompressionState = A.BlipCompressionValues.Print
                                             },
                                             new A.Stretch(new A.FillRectangle())),
                                         new PIC.ShapeProperties(
                                             new A.Transform2D(
                                                 new A.Offset() { X = 0L, Y = 0L },
                                                 new A.Extents() { Cx = 990000L * (long)(7.13 / 1.08), Cy = 792000L * (long)(8.51 / 0.87) }),
                                             new A.PresetGeometry(
                                                 new A.AdjustValueList()
                                             )
                                             { Preset = A.ShapeTypeValues.Rectangle }))
                                 )
                                 { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                         )
                         {
                             DistanceFromTop = (UInt32Value)0U,
                             DistanceFromBottom = (UInt32Value)0U,
                             DistanceFromLeft = (UInt32Value)0U,
                             DistanceFromRight = (UInt32Value)0U,
                             EditId = "50D07946"
                         });

                return element;
            }
        }
    }
}
