using ApplicationLayer.Features.Cases.Dtos;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Cases.Queries.GetAllCases
{
    public record GetAllCasesQuery
        : IRequest<OperationResult<IEnumerable<CaseDto>>>;
}
