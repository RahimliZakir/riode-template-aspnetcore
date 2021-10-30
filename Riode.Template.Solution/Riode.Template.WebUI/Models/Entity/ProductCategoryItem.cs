using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.Entity
{
    public class ProductCategoryItem : BaseEntity
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Value { get; set; }
    }
}
