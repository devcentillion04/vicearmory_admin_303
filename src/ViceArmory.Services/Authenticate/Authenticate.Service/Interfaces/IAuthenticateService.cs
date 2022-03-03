using Authenticate.DataContract;
using System.Threading.Tasks;

namespace Authenticate.Service.Interfaces
{
    public interface IAuthenticateService
    {
        /// <summary>
        /// Define Autheticate property
        /// </summary>
        /// <param name="model">AuthenticateRequest object</param>
        /// <returns></returns>
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        
        /// <summary>
        /// Get Login Details
        /// </summary>
        /// <returns>Return User login details</returns>
        Task<UserLogin> GetLoginDetails(string userName);
    }
}
