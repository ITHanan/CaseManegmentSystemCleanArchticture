using ApplicationLayer.Features.CaseNotes.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.CaseNotes.Queries.GetNotesByCaseId
{
    public class GetNotesByCaseIdQueryHandler
        : IRequestHandler<GetNotesByCaseIdQuery, OperationResult<List<CaseNoteDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IGenericRepository<CaseNote> _noteRepo;

        public GetNotesByCaseIdQueryHandler(IGenericRepository<CaseNote> repo, IMapper mapper)
        {
            _noteRepo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<CaseNoteDto>>> Handle(GetNotesByCaseIdQuery request, CancellationToken cancellationToken)
        {
            var notes = _noteRepo.AsQueryable()
                .Where(n => n.CaseId == request.CaseId)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();

            var dtos = _mapper.Map<List<CaseNoteDto>>(notes);

            return OperationResult<List<CaseNoteDto>>.Success(dtos);
        }
    }
}
