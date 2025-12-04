using ApplicationLayer.Features.Cases.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Cases.Queries.GetQueryByTagId
{
    public class GetCasesByTagIdQueryHandler
      : IRequestHandler<GetCasesByTagIdQuery, OperationResult<List<CaseDto>>>
    {
        private readonly ICaseRepository _caseRepo;
        private readonly IMapper _mapper;

        public GetCasesByTagIdQueryHandler(ICaseRepository caseRepo, IMapper mapper)
        {
            _caseRepo = caseRepo;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<CaseDto>>> Handle(GetCasesByTagIdQuery request, CancellationToken cancellationToken)
        {
            var cases = await _caseRepo.GetCasesByTagIdAsync(request.TagId);

            var dtoList = _mapper.Map<List<CaseDto>>(cases);

            return OperationResult<List<CaseDto>>.Success(dtoList);
        }
    }

}
