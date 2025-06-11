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
    public class BeersControllerTests
    {
        [Fact]
        public async Task Index_should_return_index_view()
        {
            var mockService = new Mock<IBeerService>();
            mockService.Setup(s => s.GetBeersAsync(It.IsAny<BeerSearchParameters>())).ReturnsAsync(new List<Beer>());
            mockService.Setup(s => s.GetTotalBeersCountAsync(It.IsAny<BeerSearchParameters>())).ReturnsAsync(0);

            var controller = new BeersController(mockService.Object);
            var result = await controller.Index(new BeerSearchParameters()) as ViewResult;
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Index" || string.IsNullOrEmpty(result.ViewName));
        }
    }
} 