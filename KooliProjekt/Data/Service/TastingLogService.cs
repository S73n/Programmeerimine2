using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<TastingLog> CreateTastingLogAsync(TastingLog tastingLog)
        {
            _context.TastingLogs.Add(tastingLog);
            await _context.SaveChangesAsync();
            return tastingLog;
        }

        public async Task<TastingLog> UpdateTastingLogAsync(TastingLog tastingLog)
        {
            _context.TastingLogs.Update(tastingLog);
            await _context.SaveChangesAsync();
            return tastingLog;
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
    }
}
