using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Table = DocumentFormat.OpenXml.InkML.Table;

namespace AutoDoc.BL.ModelsUtilities
{
    public static class TableUtil
    {
        public static Table GetTable<T>(List<T> tableData, int[] tableHeadingCount, string[] columnHeadings)
        {
            var table = new Table();
            var tableBorderTop = new TopBorder();
            var tableBorderBottom = new BottomBorder();
            var tableBorderLeft = new LeftBorder();
            var tableBorderRight = new RightBorder();
            var tableBorderHorizontal = new InsideHorizontalBorder();
            var tableBorderVertical = new InsideVerticalBorder();
            var tableProperties = new TableProperties();
            var borders = new TableBorders();


            // Set Border Styles for Table
            tableBorderTop.Val = BorderValues.Single;
            tableBorderTop.Size = 6;
            tableBorderBottom.Val = BorderValues.Single;
            tableBorderBottom.Size = 6;
            tableBorderLeft.Val = BorderValues.Single;
            tableBorderLeft.Size = 6;
            tableBorderRight.Val = BorderValues.Single;
            tableBorderRight.Size = 6;
            tableBorderHorizontal.Val = BorderValues.Single;
            tableBorderHorizontal.Size = 6;
            tableBorderVertical.Val = BorderValues.Single;
            tableBorderVertical.Size = 6;

            // Assign Border Styles to Table Borders
            borders.TopBorder = tableBorderTop;
            borders.BottomBorder = tableBorderBottom;
            borders.LeftBorder = tableBorderLeft;
            borders.RightBorder = tableBorderRight;
            borders.InsideHorizontalBorder = tableBorderHorizontal;
            borders.InsideVerticalBorder = tableBorderVertical;


            // Append Border Styles to Table Properties
            tableProperties.Append(borders);

            // Assign Table Properties to Table
            table.Append(tableProperties);

            var tableRowHeader = new TableRow();
            tableRowHeader.Append(new TableRowHeight() {Val = 2000});

            for (int i = 0; i < tableHeadingCount.Length; i++)
            {
                var tableCellHeader = new TableCell();

                //Assign Font Properties to Run
                var runPropHeader = new RunProperties();
                runPropHeader.Append(new Bold());
                runPropHeader.Append(new Color() {Val = "000000"});

                //Create New Run
                var runHeader = new Run();
                //Assign Font Properties to Run
                runHeader.Append(runPropHeader);

                var columnHeader = new Text();
                //Assign the Pay Rule Name to the Run
                columnHeader = new Text(columnHeadings[i]);

                runHeader.Append(columnHeader);

                //Create Properties for Paragraph
                var justificationHeader = new Justification();
                justificationHeader.Val = JustificationValues.Left;

                var paraPropsHeader = new ParagraphProperties(justificationHeader);
                SpacingBetweenLines spacing = new SpacingBetweenLines()
                {
                    Line = "240",
                    LineRule = LineSpacingRuleValues.Auto,
                    Before = "0",
                    After = "0"
                };
                paraPropsHeader.Append(spacing);

                var paragraphHeader = new Paragraph();

                paragraphHeader.Append(paraPropsHeader);
                paragraphHeader.Append(runHeader);
                tableCellHeader.Append(paragraphHeader);

                var tableCellPropertiesHeader = new TableCellProperties();
                var tableCellWidthHeader = new TableCellWidth();

                tableCellPropertiesHeader.Append(new Shading()
                {
                    Val = ShadingPatternValues.Clear,
                    Color = "auto",
                    Fill = "#C0C0C0"
                });

                var textDirectionHeader = new TextDirection();
                textDirectionHeader.Val = TextDirectionValues.BottomToTopLeftToRight;
                tableCellPropertiesHeader.Append(textDirectionHeader);

                tableCellWidthHeader.Type = TableWidthUnitValues.Dxa;
                tableCellWidthHeader.Width = "2000";

                tableCellPropertiesHeader.Append(tableCellWidthHeader);

                tableCellHeader.Append(tableCellPropertiesHeader);
                tableRowHeader.Append(tableCellHeader);

            }

            tableRowHeader.AppendChild(new TableHeader());

            table.Append(tableRowHeader);

            //Create New Row in Table for Each Record

            foreach (var record in tableData)
            {
                var tableRow = new TableRow();
                for (int i = 0; i < tableHeadingCount.Length; i++)
                {

                    //**** This is where I dynamically want to iterate through selected properties and output the value ****

                    var propertyText = "Test";

                    var tableCell = new TableCell();

                    //Assign Font Properties to Run
                    var runProp = new RunProperties();
                    runProp.Append(new Bold());
                    runProp.Append(new Color() {Val = "000000"});


                    //Create New Run
                    var run = new Run();
                    //Assign Font Properties to Run
                    run.Append(runProp);

                    //Assign the text to the Run
                    var text = new Text(propertyText);
                    run.Append(text);

                    //Create Properties for Paragraph
                    var justification = new Justification();
                    justification.Val = JustificationValues.Left;
                    var paraProps = new ParagraphProperties(justification);

                    var paragraph = new Paragraph();

                    paragraph.Append(paraProps);
                    paragraph.Append(run);
                    tableCell.Append(paragraph);

                    var tableCellProperties = new TableCellProperties();
                    var tableCellWidth = new TableCellWidth();
                    tableCellWidth.Type = TableWidthUnitValues.Dxa;
                    tableCellWidth.Width = "2000";
                    tableCellProperties.Append(tableCellWidth);
                    tableCell.Append(tableCellProperties);
                    tableRow.Append(tableCell);
                }

                table.Append(tableRow);
            }

            return table;


        }
    }

}
