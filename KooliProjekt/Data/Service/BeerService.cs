using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Service
{
    public class BeerService : IBeerService
    {
        private readonly ApplicationDbContext _context;

        public BeerService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Kõik õlled
        public async Task<IEnumerable<Beer>> GetAllBeersAsync()
        {
            return await _context.Beers.ToListAsync();
        }

        // Üks õlu ID järgi
        public async Task<Beer> GetBeerByIdAsync(int id)
        {
            return await _context.Beers.FindAsync(id);
        }

        // Loo uus õlu
        public async Task<Beer> CreateBeerAsync(Beer beer)
        {
            _context.Beers.Add(beer);
            await _context.SaveChangesAsync();
            return beer;
        }

        // Uuenda olemasolev õlu
        public async Task<Beer> UpdateBeerAsync(Beer beer)
        {
            _context.Beers.Update(beer);
            await _context.SaveChangesAsync();
            return beer;
        }

        // Kustuta õlu
        public async Task<bool> DeleteBeerAsync(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer != null)
            {
                _context.Beers.Remove(beer);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Kontrolli, kas õlu eksisteerib
        public async Task<bool> BeerExistsAsync(int id)
        {
            return await _context.Beers.AnyAsync(e => e.Id == id);
        }

        // Paged õlled
        public async Task<PagedResult<Beer>> GetBeersAsync(int page, int pageSize)
        {
            var query = _context.Beers.AsQueryable();
            var pagedData = await query.GetPagedAsync(page, pageSize);
            return pagedData;
        }
    }
}
