using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.Entity
{
    public class Brand
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Brendin adını boş qoymayın!")]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
