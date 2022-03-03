using Account.DataContract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services.Interfaces
{
    public interface IMerchantUserService
    {
        Task<IEnumerable<MerchantUser>> GetMerchantUsers();
        Task<MerchantUser> GetMerchantUserById(string id);
        Task CreateMerchantUser(MerchantUser user);
        Task<bool> UpdateMerchantUser(MerchantUser User);
        Task<bool> DeleteMerchantUser(string id);
    }
}
