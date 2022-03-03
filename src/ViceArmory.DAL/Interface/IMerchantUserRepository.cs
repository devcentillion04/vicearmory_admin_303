using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Account;

namespace ViceArmory.DAL.Interface
{
    public interface IMerchantUserRepository
    {
        /// <summary>
        /// Get all MerchantUser
        /// </summary>
        /// <returns>Return all MerchantUser</returns>
        Task<IEnumerable<MerchantUserResponseDTO>> GetMerchantUsers();

        /// <summary>
        /// Get MerchantUser Detail
        /// </summary>
        /// <param name="id">MerchantUser id</param>
        /// <returns>Return MerchantUser result</returns>
        Task<MerchantUserResponseDTO> GetMerchantUserById(string id);

        /// <summary>
        /// Create MerchantUser
        /// </summary>
        /// <param name="merchantUser">MerchantUser Object</param>
        /// <returns></returns>
        Task CreateMerchantUser(MerchantUserResponseDTO user);

        /// <summary>
        /// Update MerchantUser
        /// </summary>
        /// <param name="merchantUser">MerchantUser Object</param>
        /// <returns></returns>
        Task<bool> UpdateMerchantUser(MerchantUserResponseDTO User);

        /// <summary>
        /// Delete MerchantUser
        /// </summary>
        /// <param name="id">MerchantUser id</param>
        /// <returns></returns>
        Task<bool> DeleteMerchantUser(string id);
    }
}
