#region Using Directives

using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Database.Models.Display;
using Utilities;
using Utilities.Extensions;

#endregion

namespace Core.Logic.ExcelGenerators
{
    public static class BaseNamedModelToExcel
    {
        public static void ToExcel<T>(this List<T> data, DisplayWithName display, string path) where T : BaseEntity
        {
            var document = new List<List<object>>();

            if (display.Name)
            {
                var row = new List<object>
                {
                    typeof(BaseEntity).GetSubProperty(nameof(BaseNamedModel.Name)).GetDisplayName()
                };
                row.AddRange(data.Select(x => x.Name));
                document.Add(row);
            }

            document.ExportToExcel(path);
        }
    }
}