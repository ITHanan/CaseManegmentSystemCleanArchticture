using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Xunit;
using System.Threading.Tasks;

namespace ApiLayer.IntegrationTests.Tests
{
    public class AuthorizationTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AuthorizationTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        private async Task<string> LoginAsNormalUserAsync()
        {
            var login = new { userName = "mimi", password = "Mimi123@" };

            var response = await _client.PostAsJsonAsync("/api/Auth/login", login);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

            return result!.Data!;
        }

        [Fact]
        public async Task DeleteCase_ShouldReturnForbidden_WhenUserIsNotAdmin()
        {
            // Arrange
            var token = await LoginAsNormalUserAsync();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync("/api/Cases/1");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }
    }
}
