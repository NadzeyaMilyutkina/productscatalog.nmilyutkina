using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductsCatalog.Client.Data;
using ProductsCatalog.Client.Domain;

namespace ProductsCatalog.Client.Controllers.Catalog
{
    public class CatalogController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Catalog
        public async Task<IActionResult> Index()
        {
              return _context.Catalog != null ?
                          View(await _context.Catalog.Include(x => x.ProductCategories).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Catalog'  is null.");
        }

        // GET: Catalog/Details/{id}
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Catalog == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalog
                .Include(d =>d.ProductCategories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // GET: Catalog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Catalog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Domain.Catalog catalog)
        {
            catalog.Id = Guid.NewGuid();
            _context.Add(catalog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Catalog/Edit/{id}
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Catalog == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalog.FindAsync(id);
            if (catalog == null)
            {
                return NotFound();
            }
            return View(catalog);
        }

        // POST: Catalog/Edit/{id}
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Domain.Catalog catalog)
        {
            if (id != catalog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogExists(catalog.Id))
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
            return View(catalog);
        }

        // GET: Catalog/Delete/{id}
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Catalog == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalog
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // POST: Catalog/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Catalog == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Catalog'  is null.");
            }
            var catalog = await _context.Catalog.FindAsync(id);
            if (catalog != null)
            {
                _context.Catalog.Remove(catalog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogExists(Guid id)
        {
          return (_context.Catalog?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
