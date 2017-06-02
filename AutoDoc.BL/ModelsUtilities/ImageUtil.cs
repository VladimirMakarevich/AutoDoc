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
        public Drawing GetImage(string filepath, WordprocessingDocument doc)
        {
            Bitmap bitmap = (Bitmap) Image.FromFile(filepath, true);
            int incremental = bitmap.GetHashCode();


            MainDocumentPart mainPart = doc.MainDocumentPart;
            ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);

            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                ms.Position = 0;
                imagePart.FeedData(ms);
            }

            var widthPx = bitmap.Width;
            var heightPx = bitmap.Height;

            var horzRezDpi = bitmap.HorizontalResolution;
            var vertRezDpi = bitmap.VerticalResolution;

            const int emusPerInch = 914400;
            const int emusPerCm = 360000;

            var maxWidthCm = 16.51;

            var widthEmus = (long)(widthPx / horzRezDpi * emusPerInch);
            var heightEmus = (long)(heightPx / vertRezDpi * emusPerInch);

            var maxWidthEmus = (long)(maxWidthCm * emusPerCm);

            if (widthEmus > maxWidthEmus)
            {
                var ratio = (heightEmus * 1.0m) / widthEmus;
                widthEmus = maxWidthEmus;
                heightEmus = (long)(widthEmus * ratio);
            }

            var relID = mainPart.GetIdOfPart(imagePart);

            var element =
                new Drawing(
                    new DW.Inline(
                        //new DW.Extent() {Cx = 990000L * (long) (7.13 / 1.08), Cy = 792000L * (long) (8.51 / 0.87)},
                        new DW.Extent() { Cx = 990000L, Cy = 792000L },
                        new DW.EffectExtent()
                        {
                            LeftEdge = 0L,
                            TopEdge = 0L,
                            RightEdge = 0L,
                            BottomEdge = 0L
                        },
                        new DW.DocProperties()
                        {
                            Id = (UInt32Value) 1U,
                            Name = "img" + incremental
                        },
                        new DW.NonVisualGraphicFrameDrawingProperties(
                            new A.GraphicFrameLocks() {NoChangeAspect = true}),
                        new A.Graphic(
                            new A.GraphicData(
                                    new PIC.Picture(
                                        new PIC.NonVisualPictureProperties(
                                            new PIC.NonVisualDrawingProperties()
                                            {
                                                Id = (UInt32Value) 0U,
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
                                                new A.Offset() {X = 0L, Y = 0L},
                                                new A.Extents()
                                                {
                                                    //Cx = 990000L * (long) (7.13 / 1.08),
                                                    //Cy = 792000L * (long) (8.51 / 0.87)
                                                    Cx = widthEmus,
                                                    Cy = heightEmus
                                                }),
                                            new A.PresetGeometry(
                                                    new A.AdjustValueList()
                                                )
                                                {Preset = A.ShapeTypeValues.Rectangle}))
                                )
                                {Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"})
                    )
                    {
                        DistanceFromTop = (UInt32Value) 0U,
                        DistanceFromBottom = (UInt32Value) 0U,
                        DistanceFromLeft = (UInt32Value) 0U,
                        DistanceFromRight = (UInt32Value) 0U,
                        EditId = "50D07946"
                    });

            return element;
        }
    }

}
