using ApplicationLayer.Features.Cases.Dtos;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Cases.Queries.GetCasesByTagName
{
    public record GetCasesByTagNameQuery(string TagName)
    : IRequest<OperationResult<List<CaseDto>>>;

}
