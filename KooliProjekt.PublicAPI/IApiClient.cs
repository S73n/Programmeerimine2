using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.PublicAPI
{
    public interface IApiClient
    {
        Task<IEnumerable<Beer>> GetBeersAsync();
        Task<Beer> GetBeerByIdAsync(int id);
        Task CreateBeerAsync(Beer beer);
        Task UpdateBeerAsync(Beer beer);
        Task DeleteBeerAsync(int id);

        Task<IEnumerable<Ingredient>> GetIngredientsAsync();
        Task<Ingredient> GetIngredientByIdAsync(int id);
        Task CreateIngredientAsync(Ingredient ingredient);
        Task UpdateIngredientAsync(Ingredient ingredient);
        Task DeleteIngredientAsync(int id);

        Task<IEnumerable<Batch>> GetBatchesAsync();
        Task<Batch> GetBatchByIdAsync(int id);
        Task CreateBatchAsync(Batch batch);
        Task UpdateBatchAsync(Batch batch);
        Task DeleteBatchAsync(int id);
    }
} 