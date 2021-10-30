using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.Entity
{
    public class Specification : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<SpecificationProductItem> SpecificationProductItems { get; set; }
    }
}
