using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Service;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ProductCategoryServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task CreateProductCategoryAsync_should_add_category()
        {
            var service = new ProductCategoryService(DbContext);
            var category = new ProductCategory { Name = "Test Category", Description = "desc" };
            await service.CreateProductCategoryAsync(category);
            Assert.Equal(1, DbContext.ProductCategories.Count());
        }

        [Fact]
        public async Task UpdateProductCategoryAsync_should_update_category()
        {
            var service = new ProductCategoryService(DbContext);
            var category = new ProductCategory { Name = "Test Category", Description = "desc" };
            DbContext.ProductCategories.Add(category);
            DbContext.SaveChanges();
            category.Name = "Updated";
            category.Description = "updated desc";
            await service.UpdateProductCategoryAsync(category);
            Assert.Equal("Updated", DbContext.ProductCategories.First().Name);
            Assert.Equal("updated desc", DbContext.ProductCategories.First().Description);
        }

        [Fact]
        public async Task DeleteProductCategoryAsync_should_remove_category()
        {
            var service = new ProductCategoryService(DbContext);
            var category = new ProductCategory { Name = "Test Category", Description = "desc" };
            DbContext.ProductCategories.Add(category);
            DbContext.SaveChanges();
            await service.DeleteProductCategoryAsync(category.Id);
            Assert.Empty(DbContext.ProductCategories);
        }

        [Fact]
        public async Task GetProductCategoryByIdAsync_should_return_category()
        {
            var service = new ProductCategoryService(DbContext);
            var category = new ProductCategory { Name = "Test Category", Description = "desc" };
            DbContext.ProductCategories.Add(category);
            DbContext.SaveChanges();
            var result = await service.GetProductCategoryByIdAsync(category.Id);
            Assert.NotNull(result);
            Assert.Equal(category.Name, result.Name);
            Assert.Equal(category.Description, result.Description);
        }
    }
} 