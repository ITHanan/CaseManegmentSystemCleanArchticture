using ApplicationLayer.Features.Authorize.Commands.Register;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Common.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterCommand, User>()

                // Map simple user fields
                .ForMember(
                    user => user.FirstName,
                    mapping => mapping.MapFrom(command => command.FirstName)
                )
                .ForMember(
                    user => user.LastName,
                    mapping => mapping.MapFrom(command => command.LastName)
                )
                .ForMember(
                    user => user.PhoneNumber,
                    mapping => mapping.MapFrom(command => command.PhoneNumber)
                )

                // Normalize email
                .ForMember(
                    user => user.UserEmail,
                    mapping => mapping.MapFrom(command => command.UserEmail.ToLower())
                )

                // We generate the password hash manually in the handler
                .ForMember(
                    user => user.PasswordHash,
                    mapping => mapping.Ignore()
                )

                // Navigation collection shouldn't be mapped on register
                .ForMember(
                    user => user.AssignedCases,
                    mapping => mapping.Ignore()
                )

                .ForMember(
                    user => user.Role,
                    mapping => mapping.MapFrom(Common => Common.Role)
                );
        }
    }
}
