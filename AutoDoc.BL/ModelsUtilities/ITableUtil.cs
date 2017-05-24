using DocumentFormat.OpenXml.InkML;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoDoc.BL.ModelsUtilities
{
    public interface ITableUtil
    {
        Table GetTable<T>(List<T> tableData, int[] tableHeadingCount, string[] columnHeadings);
    }
}
