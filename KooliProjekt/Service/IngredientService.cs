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
    public class IngredientService : IIngredientService
    {
        private readonly ApplicationDbContext _context;

        public IngredientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync(IngredientSearchParameters searchParameters)
        {
            var query = _context.Ingredients.AsQueryable();

            // Apply search filters
            if (!string.IsNullOrWhiteSpace(searchParameters.SearchString))
            {
                query = query.Where(i => i.Name.Contains(searchParameters.SearchString) ||
                                       i.Description.Contains(searchParameters.SearchString));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Name))
            {
                query = query.Where(i => i.Name.Contains(searchParameters.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Type))
            {
                query = query.Where(i => i.Type.Contains(searchParameters.Type));
            }

            if (searchParameters.MinQuantity.HasValue)
            {
                query = query.Where(i => i.Quantity >= searchParameters.MinQuantity.Value);
            }

            if (searchParameters.MaxQuantity.HasValue)
            {
                query = query.Where(i => i.Quantity <= searchParameters.MaxQuantity.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Unit))
            {
                query = query.Where(i => i.Unit == searchParameters.Unit);
            }

            if (searchParameters.MinPrice.HasValue)
            {
                query = query.Where(i => i.Price >= searchParameters.MinPrice.Value);
            }

            if (searchParameters.MaxPrice.HasValue)
            {
                query = query.Where(i => i.Price <= searchParameters.MaxPrice.Value);
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(searchParameters.SortColumn))
            {
                query = searchParameters.SortOrder?.ToLower() == "desc" 
                    ? query.OrderByDescending(i => EF.Property<object>(i, searchParameters.SortColumn))
                    : query.OrderBy(i => EF.Property<object>(i, searchParameters.SortColumn));
            }

            // Apply paging
            return await query
                .Skip((searchParameters.PageNumber - 1) * searchParameters.PageSize)
                .Take(searchParameters.PageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalIngredientsCountAsync(IngredientSearchParameters searchParameters)
        {
            var query = _context.Ingredients.AsQueryable();

            // Apply the same filters as in GetIngredientsAsync
            if (!string.IsNullOrWhiteSpace(searchParameters.SearchString))
            {
                query = query.Where(i => i.Name.Contains(searchParameters.SearchString) ||
                                       i.Description.Contains(searchParameters.SearchString));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Name))
            {
                query = query.Where(i => i.Name.Contains(searchParameters.Name));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Type))
            {
                query = query.Where(i => i.Type.Contains(searchParameters.Type));
            }

            if (searchParameters.MinQuantity.HasValue)
            {
                query = query.Where(i => i.Quantity >= searchParameters.MinQuantity.Value);
            }

            if (searchParameters.MaxQuantity.HasValue)
            {
                query = query.Where(i => i.Quantity <= searchParameters.MaxQuantity.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Unit))
            {
                query = query.Where(i => i.Unit == searchParameters.Unit);
            }

            if (searchParameters.MinPrice.HasValue)
            {
                query = query.Where(i => i.Price >= searchParameters.MinPrice.Value);
            }

            if (searchParameters.MaxPrice.HasValue)
            {
                query = query.Where(i => i.Price <= searchParameters.MaxPrice.Value);
            }

            return await query.CountAsync();
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await _context.Ingredients
                .Include(i => i.Beers)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task CreateIngredientAsync(Ingredient ingredient)
        {
            _context.Add(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            _context.Update(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIngredientAsync(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IngredientExistsAsync(int id)
        {
            return await _context.Ingredients.AnyAsync(e => e.Id == id);
        }
    }
} 