using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.Account;

namespace ViceArmory.DAL.Repository
{
    public class MerchantUserRepository : IMerchantUserRepository
    {
        #region Members
        private readonly IAccountContext _context;
        #endregion

        #region Construction
        public MerchantUserRepository(IAccountContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region Methods

        /// <summary>
        /// Create MerchantUser
        /// </summary>
        /// <param name="merchantUser">MerchantUser Object</param>
        /// <returns></returns>
        public async Task CreateMerchantUser(MerchantUserResponseDTO user)
        {
            user.CreatedDate = DateTime.UtcNow;
            await _context.MerchantUsers.InsertOneAsync(user);
        }

        /// <summary>
        /// Delete MerchantUser
        /// </summary>
        /// <param name="id">MerchantUser id</param>
        /// <returns></returns>
        public async Task<bool> DeleteMerchantUser(string id)
        {
            FilterDefinition<MerchantUserResponseDTO> filter = Builders<MerchantUserResponseDTO>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context
                                               .MerchantUsers
                                               .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        /// <summary>
        /// Get MerchantUser Detail
        /// </summary>
        /// <param name="id">MerchantUser id</param>
        /// <returns>Return MerchantUser result</returns>
        public async Task<MerchantUserResponseDTO> GetMerchantUserById(string id)
        {
            return await _context
                          .MerchantUsers
                          .Find(p => p.Id == id)
                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get all MerchantUser
        /// </summary>
        /// <returns>Return all MerchantUser</returns>
        public async Task<IEnumerable<MerchantUserResponseDTO>> GetMerchantUsers()
        {
            return await _context
                            .MerchantUsers
                            .Find(p => true)
                            .ToListAsync();
        }

        /// <summary>
        /// Update MerchantUser
        /// </summary>
        /// <param name="merchantUser">MerchantUser Object</param>
        /// <returns></returns>
        public async Task<bool> UpdateMerchantUser(MerchantUserResponseDTO user)
        {
            user.UpdatedDate = DateTime.UtcNow;
            var updateResult = await _context
                                        .MerchantUsers
                                        .ReplaceOneAsync(filter: g => g.Id == user.Id, replacement: user);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        #endregion
    }
}
