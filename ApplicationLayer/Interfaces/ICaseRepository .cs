using ApplicationLayer.Interfaces;
using DomainLayer.Models;

public interface ICaseRepository : IGenericRepository<Case>
{
    Task<Case?> GetCaseWithDetailsAsync(int id);
}
