using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using RunProperties = DocumentFormat.OpenXml.Wordprocessing.RunProperties;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace AutoDoc.BL.ModelsUtilities
{
    public class TableUtil : ITableUtil
    {
        public Table GetTable(string tableContext)
        {
            Table tbl = new Table();

            TableProperties properties = new TableProperties();
            TableBorders borders = new TableBorders();

            borders.TopBorder = new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single) };
            borders.BottomBorder = new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single) };
            borders.LeftBorder = new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single) };
            borders.RightBorder = new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single) };
            borders.InsideHorizontalBorder = new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single) };
            borders.InsideVerticalBorder = new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single) };

            TableWidth tableWidth = new TableWidth() { Width = "5000", Type = TableWidthUnitValues.Pct };
            properties.Append(tableWidth);
            properties.Append(borders);

            tbl.Append(properties);

            dynamic Structure = JsonConvert.DeserializeObject(tableContext) as JObject;

            dynamic tableHeading = Structure.settings.columns;
            dynamic tableData = Structure.data;

            TableRow headingsTableRow = new TableRow();
            foreach (var header in tableHeading)
            {
                Run headerRun = new Run(new Text(header.Name));

                RunProperties headerRunProperties = new RunProperties();
                headerRunProperties.Append(new Bold());
                headerRunProperties.Append(new Justification() { Val = JustificationValues.Center });
                headerRunProperties.Append(new Color() { Val = "FF0000" });
                `
                headerRun.Append(headerRunProperties);

                TableCell cell = new TableCell(new Paragraph(headerRun));


                headingsTableRow.Append(cell);
            }
            tbl.AppendChild(headingsTableRow);

            foreach (dynamic record in tableData)
            {
                TableRow recordTableRow = new TableRow();
                foreach (var recordCell in tableHeading)
                {
                    string value = record[recordCell.Name.ToString()];
                    TableCell cell = new TableCell(new Paragraph(new Run(new Text(value.ToString()))));

                    recordTableRow.Append(cell);
                }
                tbl.Append(recordTableRow);
            }

            return tbl;
        }
    }

}