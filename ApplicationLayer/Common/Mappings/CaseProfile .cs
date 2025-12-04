using ApplicationLayer.Features.Cases.Dtos;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Common.Mappings
{
    public class CaseProfile : Profile
    {
        public CaseProfile()
        {
            CreateMap<Case, CaseDto>()

                // Convert status enum to string
                .ForMember(
                    destination => destination.Status,
                    config => config.MapFrom(source => source.Status.ToString())
                )

                // Map Created By User Full Name safely
                .ForMember(
                    destination => destination.CreatedByUserName,
                    config => config.MapFrom(source =>
                        source.CreatedByUser != null
                            ? $"{source.CreatedByUser.FirstName ?? ""} {source.CreatedByUser.LastName ?? ""}".Trim()
                            : null
                    )
                )

                // Map Assigned User Full Name safely
                .ForMember(
                    destination => destination.AssignedToUserName,
                    config => config.MapFrom(source =>
                        source.AssignedTo != null
                            ? $"{source.AssignedTo.FirstName ?? ""} {source.AssignedTo.LastName ?? ""}".Trim()
                            : null
                    )
                )

                .ForMember(
                    dto => dto.Tags,
                    config => config.MapFrom(source =>
                        source.CaseTags.Select(ct => ct.Tag.Name).ToList()
                    )
                )

                .ForMember(
                    dest => dest.Notes,
                    opt => opt.MapFrom(src => src.Notes)
                );

        }
    }
}
