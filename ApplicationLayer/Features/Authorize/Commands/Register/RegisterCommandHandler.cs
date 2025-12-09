using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.Authorize.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, OperationResult<string>>
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IAuthRepository authRepository, IJwtGenerator jwtGenerator, IMapper mapper)
        {
            _authRepository = authRepository;
            _jwtGenerator = jwtGenerator;
            _mapper = mapper;
        }

        public async Task<OperationResult<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Normalize the email to lowercase once
                var normalizedEmail = request.UserEmail.ToLower();

                // Check if the email is already registered
                var emailExists = await _authRepository.EmailExistsAsync(normalizedEmail);
                if (emailExists)
                    return OperationResult<string>.Failure("Email is already registered.");

                var usernameExists = await _authRepository.UsernameExistsAsync(request.UserName);
                  if (usernameExists)
                    return OperationResult<string>.Failure("Username is already taken.");

                // Map the DTO to a User entity
                var user = _mapper.Map<DomainLayer.Models.User>(request);

                //  Explicitly set missing fields (Mapper won't do this himself unless mapped)
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.PhoneNumber = request.PhoneNumber;

                // Hash the password manually
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                user.UserEmail = user.UserEmail!.ToLower();
                user.Role = request.Role == "Admim"? "Admin" : "User";

                // Add the user (not saved yet)
                await _authRepository.CreateUserAsync(user);

                // Save changes
                var saveResult = await _authRepository.SaveChangesAsync();
                if (!saveResult.IsSuccess)
                    return OperationResult<string>.Failure(saveResult.ErrorMessage!);

                // Generate token
                var token = _jwtGenerator.GenerateToken(user);

                // Return success
                return OperationResult<string>.Success(token);
            }
            catch (Exception ex)
            {
                return OperationResult<string>.Failure($"Error during registration: {ex.Message}");
            }
        }
    }
}
