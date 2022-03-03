using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Account;

namespace ViceArmory.DAL.Interface
{
    public interface IUserAccessRepository
    {
        /// <summary>
        /// Get all UserAccess
        /// </summary>
        /// <returns>Return all UserAccess</returns>
        Task<IEnumerable<UserAccessResponseDTO>> GetUserAccess();

        /// <summary>
        /// Get UserAccess Detail
        /// </summary>
        /// <param name="id">UserAccess id</param>
        /// <returns>Return UserAccess result</returns>
        Task<UserAccessResponseDTO> GetUserAccessById(string id);

        /// <summary>
        /// Create UserAccess
        /// </summary>
        /// <param name="userAccess">UserAccess Object</param>
        /// <returns></returns>
        Task CreateUserAccess(UserAccessResponseDTO userAccess);

        /// <summary>
        /// Update UserAccess
        /// </summary>
        /// <param name="userAccess">UserAccess Object</param>
        /// <returns></returns>
        Task<bool> UpdateUserAccess(UserAccessResponseDTO userAccess);

        /// <summary>
        /// Delete UserAccess
        /// </summary>
        /// <param name="id">UserAccess id</param>
        /// <returns></returns>
        Task<bool> DeleteUserAccess(string id);
    }
}
