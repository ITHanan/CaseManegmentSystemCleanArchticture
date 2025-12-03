using ApplicationLayer.Features.Cases.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.Cases.Queries.GetAllCases
{
    public class GetAllCasesQueryHandler
        : IRequestHandler<GetAllCasesQuery, OperationResult<IEnumerable<CaseDto>>>
    {
        private readonly IGenericRepository<Case> _repo;
        private readonly IMapper _mapper;

        public GetAllCasesQueryHandler(IGenericRepository<Case> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<IEnumerable<CaseDto>>> Handle(GetAllCasesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetAllAsync();

            if (!result.IsSuccess)
                return OperationResult<IEnumerable<CaseDto>>.Failure(result.ErrorMessage!);

            var dto = _mapper.Map<IEnumerable<CaseDto>>(result.Data!);
            return OperationResult<IEnumerable<CaseDto>>.Success(dto);
        }
    }
}
