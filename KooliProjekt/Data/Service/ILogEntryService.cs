using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public interface ILogEntryService
    {
        Task<IEnumerable<LogEntry>> GetAllLogEntriesAsync();
        Task<LogEntry> GetLogEntryByIdAsync(int id);
        Task<LogEntry> CreateLogEntryAsync(LogEntry logEntry);
        Task<LogEntry> UpdateLogEntryAsync(LogEntry logEntry);
        Task DeleteLogEntryAsync(int id);
    }
}
