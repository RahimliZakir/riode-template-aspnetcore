using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.Entity
{
    public class Size : BaseEntity
    {
        [Required(ErrorMessage = "Bu hissə boş saxlanıla bilməz!")]
        public string Abbr { get; set; }

        [Required(ErrorMessage = "Bu hissə boş saxlanıla bilməz!")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
