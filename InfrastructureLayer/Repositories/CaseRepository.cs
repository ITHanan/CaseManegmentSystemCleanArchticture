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

}
