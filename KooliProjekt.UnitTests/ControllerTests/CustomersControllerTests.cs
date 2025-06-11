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

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class CustomersControllerTests
    {
        [Fact]
        public async Task Index_should_return_index_view()
        {
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(s => s.GetCustomersAsync(It.IsAny<CustomerSearchParameters>())).ReturnsAsync(new List<Customer>());
            mockService.Setup(s => s.GetTotalCustomersCountAsync(It.IsAny<CustomerSearchParameters>())).ReturnsAsync(0);

            var controller = new CustomersController(null, mockService.Object);
            var result = await controller.Index(new CustomerSearchParameters()) as ViewResult;
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Index" || string.IsNullOrEmpty(result.ViewName));
        }
    }
} 