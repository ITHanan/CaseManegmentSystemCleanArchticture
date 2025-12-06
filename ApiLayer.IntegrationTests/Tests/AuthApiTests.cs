using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace ApiLayer.IntegrationTests.Tests
{
    public class AuthApiTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AuthApiTests(TestWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenCredentialsAreValid()
        {
            var loginModel = new
            {
                userName = "mimi",   
                password = "Mimi123@"
            };

            var response = await _client.PostAsJsonAsync("/api/Auth/login", loginModel);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<string>>();

            Assert.True(result!.IsSuccess);
            Assert.NotNull(result.Data);
        }
    }

    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
    }
}
