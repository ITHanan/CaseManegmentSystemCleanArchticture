using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ApiLayer.IntegrationTests.Tests
{
    public class CasesApiTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public CasesApiTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCases_ShouldReturnUnauthorized_WhenNoToken()
        {
            var response = await _client.GetAsync("/api/Cases");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
