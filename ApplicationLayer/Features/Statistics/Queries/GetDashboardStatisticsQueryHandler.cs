using ApplicationLayer.Features.Statistics.Dtos;
using ApplicationLayer.Features.Statistics.Queries;
using ApplicationLayer.Interfaces;
using DomainLayer.Common;
using MediatR;

public class GetDashboardStatisticsQueryHandler
    : IRequestHandler<GetDashboardStatisticsQuery, OperationResult<StatisticsDto>>
{
    private readonly IStatisticsRepository _statsRepo;

    public GetDashboardStatisticsQueryHandler(IStatisticsRepository statsRepo)
    {
        _statsRepo = statsRepo;
    }

    public async Task<OperationResult<StatisticsDto>> Handle(
        GetDashboardStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        var stats = new StatisticsDto
        {
            TotalCases = await _statsRepo.GetTotalCasesAsync(),
            OpenCases = await _statsRepo.GetOpenCasesAsync(),
            InProgressCases = await _statsRepo.GetInProgressCasesAsync(),
            ClosedCases = await _statsRepo.GetClosedCasesAsync(),

            CasesPerUser = await _statsRepo.GetCasesPerUserAsync(),
            CasesPerClient = await _statsRepo.GetCasesPerClientAsync(),
            TopTags = await _statsRepo.GetTopTagsAsync(),

            AverageResolutionTimeInDays = await _statsRepo.GetAverageClosingTimeAsync(),

                    GeneratedAt = DateTime.UtcNow

        };

        return OperationResult<StatisticsDto>.Success(stats);
    }
}
