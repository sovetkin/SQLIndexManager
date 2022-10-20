using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLIndexManager_WPF.Infrastructure.Extensions
{
    internal static class BaseTypesExtensions
    {
        public static string OnOff(this bool value) => value ? "ON" : "OFF";

        public static bool IsBetween(this int value, int minimum, int maximum) => value >= minimum && value <= maximum;

        public static string Sort(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value.Replace("], ", "],")
                .Split(',')
                .ToList()
                .OrderBy(_ => _)
                .Aggregate((_, __) => _ + __);
        }

        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            maxLength = Math.Abs(maxLength);

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string ToQuota(this string value) =>
            $"[{value?.Replace("[", "[[").Replace("]", "]]")}]";

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static int PageSize(this int val) => val * 1024 / 8;

        public static string FormatSize(this decimal val)
        {
            decimal aval = Math.Abs(val);
            string dimension = "KB";

            if (aval > 1024 * 1024 * 1024)
            {
                val = val / 1024 / 1024 / 1024;
                dimension = "TB";
            }
            else if (aval > 1024 * 1024)
            {
                val = val / 1024 / 1024;
                dimension = "GB";
            }
            else if (aval > 1024)
            {
                val /= 1024;
                dimension = "MB";
            }

            //TODO: Add format provider in ToString() function to better compatibility
            return $"{ (val.ToString(val - Math.Truncate(val) == 0 ? "N0" : "N2")) } {dimension}";
        }

        public static string FormatMbSize(this int val)
        {
            decimal value = val;
            string dimension = "MB";

            if (value > 1000)
            {
                value /= 1024;
                dimension = "GB";
            }

            return $"{ (value.ToString(value - Math.Truncate(value) == 0 ? "N0" : "N2")) } {dimension}";
        }

        public static string Description(this Enum value)
        {
            var da = (DescriptionAttribute[])value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return da.Length > 0 ? da[0].Description : value.ToString();
        }
    }
}
