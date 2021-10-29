using Microsoft.AspNetCore.Html;
using Riode.Template.WebUI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        //Recursion
        public static IHtmlContent GetCategoriesRaw(this IEnumerable<Category> categories)
        {
            if (categories == null || !categories.Any())
                return HtmlString.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class='widget-body filter-items search-ul'>");

            foreach (Category category in categories.Where(c => c.ParentId == null && c.DeletedDate == null))
            {
                FillCategoriesRaw(category);
            }

            sb.Append("</ul>");

            return new HtmlString(sb.ToString());

            void FillCategoriesRaw(Category category)
            {
                sb.Append($"<li><a href='#'>{category.Name}</a>");

                if (category.Children != null && category.Children.Any())
                {
                    sb.Append("<ul>");

                    foreach (Category item in category.Children)
                    {
                        FillCategoriesRaw(item);
                    }

                    sb.Append("</ul>");
                }

                sb.Append("</li>");
            }
        }
    }
}
