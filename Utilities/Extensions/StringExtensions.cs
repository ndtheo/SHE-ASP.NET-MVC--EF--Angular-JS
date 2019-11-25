using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Utilities.Extensions {
    public static class StringExtensions
    {
        public static List<string> SplitCamelCase(this string camelCased)
        {
            if(string.IsNullOrWhiteSpace(camelCased)) return new List<string>();
            return Regex.Replace(camelCased, "(\\B[A-Z])", " $1").Split(' ').ToList();
        }

        public static string GetAngularControllerAsName(this string controllerName)
        {
            if(string.IsNullOrWhiteSpace(controllerName)) return string.Empty;

            var words = controllerName.SplitCamelCase();

            if (words.Count > 0 && words[0].Length > 0)
                words[0] = words[0].ToLower();

            var letters = new List<char>();

            foreach (var word in words)
            {
                if(word == "Controller")
                {
                    letters.AddRange("Ctrl");
                }
                else
                {
                    letters.AddRange(word.Take(2).ToList());
                }
            }
            return string.Join("", letters);
        }

        /// <summary>
        /// This method gets a text that may have thousand separators and currency symbols and parses it to a decimal
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static decimal ParseCurrencyTextToDecimal(this string text)
        {
            decimal money = 0;
            if (!string.IsNullOrWhiteSpace(text))
            {
                if (decimal.TryParse(text, out money))
                {
                    return money;
                }
                else
                {
                    var usCulture = new CultureInfo("es-ES", false);
                    if (decimal.TryParse(text, NumberStyles.Currency, usCulture, out money))
                    {
                        return money;
                    }
                } 
            }
            return money;
        }
    }
}