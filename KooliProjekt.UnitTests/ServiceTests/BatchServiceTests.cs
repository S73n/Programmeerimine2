using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Service;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class BatchServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task CreateBatchAsync_should_add_batch()
        {
            var service = new BatchService(DbContext);
            var beer = new Beer {
                BeerName = "Test Beer",
                BeerDescription = "desc",
                Description = "desc",
                Style = "style"
            };
            DbContext.Beers.Add(beer);
            DbContext.SaveChanges();
            var batch = new Batch {
                BatchCode = "B1",
                BatchDescription = "desc",
                BatchNumber = "1",
                Notes = "notes",
                Status = "status",
                Summary = "summary",
                BeerId = beer.Id,
                StartDate = System.DateTime.UtcNow,
                Volume = 100
            };
            await service.CreateBatchAsync(batch);
            Assert.Equal(1, DbContext.Batches.Count());
        }

        [Fact]
        public async Task UpdateBatchAsync_should_update_batch()
        {
            var service = new BatchService(DbContext);
            var batch = new Batch {
                BatchCode = "B1",
                BatchDescription = "desc",
                BatchNumber = "1",
                Notes = "notes",
                Status = "status",
                Summary = "summary"
            };
            DbContext.Batches.Add(batch);
            DbContext.SaveChanges();
            batch.Status = "updated";
            await service.UpdateBatchAsync(batch);
            Assert.Equal("updated", DbContext.Batches.First().Status);
        }

        [Fact]
        public async Task DeleteBatchAsync_should_remove_batch()
        {
            var service = new BatchService(DbContext);
            var batch = new Batch {
                BatchCode = "B1",
                BatchDescription = "desc",
                BatchNumber = "1",
                Notes = "notes",
                Status = "status",
                Summary = "summary"
            };
            DbContext.Batches.Add(batch);
            DbContext.SaveChanges();
            await service.DeleteBatchAsync(batch.Id);
            Assert.Empty(DbContext.Batches);
        }

        [Fact]
        public async Task GetBatchByIdAsync_should_return_batch()
        {
            var service = new BatchService(DbContext);
            var beer = new Beer {
                BeerName = "Test Beer",
                BeerDescription = "desc",
                Description = "desc",
                Style = "style"
            };
            DbContext.Beers.Add(beer);
            DbContext.SaveChanges();
            var batch = new Batch {
                BatchCode = "B1",
                BatchDescription = "desc",
                BatchNumber = "1",
                Notes = "notes",
                Status = "status",
                Summary = "summary",
                BeerId = beer.Id,
                StartDate = System.DateTime.UtcNow,
                Volume = 100
            };
            DbContext.Batches.Add(batch);
            DbContext.SaveChanges();
            var result = await service.GetBatchByIdAsync(batch.Id);
            Assert.NotNull(result);
            Assert.Equal(batch.BatchCode, result.BatchCode);
        }
    }
} 