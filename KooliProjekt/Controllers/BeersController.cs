using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Data;
using KooliProjekt.Service;  // Lisage teenuse nimi

namespace KooliProjekt.Controllers
{
    public class BeersController : Controller
    {
        private readonly IBeerService _beerService;  // Muudame teenuse liidese muutujaks

        // Konstruktor, et teenus oleks kontrolleris kasutamiseks
        public BeersController(IBeerService beerService)
        {
            _beerService = beerService;
        }

        // GET: Beers
        public async Task<IActionResult> Index(int page = 1)
        {
            var pagedData = await _beerService.GetBeersAsync(page, 10); // Kasutame teenuse meetodit
            return View(pagedData);
        }

        // GET: Beers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beer = await _beerService.GetBeerByIdAsync(id.Value);  // Kasutame teenuse meetodit
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
                await _beerService.CreateBeerAsync(beer);  // Kasutame teenuse meetodit
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

            var beer = await _beerService.GetBeerByIdAsync(id.Value);  // Kasutame teenuse meetodit
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
                    await _beerService.UpdateBeerAsync(beer);  // Kasutame teenuse meetodit
                }
                catch
                {
                    if (!await _beerService.BeerExistsAsync(beer.Id))  // Kasutame teenuse meetodit
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

            var beer = await _beerService.GetBeerByIdAsync(id.Value);  // Kasutame teenuse meetodit
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
            await _beerService.DeleteBeerAsync(id);  // Kasutame teenuse meetodit
            return RedirectToAction(nameof(Index));
        }
    }
}
