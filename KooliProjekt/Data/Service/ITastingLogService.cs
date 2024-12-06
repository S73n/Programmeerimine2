using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public interface ITastingLogService
    {
        Task<IEnumerable<TastingLog>> GetAllTastingLogsAsync();
        Task<TastingLog> GetTastingLogByIdAsync(int id);
        Task<TastingLog> CreateTastingLogAsync(TastingLog tastingLog);
        Task<TastingLog> UpdateTastingLogAsync(TastingLog tastingLog);
        Task DeleteTastingLogAsync(int id);
    }
}
