using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Service;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class InvoiceLineServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task CreateInvoiceLineAsync_should_add_line()
        {
            var service = new InvoiceLineService(DbContext);
            var line = new InvoiceLine { Quantity = 1, Price = 10, BeerId = 1 };
            await service.CreateInvoiceLineAsync(line);
            Assert.Equal(1, DbContext.InvoiceLines.Count());
        }

        [Fact]
        public async Task UpdateInvoiceLineAsync_should_update_line()
        {
            var service = new InvoiceLineService(DbContext);
            var line = new InvoiceLine { Quantity = 1, Price = 10, BeerId = 1 };
            DbContext.InvoiceLines.Add(line);
            DbContext.SaveChanges();
            line.Quantity = 2;
            await service.UpdateInvoiceLineAsync(line);
            Assert.Equal(2, DbContext.InvoiceLines.First().Quantity);
        }

        [Fact]
        public async Task DeleteInvoiceLineAsync_should_remove_line()
        {
            var service = new InvoiceLineService(DbContext);
            var line = new InvoiceLine { Quantity = 1, Price = 10, BeerId = 1 };
            DbContext.InvoiceLines.Add(line);
            DbContext.SaveChanges();
            await service.DeleteInvoiceLineAsync(line.Id);
            Assert.Empty(DbContext.InvoiceLines);
        }

        [Fact]
        public async Task GetInvoiceLineByIdAsync_should_return_line()
        {
            var service = new InvoiceLineService(DbContext);
            var line = new InvoiceLine { Quantity = 1, Price = 10, BeerId = 1 };
            DbContext.InvoiceLines.Add(line);
            DbContext.SaveChanges();
            var result = await service.GetInvoiceLineByIdAsync(line.Id);
            Assert.NotNull(result);
            Assert.Equal(line.Quantity, result.Quantity);
        }
    }
} 