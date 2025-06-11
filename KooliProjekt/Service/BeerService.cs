using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;
using KooliProjekt.Search;
using KooliProjekt.Models;

namespace KooliProjekt.Service
{
    public class BeerService : IBeerService
    {
        private readonly ApplicationDbContext _context;

        public BeerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> GetBeersAsync(BeerSearchParameters searchParameters)
        {
            var query = _context.Beers.AsQueryable();

            // Apply search filters
            if (!string.IsNullOrWhiteSpace(searchParameters.SearchString))
            {
                query = query.Where(b => b.BeerName.Contains(searchParameters.SearchString) ||
                                       b.Description.Contains(searchParameters.SearchString));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.BeerName))
            {
                query = query.Where(b => b.BeerName.Contains(searchParameters.BeerName));
            }

            if (searchParameters.MinAlcoholContent.HasValue)
            {
                query = query.Where(b => b.AlcoholContent >= searchParameters.MinAlcoholContent.Value);
            }

            if (searchParameters.MaxAlcoholContent.HasValue)
            {
                query = query.Where(b => b.AlcoholContent <= searchParameters.MaxAlcoholContent.Value);
            }

            if (searchParameters.MinIBU.HasValue)
            {
                query = query.Where(b => b.IBU >= searchParameters.MinIBU.Value);
            }

            if (searchParameters.MaxIBU.HasValue)
            {
                query = query.Where(b => b.IBU <= searchParameters.MaxIBU.Value);
            }

            if (searchParameters.MinSRM.HasValue)
            {
                query = query.Where(b => b.SRM >= searchParameters.MinSRM.Value);
            }

            if (searchParameters.MaxSRM.HasValue)
            {
                query = query.Where(b => b.SRM <= searchParameters.MaxSRM.Value);
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(searchParameters.SortColumn))
            {
                query = searchParameters.SortOrder?.ToLower() == "desc" 
                    ? query.OrderByDescending(b => EF.Property<object>(b, searchParameters.SortColumn))
                    : query.OrderBy(b => EF.Property<object>(b, searchParameters.SortColumn));
            }

            // Apply paging
            return await query
                .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                .Take(searchParameters.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalBeersCountAsync(BeerSearchParameters searchParameters)
        {
            var query = _context.Beers.AsQueryable();

            // Apply the same filters as in GetBeersAsync
            if (!string.IsNullOrWhiteSpace(searchParameters.SearchString))
            {
                query = query.Where(b => b.BeerName.Contains(searchParameters.SearchString) ||
                                       b.Description.Contains(searchParameters.SearchString));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.BeerName))
            {
                query = query.Where(b => b.BeerName.Contains(searchParameters.BeerName));
            }

            if (searchParameters.MinAlcoholContent.HasValue)
            {
                query = query.Where(b => b.AlcoholContent >= searchParameters.MinAlcoholContent.Value);
            }

            if (searchParameters.MaxAlcoholContent.HasValue)
            {
                query = query.Where(b => b.AlcoholContent <= searchParameters.MaxAlcoholContent.Value);
            }

            if (searchParameters.MinIBU.HasValue)
            {
                query = query.Where(b => b.IBU >= searchParameters.MinIBU.Value);
            }

            if (searchParameters.MaxIBU.HasValue)
            {
                query = query.Where(b => b.IBU <= searchParameters.MaxIBU.Value);
            }

            if (searchParameters.MinSRM.HasValue)
            {
                query = query.Where(b => b.SRM >= searchParameters.MinSRM.Value);
            }

            if (searchParameters.MaxSRM.HasValue)
            {
                query = query.Where(b => b.SRM <= searchParameters.MaxSRM.Value);
            }

            return await query.CountAsync();
        }

        public async Task<Beer> GetBeerByIdAsync(int id)
        {
            return await _context.Beers
                .Include(b => b.Ingredients)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task CreateBeerAsync(Beer beer)
        {
            _context.Add(beer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBeerAsync(Beer beer)
        {
            _context.Update(beer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBeerAsync(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer != null)
            {
                _context.Beers.Remove(beer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> BeerExistsAsync(int id)
        {
            return await _context.Beers.AnyAsync(e => e.Id == id);
        }
    }
} 