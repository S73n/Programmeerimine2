using System.Threading.Tasks;
using KooliProjekt.IntegrationTests.Helpers;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class InvoicesControllerTests : TestBase
    {
        [Theory]
        [InlineData("/Invoices")]
        [InlineData("/Invoices/Create")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var client = Factory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
} 