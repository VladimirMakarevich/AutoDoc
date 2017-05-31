using DocumentFormat.OpenXml.Wordprocessing;

namespace AutoDoc.BL.ModelsUtilities
{
    public interface ITableUtil
    {
        Table GetTable(string tableData);
    }
}