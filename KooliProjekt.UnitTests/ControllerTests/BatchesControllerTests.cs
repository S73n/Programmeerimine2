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
    public class BatchesControllerTests
    {
        [Fact]
        public async Task Index_should_return_index_view()
        {
            var mockService = new Mock<IBatchService>();
            mockService.Setup(s => s.GetBatchesAsync(It.IsAny<BatchSearchParameters>())).ReturnsAsync(new List<Batch>());
            mockService.Setup(s => s.GetTotalBatchesCountAsync(It.IsAny<BatchSearchParameters>())).ReturnsAsync(0);

            var controller = new BatchesController(null, mockService.Object);
            var result = await controller.Index(new BatchSearchParameters()) as ViewResult;
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Index" || string.IsNullOrEmpty(result.ViewName));
        }
    }
} 