using ApplicationLayer.Features.User.Dtos;
using ApplicationLayer.Interfaces;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    UserName = u.UserName!,
                    FullName = $"{u.FirstName} {u.LastName}",
                    UserEmail = u.UserEmail!,
                    PhoneNumber = u.PhoneNumber
                })
                .ToListAsync();
        }
    }
}
