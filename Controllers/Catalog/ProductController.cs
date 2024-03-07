using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Google.Protobuf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;
using ProductsCatalog.Client.Data;
using ProductsCatalog.Client.Domain;

namespace ProductsCatalog.Client.Controllers.Catalog
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index([Bind("Name,Id,CatalogId")] Product product)
        {
            return _context.Products != null ?
                View(_context.Products
                    .Include(p => p.Category)
                    .Include(p => p.GeneralNote)
                    .Include(p => p.SpecialNote).Where(p => p.CategoryId.Equals(product.CategoryId) || true)) :
                Problem("Entity set 'ApplicationDbContext.Products' for the catalog is null.");
        }

        // GET: Product/Details/{id}
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            SelectedCategoryWithProduct();
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.GeneralNote)
                .Include(p => p.SpecialNote)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            SelectedCategoryWithProduct();

            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategoryId,Description,Price,GeneralNote,SpecificNote")] Product product)
        {
            SelectedCategoryWithProduct(product.CategoryId);
            product.Id = Guid.NewGuid();

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Product/Edit/{id}
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "Id", product.CategoryId);

            SelectedCategoryWithProduct(product.CategoryId);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,CategoryId,Description,Price,GeneralNote,SpecificNote")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
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
            ViewData["CategoryId"] = new SelectList(_context.ProductCategories, "Id", "Id", product.CategoryId);

            return RedirectToAction(nameof(Index));
        }

        // GET: Product/Delete/{id}
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .ThenInclude(t => t.Catalog)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        protected void SelectedCategoryWithProduct(Guid? selected = default)
        {
            List<ProductCategory> category = _context.ProductCategories
                .ToList();

            ViewBag.SelectedList = new SelectList(category, "Id", "Name", selected);
        }

        // --- not needed for this time
        // [ResponseCache(Duration = 600, VaryByHeader = "currency",Location = ResponseCacheLocation.Any)]
        // private decimal RequestCurrentUSDCurrency()
        // {
        //     var request = (HttpWebRequest) WebRequest.Create("https://api.nbrb.by/exrates/rates/840?parammode=1");
        //
        //     var response = (HttpWebResponse) request.GetResponse();
        //
        //     var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
        //
        //     var data = JObject.Parse(responseString);
        //
        //     return Decimal.Parse(data.GetValue("Cur_OfficialRate").ToString());
        // }
    }
}
