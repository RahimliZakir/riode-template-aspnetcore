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
    public class ProductSizeColorCategoryCollectionsController : Controller
    {
        private readonly RiodeDbContext _context;

        public ProductSizeColorCategoryCollectionsController(RiodeDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductSizeColorCategoryCollections
        public async Task<IActionResult> Index()
        {
            var riodeDbContext = _context.ProductSizeColorCategoryCollections.Include(p => p.Category).Include(p => p.Color).Include(p => p.Product).Include(p => p.Size);
            return View(await riodeDbContext.ToListAsync());
        }

        // GET: Admin/ProductSizeColorCategoryCollections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSizeColorCategoryCollection = await _context.ProductSizeColorCategoryCollections
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Product)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSizeColorCategoryCollection == null)
            {
                return NotFound();
            }

            return View(productSizeColorCategoryCollection);
        }

        // GET: Admin/ProductSizeColorCategoryCollections/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "HexCode");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Abbr");
            return View();
        }

        // POST: Admin/ProductSizeColorCategoryCollections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,SizeId,ColorId,CategoryId,Id,CreatedDate,CreatedByUserId,DeletedDate,DeletedByUserId")] ProductSizeColorCategoryCollection productSizeColorCategoryCollection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productSizeColorCategoryCollection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productSizeColorCategoryCollection.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "HexCode", productSizeColorCategoryCollection.ColorId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productSizeColorCategoryCollection.ProductId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Abbr", productSizeColorCategoryCollection.SizeId);
            return View(productSizeColorCategoryCollection);
        }

        // GET: Admin/ProductSizeColorCategoryCollections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSizeColorCategoryCollection = await _context.ProductSizeColorCategoryCollections.FindAsync(id);
            if (productSizeColorCategoryCollection == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productSizeColorCategoryCollection.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "HexCode", productSizeColorCategoryCollection.ColorId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", productSizeColorCategoryCollection.ProductId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Abbr", productSizeColorCategoryCollection.SizeId);
            return View(productSizeColorCategoryCollection);
        }

        // POST: Admin/ProductSizeColorCategoryCollections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,SizeId,ColorId,CategoryId,Id,CreatedDate,CreatedByUserId,DeletedDate,DeletedByUserId")] ProductSizeColorCategoryCollection productSizeColorCategoryCollection)
        {
            if (id != productSizeColorCategoryCollection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productSizeColorCategoryCollection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductSizeColorCategoryCollectionExists(productSizeColorCategoryCollection.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productSizeColorCategoryCollection.CategoryId);
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "HexCode", productSizeColorCategoryCollection.ColorId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productSizeColorCategoryCollection.ProductId);
            ViewData["SizeId"] = new SelectList(_context.Sizes, "Id", "Abbr", productSizeColorCategoryCollection.SizeId);
            return View(productSizeColorCategoryCollection);
        }

        // GET: Admin/ProductSizeColorCategoryCollections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productSizeColorCategoryCollection = await _context.ProductSizeColorCategoryCollections
                .Include(p => p.Category)
                .Include(p => p.Color)
                .Include(p => p.Product)
                .Include(p => p.Size)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productSizeColorCategoryCollection == null)
            {
                return NotFound();
            }

            return View(productSizeColorCategoryCollection);
        }

        // POST: Admin/ProductSizeColorCategoryCollections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productSizeColorCategoryCollection = await _context.ProductSizeColorCategoryCollections.FindAsync(id);
            _context.ProductSizeColorCategoryCollections.Remove(productSizeColorCategoryCollection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductSizeColorCategoryCollectionExists(int id)
        {
            return _context.ProductSizeColorCategoryCollections.Any(e => e.Id == id);
        }
    }
}
