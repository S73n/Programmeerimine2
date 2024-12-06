using KooliProjekt.Data;

namespace KooliProjekt.Service
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRole>> GetAllUserRolesAsync();
        Task<UserRole> GetUserRoleByIdAsync(int id);
        Task<UserRole> CreateUserRoleAsync(UserRole userRole);
        Task<UserRole> UpdateUserRoleAsync(UserRole userRole);
        Task DeleteUserRoleAsync(int id);
    }
}
