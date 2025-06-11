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
    public class IngredientsControllerTests
    {
        private readonly Mock<IIngredientService> _ingredientServiceMock = new Mock<IIngredientService>();
        private readonly ApplicationDbContext _dbContext;
        private readonly IngredientsController _controller;

        public IngredientsControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _controller = new IngredientsController(_dbContext, _ingredientServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_index_view()
        {
            var mockService = new Mock<IIngredientService>();
            mockService.Setup(s => s.GetIngredientsAsync(It.IsAny<IngredientSearchParameters>())).ReturnsAsync(new List<Ingredient>());
            mockService.Setup(s => s.GetTotalIngredientsCountAsync(It.IsAny<IngredientSearchParameters>())).ReturnsAsync(0);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            var dbContext = new ApplicationDbContext(options);
            var controller = new IngredientsController(dbContext, mockService.Object);
            var result = await controller.Index(new IngredientSearchParameters()) as ViewResult;
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
        public async Task Details_should_return_notfound_when_ingredient_is_missing()
        {
            int id = 1;
            _ingredientServiceMock.Setup(x => x.GetIngredientByIdAsync(id)).ReturnsAsync((Ingredient)null);
            var result = await _controller.Details(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_ingredient_found()
        {
            int id = 1;
            var ingredient = new Ingredient {
                Id = id,
                Name = "Sugar",
                Description = "desc",
                Type = "type",
                Unit = "kg"
            };
            _dbContext.Ingredients.Add(ingredient);
            _dbContext.SaveChanges();
            _ingredientServiceMock.Setup(x => x.GetIngredientByIdAsync(id)).ReturnsAsync(ingredient);
            var result = await _controller.Details(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details");
            Assert.Equal(ingredient, result.Model);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_missing()
        {
            int? id = null;
            var result = await _controller.Edit(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_ingredient_is_missing()
        {
            int id = 1;
            _ingredientServiceMock.Setup(x => x.GetIngredientByIdAsync(id)).ReturnsAsync((Ingredient)null);
            var result = await _controller.Edit(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_ingredient_found()
        {
            int id = 1;
            var ingredient = new Ingredient {
                Id = id,
                Name = "Sugar",
                Description = "desc",
                Type = "type",
                Unit = "kg"
            };
            _dbContext.Ingredients.Add(ingredient);
            _dbContext.SaveChanges();
            _ingredientServiceMock.Setup(x => x.GetIngredientByIdAsync(id)).ReturnsAsync(ingredient);
            var result = await _controller.Edit(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
            Assert.Equal(ingredient, result.Model);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_missing()
        {
            int? id = null;
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_ingredient_is_missing()
        {
            int id = 1;
            _ingredientServiceMock.Setup(x => x.GetIngredientByIdAsync(id)).ReturnsAsync((Ingredient)null);
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_ingredient_found()
        {
            int id = 1;
            var ingredient = new Ingredient {
                Id = id,
                Name = "Sugar",
                Description = "desc",
                Type = "type",
                Unit = "kg"
            };
            _dbContext.Ingredients.Add(ingredient);
            _dbContext.SaveChanges();
            _ingredientServiceMock.Setup(x => x.GetIngredientByIdAsync(id)).ReturnsAsync(ingredient);
            var result = await _controller.Delete(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Delete");
            Assert.Equal(ingredient, result.Model);
        }
    }
} 