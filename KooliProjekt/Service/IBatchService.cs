using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Service
{
    public interface IBatchService
    {
        Task<IEnumerable<Batch>> GetBatchesAsync(BatchSearchParameters searchParameters);
        Task<int> GetTotalBatchesCountAsync(BatchSearchParameters searchParameters);
        Task<Batch> GetBatchByIdAsync(int id);
        Task CreateBatchAsync(Batch batch);
        Task UpdateBatchAsync(Batch batch);
        Task DeleteBatchAsync(int id);
        Task<bool> BatchExistsAsync(int id);
    }
} 