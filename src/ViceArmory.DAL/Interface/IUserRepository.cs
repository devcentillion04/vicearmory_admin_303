using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Account;

namespace ViceArmory.DAL.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserResponseDTO>> GetUsers();
        Task<IEnumerable<UserResponseDTO>> GetAdmin();
        Task<UserResponseDTO> GetUserById(string id);
        Task<string> CreateUser(UserResponseDTO User);
        Task<bool> UpdateUser(UserResponseDTO User);
        Task<bool> DeleteUser(string id);
        Task<string> SendEmail(string smtpAddress, int portNumber, string userName, string password, string to, string from, string fromName, string subject, string body);
        Task<string> VerifyEmail(string Email, string Id);
    }
}
