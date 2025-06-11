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
        private readonly Mock<IBeerService> _beerServiceMock = new Mock<IBeerService>();
        private readonly BeersController _controller;

        public BeersControllerTests()
        {
            _controller = new BeersController(_beerServiceMock.Object);
        }

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
        public async Task Details_should_return_notfound_when_beer_is_missing()
        {
            int id = 1;
            _beerServiceMock.Setup(x => x.GetBeerByIdAsync(id)).ReturnsAsync((Beer)null);
            var result = await _controller.Details(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_beer_found()
        {
            int id = 1;
            var beer = new Beer { Id = id };
            _beerServiceMock.Setup(x => x.GetBeerByIdAsync(id)).ReturnsAsync(beer);
            var result = await _controller.Details(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details");
            Assert.Equal(beer, result.Model);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_missing()
        {
            int? id = null;
            var result = await _controller.Edit(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_beer_is_missing()
        {
            int id = 1;
            _beerServiceMock.Setup(x => x.GetBeerByIdAsync(id)).ReturnsAsync((Beer)null);
            var result = await _controller.Edit(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_beer_found()
        {
            int id = 1;
            var beer = new Beer { Id = id };
            _beerServiceMock.Setup(x => x.GetBeerByIdAsync(id)).ReturnsAsync(beer);
            var result = await _controller.Edit(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
            Assert.Equal(beer, result.Model);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_missing()
        {
            int? id = null;
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_beer_is_missing()
        {
            int id = 1;
            _beerServiceMock.Setup(x => x.GetBeerByIdAsync(id)).ReturnsAsync((Beer)null);
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_beer_found()
        {
            int id = 1;
            var beer = new Beer { Id = id };
            _beerServiceMock.Setup(x => x.GetBeerByIdAsync(id)).ReturnsAsync(beer);
            var result = await _controller.Delete(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Delete");
            Assert.Equal(beer, result.Model);
        }
    }
} 