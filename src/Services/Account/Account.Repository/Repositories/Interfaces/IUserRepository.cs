using Account.DataContract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Repository.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(string id);
        Task CreateUser(User User);
        Task<bool> UpdateUser(User User);
        Task<bool> DeleteUser(string id);
    }
}
