#region Using Directives

using System.Collections.Generic;

#endregion

namespace WebApplication.VisionToolkit.Models
{
    public class ExcelExportRequest<T, TV>
    {
        public List<T> Data { get; set; }
        public T SearchCriteria { get; set; }
        public TV Display { get; set; }
    }
}