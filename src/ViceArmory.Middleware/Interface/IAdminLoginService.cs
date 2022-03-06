using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Account;

namespace ViceArmory.Middleware.Interface
{
    public interface IAdminLoginService
    {
        Task<string> CreateUser(UserResponseDTO User);
        Task<string> VerifyEmail(string Email,string Id);
        Task<string> SendEmail(string smtpAddress, int portNumber, string userName, string password, string to, string from, string fromName, string subject, string body);
    }
}
