using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KooliProjekt.PublicAPI
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Beer>> GetBeersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Beer>>("beer");
        }

        public async Task<Beer> GetBeerByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Beer>($"beer/{id}");
        }

        public async Task CreateBeerAsync(Beer beer)
        {
            await _httpClient.PostAsJsonAsync("beer", beer);
        }

        public async Task UpdateBeerAsync(Beer beer)
        {
            await _httpClient.PutAsJsonAsync($"beer/{beer.Id}", beer);
        }

        public async Task DeleteBeerAsync(int id)
        {
            await _httpClient.DeleteAsync($"beer/{id}");
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Ingredient>>("ingredient");
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Ingredient>($"ingredient/{id}");
        }

        public async Task CreateIngredientAsync(Ingredient ingredient)
        {
            await _httpClient.PostAsJsonAsync("ingredient", ingredient);
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            await _httpClient.PutAsJsonAsync($"ingredient/{ingredient.Id}", ingredient);
        }

        public async Task DeleteIngredientAsync(int id)
        {
            await _httpClient.DeleteAsync($"ingredient/{id}");
        }

        public async Task<IEnumerable<Batch>> GetBatchesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Batch>>("batch");
        }

        public async Task<Batch> GetBatchByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Batch>($"batch/{id}");
        }

        public async Task CreateBatchAsync(Batch batch)
        {
            await _httpClient.PostAsJsonAsync("batch", batch);
        }

        public async Task UpdateBatchAsync(Batch batch)
        {
            await _httpClient.PutAsJsonAsync($"batch/{batch.Id}", batch);
        }

        public async Task DeleteBatchAsync(int id)
        {
            await _httpClient.DeleteAsync($"batch/{id}");
        }
    }
} 