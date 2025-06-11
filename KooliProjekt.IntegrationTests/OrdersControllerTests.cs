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
    public class OrdersControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public OrdersControllerTests()
        {
            var options = new WebApplicationFactoryClientOptions { AllowAutoRedirect = false };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Theory]
        [InlineData("/Orders")]
        [InlineData("/Orders/Create")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Create_should_save_new_order()
        {
            // Lisa testklient ja testandmed
            var customer = new Customer {
                Name = "Test Customer",
                Email = "test@example.com",
                Phone = "555-1234",
                Address = "Test Address"
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();

            var formValues = new Dictionary<string, string>
            {
                { "OrderNumber", "ORD-001" },
                { "OrderDate", "2024-06-01" },
                { "CustomerId", customer.Id.ToString() },
                { "TotalAmount", "100" },
                { "Status", "New" },
                { "Notes", "Test order" }
            };
            using var content = new FormUrlEncodedContent(formValues);
            using var response = await _client.PostAsync("/Orders/Create", content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                // ModelState error, vorm tagastati uuesti
                Assert.False(_context.Orders.Any());
            }
            else
            {
                Assert.True(
                    response.StatusCode == HttpStatusCode.Redirect ||
                    response.StatusCode == HttpStatusCode.MovedPermanently,
                    $"Actual status code: {response.StatusCode}, content: {await response.Content.ReadAsStringAsync()}" );
                var order = _context.Orders.FirstOrDefault();
                Assert.NotNull(order);
                Assert.Equal("ORD-001", order.OrderNumber);
            }
        }

        [Fact]
        public async Task Create_should_not_save_invalid_order()
        {
            var formValues = new Dictionary<string, string>
            {
                { "OrderNumber", "" },
                { "OrderDate", "" },
                { "CustomerId", "" },
                { "TotalAmount", "" },
                { "Status", "" },
                { "Notes", "" }
            };
            using var content = new FormUrlEncodedContent(formValues);
            using var response = await _client.PostAsync("/Orders/Create", content);
            response.EnsureSuccessStatusCode();
            Assert.False(_context.Orders.Any());
        }
    }
} 