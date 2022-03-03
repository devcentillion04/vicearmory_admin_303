using Account.DataContract.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Repository.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Get all Role
        /// </summary>
        /// <returns>Return all Role</returns>
        Task<IEnumerable<Role>> GetRoles();

        /// <summary>
        /// Get Role Detail
        /// </summary>
        /// <param name="id">Role id</param>
        /// <returns>Return Role result</returns>
        Task<Role> GetRoleById(string id);

        /// <summary>
        /// Create Role
        /// </summary>
        /// <param name="role">Role Object</param>
        /// <returns></returns>
        Task CreateRole(Role role);

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="role">Role Object</param>
        /// <returns></returns>
        Task<bool> UpdateRole(Role role);

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns></returns>
        Task<bool> DeleteRole(string id);
    }
}
