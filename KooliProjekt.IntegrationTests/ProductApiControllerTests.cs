using System.Threading.Tasks;
using KooliProjekt.IntegrationTests.Helpers;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class ProductApiControllerTests : TestBase
    {
        [Theory]
        [InlineData("/api/Products")]
        [InlineData("/api/Products/1")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            var client = Factory.CreateClient();
            var response = await client.GetAsync(url);
            Assert.True(response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }
    }
} 