using ViceArmory.Middleware.Interface;
using System;
using System.Collections.Generic;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.DTO.RequestObject.Authenticate;
using Middleware.Infrastructure;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using ViceArmory.DTO.ResponseObject.AppSettings;
using System.Threading.Tasks;

namespace ViceArmory.Middleware.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private IOptions<APISettings> _options;

        public AuthenticateService(IOptions<APISettings> options)
        {
            _options = options;

        }
        #region '---- Interface Implementations ----'

        /// <summary>
        /// Method to get list of employees
        /// </summary>
        /// <param name="request">request object</param>
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest req)
        {
            try
            {
                AuthenticateResponse res = new AuthenticateResponse();
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.AUTHENTICATE, req, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<AuthenticateResponse>(responseString);

                return res;
            }
            catch
            {
                throw;
            }
        }

        ///// <summary>
        ///// Method to authenticate user.
        ///// </summary>
        ///// <param name="request">request object</param>
        ///// <returns>true or false</returns>
        //public bool IsEmployeeAuthenticated(EmployeeLoginRequestDTO request)
        //{
        //    try
        //    {
        //        bool isSuccess = false;
        //        // Make an api post call and get the response string.
        //        string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.IS_EMPLOYEE_AUTHENTICATED, request);

        //        //Convert the string in to desired object.
        //        isSuccess = responseString == "True" ? true : false;

        //        return isSuccess;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ExceptionHandler.HandleException(ex, true);
        //    }
        //}

        #endregion
    }
}
