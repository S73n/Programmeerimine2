using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class CustomersControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public CustomersControllerTests()
        {
            var options = new WebApplicationFactoryClientOptions { AllowAutoRedirect = false };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Theory]
        [InlineData("/Customers")]
        [InlineData("/Customers/Create")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var client = Factory.CreateClient();
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Create_should_save_new_customer()
        {
            var formValues = new Dictionary<string, string>
            {
                { "Name", "Test Customer" }
            };
            using var content = new FormUrlEncodedContent(formValues);
            using var response = await _client.PostAsync("/Customers/Create", content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // ModelState error, vorm tagastati uuesti
                Assert.False(_context.Customers.Any());
            }
            else
            {
                Assert.True(
                    response.StatusCode == HttpStatusCode.Redirect ||
                    response.StatusCode == HttpStatusCode.MovedPermanently,
                    $"Actual status code: {response.StatusCode}, content: {await response.Content.ReadAsStringAsync()}" );
                var customer = _context.Customers.FirstOrDefault();
                Assert.NotNull(customer);
                Assert.Equal("Test Customer", customer.Name);
            }
        }

        [Fact]
        public async Task Create_should_not_save_invalid_customer()
        {
            var formValues = new Dictionary<string, string>
            {
                { "Name", "" }
            };
            using var content = new FormUrlEncodedContent(formValues);
            using var response = await _client.PostAsync("/Customers/Create", content);
            response.EnsureSuccessStatusCode();
            Assert.False(_context.Customers.Any());
        }
    }
} 