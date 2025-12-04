using ApplicationLayer.Features.Cases.Dtos;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;

namespace ApplicationLayer.Features.Cases.Commands.UpdateCase
{
    public record UpdateCaseCommand(
     int Id,
     string Title,
     string? Description,
     int? AssignedToUserId,
     List<int> TagIds,
     CaseStatus Status
 ) : IRequest<OperationResult<CaseDto>>;

}
