using ApplicationLayer.Features.Cases.Dtos;
using ApplicationLayer.Features.Cases.Queries.GetCaseById;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using MediatR;

public class GetCaseByIdQueryHandler
    : IRequestHandler<GetCaseByIdQuery, OperationResult<CaseDto>>
{
    private readonly ICaseRepository _caseRepo;
    private readonly IMapper _mapper;

    public GetCaseByIdQueryHandler(ICaseRepository caseRepo, IMapper mapper)
    {
        _caseRepo = caseRepo;
        _mapper = mapper;
    }

    public async Task<OperationResult<CaseDto>> Handle(GetCaseByIdQuery request, CancellationToken cancellationToken)
    {
        var caseEntity = await _caseRepo.GetCaseWithDetailsAsync(request.Id);

        if (caseEntity == null)
            return OperationResult<CaseDto>.Failure("Case not found");

        var dto = _mapper.Map<CaseDto>(caseEntity);

        return OperationResult<CaseDto>.Success(dto);
    }
}
