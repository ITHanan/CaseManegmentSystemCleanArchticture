using ApplicationLayer.Interfaces;
using DomainLayer.Models;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly AppDbContext _context;

        public StatisticsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalCasesAsync()
            => await _context.Cases.CountAsync();

        public async Task<int> GetOpenCasesAsync()
            => await _context.Cases.CountAsync(c => c.Status == CaseStatus.Open);

        public async Task<int> GetInProgressCasesAsync()
            => await _context.Cases.CountAsync(c => c.Status == CaseStatus.InProgress);

        public async Task<int> GetClosedCasesAsync()
            => await _context.Cases.CountAsync(c => c.Status == CaseStatus.Closed);

        public async Task<Dictionary<string, int>> GetCasesPerUserAsync()
            => await _context.Users
                .Include(u => u.AssignedCases)
                .ToDictionaryAsync(
                    u => u.UserName!,
                    u => u.AssignedCases.Count
                );

        public async Task<Dictionary<string, int>> GetCasesPerClientAsync()
            => await _context.Clients
                .Include(c => c.Cases)
                .ToDictionaryAsync(
                    c => c.FullName,
                    c => c.Cases.Count
                );

        public async Task<List<string>> GetTopTagsAsync(int top = 5)
            => await _context.Tags
                .OrderByDescending(t => t.CaseTags.Count)
                .Select(t => $"{t.Name} ({t.CaseTags.Count})")
                .Take(top)
                .ToListAsync();

        public async Task<double> GetAverageClosingTimeAsync()
        {
            var closedCases = await _context.Cases
                .Where(c => c.ClosedAt != null)
                .ToListAsync();

            if (!closedCases.Any())
                return 0.0;

            return closedCases.Average(c =>
                (c.ClosedAt!.Value - c.CreatedAt).TotalDays
            );
        }
    }
}
