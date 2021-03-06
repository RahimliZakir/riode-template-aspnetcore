using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.Entity
{
    public class Faq : BaseEntity
    {
        [Required(ErrorMessage = "Bu hissə boş saxlanıla bilməz!")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Bu hissə boş saxlanıla bilməz!")]
        public string Answer { get; set; }
    }
}
