using DomainLayer.Models;
using ApplicationLayer.Common.Pagination;

namespace ApplicationLayer.Interfaces;
public interface ICaseRepository : IGenericRepository<Case>
{
    Task<Case?> GetCaseWithDetailsAsync(int id);
    Task<List<Case>> GetAllCasesWithDetailsAsync();
    Task<List<Case>> GetCasesByTagIdAsync(int tagId);
    Task <List<Case>> GetCasesByTagNameAsync(string tagName);

    Task<PaginatedResult<Case>> GetPaginatedCasesAsync(int pageNumber, int pageSize);
}
