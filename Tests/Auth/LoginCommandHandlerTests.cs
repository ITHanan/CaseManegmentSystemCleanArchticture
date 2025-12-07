using ApplicationLayer.Features.Authorize.Queries.Login;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests.Auth
{
    public class LoginQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var mockRepo = new Mock<IAuthRepository>();
            var mockJwt = new Mock<IJwtGenerator>();
            var mockMapper = new Mock<IMapper>();

            var user = new User
            {
                Id = 1,
                UserName = "testuser",
                UserEmail = "test@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Password123"),
                Role = "User",
                PhoneNumber = "0700000000"
            };

            mockRepo
                .Setup(r => r.GetUserByUsernameAsync("testuser"))
                .ReturnsAsync(user);

            mockJwt
                .Setup(j => j.GenerateToken(It.IsAny<User>()))
                .Returns("fake-jwt-token");

            var handler = new LoginQueryHandler(
                mockRepo.Object,
                mockJwt.Object,
                mockMapper.Object
            );

            var command = new LoginQuery("testuser", "Password123");

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert (FluentAssertions)
            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be("fake-jwt-token");
        }
    }
}
