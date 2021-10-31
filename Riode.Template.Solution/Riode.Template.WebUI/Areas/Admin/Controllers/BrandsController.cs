using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.Template.WebUI.Models.DataContext;
using Riode.Template.WebUI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Riode.Template.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        readonly RiodeDbContext db;

        public BrandsController(RiodeDbContext db)
        {
            this.db = db;
        }

        async public Task<IActionResult> Index()
        {
            IEnumerable<Brand> brands = await db.Brands.Where(b => b.DeletedDate == null).ToListAsync();

            return View(brands);
            //return PartialView("_Navbar", brands);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        async public Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                await db.Brands.AddAsync(brand);
                await db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        async public Task<IActionResult> Edit(int? id)
        {
            Brand brand = await db.Brands.FirstOrDefaultAsync(b => b.Id.Equals(id) && b.DeletedDate == null);

            return View(brand);
        }

        [HttpPost]
        async public Task<IActionResult> Edit([FromRoute] int? id, Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid == false)
            {
                return View(brand);
            }

            db.Brands.Update(brand);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        async public Task<IActionResult> Details(int? id)
        {
            Brand brand = await db.Brands.FirstOrDefaultAsync(b => b.Id.Equals(id) && b.DeletedDate == null);

            return View(brand);
        }

        [HttpPost]
        async public Task<IActionResult> Delete(int? id)
        {
            Brand brand = await db.Brands.FirstOrDefaultAsync(b => b.Id.Equals(id) && b.DeletedDate == null);

            if (brand == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Məlumat tapılmadı!"
                });
            }

            brand.DeletedDate = DateTime.UtcNow.AddHours(4);
            await db.SaveChangesAsync();

            return Json(new
            {
                error = false,
                message = "Seçdiyiniz məlumat uğurla silindi!"
            });
        }
    }
}
