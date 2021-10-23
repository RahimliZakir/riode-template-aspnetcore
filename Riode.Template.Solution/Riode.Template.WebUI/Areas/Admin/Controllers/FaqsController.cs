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
    public class FaqsController : Controller
    {
        private readonly RiodeDbContext db;

        public FaqsController(RiodeDbContext db)
        {
            this.db = db;
        }

        // GET: Admin/Faqs
        public async Task<IActionResult> Index()
        {
            return View(await db.Faqs.Where(c => c.DeletedDate == null).ToListAsync());
        }

        // GET: Admin/Faqs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await db.Faqs
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedDate == null);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

        // GET: Admin/Faqs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Faqs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Question,Answer,CreatedDate,CreatedByUserId,DeletedDate,DeletedByUserId")] Faq faq)
        {
            if (ModelState.IsValid)
            {
                db.Add(faq);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faq);
        }

        // GET: Admin/Faqs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faq = await db.Faqs.FirstOrDefaultAsync(c => c.Id == id && c.DeletedDate == null);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        // POST: Admin/Faqs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Question,Answer,CreatedDate,CreatedByUserId,DeletedDate,DeletedByUserId")] Faq faq)
        {
            if (id != faq.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(faq);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaqExists(faq.Id))
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
            return View(faq);
        }

        // POST: Admin/Faqs/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            Faq faq = await db.Faqs
                           .FirstOrDefaultAsync(m => m.Id == id && m.DeletedDate == null);

            if (faq == null)
            {
                return Json(new
                {
                    error = true,
                    message = "Məlumat tapılmadı!"
                });
            }

            faq.DeletedDate = DateTime.UtcNow.AddHours(4);
            await db.SaveChangesAsync();

            return Json(new
            {
                error = false,
                message = "Seçdiyiniz məlumat uğurla silindi!"
            });
        }

        private bool FaqExists(int id)
        {
            return db.Faqs.Any(e => e.Id == id);
        }
    }
}
