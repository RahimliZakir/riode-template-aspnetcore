using Riode.Template.WebUI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static IEnumerable<Category> GetChildCategories(this Category parent)
        {
            if (parent.ParentId != null)
                yield return parent;

            foreach (var child in parent.Children.SelectMany(c => c.GetChildCategories()))
            {
                yield return child;
            }
        }
    }
}
