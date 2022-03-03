using Account.DataContract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(string id);
        Task CreateUser(User User);
        Task<bool> UpdateUser(User User);
        Task<bool> DeleteUser(string id);
    }
}
