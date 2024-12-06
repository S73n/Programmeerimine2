using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public interface IBatchService
    {
        Task<IEnumerable<Batch>> GetAllBatchesAsync();
        Task<Batch> GetBatchByIdAsync(int id);
        Task<Batch> CreateBatchAsync(Batch batch);
        Task<Batch> UpdateBatchAsync(Batch batch);
        Task DeleteBatchAsync(int id);
    }
}
