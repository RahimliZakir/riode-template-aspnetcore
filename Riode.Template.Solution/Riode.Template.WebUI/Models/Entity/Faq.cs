using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.Entity
{
    public class Faq
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bu hissə boş saxlanıla bilməz!")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Bu hissə boş saxlanıla bilməz!")]
        public string Answer { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

#nullable enable
        public int? CreatedByUserId { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedByUserId { get; set; }
#nullable disable
    }
}
