using Account.DataContract.Entities;
using Account.Repository.Data.Interfaces;
using Account.Repository.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Repository.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        #region Members
        private readonly IAccountContext _context;
        #endregion

        #region Construction
        public MenuRepository(IAccountContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region Methods

        /// <summary>
        /// Create Menu
        /// </summary>
        /// <param name="menu">Menu Object</param>
        /// <returns></returns>
        public async Task CreateMenu(Menu menu)
        {
            menu.CreatedDate = DateTime.UtcNow;
            await _context.Menu.InsertOneAsync(menu);
        }

        /// <summary>
        /// Delete Menu
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns></returns>
        public async Task<bool> DeleteMenu(string id)
        {
            FilterDefinition<Menu> filter = Builders<Menu>.Filter.Eq(p => p.Id, id);
            var update = Builders<Menu>.Update.Set(nameof(Menu.IsDeleted), true);
            var deleteResult = await _context.Menu.UpdateOneAsync(filter, update);

            return deleteResult.IsAcknowledged
                && deleteResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Get Menu Detail
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns>Return Menu result</returns>
        public async Task<Menu> GetMenuById(string id)
        {
            return await _context
                          .Menu
                          .Find(p => p.Id == id)
                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get all Menu
        /// </summary>
        /// <returns>Return all Menu</returns>
        public async Task<IEnumerable<Menu>> GetMenu()
        {
            var builder = Builders<Menu>.Filter;
            FilterDefinition<Menu> filter = FilterDefinition<Menu>.Empty;
            filter = filter & builder.Eq(p => p.IsDeleted, false);
            return (await _context
                            .Menu
                            .FindAsync(filter))
                            .ToList();
        }

        /// <summary>
        /// Update Menu
        /// </summary>
        /// <param name="menu">Menu Object</param>
        /// <returns></returns>
        public async Task<bool> UpdateMenu(Menu menu)
        {
            menu.UpdatedDate = DateTime.UtcNow;
            var updateResult = await _context
                                        .Menu
                                        .ReplaceOneAsync(filter: g => g.Id == menu.Id, replacement: menu);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        #endregion
    }
}
