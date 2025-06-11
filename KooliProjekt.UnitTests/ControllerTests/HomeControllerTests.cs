using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Service;
using KooliProjekt.Search;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_should_return_index_view()
        {
            var controller = new HomeController();
            var result = controller.Index() as ViewResult;
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Index" || string.IsNullOrEmpty(result.ViewName));
        }

        [Fact]
        public void Privacy_should_return_privacy_view()
        {
            var controller = new HomeController();
            var result = controller.Privacy() as ViewResult;
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Privacy" || string.IsNullOrEmpty(result.ViewName));
        }

        [Fact]
        public void Error_should_return_error_view()
        {
            var controller = new HomeController();
            var result = controller.Error() as ViewResult;
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Error" || string.IsNullOrEmpty(result.ViewName));
        }
    }
}