using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Product Pages
        public IActionResult SimpleProduct()
        {
            return View();
        }

        public IActionResult VariableProduct()
        {
            return View();
        }

        public IActionResult SaleProduct()
        {
            return View();
        }

        public IActionResult FeaturedAndOnSale()
        {
            return View();
        }

        public IActionResult WithLeftSidebar()
        {
            return View();
        }

        public IActionResult WithRightSidebar()
        {
            return View();
        }

        public IActionResult AddCartSticky()
        {
            return View();
        }

        public IActionResult TabInside()
        {
            return View();
        }
        // Product Pages

        //Product Layouts
        public IActionResult GridImages()
        {
            return View();
        }

        public IActionResult Masonry()
        {
            return View();
        }

        public IActionResult GalleryType()
        {
            return View();
        }

        public IActionResult FullWidthLayout()
        {
            return View();
        }

        public IActionResult StickyInfo()
        {
            return View();
        }

        public IActionResult LeftAndRightSticky()
        {
            return View();
        }

        public IActionResult HorizontalThumb()
        {
            return View();
        }

        public IActionResult BuildYourOwn()
        {
            return RedirectToAction(nameof(Index), "Home");
        }
        //Product Layouts
    }
}
