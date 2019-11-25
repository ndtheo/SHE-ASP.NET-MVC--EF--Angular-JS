#region Using Directives

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

#endregion

namespace Utilities.Extensions
{
    public static class EnumExtensions
    {
        public static Dictionary<int, string> GetEnumAsDictionary<TEnum>() where TEnum : struct, IComparable, IFormattable, IConvertible
        {
            var typeOfEnum = typeof(TEnum);

            if (!typeOfEnum.IsEnum)
                throw new InvalidEnumArgumentException();

            var dictionary = Enum.GetValues(typeOfEnum)
                .Cast<TEnum>()
                .ToDictionary(x => x.ToInt32(null), x => x.ToString(CultureInfo.InvariantCulture));

            return dictionary;
        }
    }
}