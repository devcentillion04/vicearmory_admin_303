using Account.DataContract.Entities;
using Authenticate.DataContract;
using System.Threading.Tasks;

namespace Authenticate.Repository.Interfaces
{
    /// <summary>
    /// Inserface IAuthenticateRepository
    /// </summary>
    public interface IAuthenticateRepository
    {
        /// <summary>
        /// Define UserLogin details 
        /// </summary>
        /// <param name="req">user credential object</param>
        /// <returns></returns>
        Task<UserLogin> GetLoginDetails(string userName);
        
        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="req">AuthenticateRequest object</param>
        /// <returns></returns>
        Task<User> GetUsers(AuthenticateRequest req);

        /// <summary>
        /// Create UserLogin 
        /// </summary>
        /// <param name="req">UserLogin Object</param>
        /// <returns></returns>
        Task CreateUserLogin(UserLogin req);

        /// <summary>
        /// Update User Login Details
        /// </summary>
        /// <param name="req">UserLogin object</param>
        /// <returns></returns>
        Task<bool> UpdateUserLogin(UserLogin req);
    }
}
