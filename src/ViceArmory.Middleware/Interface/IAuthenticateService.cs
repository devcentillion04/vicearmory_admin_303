using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.DTO.RequestObject.Authenticate;
using ViceArmory.DTO.RequestObject.Employee;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.DTO.ResponseObject.Employee;

namespace ViceArmory.Middleware.Interface
{
    public interface IAuthenticateService
    {
        /// <summary>
        /// Method to get AuthenticateResponse
        /// </summary>
        /// <param name="request">request object</param>
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest req);

        ///// <summary>
        ///// Method to authenticate user.
        ///// </summary>
        ///// <param name="request">request object</param>
        ///// <returns>true or false</returns>
        //bool IsEmployeeAuthenticated(EmployeeLoginRequestDTO request);
    }
}
