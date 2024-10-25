using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class BeersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BeersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Beers
        public async Task<IActionResult> Index(int page = 1)
        {
            // Kasuta GetPagedAsync meetodit
            var query = _context.Beers.AsQueryable();
            var pagedData = await query.GetPagedAsync(page, pageSize: 10);

            return View(pagedData);
        }

        // GET: Beers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers.FirstOrDefaultAsync(m => m.Id == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // GET: Beers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Beers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BeerName,BeerDescription")] Beer beer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beer);
        }

        // GET: Beers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            return View(beer);
        }

        // POST: Beers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BeerName,BeerDescription")] Beer beer)
        {
            if (id != beer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeerExists(beer.Id))
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
            return View(beer);
        }

        // GET: Beers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _context.Beers.FirstOrDefaultAsync(m => m.Id == id);
            if (beer == null)
            {
                return NotFound();
            }

            return View(beer);
        }

        // POST: Beers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer != null)
            {
                _context.Beers.Remove(beer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeerExists(int id)
        {
            return _context.Beers.Any(e => e.Id == id);
        }
    }
}
