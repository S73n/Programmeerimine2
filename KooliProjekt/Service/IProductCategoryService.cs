using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategory>> GetAllProductCategoriesAsync();
        Task<ProductCategory> GetProductCategoryByIdAsync(int id);
        Task CreateProductCategoryAsync(ProductCategory productCategory);
        Task UpdateProductCategoryAsync(ProductCategory productCategory);
        Task DeleteProductCategoryAsync(int id);
        Task<bool> ProductCategoryExistsAsync(int id);
    }
} 