using Account.DataContract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<IEnumerable<UserRole>> GetUserRoles();
        Task<UserRole> GetUserRoleById(string id);
        Task CreateUserRole(UserRole userRole);
        Task<bool> UpdateUserRole(UserRole userRole);
        Task<bool> DeleteUserRole(string id);
    }
}
