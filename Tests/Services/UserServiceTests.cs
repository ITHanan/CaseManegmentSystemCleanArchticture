
using Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace Tests.Services;

public class CurrentUserServiceTests
{
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly CurrentUserService _service;

    public CurrentUserServiceTests()
    {
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        // Default context
        var context = new DefaultHttpContext();
        _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(context);

        _service = new CurrentUserService(_httpContextAccessorMock.Object);
    }

    private void SetClaims(string userId, string username, string email, string role)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

        var identity = new ClaimsIdentity(claims);
        var user = new ClaimsPrincipal(identity);

        var httpContext = new DefaultHttpContext();
        httpContext.User = user;

        _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContext);
    }

    [Test]
    public void UserId_Should_Return_Correct_Value()
    {
        SetClaims("10", "mimi", "mimi@test.com", "Admin");

        _service.UserId.Should().Be(10);
    }

    [Test]
    public void UserName_Should_Return_Correct_Value()
    {
        SetClaims("10", "mimi", "mimi@test.com", "Admin");

        _service.UserName.Should().Be("mimi");
    }

    [Test]
    public void Email_Should_Return_Correct_Value()
    {
        SetClaims("10", "mimi", "mimi@test.com", "Admin");

        _service.Email.Should().Be("mimi@test.com");
    }

    [Test]
    public void Role_Should_Return_Correct_Value()
    {
        SetClaims("10", "mimi", "mimi@test.com", "Admin");

        _service.Role.Should().Be("Admin");
    }

    [Test]
    public void Should_Return_Default_When_No_Claims()
    {
        // No claims set
        _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(new DefaultHttpContext());

        _service.UserId.Should().Be(0);
        _service.UserName.Should().BeNull();
        _service.Email.Should().BeNull();
        _service.Role.Should().Be("User"); // default fallback
    }
}
