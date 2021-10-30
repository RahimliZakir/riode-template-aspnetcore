using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Riode.Template.WebUI.Areas.Admin.Models.FormModel;
using Riode.Template.WebUI.Models.DataContext;
using Riode.Template.WebUI.Models.Entity;

namespace Riode.Template.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly RiodeDbContext db;
        readonly IWebHostEnvironment env;

        public ProductsController(RiodeDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        public async Task<IActionResult> Index()
        {
            IIncludableQueryable<Product, ICollection<ProductImage>> riodeDbContext = db.Products
                                                                                      .Include(p => p.Brand)
                                                                                      .Include(p => p.ProductImages);

            return View(await riodeDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await db.Products
                                    .Include(p => p.Brand)
                                    .Include(p => p.ProductImages)
                                    .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(db.Brands, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Files,StockKeepingUnit,Name,ShortDescription,BrandId,Description,Id")] Product product)
        {
            product.ProductImages = new List<ProductImage>();

            foreach (ImageItemFormModel item in product.Files)
            {
                string ext = Path.GetExtension(item.File.FileName);
                string filename = $"product-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                string fullname = Path.Combine(env.WebRootPath, "uploads", "products", filename);

                using (FileStream fs = new FileStream(fullname, FileMode.Create, FileAccess.Write))
                {
                    await item.File.CopyToAsync(fs);
                }

                product.ProductImages.Add(new ProductImage
                {
                    ImagePath = filename,
                    IsMain = item.IsMain
                });
            }


            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BrandId"] = new SelectList(db.Brands, "Id", "Name", product.BrandId);

            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product product = await db.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(c => c.Id.Equals(id));

            if (product == null)
            {
                return NotFound();
            }

            ViewData["BrandId"] = new SelectList(db.Brands, "Id", "Name", product.BrandId);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Files,StockKeepingUnit,Name,ShortDescription,BrandId,Description,Id,CreatedDate,CreatedByUserId,DeletedDate,DeletedByUserId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                IQueryable<Product> entity = db.Products.AsNoTracking().Where(p => p.Id.Equals(id));

                if (entity == null)
                {
                    return NotFound();
                }

                IEnumerable<ProductImage> images = await db.ProductImages.Where(pi => pi.ProductId.Equals(id)).ToListAsync();

                foreach (ProductImage item in images)
                {
                    if (product.Files.Any(f => f.File == null && string.IsNullOrWhiteSpace(f.TempPath) && f.Id == item.Id))
                    {
                        string path = Path.Combine(env.WebRootPath, "uploads", "products", item.ImagePath);

                        if (System.IO.File.Exists(path) && !string.IsNullOrWhiteSpace(path))
                        {
                            System.IO.File.Delete(path);
                        }

                        db.ProductImages.Remove(item);
                    }
                    else if (product.Files.Any(f => f.IsMain && f.Id == item.Id))
                    {
                        item.IsMain = true;
                    }
                    else
                    {
                        item.IsMain = false;
                    }
                }

                product.ProductImages = new List<ProductImage>();

                foreach (ImageItemFormModel item in product.Files.Where(f => f.File != null))
                {
                    string ext = Path.GetExtension(item.File.FileName);
                    string filename = $"product-{Guid.NewGuid().ToString().Replace("-", "")}{ext}";
                    string fullname = Path.Combine(env.WebRootPath, "uploads", "products", filename);

                    using (FileStream fs = new FileStream(fullname, FileMode.Create, FileAccess.Write))
                    {
                        await item.File.CopyToAsync(fs);
                    }

                    product.ProductImages.Add(new ProductImage
                    {
                        ImagePath = filename,
                        IsMain = item.IsMain
                    });
                }

                try
                {
                    db.Products.Update(product);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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

            ViewData["BrandId"] = new SelectList(db.Brands, "Id", "Name", product.BrandId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);

            db.Products.Remove(product);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return db.Products.Any(e => e.Id == id);
        }
    }
}
