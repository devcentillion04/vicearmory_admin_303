using Account.DataContract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Repository.Repositories.Interfaces
{
    public interface IUserAccessRepository
    {
        /// <summary>
        /// Get all UserAccess
        /// </summary>
        /// <returns>Return all UserAccess</returns>
        Task<IEnumerable<UserAccess>> GetUserAccess();

        /// <summary>
        /// Get UserAccess Detail
        /// </summary>
        /// <param name="id">UserAccess id</param>
        /// <returns>Return UserAccess result</returns>
        Task<UserAccess> GetUserAccessById(string id);

        /// <summary>
        /// Create UserAccess
        /// </summary>
        /// <param name="userAccess">UserAccess Object</param>
        /// <returns></returns>
        Task CreateUserAccess(UserAccess userAccess);

        /// <summary>
        /// Update UserAccess
        /// </summary>
        /// <param name="userAccess">UserAccess Object</param>
        /// <returns></returns>
        Task<bool> UpdateUserAccess(UserAccess userAccess);

        /// <summary>
        /// Delete UserAccess
        /// </summary>
        /// <param name="id">UserAccess id</param>
        /// <returns></returns>
        Task<bool> DeleteUserAccess(string id);
    }
}
