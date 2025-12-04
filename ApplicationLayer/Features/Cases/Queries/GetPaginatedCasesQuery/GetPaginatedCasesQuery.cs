using ApplicationLayer.Common.Pagination;
using ApplicationLayer.Features.Cases.Dtos;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Cases.Queries.GetPaginatedCasesQuery
{

    public record GetPaginatedCasesQuery(int PageNumber, int PageSize)
        : IRequest<OperationResult<PaginatedResult<CaseDto>>>;
}
