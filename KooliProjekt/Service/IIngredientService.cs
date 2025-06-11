using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Service
{
    public interface IIngredientService
    {
        Task<IEnumerable<Ingredient>> GetIngredientsAsync(IngredientSearchParameters searchParameters);
        Task<int> GetTotalIngredientsCountAsync(IngredientSearchParameters searchParameters);
        Task<Ingredient> GetIngredientByIdAsync(int id);
        Task CreateIngredientAsync(Ingredient ingredient);
        Task UpdateIngredientAsync(Ingredient ingredient);
        Task DeleteIngredientAsync(int id);
        Task<bool> IngredientExistsAsync(int id);
    }
} 