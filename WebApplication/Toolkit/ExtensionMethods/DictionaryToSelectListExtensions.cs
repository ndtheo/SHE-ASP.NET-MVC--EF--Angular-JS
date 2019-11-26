#region Using Directives

using System.Collections.Generic;
using System.Web.Mvc;

#endregion

namespace WebApplication.Toolkit.ExtensionMethods
{
    public static class DictionaryToSelectListExtensions
    {
        public static SelectList ToSelectList<T, TV>(this Dictionary<T, TV> dictionary)
        {
            return new SelectList(dictionary, "Key", "Value");
        }

        public static SelectList ToSelectList<T, TV>(this Dictionary<T, TV> dictionary, string selectedValue)
        {
            return new SelectList(dictionary, "Key", "Value", selectedValue);
        }
    }
}