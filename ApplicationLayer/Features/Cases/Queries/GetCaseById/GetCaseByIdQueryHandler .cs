using ApplicationLayer.Features.Cases.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.Cases.Queries.GetCaseById
{
    public class GetCaseByIdQueryHandler
        : IRequestHandler<GetCaseByIdQuery, OperationResult<CaseDto>>
    {
        private readonly IGenericRepository<Case> _repo;
        private readonly IMapper _mapper;

        public GetCaseByIdQueryHandler(IGenericRepository<Case> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OperationResult<CaseDto>> Handle(GetCaseByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repo.GetByIdAsync(request.Id);

            if (!result.IsSuccess)
                return OperationResult<CaseDto>.Failure(result.ErrorMessage!);

            var dto = _mapper.Map<CaseDto>(result.Data!);
            return OperationResult<CaseDto>.Success(dto);
        }
    }
}
