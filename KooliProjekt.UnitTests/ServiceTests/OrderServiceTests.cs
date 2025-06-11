using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Service;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class OrderServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task CreateOrderAsync_should_add_order()
        {
            var service = new OrderService(DbContext);
            var customer = new Customer { Name = "Test", Email = "test@example.com", Phone = "12345678", Address = "Test Address" };
            DbContext.Customers.Add(customer);
            DbContext.SaveChanges();
            var order = new Order { OrderNumber = "ORD-1", Status = "status", CustomerId = customer.Id, Notes = "test notes" };
            await service.CreateOrderAsync(order);
            Assert.Equal(1, DbContext.Orders.Count());
        }

        [Fact]
        public async Task UpdateOrderAsync_should_update_order()
        {
            var service = new OrderService(DbContext);
            var customer = new Customer { Name = "Test", Email = "test@example.com", Phone = "12345678", Address = "Test Address" };
            DbContext.Customers.Add(customer);
            DbContext.SaveChanges();
            var order = new Order { OrderNumber = "ORD-1", Status = "status", CustomerId = customer.Id, Notes = "test notes" };
            DbContext.Orders.Add(order);
            DbContext.SaveChanges();
            order.Status = "updated";
            order.Notes = "updated notes";
            await service.UpdateOrderAsync(order);
            Assert.Equal("updated", DbContext.Orders.First().Status);
        }

        [Fact]
        public async Task DeleteOrderAsync_should_remove_order()
        {
            var service = new OrderService(DbContext);
            var customer = new Customer { Name = "Test", Email = "test@example.com", Phone = "12345678", Address = "Test Address" };
            DbContext.Customers.Add(customer);
            DbContext.SaveChanges();
            var order = new Order { OrderNumber = "ORD-1", Status = "status", CustomerId = customer.Id, Notes = "test notes" };
            DbContext.Orders.Add(order);
            DbContext.SaveChanges();
            await service.DeleteOrderAsync(order.Id);
            Assert.Empty(DbContext.Orders);
        }

        [Fact]
        public async Task GetOrderByIdAsync_should_return_order()
        {
            var service = new OrderService(DbContext);
            var customer = new Customer { Name = "Test", Email = "test@example.com", Phone = "12345678", Address = "Test Address" };
            DbContext.Customers.Add(customer);
            DbContext.SaveChanges();
            var order = new Order { OrderNumber = "ORD-1", Status = "status", CustomerId = customer.Id, Notes = "test notes" };
            DbContext.Orders.Add(order);
            DbContext.SaveChanges();
            var result = await service.GetOrderByIdAsync(order.Id);
            Assert.NotNull(result);
            Assert.Equal(order.OrderNumber, result.OrderNumber);
        }
    }
} 