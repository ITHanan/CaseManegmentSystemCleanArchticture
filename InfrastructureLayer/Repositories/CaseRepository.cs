using ApplicationLayer.Common.Pagination;
using ApplicationLayer.Interfaces;
using DomainLayer.Models;
using InfrastructureLayer.Data;
using InfrastructureLayer.Repositories;
using Microsoft.EntityFrameworkCore;

public class CaseRepository : GenericRepository<Case>, ICaseRepository
{
    private readonly AppDbContext _context;

    public CaseRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Case?> GetCaseWithDetailsAsync(int id)
    {
        return await _context.Cases
            .Include(c => c.Client)
            .Include(c => c.AssignedTo)
            .Include(c => c.CreatedByUser)
            .Include(c => c.Notes)
            .Include(c => c.CaseTags)
                .ThenInclude(ct => ct.Tag)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Case>> GetAllCasesWithDetailsAsync()
    {
        return await _context.Cases
            .Include(c => c.CreatedByUser)
            .Include(c => c.AssignedTo)
            .Include(c => c.Client)
            .Include(c => c.CaseTags).ThenInclude(ct => ct.Tag)
            .Include(c => c.Notes)
            .ToListAsync();
    }


    public async Task<List<Case>> GetCasesByTagIdAsync(int tagId)
    {
        return await _context.Cases
            .Include(c => c.CreatedByUser)
            .Include(c => c.AssignedTo)
            .Include(c => c.CaseTags)
                .ThenInclude(ct => ct.Tag)
            .Where(c => c.CaseTags.Any(ct => ct.TagId == tagId))
            .ToListAsync();
    }

    public async Task<List<Case>> GetCasesByTagNameAsync(string tagName)
    {
        return await _context.Cases
            .Include(c => c.CreatedByUser)
            .Include(c => c.AssignedTo)
            .Include(c => c.CaseTags)
                .ThenInclude(ct => ct.Tag)
            .Where(c => c.CaseTags.Any(ct => ct.Tag.Name.ToLower() == tagName.ToLower()))
            .ToListAsync();
    }

    public async Task<PaginatedResult<Case>> GetPaginatedCasesAsync(int pageNumber, int pageSize)
    {
        var query = _context.Cases
            .Include(c => c.CreatedByUser)
            .Include(c => c.AssignedTo)
            .Include(c => c.CaseTags)
                .ThenInclude(ct => ct.Tag)
            .AsQueryable();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(c => c.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<Case>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

}
