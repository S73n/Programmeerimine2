using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public interface ILogEntryService
    {
        Task<IEnumerable<LogEntry>> GetAllLogEntriesAsync();
        Task<LogEntry> GetLogEntryByIdAsync(int id);
        Task CreateLogEntryAsync(LogEntry logEntry);
        Task UpdateLogEntryAsync(LogEntry logEntry);
        Task DeleteLogEntryAsync(int id);
        Task<bool> LogEntryExistsAsync(int id);
    }
} 