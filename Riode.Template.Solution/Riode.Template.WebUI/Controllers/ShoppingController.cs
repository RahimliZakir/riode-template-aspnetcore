using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Controllers
{
    public class ShoppingController : Controller
    {
        public IActionResult Cart()
        {
            return View();
        }
    }
}
