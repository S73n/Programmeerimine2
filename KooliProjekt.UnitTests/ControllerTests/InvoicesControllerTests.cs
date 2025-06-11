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
    public class InvoicesControllerTests
    {
        private readonly Mock<IInvoiceService> _invoiceServiceMock = new Mock<IInvoiceService>();
        private readonly ApplicationDbContext _dbContext;
        private readonly InvoicesController _controller;

        public InvoicesControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _controller = new InvoicesController(_dbContext, _invoiceServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_index_view()
        {
            var mockService = new Mock<IInvoiceService>();
            mockService.Setup(s => s.GetInvoicesAsync(It.IsAny<InvoiceSearchParameters>())).ReturnsAsync(new List<Invoice>());
            mockService.Setup(s => s.GetTotalInvoicesCountAsync(It.IsAny<InvoiceSearchParameters>())).ReturnsAsync(0);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            var dbContext = new ApplicationDbContext(options);
            var controller = new InvoicesController(dbContext, mockService.Object);
            var result = await controller.Index(new InvoiceSearchParameters()) as ViewResult;
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
        public async Task Details_should_return_notfound_when_invoice_is_missing()
        {
            int id = 1;
            _invoiceServiceMock.Setup(x => x.GetInvoiceByIdAsync(id)).ReturnsAsync((Invoice)null);
            var result = await _controller.Details(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_invoice_found()
        {
            int id = 1;
            var customer = new Customer {
                Id = 1,
                Name = "Test Name",
                Email = "test@example.com",
                Phone = "12345678",
                Address = "Test Address"
            };
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            var invoice = new Invoice {
                Id = id,
                InvoiceNumber = "INV-1",
                Status = "status",
                IssueDate = System.DateTime.Today,
                DueDate = System.DateTime.Today.AddDays(30),
                CustomerId = 1,
                TotalAmount = 100.0m
            };
            _dbContext.Invoices.Add(invoice);
            _dbContext.SaveChanges();
            _invoiceServiceMock.Setup(x => x.GetInvoiceByIdAsync(id)).ReturnsAsync(invoice);
            var result = await _controller.Details(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details");
            Assert.Equal(invoice, result.Model);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_missing()
        {
            int? id = null;
            var result = await _controller.Edit(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_invoice_is_missing()
        {
            int id = 1;
            _invoiceServiceMock.Setup(x => x.GetInvoiceByIdAsync(id)).ReturnsAsync((Invoice)null);
            var result = await _controller.Edit(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_invoice_found()
        {
            int id = 1;
            var invoice = new Invoice {
                Id = id,
                InvoiceNumber = "INV-1",
                Status = "status"
            };
            _dbContext.Invoices.Add(invoice);
            _dbContext.SaveChanges();
            _invoiceServiceMock.Setup(x => x.GetInvoiceByIdAsync(id)).ReturnsAsync(invoice);
            var result = await _controller.Edit(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
            Assert.Equal(invoice, result.Model);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_missing()
        {
            int? id = null;
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_invoice_is_missing()
        {
            int id = 1;
            _invoiceServiceMock.Setup(x => x.GetInvoiceByIdAsync(id)).ReturnsAsync((Invoice)null);
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_invoice_found()
        {
            int id = 1;
            var customer = new Customer {
                Id = 1,
                Name = "Test Name",
                Email = "test@example.com",
                Phone = "12345678",
                Address = "Test Address"
            };
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
            var invoice = new Invoice {
                Id = id,
                InvoiceNumber = "INV-1",
                Status = "status",
                IssueDate = System.DateTime.Today,
                DueDate = System.DateTime.Today.AddDays(30),
                CustomerId = 1,
                TotalAmount = 100.0m
            };
            _dbContext.Invoices.Add(invoice);
            _dbContext.SaveChanges();
            _invoiceServiceMock.Setup(x => x.GetInvoiceByIdAsync(id)).ReturnsAsync(invoice);
            var result = await _controller.Delete(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Delete");
            Assert.Equal(invoice, result.Model);
        }
    }
} 