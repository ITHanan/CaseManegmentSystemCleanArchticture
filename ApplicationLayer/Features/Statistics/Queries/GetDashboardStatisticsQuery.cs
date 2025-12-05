using ApplicationLayer.Features.Statistics.Dtos;
using DomainLayer.Common;
using MediatR;

namespace ApplicationLayer.Features.Statistics.Queries
{
    public class GetDashboardStatisticsQuery: IRequest<OperationResult<StatisticsDto>>
    {
    }
}
