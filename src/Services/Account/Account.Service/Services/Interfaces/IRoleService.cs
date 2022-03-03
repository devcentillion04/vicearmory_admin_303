using Account.DataContract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRoleById(string id);
        Task CreateRole(Role role);
        Task<bool> UpdateRole(Role role);
        Task<bool> DeleteRole(string id);
    }
}
