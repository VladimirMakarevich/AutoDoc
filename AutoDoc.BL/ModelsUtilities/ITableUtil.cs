using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Wordprocessing;

namespace AutoDoc.BL.ModelsUtilities
{
    public interface ITableUtil
    {
        //Table GetTable<T>(List<T> tableData, int[] tableHeadingCount, string[] columnHeadings);
        Table GetTable(string tableData);
    }
}
