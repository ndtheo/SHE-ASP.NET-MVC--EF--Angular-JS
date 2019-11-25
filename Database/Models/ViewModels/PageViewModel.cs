#region Using Directives

using System.Collections.Generic;

#endregion

namespace Database.Models.ViewModels
{
    public class PageViewModel<T>
    {
        public List<T> Data { get; set; }
        public int TotalRecords { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}