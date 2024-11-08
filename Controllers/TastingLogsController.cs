using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Controllers
{
    public class TastingLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TastingLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TastingLogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.TastingLogs.ToListAsync());
        }

        // GET: TastingLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tastingLog = await _context.TastingLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tastingLog == null)
            {
                return NotFound();
            }

            return View(tastingLog);
        }

        // GET: TastingLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TastingLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,User,Rating")] TastingLog tastingLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tastingLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tastingLog);
        }

        // GET: TastingLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tastingLog = await _context.TastingLogs.FindAsync(id);
            if (tastingLog == null)
            {
                return NotFound();
            }
            return View(tastingLog);
        }

        // POST: TastingLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,User,Rating")] TastingLog tastingLog)
        {
            if (id != tastingLog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tastingLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TastingLogExists(tastingLog.Id))
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
            return View(tastingLog);
        }

        // GET: TastingLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tastingLog = await _context.TastingLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tastingLog == null)
            {
                return NotFound();
            }

            return View(tastingLog);
        }

        // POST: TastingLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tastingLog = await _context.TastingLogs.FindAsync(id);
            if (tastingLog != null)
            {
                _context.TastingLogs.Remove(tastingLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TastingLogExists(int id)
        {
            return _context.TastingLogs.Any(e => e.Id == id);
        }
    }
}
