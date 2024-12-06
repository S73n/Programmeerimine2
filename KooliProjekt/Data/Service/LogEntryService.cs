using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Service
{
    public class LogEntryService : ILogEntryService
    {
        private readonly ApplicationDbContext _context;

        public LogEntryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LogEntry>> GetAllLogEntriesAsync()
        {
            return await _context.LogEntries.ToListAsync();
        }

        public async Task<LogEntry> GetLogEntryByIdAsync(int id)
        {
            return await _context.LogEntries.FindAsync(id);
        }

        public async Task<LogEntry> CreateLogEntryAsync(LogEntry logEntry)
        {
            _context.LogEntries.Add(logEntry);
            await _context.SaveChangesAsync();
            return logEntry;
        }

        public async Task<LogEntry> UpdateLogEntryAsync(LogEntry logEntry)
        {
            _context.LogEntries.Update(logEntry);
            await _context.SaveChangesAsync();
            return logEntry;
        }

        public async Task DeleteLogEntryAsync(int id)
        {
            var logEntry = await _context.LogEntries.FindAsync(id);
            if (logEntry != null)
            {
                _context.LogEntries.Remove(logEntry);
                await _context.SaveChangesAsync();
            }
        }
    }
}
