using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Service
{
    public interface IBeerService
    {
        Task<IEnumerable<Beer>> GetBeersAsync(BeerSearchParameters searchParameters);
        Task<int> GetTotalBeersCountAsync(BeerSearchParameters searchParameters);
        Task<Beer> GetBeerByIdAsync(int id);
        Task CreateBeerAsync(Beer beer);
        Task UpdateBeerAsync(Beer beer);
        Task DeleteBeerAsync(int id);
        Task<bool> BeerExistsAsync(int id);
    }
} 