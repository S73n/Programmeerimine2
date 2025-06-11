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
    public class InvoicesControllerTests
    {
        [Fact]
        public async Task Index_should_return_index_view()
        {
            var mockService = new Mock<IInvoiceService>();
            mockService.Setup(s => s.GetInvoicesAsync(It.IsAny<InvoiceSearchParameters>())).ReturnsAsync(new List<Invoice>());
            mockService.Setup(s => s.GetTotalInvoicesCountAsync(It.IsAny<InvoiceSearchParameters>())).ReturnsAsync(0);

            var controller = new InvoicesController(null, mockService.Object);
            var result = await controller.Index(new InvoiceSearchParameters()) as ViewResult;
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Index" || string.IsNullOrEmpty(result.ViewName));
        }
    }
} 