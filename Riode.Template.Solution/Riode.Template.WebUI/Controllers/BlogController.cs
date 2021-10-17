using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Dropdown
        public IActionResult Classic()
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Listing()
        {
            return View();
        }

        //Grid
        public IActionResult GridTwoColumns()
        {
            return View();
        }

        public IActionResult GridThreeColumns()
        {
            return View();
        }

        public IActionResult GridFourColumns()
        {
            return View();
        }

        public IActionResult GridSidebar()
        {
            return View();
        }
        //Grid

        //Masonry
        public IActionResult MasonryTwoColumns()
        {
            return View();
        }

        public IActionResult MasonryThreeColumns()
        {
            return View();
        }

        public IActionResult MasonryFourColumns()
        {
            return View();
        }

        public IActionResult MasonrySidebar()
        {
            return View();
        }
        //Masonry

        //Mask
        public IActionResult BlogMaskGrid()
        {
            return View();
        }

        public IActionResult BlogMaskMasonry()
        {
            return View();
        }
        //Mask

        public IActionResult SinglePost()
        {
            return View();
        }
        //Dropdown
    }
}
