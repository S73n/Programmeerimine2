using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public class TastingLogService : ITastingLogService
    {
        private readonly ApplicationDbContext _context;

        public TastingLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TastingLog>> GetAllTastingLogsAsync()
        {
            return await _context.TastingLogs.ToListAsync();
        }

        public async Task<TastingLog> GetTastingLogByIdAsync(int id)
        {
            return await _context.TastingLogs.FindAsync(id);
        }

        public async Task CreateTastingLogAsync(TastingLog tastingLog)
        {
            _context.Add(tastingLog);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTastingLogAsync(TastingLog tastingLog)
        {
            _context.Update(tastingLog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTastingLogAsync(int id)
        {
            var tastingLog = await _context.TastingLogs.FindAsync(id);
            if (tastingLog != null)
            {
                _context.TastingLogs.Remove(tastingLog);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> TastingLogExistsAsync(int id)
        {
            return await _context.TastingLogs.AnyAsync(e => e.Id == id);
        }
    }
} 