using ApplicationLayer.Features.Cases.Dtos;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Cases.Queries.GetQueryByTagId
{
    public record GetCasesByTagIdQuery(int TagId)
     : IRequest<OperationResult<List<CaseDto>>>;

}
