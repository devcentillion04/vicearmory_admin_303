using Account.DataContract.Entities;
using Account.Repository.Data.Interfaces;
using Account.Repository.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Repository.Repositories
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
        public async Task CreateUserAccess(UserAccess userAccess)
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
            FilterDefinition<UserAccess> filter = Builders<UserAccess>.Filter.Eq(p => p.Id, id);
            var update = Builders<UserAccess>.Update.Set(nameof(UserAccess.IsDeleted), true);
            var deleteResult = await _context.UserAccess.UpdateOneAsync(filter, update);

            return deleteResult.IsAcknowledged
                && deleteResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Get UserAccess Detail
        /// </summary>
        /// <param name="id">UserAccess id</param>
        /// <returns>Return UserAccess result</returns>
        public async Task<UserAccess> GetUserAccessById(string id)
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
        public async Task<IEnumerable<UserAccess>> GetUserAccess()
        {
            var builder = Builders<UserAccess>.Filter;
            FilterDefinition<UserAccess> filter = FilterDefinition<UserAccess>.Empty;
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
        public async Task<bool> UpdateUserAccess(UserAccess userAccess)
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
