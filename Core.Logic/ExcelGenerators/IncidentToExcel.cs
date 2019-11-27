#region Using Directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Entities;
using Database.Models.Display;
using Utilities;
using Utilities.Extensions;

#endregion

namespace Core.Logic.ExcelGenerators
{
	public static class IncidentToExcel
	{
        private static List<object> getExcelColumn(Type type, string displayName, Expression exrpession, List<Incident> data)
        {
            var row = new List<object> { type.GetSubProperty("AccidentType.Name").GetDisplayName() };
            row.AddRange(data.Select(x=>exrpession));
            return row;
        }

        public static void ToExcel(this List<Incident> data, DisplayIncident display, string path)
		{
			var document = new List<List<object>>();
            var type = typeof(Incident);

			if (display.IncidentType)
			{
				var row = new List<object> {typeof(Incident).GetSubProperty("IncidentType.Name").GetDisplayName()};
				row.AddRange(data.Select(x => x.IncidentType.Name));
				document.Add(row);
			}

			if (display.IncidentDate)
			{
				var row = new List<object> {typeof(Incident).GetSubProperty("IncidentDate").GetDisplayName()};
				row.AddRange(data.Select(x => x.IncidentDate as object));
				document.Add(row);
			}

			if (display.IncidentTime)
			{
				var row = new List<object> {typeof(Incident).GetSubProperty("IncidentTime").GetDisplayName()};
				row.AddRange(data.Select(x => x.IncidentTime as object));
				document.Add(row);
			}

			if (display.Person)
			{
				var row = new List<object> {typeof(Incident).GetSubProperty("Person").GetDisplayName()};
				row.AddRange(data.Select(x => x.Person));
				document.Add(row);
			}

			if (display.Description)
			{
				var row = new List<object> {typeof(Incident).GetSubProperty("Description").GetDisplayName()};
				row.AddRange(data.Select(x => x.Description));
				document.Add(row);
			}
			document.ExportToExcel(path);
		}
	}
}
