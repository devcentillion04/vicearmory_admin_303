using Microsoft.Extensions.Options;
using Middleware.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.Middleware.Interface;

namespace ViceArmory.Middleware.Service
{
    public class AdminLoginService : IAdminLoginService
    {
        #region Members
        private IOptions<APISettings> _options;
        private AuthenticateResponse _UserInfo;
        #endregion
        #region Construction
        public AdminLoginService(IOptions<APISettings> options)
        {
            _options = options;
           
        }

        #endregion

        #region Methods
        public async Task<string> CreateUser(UserResponseDTO user)
        {
            try
            {
                user.IsAdmin = true;
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.CREATEUSER, user, _options.Value.APIUrl, _UserInfo);
                return responseString;
              
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> VerifyEmail(string Email, string Id)
        {
            UserResponseDTO user = new UserResponseDTO();
            user.Email = Email;
            user.Id = Id;
            try
            {
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.VERIFYUSER, user, _options.Value.APIUrl, _UserInfo);
                return responseString;
            }
            catch
            {
                return Constants.EMAILNOTVERIFIED;
                throw;
            }
        }
        #endregion
    }
}
