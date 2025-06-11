using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Service;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ProductServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task CreateProductAsync_should_add_product()
        {
            var service = new ProductService(DbContext);
            var category = new ProductCategory { Name = "Cat", Description = "desc" };
            DbContext.ProductCategories.Add(category);
            DbContext.SaveChanges();
            var product = new Product { Name = "Test Product", CategoryId = category.Id, Description = "desc", SKU = "SKU123", Price = 9.99m, StockQuantity = 10 };
            await service.CreateProductAsync(product);
            Assert.Equal(1, DbContext.Products.Count());
        }

        [Fact]
        public async Task UpdateProductAsync_should_update_product()
        {
            var service = new ProductService(DbContext);
            var category = new ProductCategory { Name = "Cat", Description = "desc" };
            DbContext.ProductCategories.Add(category);
            DbContext.SaveChanges();
            var product = new Product { Name = "Test Product", CategoryId = category.Id, Description = "desc", SKU = "SKU123", Price = 9.99m, StockQuantity = 10 };
            DbContext.Products.Add(product);
            DbContext.SaveChanges();
            product.Name = "Updated";
            product.Description = "updated desc";
            await service.UpdateProductAsync(product);
            Assert.Equal("Updated", DbContext.Products.First().Name);
        }

        [Fact]
        public async Task DeleteProductAsync_should_remove_product()
        {
            var service = new ProductService(DbContext);
            var category = new ProductCategory { Name = "Cat", Description = "desc" };
            DbContext.ProductCategories.Add(category);
            DbContext.SaveChanges();
            var product = new Product { Name = "Test Product", CategoryId = category.Id, Description = "desc", SKU = "SKU123", Price = 9.99m, StockQuantity = 10 };
            DbContext.Products.Add(product);
            DbContext.SaveChanges();
            await service.DeleteProductAsync(product.Id);
            Assert.Empty(DbContext.Products);
        }

        [Fact]
        public async Task GetProductByIdAsync_should_return_product()
        {
            var service = new ProductService(DbContext);
            var category = new ProductCategory { Name = "Cat", Description = "desc" };
            DbContext.ProductCategories.Add(category);
            DbContext.SaveChanges();
            var product = new Product { Name = "Test Product", CategoryId = category.Id, Description = "desc", SKU = "SKU123", Price = 9.99m, StockQuantity = 10 };
            DbContext.Products.Add(product);
            DbContext.SaveChanges();
            var result = await service.GetProductByIdAsync(product.Id);
            Assert.NotNull(result);
            Assert.Equal(product.Name, result.Name);
        }
    }
} 