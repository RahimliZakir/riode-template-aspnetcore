using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Areas.Admin.Models.FormModel
{
    public class ImageItemFormModel
    {
        public int? Id { get; set; }

        public bool IsMain { get; set; }

        public string TempPath { get; set; }

        public IFormFile File { get; set; }
    }
}
