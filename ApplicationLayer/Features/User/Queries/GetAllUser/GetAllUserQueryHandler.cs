using ApplicationLayer.Features.User.Dtos;
using ApplicationLayer.Features.User.Queries.GetAllUser;
using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Users.Queries.GetAllUsers
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, OperationResult<List<UserDto>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<List<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync();
            return OperationResult<List<UserDto>>.Success(users);
        }
    }

   
}