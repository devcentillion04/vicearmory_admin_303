using Account.DataContract.Entities;
using Account.Repository.Data.Interfaces;
using Account.Repository.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Repository.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        #region Members
        private readonly IAccountContext _context;
        #endregion

        #region Construction
        public RoleRepository(IAccountContext context)
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
        public async Task CreateRole(Role role)
        {
            role.CreatedDate = DateTime.UtcNow;
            await _context.Roles.InsertOneAsync(role);
        }

        /// <summary>
        /// Delete UserRole
        /// </summary>
        /// <param name="id">UserRole id</param>
        /// <returns></returns>
        public async Task<bool> DeleteRole(string id)
        {
            FilterDefinition<Role> filter = Builders<Role>.Filter.Eq(p => p.Id, id);
            var update = Builders<Role>.Update.Set(nameof(Role.IsDeleted), true);
            var deleteResult = await _context.Roles.UpdateOneAsync(filter, update);

            return deleteResult.IsAcknowledged
                && deleteResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Get UserRole Detail
        /// </summary>
        /// <param name="id">UserRole id</param>
        /// <returns>Return UserRole result</returns>
        public async Task<Role> GetRoleById(string id)
        {
            return await _context
                          .Roles
                          .Find(p => p.Id == id)
                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get all UserRole
        /// </summary>
        /// <returns>Return all UserRole</returns>
        public async Task<IEnumerable<Role>> GetRoles()
        {
            var builder = Builders<Role>.Filter;
            FilterDefinition<Role> filter = FilterDefinition<Role>.Empty;
            filter = filter & builder.Eq(p => p.IsDeleted, false);
            return (await _context
                            .Roles
                            .FindAsync(filter))
                            .ToList();
        }

        /// <summary>
        /// Update UserRole
        /// </summary>
        /// <param name="userRole">UserRole Object</param>
        /// <returns></returns>
        public async Task<bool> UpdateRole(Role role)
        {
            role.UpdatedDate = DateTime.UtcNow;
            var updateResult = await _context
                                        .Roles
                                        .ReplaceOneAsync(filter: g => g.Id == role.Id, replacement: role);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        #endregion
    }
}
