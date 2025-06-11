using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Service;
using KooliProjekt.Search;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class CustomersControllerTests
    {
        private readonly Mock<ICustomerService> _customerServiceMock = new Mock<ICustomerService>();
        private readonly ApplicationDbContext _dbContext;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _controller = new CustomersController(_dbContext, _customerServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_index_view()
        {
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(s => s.GetCustomersAsync(It.IsAny<CustomerSearchParameters>())).ReturnsAsync(new List<Customer>());
            mockService.Setup(s => s.GetTotalCustomersCountAsync(It.IsAny<CustomerSearchParameters>())).ReturnsAsync(0);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            var dbContext = new ApplicationDbContext(options);
            var controller = new CustomersController(dbContext, mockService.Object);
            var result = await controller.Index(new CustomerSearchParameters()) as ViewResult;
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Index" || string.IsNullOrEmpty(result.ViewName));
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
        public async Task Details_should_return_notfound_when_customer_is_missing()
        {
            int id = 1;
            _customerServiceMock.Setup(x => x.GetCustomerByIdAsync(id)).ReturnsAsync((Customer)null);
            var result = await _controller.Details(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_customer_found()
        {
            int id = 1;
            var customer = new Customer {
                Id = id,
                Name = "Test Name",
                Email = "test@example.com",
                Phone = "12345678",
                Address = "Test Address"
            };
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            _customerServiceMock.Setup(x => x.GetCustomerByIdAsync(id)).ReturnsAsync(customer);
            var result = await _controller.Details(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details");
            Assert.Equal(customer, result.Model);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_missing()
        {
            int? id = null;
            var result = await _controller.Edit(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_customer_is_missing()
        {
            int id = 1;
            _customerServiceMock.Setup(x => x.GetCustomerByIdAsync(id)).ReturnsAsync((Customer)null);
            var result = await _controller.Edit(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_customer_found()
        {
            int id = 1;
            var customer = new Customer {
                Id = id,
                Name = "Test Name",
                Email = "test@example.com",
                Phone = "12345678",
                Address = "Test Address"
            };
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            _customerServiceMock.Setup(x => x.GetCustomerByIdAsync(id)).ReturnsAsync(customer);
            var result = await _controller.Edit(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
            Assert.Equal(customer, result.Model);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_missing()
        {
            int? id = null;
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_customer_is_missing()
        {
            int id = 1;
            _customerServiceMock.Setup(x => x.GetCustomerByIdAsync(id)).ReturnsAsync((Customer)null);
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_customer_found()
        {
            int id = 1;
            var customer = new Customer {
                Id = id,
                Name = "Test Name",
                Email = "test@example.com",
                Phone = "12345678",
                Address = "Test Address"
            };
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            _customerServiceMock.Setup(x => x.GetCustomerByIdAsync(id)).ReturnsAsync(customer);
            var result = await _controller.Delete(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Delete");
            Assert.Equal(customer, result.Model);
        }
    }
} 