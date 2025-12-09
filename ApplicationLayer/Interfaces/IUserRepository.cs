using ApplicationLayer.Features.User.Dtos;

namespace ApplicationLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllUsersAsync();
    }
}

