using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Service;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class UserRoleServiceTests : ServiceTestBase
    {
        [Fact]
        public async Task CreateUserRoleAsync_should_add_role()
        {
            var service = new UserRoleService(DbContext);
            var role = new UserRole { RoleName = "TestRole" };
            await service.CreateUserRoleAsync(role);
            Assert.Equal(1, DbContext.UserRoles.Count());
        }

        [Fact]
        public async Task UpdateUserRoleAsync_should_update_role()
        {
            var service = new UserRoleService(DbContext);
            var role = new UserRole { RoleName = "TestRole" };
            DbContext.UserRoles.Add(role);
            DbContext.SaveChanges();
            role.RoleName = "Updated";
            await service.UpdateUserRoleAsync(role);
            Assert.Equal("Updated", DbContext.UserRoles.First().RoleName);
        }

        [Fact]
        public async Task DeleteUserRoleAsync_should_remove_role()
        {
            var service = new UserRoleService(DbContext);
            var role = new UserRole { RoleName = "TestRole" };
            DbContext.UserRoles.Add(role);
            DbContext.SaveChanges();
            await service.DeleteUserRoleAsync(role.Id);
            Assert.Empty(DbContext.UserRoles);
        }

        [Fact]
        public async Task GetUserRoleByIdAsync_should_return_role()
        {
            var service = new UserRoleService(DbContext);
            var role = new UserRole { RoleName = "TestRole" };
            DbContext.UserRoles.Add(role);
            DbContext.SaveChanges();
            var result = await service.GetUserRoleByIdAsync(role.Id);
            Assert.NotNull(result);
            Assert.Equal(role.RoleName, result.RoleName);
        }
    }
} 