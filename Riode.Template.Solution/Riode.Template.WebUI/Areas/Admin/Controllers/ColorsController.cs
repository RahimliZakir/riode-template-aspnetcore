using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.Template.WebUI.Models.DataContext;
using Riode.Template.WebUI.Models.Entity;

namespace Riode.Template.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorsController : Controller
    {
        private readonly RiodeDbContext db;

        public ColorsController(RiodeDbContext db)
        {
            this.db = db;
        }

        // GET: Admin/Colors
        public async Task<IActionResult> Index()
        {
            return View(await db.Colors.Where(c => c.DeletedDate == null).ToListAsync());
        }

        // GET: Admin/Colors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await db.Colors
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedDate == null);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Admin/Colors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Colors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HexCode,Name,Description,Id,CreatedDate,UpdatedDate,DeletedDate")] Color color)
        {
            if (ModelState.IsValid)
            {
                db.Add(color);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        // GET: Admin/Colors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var color = await db.Colors.FirstOrDefaultAsync(c => c.Id == id && c.DeletedDate == null);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: Admin/Colors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HexCode,Name,Description,Id,CreatedDate,UpdatedDate,DeletedDate")] Color color)
        {
            if (id != color.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    color.UpdatedDate = DateTime.UtcNow.AddHours(4);
                    db.Update(color);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorExists(color.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        // POST: Admin/Colors/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Color color = await db.Colors
                          .FirstOrDefaultAsync(m => m.Id == id && m.DeletedDate == null);

            if (color == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Məlumat tapılmadı!"
                });
            }

            color.DeletedDate = DateTime.UtcNow.AddHours(4);
            await db.SaveChangesAsync();

            return Json(new
            {
                error = false,
                message = "Seçdiyiniz məlumat uğurla silindi!"
            });
        }

        private bool ColorExists(int id)
        {
            return db.Colors.Any(e => e.Id == id && e.DeletedDate == null);
        }
    }
}
