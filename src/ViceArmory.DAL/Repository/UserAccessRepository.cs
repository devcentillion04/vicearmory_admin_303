using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.Account;

namespace ViceArmory.DAL.Repository
{
    public class UserAccessRepository : IUserAccessRepository
    {
        #region Members
        private readonly IAccountContext _context;
        #endregion

        #region Construction
        public UserAccessRepository(IAccountContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region Methods

        /// <summary>
        /// Create UserAccess
        /// </summary>
        /// <param name="userAccess">UserAccess Object</param>
        /// <returns></returns>
        public async Task CreateUserAccess(UserAccessResponseDTO userAccess)
        {
            userAccess.CreatedDate = DateTime.UtcNow;
            await _context.UserAccess.InsertOneAsync(userAccess);
        }

        /// <summary>
        /// Delete UserAccess
        /// </summary>
        /// <param name="id">UserAccess id</param>
        /// <returns></returns>
        public async Task<bool> DeleteUserAccess(string id)
        {
            FilterDefinition<UserAccessResponseDTO> filter = Builders<UserAccessResponseDTO>.Filter.Eq(p => p.Id, id);
            var update = Builders<UserAccessResponseDTO>.Update.Set(nameof(UserAccessResponseDTO.IsDeleted), true);
            var deleteResult = await _context.UserAccess.UpdateOneAsync(filter, update);

            return deleteResult.IsAcknowledged
                && deleteResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Get UserAccess Detail
        /// </summary>
        /// <param name="id">UserAccess id</param>
        /// <returns>Return UserAccess result</returns>
        public async Task<UserAccessResponseDTO> GetUserAccessById(string id)
        {
            return await _context
                          .UserAccess
                          .Find(p => p.Id == id)
                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get all UserAccess
        /// </summary>
        /// <returns>Return all UserAccess</returns>
        public async Task<IEnumerable<UserAccessResponseDTO>> GetUserAccess()
        {
            var builder = Builders<UserAccessResponseDTO>.Filter;
            FilterDefinition<UserAccessResponseDTO> filter = FilterDefinition<UserAccessResponseDTO>.Empty;
            filter = filter & builder.Eq(p => p.IsDeleted, false);
            return (await _context
                            .UserAccess
                            .FindAsync(filter))
                            .ToList();
        }

        /// <summary>
        /// Update UserAccess
        /// </summary>
        /// <param name="userAccess">UserAccess Object</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserAccess(UserAccessResponseDTO userAccess)
        {
            userAccess.UpdatedDate = DateTime.UtcNow;
            var updateResult = await _context
                                        .UserAccess
                                        .ReplaceOneAsync(filter: g => g.Id == userAccess.Id, replacement: userAccess);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        #endregion
    }
}
