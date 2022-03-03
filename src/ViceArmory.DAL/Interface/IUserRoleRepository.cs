using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Account;

namespace ViceArmory.DAL.Interface
{
    public interface IUserRoleRepository
    {
        /// <summary>
        /// Get all UserRole
        /// </summary>
        /// <returns>Return all UserRole</returns>
        Task<IEnumerable<UserRoleResponseDTO>> GetUserRoles();

        /// <summary>
        /// Get UserRole Detail
        /// </summary>
        /// <param name="id">UserRole id</param>
        /// <returns>Return UserRole result</returns>
        Task<UserRoleResponseDTO> GetUserRoleById(string id);

        /// <summary>
        /// Create UserRole
        /// </summary>
        /// <param name="userRole">UserRole Object</param>
        /// <returns></returns>
        Task CreateUserRole(UserRoleResponseDTO userRole);

        /// <summary>
        /// Update UserRole
        /// </summary>
        /// <param name="userRole">UserRole Object</param>
        /// <returns></returns>
        Task<bool> UpdateUserRole(UserRoleResponseDTO userRole);

        /// <summary>
        /// Delete UserRole
        /// </summary>
        /// <param name="id">UserRole id</param>
        /// <returns></returns>
        Task<bool> DeleteUserRole(string id);
    }
}
