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
    public class BatchService : IBatchService
    {
        private readonly ApplicationDbContext _context;

        public BatchService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Batch>> GetBatchesAsync(BatchSearchParameters searchParameters)
        {
            var query = _context.Batches.AsQueryable();

            // Apply search filters
            if (!string.IsNullOrWhiteSpace(searchParameters.SearchString))
            {
                query = query.Where(b => b.BatchNumber.Contains(searchParameters.SearchString) ||
                                       b.Beer.BeerName.Contains(searchParameters.SearchString));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.BatchNumber))
            {
                query = query.Where(b => b.BatchNumber.Contains(searchParameters.BatchNumber));
            }

            if (searchParameters.BeerId.HasValue)
            {
                query = query.Where(b => b.BeerId == searchParameters.BeerId.Value);
            }

            if (searchParameters.MinStartDate.HasValue)
            {
                query = query.Where(b => b.StartDate >= searchParameters.MinStartDate.Value);
            }

            if (searchParameters.MaxStartDate.HasValue)
            {
                query = query.Where(b => b.StartDate <= searchParameters.MaxStartDate.Value);
            }

            if (searchParameters.MinEndDate.HasValue)
            {
                query = query.Where(b => b.EndDate >= searchParameters.MinEndDate.Value);
            }

            if (searchParameters.MaxEndDate.HasValue)
            {
                query = query.Where(b => b.EndDate <= searchParameters.MaxEndDate.Value);
            }

            if (searchParameters.MinVolume.HasValue)
            {
                query = query.Where(b => b.Volume >= searchParameters.MinVolume.Value);
            }

            if (searchParameters.MaxVolume.HasValue)
            {
                query = query.Where(b => b.Volume <= searchParameters.MaxVolume.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Status))
            {
                query = query.Where(b => b.Status == searchParameters.Status);
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

        public async Task<int> GetTotalBatchesCountAsync(BatchSearchParameters searchParameters)
        {
            var query = _context.Batches.AsQueryable();

            // Apply the same filters as in GetBatchesAsync
            if (!string.IsNullOrWhiteSpace(searchParameters.SearchString))
            {
                query = query.Where(b => b.BatchNumber.Contains(searchParameters.SearchString) ||
                                       b.Beer.BeerName.Contains(searchParameters.SearchString));
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.BatchNumber))
            {
                query = query.Where(b => b.BatchNumber.Contains(searchParameters.BatchNumber));
            }

            if (searchParameters.BeerId.HasValue)
            {
                query = query.Where(b => b.BeerId == searchParameters.BeerId.Value);
            }

            if (searchParameters.MinStartDate.HasValue)
            {
                query = query.Where(b => b.StartDate >= searchParameters.MinStartDate.Value);
            }

            if (searchParameters.MaxStartDate.HasValue)
            {
                query = query.Where(b => b.StartDate <= searchParameters.MaxStartDate.Value);
            }

            if (searchParameters.MinEndDate.HasValue)
            {
                query = query.Where(b => b.EndDate >= searchParameters.MinEndDate.Value);
            }

            if (searchParameters.MaxEndDate.HasValue)
            {
                query = query.Where(b => b.EndDate <= searchParameters.MaxEndDate.Value);
            }

            if (searchParameters.MinVolume.HasValue)
            {
                query = query.Where(b => b.Volume >= searchParameters.MinVolume.Value);
            }

            if (searchParameters.MaxVolume.HasValue)
            {
                query = query.Where(b => b.Volume <= searchParameters.MaxVolume.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchParameters.Status))
            {
                query = query.Where(b => b.Status == searchParameters.Status);
            }

            return await query.CountAsync();
        }

        public async Task<Batch> GetBatchByIdAsync(int id)
        {
            return await _context.Batches
                .Include(b => b.Beer)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task CreateBatchAsync(Batch batch)
        {
            _context.Add(batch);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBatchAsync(Batch batch)
        {
            _context.Update(batch);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBatchAsync(int id)
        {
            var batch = await _context.Batches.FindAsync(id);
            if (batch != null)
            {
                _context.Batches.Remove(batch);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> BatchExistsAsync(int id)
        {
            return await _context.Batches.AnyAsync(e => e.Id == id);
        }
    }
} 