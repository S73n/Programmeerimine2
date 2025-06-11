using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Service;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class CustomerServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task CreateCustomerAsync_should_add_customer()
        {
            var service = new CustomerService(DbContext);
            var customer = new Customer { Name = "Test", Email = "test@example.com", Phone = "12345678", Address = "Test Address" };
            await service.CreateCustomerAsync(customer);
            Assert.Equal(1, DbContext.Customers.Count());
        }

        [Fact]
        public async Task UpdateCustomerAsync_should_update_customer()
        {
            var service = new CustomerService(DbContext);
            var customer = new Customer { Name = "Test", Email = "test@example.com", Phone = "12345678", Address = "Test Address" };
            DbContext.Customers.Add(customer);
            DbContext.SaveChanges();
            customer.Name = "Updated";
            await service.UpdateCustomerAsync(customer);
            Assert.Equal("Updated", DbContext.Customers.First().Name);
        }

        [Fact]
        public async Task DeleteCustomerAsync_should_remove_customer()
        {
            var service = new CustomerService(DbContext);
            var customer = new Customer { Name = "Test", Email = "test@example.com", Phone = "12345678", Address = "Test Address" };
            DbContext.Customers.Add(customer);
            DbContext.SaveChanges();
            await service.DeleteCustomerAsync(customer.Id);
            Assert.Empty(DbContext.Customers);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_should_return_customer()
        {
            var service = new CustomerService(DbContext);
            var customer = new Customer { Name = "Test", Email = "test@example.com", Phone = "12345678", Address = "Test Address" };
            DbContext.Customers.Add(customer);
            DbContext.SaveChanges();
            var result = await service.GetCustomerByIdAsync(customer.Id);
            Assert.NotNull(result);
            Assert.Equal(customer.Name, result.Name);
        }
    }
} 