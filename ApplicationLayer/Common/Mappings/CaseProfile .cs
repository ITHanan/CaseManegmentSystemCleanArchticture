using ApplicationLayer.Features.Cases.Dtos;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Profiles
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            CreateMap<Case, CaseDto>()
                // Status enum → string
                .ForMember(
                    dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString())
                )

                // Creator full name (safe mapping)
                .ForMember(
                    dest => dest.CreatedByUserName,
                    opt => opt.MapFrom(src =>
                        src.CreatedByUser != null
                            ? $"{src.CreatedByUser.FirstName} {src.CreatedByUser.LastName}"
                            : string.Empty
                    )
                )

                // AssignedTo user full name (safe mapping)
                .ForMember(
                    dest => dest.AssignedToUserName,
                    opt => opt.MapFrom(src =>
                        src.AssignedTo != null
                            ? $"{src.AssignedTo.FirstName} {src.AssignedTo.LastName}"
                            : null
                    )
                );
        }
    }
}
