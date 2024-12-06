using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public interface IBeerService
    {
        Task<IEnumerable<Beer>> GetAllBeersAsync();
        Task<Beer> GetBeerByIdAsync(int id);
        Task<Beer> CreateBeerAsync(Beer beer);
        Task<Beer> UpdateBeerAsync(Beer beer);
        Task<bool> DeleteBeerAsync(int id);
        Task<bool> BeerExistsAsync(int id);
        Task<PagedResult<Beer>> GetBeersAsync(int page, int pageSize);
    }
}
