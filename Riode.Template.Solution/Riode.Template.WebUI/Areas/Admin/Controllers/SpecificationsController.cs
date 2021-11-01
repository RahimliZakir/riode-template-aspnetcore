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
    public class SpecificationsController : Controller
    {
        private readonly RiodeDbContext _context;

        public SpecificationsController(RiodeDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Specifications
        public async Task<IActionResult> Index()
        {
            IEnumerable<Specification> data = await _context.Specifications.Where(s => s.DeletedDate == null).ToListAsync();

            return View(data);
        }

        // GET: Admin/Specifications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = await _context.Specifications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specification == null)
            {
                return NotFound();
            }

            return View(specification);
        }

        // GET: Admin/Specifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Specifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Id,CreatedDate,CreatedByUserId,DeletedDate,DeletedByUserId")] Specification specification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specification);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specification);
        }

        // GET: Admin/Specifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = await _context.Specifications.FindAsync(id);
            if (specification == null)
            {
                return NotFound();
            }
            return View(specification);
        }

        // POST: Admin/Specifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Id,CreatedDate,CreatedByUserId,DeletedDate,DeletedByUserId")] Specification specification)
        {
            if (id != specification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecificationExists(specification.Id))
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
            return View(specification);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Specification specification = await _context.Specifications.FirstOrDefaultAsync(s => s.Id == id && s.DeletedDate == null);

            if (specification == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Məlumat tapılmadı!"
                });
            }

            specification.DeletedDate = DateTime.UtcNow.AddHours(4);
            await _context.SaveChangesAsync();

            return Json(new
            {
                error = false,
                message = "Seçdiyiniz məlumat uğurla silindi!"
            });
        }

        private bool SpecificationExists(int id)
        {
            return _context.Specifications.Any(e => e.Id == id);
        }
    }
}
