using ApplicationLayer.Features.CaseNotes.Dtos;
using AutoMapper;
using DomainLayer.Models;

namespace ApplicationLayer.Common.Mappings
{
    public class CaseNoteProfile : Profile
    {
        public CaseNoteProfile()
        {
            CreateMap<CaseNote, CaseNoteDto>();
        }
    }
}
