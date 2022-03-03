using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.Account;

namespace ViceArmory.DAL.Repository
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
        public async Task CreateRole(RoleResponseDTO role)
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
            FilterDefinition<RoleResponseDTO> filter = Builders<RoleResponseDTO>.Filter.Eq(p => p.Id, id);
            var update = Builders<RoleResponseDTO>.Update.Set(nameof(RoleResponseDTO.IsDeleted), true);
            var deleteResult = await _context.Roles.UpdateOneAsync(filter, update);

            return deleteResult.IsAcknowledged
                && deleteResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Get UserRole Detail
        /// </summary>
        /// <param name="id">UserRole id</param>
        /// <returns>Return UserRole result</returns>
        public async Task<RoleResponseDTO> GetRoleById(string id)
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
        public async Task<IEnumerable<RoleResponseDTO>> GetRoles()
        {
            var builder = Builders<RoleResponseDTO>.Filter;
            FilterDefinition<RoleResponseDTO> filter = FilterDefinition<RoleResponseDTO>.Empty;
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
        public async Task<bool> UpdateRole(RoleResponseDTO role)
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
