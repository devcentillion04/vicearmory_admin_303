using Account.DataContract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Repository.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        /// <summary>
        /// Get all UserRole
        /// </summary>
        /// <returns>Return all UserRole</returns>
        Task<IEnumerable<UserRole>> GetUserRoles();

        /// <summary>
        /// Get UserRole Detail
        /// </summary>
        /// <param name="id">UserRole id</param>
        /// <returns>Return UserRole result</returns>
        Task<UserRole> GetUserRoleById(string id);

        /// <summary>
        /// Create UserRole
        /// </summary>
        /// <param name="userRole">UserRole Object</param>
        /// <returns></returns>
        Task CreateUserRole(UserRole userRole);

        /// <summary>
        /// Update UserRole
        /// </summary>
        /// <param name="userRole">UserRole Object</param>
        /// <returns></returns>
        Task<bool> UpdateUserRole(UserRole userRole);

        /// <summary>
        /// Delete UserRole
        /// </summary>
        /// <param name="id">UserRole id</param>
        /// <returns></returns>
        Task<bool> DeleteUserRole(string id);
    }
}
