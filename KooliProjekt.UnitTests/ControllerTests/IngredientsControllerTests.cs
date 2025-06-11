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
    public class IngredientsControllerTests
    {
        [Fact]
        public async Task Index_should_return_index_view()
        {
            var mockService = new Mock<IIngredientService>();
            mockService.Setup(s => s.GetIngredientsAsync(It.IsAny<IngredientSearchParameters>())).ReturnsAsync(new List<Ingredient>());
            mockService.Setup(s => s.GetTotalIngredientsCountAsync(It.IsAny<IngredientSearchParameters>())).ReturnsAsync(0);

            var controller = new IngredientsController(null, mockService.Object);
            var result = await controller.Index(new IngredientSearchParameters()) as ViewResult;
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Index" || string.IsNullOrEmpty(result.ViewName));
        }
    }
} 