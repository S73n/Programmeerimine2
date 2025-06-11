using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public class UserRoleService : IUserRoleService
    {
        private readonly ApplicationDbContext _context;

        public UserRoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserRole>> GetAllUserRolesAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task<UserRole> GetUserRoleByIdAsync(int id)
        {
            return await _context.UserRoles.FindAsync(id);
        }

        public async Task CreateUserRoleAsync(UserRole userRole)
        {
            _context.Add(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserRoleAsync(UserRole userRole)
        {
            _context.Update(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserRoleAsync(int id)
        {
            var userRole = await _context.UserRoles.FindAsync(id);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UserRoleExistsAsync(int id)
        {
            return await _context.UserRoles.AnyAsync(e => e.Id == id);
        }
    }
} 