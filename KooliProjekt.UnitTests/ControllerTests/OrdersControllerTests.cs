using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Service;
using KooliProjekt.Search;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderService> _orderServiceMock = new Mock<IOrderService>();
        private readonly ApplicationDbContext _dbContext;
        private readonly OrdersController _controller;

        public OrdersControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _controller = new OrdersController(_dbContext, _orderServiceMock.Object);
        }

        [Fact]
        public void Create_should_return_view()
        {
            var result = _controller.Create() as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Create");
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            int? id = null;
            var result = await _controller.Details(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_order_is_missing()
        {
            int id = 1;
            _orderServiceMock.Setup(x => x.GetOrderByIdAsync(id)).ReturnsAsync((Order)null);
            var result = await _controller.Details(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_order_found()
        {
            int id = 1;
            var order = new Order {
                Id = id,
                OrderNumber = "ORD-1",
                Notes = "notes",
                Status = "status"
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            _orderServiceMock.Setup(x => x.GetOrderByIdAsync(id)).ReturnsAsync(order);
            var result = await _controller.Details(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details");
            Assert.Equal(order, result.Model);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_missing()
        {
            int? id = null;
            var result = await _controller.Edit(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_order_is_missing()
        {
            int id = 1;
            _orderServiceMock.Setup(x => x.GetOrderByIdAsync(id)).ReturnsAsync((Order)null);
            var result = await _controller.Edit(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_order_found()
        {
            int id = 1;
            var order = new Order {
                Id = id,
                OrderNumber = "ORD-1",
                Notes = "notes",
                Status = "status"
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            _orderServiceMock.Setup(x => x.GetOrderByIdAsync(id)).ReturnsAsync(order);
            var result = await _controller.Edit(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
            Assert.Equal(order, result.Model);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_missing()
        {
            int? id = null;
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_order_is_missing()
        {
            int id = 1;
            _orderServiceMock.Setup(x => x.GetOrderByIdAsync(id)).ReturnsAsync((Order)null);
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_order_found()
        {
            int id = 1;
            var order = new Order {
                Id = id,
                OrderNumber = "ORD-1",
                Notes = "notes",
                Status = "status"
            };
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            _orderServiceMock.Setup(x => x.GetOrderByIdAsync(id)).ReturnsAsync(order);
            var result = await _controller.Delete(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Delete");
            Assert.Equal(order, result.Model);
        }
    }
} 