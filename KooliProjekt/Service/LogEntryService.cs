using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

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

        public async Task CreateLogEntryAsync(LogEntry logEntry)
        {
            _context.Add(logEntry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLogEntryAsync(LogEntry logEntry)
        {
            _context.Update(logEntry);
            await _context.SaveChangesAsync();
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

        public async Task<bool> LogEntryExistsAsync(int id)
        {
            return await _context.LogEntries.AnyAsync(e => e.Id == id);
        }
    }
} 