using Account.DataContract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Repository.Repositories.Interfaces
{
    public interface IMerchantUserRepository
    {
        /// <summary>
        /// Get all MerchantUser
        /// </summary>
        /// <returns>Return all MerchantUser</returns>
        Task<IEnumerable<MerchantUser>> GetMerchantUsers();

        /// <summary>
        /// Get MerchantUser Detail
        /// </summary>
        /// <param name="id">MerchantUser id</param>
        /// <returns>Return MerchantUser result</returns>
        Task<MerchantUser> GetMerchantUserById(string id);

        /// <summary>
        /// Create MerchantUser
        /// </summary>
        /// <param name="merchantUser">MerchantUser Object</param>
        /// <returns></returns>
        Task CreateMerchantUser(MerchantUser user);

        /// <summary>
        /// Update MerchantUser
        /// </summary>
        /// <param name="merchantUser">MerchantUser Object</param>
        /// <returns></returns>
        Task<bool> UpdateMerchantUser(MerchantUser User);

        /// <summary>
        /// Delete MerchantUser
        /// </summary>
        /// <param name="id">MerchantUser id</param>
        /// <returns></returns>
        Task<bool> DeleteMerchantUser(string id);
    }
}
