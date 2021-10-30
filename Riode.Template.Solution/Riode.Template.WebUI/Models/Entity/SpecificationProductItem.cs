using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.Entity
{
    public class SpecificationProductItem : BaseEntity
    {
        public int SpecificationId { get; set; }

        public virtual Specification Specification { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
