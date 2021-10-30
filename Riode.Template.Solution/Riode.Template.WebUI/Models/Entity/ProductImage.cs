using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.Entity
{
    public class ProductImage : BaseEntity
    {
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string ImagePath { get; set; }

        public bool IsMain { get; set; }
    }
}
