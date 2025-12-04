using ApplicationLayer.Common.Pagination;
using ApplicationLayer.Features.Cases.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Cases.Queries.GetPaginatedCasesQuery
{
    public class GetPaginatedCasesQueryHandler
     : IRequestHandler<GetPaginatedCasesQuery, OperationResult<PaginatedResult<CaseDto>>>
    {
        private readonly ICaseRepository _caseRepo;
        private readonly IMapper _mapper;

        public GetPaginatedCasesQueryHandler(ICaseRepository caseRepo, IMapper mapper)
        {
            _caseRepo = caseRepo;
            _mapper = mapper;
        }

        public async Task<OperationResult<PaginatedResult<CaseDto>>> Handle(GetPaginatedCasesQuery request, CancellationToken cancellationToken)
        {
            var paginatedCases = await _caseRepo.GetPaginatedCasesAsync(request.PageNumber, request.PageSize);

            var dtoList = _mapper.Map<List<CaseDto>>(paginatedCases.Items);

            var result = new PaginatedResult<CaseDto>
            {
                Items = dtoList,
                PageNumber = paginatedCases.PageNumber,
                PageSize = paginatedCases.PageSize,
                TotalCount = paginatedCases.TotalCount
            };

            return OperationResult<PaginatedResult<CaseDto>>.Success(result);
        }
    }

}
