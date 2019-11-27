#region Using Directives

using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using Core.Entities;
using Core.Entities.SearchCriteria;
using Database;
using Database.Models.ViewModels;
using Utilities.Extensions;

#endregion

namespace WebApplication.Toolkit.ExtensionMethods
{
    /// <summary>Extension methods for the <see cref="IQueryable{T}" /> interface.</summary>
    public static class QueryableExtensions
    {
        /// <summary>
        ///     Returns a <see cref="PageViewModel{T}" /> containing a page of the data given according to the search
        ///     criteria.
        ///     
        ///     Performs Ordering & Paging if applicable. When there is no order parameter, it sorts by the Id.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="criteria"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PageViewModel<T> GetPage<T>(this IQueryable<T> data, BaseSearchCriteria criteria) where T : class
        { 
            if (criteria == null)
            {
                return new PageViewModel<T>
                {
                    Data = data?.ToList()
                };
            }

            criteria.PageSize = criteria.PageSize != 0 ? criteria.PageSize : int.MaxValue;
            criteria.PageSize = criteria.PageSize > 10 ? criteria.PageSize : 10;
            criteria.Page = criteria.Page > 1 ? criteria.Page : 1;

            // if the model is of type base model, default OrderBy is Last Update Date. If not, default is the I
            criteria.OrderBy = !string.IsNullOrWhiteSpace(criteria.OrderBy)
    ? criteria.OrderBy : "Id";
            var page = new PageViewModel<T>
            {
                PageSize = criteria.PageSize,
                Page = criteria.Page,
                TotalRecords = data?.Count() ?? 0
            };
            string orderProperty = criteria.OrderBy.Split(' ')[0] ?? string.Empty;
            bool descending = (!string.IsNullOrWhiteSpace(criteria?.OrderBy) && criteria.OrderBy.EndsWith(" desc"))? true:false;

            page.Data = data?.OrderBy(criteria.OrderBy)
                        .Skip(criteria.PageSize * (criteria.Page - 1))
                        .Take(criteria.PageSize)
                        .ToList();
            return page;
        }
    }
}
