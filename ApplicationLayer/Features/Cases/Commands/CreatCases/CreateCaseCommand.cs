using ApplicationLayer.Features.Cases.Dtos;
using DomainLayer.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.Cases.Commands.CreatCases
{
    public record CreateCaseCommand(
        string Title,
        string? Description,
        int ClientId,
        int? AssignedToUserId,
        List<int>? TagIds
    ) : IRequest<OperationResult<CaseDto>>;
}
