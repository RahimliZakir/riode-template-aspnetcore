using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static string RemoveHtmlTags(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }

            Regex pattern = new Regex(@"<[^>]*>");

            return pattern.Replace(value, "");
        }
    }
}
