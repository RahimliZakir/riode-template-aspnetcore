using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.Entity
{
    public class ProductSizeColorCategoryCollection : BaseEntity
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int SizeId { get; set; }

        public virtual Size Size { get; set; }

        public int ColorId { get; set; }

        public virtual Color Color { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Price { get; set; }
    }
}
