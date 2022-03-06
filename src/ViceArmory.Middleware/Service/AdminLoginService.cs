using Microsoft.Extensions.Options;
using Middleware.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
               var userString = Newtonsoft.Json.JsonConvert.SerializeObject(user);
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

        public async Task<string> SendEmail(string smtpAddress, int portNumber, string userName, string password, string to, string from, string fromName, string subject, string body)
        {
            string msg = "";
            try
            {
                MailMessage mail = new MailMessage(from , to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpAddress;
                smtp.Credentials = new System.Net.NetworkCredential(from, password);
                smtp.Port = portNumber;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                
                smtp.Send(mail);
                return Constants.CREATEUSERMAILSENT;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Constants.CREATEUSERERROR;
            }
        }
    }
}
