using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static string EllipseText(this string value, int max = 100)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }
            else if (value.Length <= max)
            {
                return value;
            }

            return $"{value.Substring(0, max)}...";
        }
    }
}
