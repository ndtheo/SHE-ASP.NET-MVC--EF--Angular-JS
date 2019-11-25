#region Using Directives

using System;
using System.Collections.Generic;

#endregion

namespace Utilities.Extensions
{
    /// <summary>Extension methods for the <see cref="DateTime" /> class</summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     Returns true if this dateTime is between the fromDatetime and  toDateTimes
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="fromDateTime"></param>
        /// <param name="toDateTime"></param>
        public static bool Between(this DateTime dateTime, DateTime fromDateTime, DateTime toDateTime) => dateTime >= fromDateTime && dateTime <= toDateTime;

        /// <summary>
        ///     Returns the month name for a number
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string AsMonthName(this int i) => i > 0 && i <= 12 ? new DateTime(2015, i, 1).ToString("MMMM") : "Invalid";

        public static Dictionary<int, string> GetMonthsDictionary()
        {
            var months = new Dictionary<int, string>();
            for (var i = 1; i <= 12; i++)
            {
                months.Add(i, i.AsMonthName());
            }
            return months;
        }

        public static Dictionary<int, string> GetYearsDictionary(int fromYear, int toYear)
        {
            var dic = new Dictionary<int, string>();

            for (var i = fromYear; i <= toYear; i++)
            {
                dic.Add(i, i.ToString());
            }

            return dic;
        }
    }
}