using System.Threading.Tasks;
using ViceArmory.DTO.RequestObject.Authenticate;
using ViceArmory.DTO.RequestObject.User;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.DTO.ResponseObject.User;

namespace ViceArmory.DAL.Interface
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
        Task<UserResponseDTO> GetUsers(AuthenticateRequest req);

        /// <summary>
        /// Get ADmin
        /// </summary>
        /// <param name="req">AuthenticateRequest object</param>
        /// <returns></returns>
        Task<UserResponseDTO> GetAdmin(AuthenticateRequest req);
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
        
        /// <summary>
        /// Define Autheticate property
        /// </summary>
        /// <param name="model">AuthenticateRequest object</param>
        /// <returns></returns>
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);

        /// <summary>
        /// Define User Autheticate property
        /// </summary>
        /// <param name="model">AuthenticateRequest object</param>
        /// <returns></returns>
        Task<AuthenticateResponse> AuthenticateUser(AuthenticateRequest model);

    }
}
