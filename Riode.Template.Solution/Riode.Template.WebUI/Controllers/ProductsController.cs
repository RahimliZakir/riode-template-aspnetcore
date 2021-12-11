using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Template.WebUI.AppCode.Modules;
using Riode.Template.WebUI.Models.DataContext;
using Riode.Template.WebUI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        readonly RiodeDbContext db;
        readonly IMediator mediator;

        public ProductsController(RiodeDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        async public Task<IActionResult> Index(int? id)
        {
            ProductPagedQuery query = new();

            IEnumerable<ProductSizeColorCategoryCollection> data = await mediator.Send(query);
            
            return View(data);
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
