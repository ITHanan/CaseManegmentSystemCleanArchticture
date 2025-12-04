using ApplicationLayer.Features.Cases.Commands.CreatCases;
using ApplicationLayer.Features.Cases.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

public class CreateCaseCommandHandler
    : IRequestHandler<CreateCaseCommand, OperationResult<CaseDto>>
{
    private readonly ICaseRepository _caseRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public CreateCaseCommandHandler(
        ICaseRepository caseRepository,
        IMapper mapper,
        ICurrentUserService currentUser)
    {
        _caseRepository = caseRepository;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<OperationResult<CaseDto>> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
    {
        if (_currentUser.UserId == 0)
        {
            return OperationResult<CaseDto>.Failure("Unauthorized: user must be logged in.");
        }

        var entity = new Case
        {
            Title = request.Title,
            Description = request.Description,
            ClientId = request.ClientId,
            AssignedToUserId = request.AssignedToUserId,
            CreatedByUserId = _currentUser.UserId,
            CreatedAt = DateTime.UtcNow,
            Status = CaseStatus.Open
        };

        // Save the new Case
        var result = await _caseRepository.AddAsync(entity, cancellationToken);

        if (!result.IsSuccess)
            return OperationResult<CaseDto>.Failure(result.ErrorMessage!);

        // Reload the case WITH navigation properties
        var fullCase = await _caseRepository.GetCaseWithDetailsAsync(result.Data!.Id);

        var dto = _mapper.Map<CaseDto>(fullCase);
        return OperationResult<CaseDto>.Success(dto);
    }
}
