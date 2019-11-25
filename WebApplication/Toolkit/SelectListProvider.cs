using System;
using System.Web.Mvc;
using Utilities.Extensions;
using WebApplication.VisionToolkit.ExtensionMethods;

namespace WebApplication.VisionToolkit
{
    public static class SelectListProvider
    {
        public static SelectList FromEnum<TEnum>() where TEnum : struct, IComparable, IFormattable, IConvertible
        {
           return EnumExtensions.GetEnumAsDictionary<TEnum>().ToSelectList();
        }

        public static SelectList MonthSelectList(int? selected)
        {
            return DateTimeExtensions.GetMonthsDictionary().ToSelectList(selected?.ToString());
        }
    }
}