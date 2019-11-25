#region Using Directives

using System;
using System.Collections.Generic;
using ClosedXML.Excel;

#endregion

// ------------------------------------------------------------------------
// This sample shows how to call the ExportToExcel method.
// List<List<string>> columns = new List<List<string>>()
// 
// string path = cars.ExportToExcel(properties);
// 
// Excel generated using the ClosedXML wrapper
// https://github.com/ClosedXML/ClosedXML
// ------------------------------------------------------------------------

namespace Utilities
{
	public static class ExcelExporter
	{
		/// <summary>Exports a List of string Lists (columns) into an excel document and saves it in the path given.</summary>
		/// <param name="columns"></param>
		/// <param name="path"></param>
		public static void ExportToExcel<T>(this List<List<T>> columns, string path) where T : class
		{
			var workbook = new XLWorkbook();
			IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet1");

			worksheet.WriteData(columns);

            // Add a new sheet for our pivot table
            //	IXLWorksheet pivotWorksheet = workbook.Worksheets.Add("Pivot Table");
            //pivotWorksheet.PivotTables.AddNew("Pivot Table", pivotWorksheet.Cell(1, 1), worksheet.RangeUsed());

            worksheet.Columns().AdjustToContents();
			workbook.SaveAs(path);
		}
        public static void ExportToExcelTree<T>(this List<List<T>> columns, Dictionary<string, string> groupingList, string path) where T : class
        {
            var workbook = new XLWorkbook();
            IXLWorksheet worksheet = workbook.Worksheets.Add("Sheet1");

            worksheet.WriteData(columns);
            var mainrowFromWorksheet = worksheet.Row(1);
            mainrowFromWorksheet.Style.Fill.BackgroundColor = XLColor.Amethyst;
            if (groupingList?.Count > 0)
            {
                foreach(var group in groupingList)
                {
                    worksheet.Rows(group.Key + ":" + group.Value).Group(true);
                    int header;
                    Int32.TryParse(group.Key, out header);
                    if (header > 0)
                    {
                        var rowFromWorksheet = worksheet.Row(header);
                        rowFromWorksheet.Style.Fill.BackgroundColor = XLColor.LightGray;
                    }
                    
                    int mainRow;
                    Int32.TryParse(group.Value, out mainRow);
                    if (mainRow > 0)
                    {
                        mainRow++;
                        worksheet.Rows(mainRow.ToString() + ":" + mainRow.ToString()).Style.Font.Bold = true;
                    }
                  
                }
            }
            //worksheet.Rows(1.ToString() + ":" + 5.ToString()).Group();
            //worksheet.Rows(1.ToString() + ":" + 5.ToString()).Style.Font.FontColor = XLColor.Red;
            // Add a new sheet for our pivot table
            //	IXLWorksheet pivotWorksheet = workbook.Worksheets.Add("Pivot Table");
            //pivotWorksheet.PivotTables.AddNew("Pivot Table", pivotWorksheet.Cell(1, 1), worksheet.RangeUsed());

            worksheet.Columns().AdjustToContents();
            workbook.SaveAs(path);
        }
        private static void WriteData<T>(this IXLWorksheet worksheet, List<List<T>> columns) where T : class
		{
			var col = 1;
			foreach (var column in columns)
			{
				var row = 1;
				foreach (var cell in column)
				{
					if (cell?.GetType() == typeof(string))
					{
						worksheet.Cell(row, col).Value = "'" + cell; // to be used as en explisit text
					}
					else if (cell?.GetType() == typeof(bool) || cell?.GetType() == typeof(bool?))
					{
						var cellBool = cell as bool?;
						worksheet.Cell(row, col).Value = cellBool == true ? "Yes" : cellBool == false ? "No" : null;
					}
					else
					{
						worksheet.Cell(row, col).Value = cell;
					}
					row++;
				}
				col++;
			}
		}
	}
}