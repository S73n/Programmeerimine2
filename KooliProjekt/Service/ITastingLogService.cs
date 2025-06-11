using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public interface ITastingLogService
    {
        Task<IEnumerable<TastingLog>> GetAllTastingLogsAsync();
        Task<TastingLog> GetTastingLogByIdAsync(int id);
        Task CreateTastingLogAsync(TastingLog tastingLog);
        Task UpdateTastingLogAsync(TastingLog tastingLog);
        Task DeleteTastingLogAsync(int id);
        Task<bool> TastingLogExistsAsync(int id);
    }
} 