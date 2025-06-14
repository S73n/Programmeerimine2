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
    public class BatchesControllerTests
    {
        private readonly Mock<IBatchService> _batchServiceMock = new Mock<IBatchService>();
        private readonly ApplicationDbContext _dbContext;
        private readonly BatchesController _controller;

        public BatchesControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _controller = new BatchesController(_dbContext, _batchServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_index_view()
        {
            var mockService = new Mock<IBatchService>();
            mockService.Setup(s => s.GetBatchesAsync(It.IsAny<BatchSearchParameters>())).ReturnsAsync(new List<Batch>());
            mockService.Setup(s => s.GetTotalBatchesCountAsync(It.IsAny<BatchSearchParameters>())).ReturnsAsync(0);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
                .Options;
            var dbContext = new ApplicationDbContext(options);
            var controller = new BatchesController(dbContext, mockService.Object);
            var result = await controller.Index(new BatchSearchParameters()) as ViewResult;
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
        public async Task Details_should_return_notfound_when_batch_is_missing()
        {
            int id = 1;
            _batchServiceMock.Setup(x => x.GetBatchByIdAsync(id)).ReturnsAsync((Batch)null);
            var result = await _controller.Details(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_view_with_model_when_batch_found()
        {
            int id = 1;
            var batch = new Batch {
                Id = id,
                BatchCode = "B1",
                BatchDescription = "desc",
                BatchNumber = "1",
                Notes = "notes",
                Status = "status",
                Summary = "summary"
            };
            _dbContext.Batches.Add(batch);
            _dbContext.SaveChanges();
            _batchServiceMock.Setup(x => x.GetBatchByIdAsync(id)).ReturnsAsync(batch);
            var result = await _controller.Details(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details");
            Assert.Equal(batch, result.Model);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_missing()
        {
            int? id = null;
            var result = await _controller.Edit(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_batch_is_missing()
        {
            int id = 1;
            _batchServiceMock.Setup(x => x.GetBatchByIdAsync(id)).ReturnsAsync((Batch)null);
            var result = await _controller.Edit(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_view_with_model_when_batch_found()
        {
            int id = 1;
            var batch = new Batch {
                Id = id,
                BatchCode = "B1",
                BatchDescription = "desc",
                BatchNumber = "1",
                Notes = "notes",
                Status = "status",
                Summary = "summary"
            };
            _dbContext.Batches.Add(batch);
            _dbContext.SaveChanges();
            _batchServiceMock.Setup(x => x.GetBatchByIdAsync(id)).ReturnsAsync(batch);
            var result = await _controller.Edit(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
            Assert.Equal(batch, result.Model);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_missing()
        {
            int? id = null;
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_notfound_when_batch_is_missing()
        {
            int id = 1;
            _batchServiceMock.Setup(x => x.GetBatchByIdAsync(id)).ReturnsAsync((Batch)null);
            var result = await _controller.Delete(id) as NotFoundResult;
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_batch_found()
        {
            int id = 1;
            var batch = new Batch {
                Id = id,
                BatchCode = "B1",
                BatchDescription = "desc",
                BatchNumber = "1",
                Notes = "notes",
                Status = "status",
                Summary = "summary"
            };
            _dbContext.Batches.Add(batch);
            _dbContext.SaveChanges();
            _batchServiceMock.Setup(x => x.GetBatchByIdAsync(id)).ReturnsAsync(batch);
            var result = await _controller.Delete(id) as ViewResult;
            Assert.NotNull(result);
            Assert.True(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Delete");
            Assert.Equal(batch, result.Model);
        }
    }
} 