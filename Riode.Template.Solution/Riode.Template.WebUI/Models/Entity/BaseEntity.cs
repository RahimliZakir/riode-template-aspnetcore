using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Models.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);

#nullable enable
        public int? CreatedByUserId { get; set; }

        public DateTime? DeletedDate { get; set; }

        public int? DeletedByUserId { get; set; }
#nullable disable
    }
}
