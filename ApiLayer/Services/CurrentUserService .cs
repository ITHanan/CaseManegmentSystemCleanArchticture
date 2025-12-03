using ApplicationLayer.Interfaces;
using System.Security.Claims;

namespace Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?
                    .User?
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return id != null ? int.Parse(id) : 0;
            }
        }

        public string? UserName =>
            _httpContextAccessor.HttpContext?
                .User?
                .FindFirst(ClaimTypes.Name)?.Value;

        public string? Email =>
            _httpContextAccessor.HttpContext?
                .User?
                .FindFirst(ClaimTypes.Email)?.Value;
    }
}
