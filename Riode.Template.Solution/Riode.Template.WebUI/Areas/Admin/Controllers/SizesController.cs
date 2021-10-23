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
    public class SizesController : Controller
    {
        private readonly RiodeDbContext _context;

        public SizesController(RiodeDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Sizes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sizes.Where(m => m.DeletedDate == null).ToListAsync());
        }

        // GET: Admin/Sizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _context.Sizes
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedDate == null);
            if (size == null)
            {
                return NotFound();
            }

            return View(size);
        }

        // GET: Admin/Sizes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Abbr,Name,Description,Id,CreatedDate,CreatedByUserId,DeletedDate,DeletedByUserId")] Size size)
        {
            if (ModelState.IsValid)
            {
                _context.Add(size);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(size);
        }

        // GET: Admin/Sizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var size = await _context.Sizes.FirstOrDefaultAsync(m => m.Id == id && m.DeletedDate == null);
            if (size == null)
            {
                return NotFound();
            }
            return View(size);
        }

        // POST: Admin/Sizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Abbr,Name,Description,Id,CreatedDate,CreatedByUserId,DeletedDate,DeletedByUserId")] Size size)
        {
            if (id != size.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(size);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SizeExists(size.Id))
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
            return View(size);
        }

        // POST: Admin/Sizes/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Size size = await _context.Sizes.FirstOrDefaultAsync(m => m.Id.Equals(id) && m.DeletedDate == null);

            if (size == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Məlumat tapılmadı!"
                });
            }

            size.DeletedDate = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            return Json(new
            {
                error = false,
                message = "Seçdiyiniz məlumat uğurla silindi!"
            });
        }

        private bool SizeExists(int id)
        {
            return _context.Sizes.Any(e => e.Id == id && e.DeletedDate == null);
        }
    }
}
