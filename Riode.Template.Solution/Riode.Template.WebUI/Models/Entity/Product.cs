﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.Entity
{
    public class Product : BaseEntity
    {
        public string StockKeepingUnit { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public int BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<ProductCategoryItem> ProductCategoryItems { get; set; }
        public virtual ICollection<SpecificationProductItem> SpecificationProductItems { get; set; }
    }
}