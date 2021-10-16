using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.Entity
{
    public class Contact
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Bu xana boş buraxılmamalıdır!")]
        public string Name { get; set; }

        [MaxLength(75, ErrorMessage = "Maksimum simvol sayını keçmək olmaz!")]
        [Required(ErrorMessage = "Bu xana boş buraxılmamalıdır!")]
        public string Email { get; set; }

        [StringLength(200, ErrorMessage = "Maksimum simvol sayını keçmək olmaz!")]
        [Required(ErrorMessage = "Bu xana boş buraxılmamalıdır!")]
        public string Comment { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; }

        public string AnswerComment { get; set; }

        public DateTime? AnswerDate { get; set; }

        public int? AnsweredByUserId { get; set; }
    }
}
