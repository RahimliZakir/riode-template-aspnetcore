using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Template.WebUI.Models.DataContext;
using Riode.Template.WebUI.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        readonly RiodeDbContext db;

        public CategoriesController(RiodeDbContext db)
        {
            this.db = db;
        }

        async public Task<IActionResult> Index()
        {
            var viewModel = new CategoryViewModel();

            viewModel.Categories = await db.Categories
                                  .Include(c => c.Children)
                                  .ThenInclude(c => c.Children) // TehnInclude Include elediyimiz Children'in ichindeki Children-lari getirecek qatman-qatman gedirik!
                                  .Where(c => c.DeletedDate == null)
                                  .ToListAsync();

            viewModel.Brands = await db.Brands.Where(b => b.DeletedDate == null).ToListAsync();
            viewModel.Colors = await db.Colors.Where(c => c.DeletedDate == null).ToListAsync();
            viewModel.Sizes = await db.Sizes.Where(s => s.DeletedDate == null).ToListAsync();

            return View(viewModel);
        }

        // Variations - 1
        public IActionResult BannerWithSidebar()
        {
            return View();
        }

        public IActionResult BoxedBanner()
        {
            return View();
        }

        public IActionResult InfiniteAJAXScroll()
        {
            return View();
        }

        public IActionResult HorizontalFilter()
        {
            return View();
        }

        public IActionResult NavigationFilter()
        {
            return View();
        }

        public IActionResult OffCanvasFilter()
        {
            return View();
        }

        public IActionResult RightToggleSidebar()
        {
            return View();
        }
        // Variations - 1

        // Variations - 2
        public IActionResult ThreeColumnsMode()
        {
            return View();
        }

        public IActionResult FourColumnsMode()
        {
            return View();
        }

        public IActionResult FiveColumnsMode()
        {
            return View();
        }

        public IActionResult SixColumnsMode()
        {
            return View();
        }

        public IActionResult SevenColumnsMode()
        {
            return View();
        }

        public IActionResult EightColumnsMode()
        {
            return View();
        }

        public IActionResult ListMode()
        {
            return View();
        }
        // Variations - 1
    }
}
