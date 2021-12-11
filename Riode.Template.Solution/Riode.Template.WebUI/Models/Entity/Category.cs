using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.Entity
{
    public class Category : BaseEntity
    {
        public int? ParentId { get; set; }

        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> Children { get; set; }

        [Required(ErrorMessage = "Bu xana boş buraxıla bilməz!")]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ProductCategoryItem> ProductCategoryItems { get; set; }

        public virtual ICollection<ProductSizeColorCategoryCollection> ProductSizeColorCategoryCollections { get; set; }
    }
}


