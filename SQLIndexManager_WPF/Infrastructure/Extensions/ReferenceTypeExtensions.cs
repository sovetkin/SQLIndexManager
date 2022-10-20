using System;
using System.Collections.Generic;

namespace SQLIndexManager_WPF.Infrastructure.Extensions
{
    internal static class ReferenceTypeExtensions
    {
        public static T ToEnum<T>(this object value) => (T)Enum.Parse(typeof(T), value.ToString(), true);

        public static List<string> RemoveInvalidTokens(this List<string> value)
        {
            var items = new List<string>();
            foreach (string item in value)
            {
                string t = item.Replace("'", "").Trim();
                if (!string.IsNullOrEmpty(t))
                    items.Add(t);
            }
            return items;
        }
    }
}
