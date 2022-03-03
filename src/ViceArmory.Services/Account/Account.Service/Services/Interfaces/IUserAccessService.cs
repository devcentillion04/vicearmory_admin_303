using Account.DataContract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services.Interfaces
{
    public interface IUserAccessService
    {
        Task<IEnumerable<UserAccess>> GetUserAccess();
        Task<UserAccess> GetUserAccessById(string id);
        Task CreateUserAccess(UserAccess userAccess);
        Task<bool> UpdateUserAccess(UserAccess userAccess);
        Task<bool> DeleteUserAccess(string id);
    }
}
