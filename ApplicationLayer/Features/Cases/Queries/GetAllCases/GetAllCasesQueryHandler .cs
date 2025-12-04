using ApplicationLayer.Features.Cases.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.Cases.Queries.GetAllCases
{
    public class GetAllCasesQueryHandler
        : IRequestHandler<GetAllCasesQuery, OperationResult<List<CaseDto>>>
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IMapper _mapper;

        public GetAllCasesQueryHandler(ICaseRepository caseRepository, IMapper mapper)
        {
            _caseRepository = caseRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<CaseDto>>> Handle(GetAllCasesQuery request, CancellationToken cancellationToken)
        {
            var cases = await _caseRepository.GetAllCasesWithDetailsAsync();

            var dtoList = _mapper.Map<List<CaseDto>>(cases);

            return OperationResult<List<CaseDto>>.Success(dtoList);
        }
    }
}
