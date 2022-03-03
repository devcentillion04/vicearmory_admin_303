using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.Account;

namespace ViceArmory.DAL.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        #region Members
        private readonly IAccountContext _context;
        #endregion

        #region Construction
        public UserRoleRepository(IAccountContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region Methods

        /// <summary>
        /// Create UserRole
        /// </summary>
        /// <param name="userRole">UserRole Object</param>
        /// <returns></returns>
        public async Task CreateUserRole(UserRoleResponseDTO userRole)
        {
            userRole.CreatedDate = DateTime.UtcNow;
            await _context.UserRoles.InsertOneAsync(userRole);
        }

        /// <summary>
        /// Delete UserRole
        /// </summary>
        /// <param name="id">UserRole id</param>
        /// <returns></returns>
        public async Task<bool> DeleteUserRole(string id)
        {
            FilterDefinition<UserRoleResponseDTO> filter = Builders<UserRoleResponseDTO>.Filter.Eq(p => p.Id, id);
            var update = Builders<UserRoleResponseDTO>.Update.Set(nameof(UserRoleResponseDTO.IsDeleted), true);
            var deleteResult = await _context.UserRoles.UpdateOneAsync(filter, update);

            return deleteResult.IsAcknowledged
                && deleteResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Get UserRole Detail
        /// </summary>
        /// <param name="id">UserRole id</param>
        /// <returns>Return UserRole result</returns>
        public async Task<UserRoleResponseDTO> GetUserRoleById(string id)
        {
            return await _context
                          .UserRoles
                          .Find(p => p.Id == id)
                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get all UserRole
        /// </summary>
        /// <returns>Return all UserRole</returns>
        public async Task<IEnumerable<UserRoleResponseDTO>> GetUserRoles()
        {
            var builder = Builders<UserRoleResponseDTO>.Filter;
            FilterDefinition<UserRoleResponseDTO> filter = FilterDefinition<UserRoleResponseDTO>.Empty;
            filter = filter & builder.Eq(p => p.IsDeleted, false);
            return (await _context
                            .UserRoles
                            .FindAsync(filter))
                            .ToList();
        }

        /// <summary>
        /// Update UserRole
        /// </summary>
        /// <param name="userRole">UserRole Object</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserRole(UserRoleResponseDTO userRole)
        {
            userRole.UpdatedDate = DateTime.UtcNow;
            var updateResult = await _context
                                        .UserRoles
                                        .ReplaceOneAsync(filter: g => g.Id == userRole.Id, replacement: userRole);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        #endregion
    }
}
