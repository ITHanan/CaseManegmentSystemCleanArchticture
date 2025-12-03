using ApplicationLayer.Features.Cases.Dtos;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Cases.Queries.GetCaseById
{
    public record GetCaseByIdQuery(int Id)
        : IRequest<OperationResult<CaseDto>>;
}
