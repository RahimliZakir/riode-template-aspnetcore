using Riode.Template.WebUI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.ViewModel
{
    public class CategoryViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Size> Sizes { get; set; }

        public IEnumerable<Color> Colors { get; set; }

        public IEnumerable<Brand> Brands { get; set; }

        public IEnumerable<ProductSizeColorCategoryCollection> ProductSizeColorCategoryCollections { get; set; }
    }
}
