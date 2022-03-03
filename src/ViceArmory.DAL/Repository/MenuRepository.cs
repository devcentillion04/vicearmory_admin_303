using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.Account;
using Newtonsoft.Json;
using ViceArmory.DTO.RequestObject.ApiConfiguration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using ViceArmory.Utility;
using ViceArmory.DTO.MapObject;
using AutoMapper;

namespace ViceArmory.DAL.Repository
{
    public class MenuRepository : IMenuRepository
    {
        #region Members
        private readonly IAccountContext _context;
        private readonly IApiConfigurationService _iApiConfigurationService;
        private readonly IOptions<ApiConfigurationSetting> _apiConfigurationSetting;
        #endregion

        #region Construction
        public MenuRepository(IAccountContext context, IApiConfigurationService iApiConfigurationService, IOptions<ApiConfigurationSetting> apiConfigurationSetting)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _iApiConfigurationService = iApiConfigurationService;
            this._apiConfigurationSetting = apiConfigurationSetting;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Create Menu
        /// </summary>
        /// <param name="menu">Menu Object</param>
        /// <returns></returns>
        public async Task CreateMenu(MenuResponseDTO menu)
        {
            menu.CreatedDate = DateTime.UtcNow;
            await _context.Menu.InsertOneAsync(menu);
        }

        /// <summary>
        /// Create Menu List
        /// </summary>
        /// <param name="menu">Menu List Object</param>
        /// <returns></returns>
        public async Task CreateMenuList(List<MenuResponseDTO> menu)
        {
            foreach (var item in menu)
            {
                item.CreatedDate = DateTime.UtcNow;
            }
            foreach (var item in menu)
            {
                var _existingData = await _context
                          .Menu
                          .Find(p => p.Id == item.Id)
                          .FirstOrDefaultAsync();
                if(_existingData!=null)
                {
                    await _context.Menu.ReplaceOneAsync(filter: g => g.Id == item.Id, replacement: item);
                }
                else
                {
                    await _context.Menu.InsertOneAsync(item);
                }
            }
        }

        /// <summary>
        /// Delete Menu
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns></returns>
        public async Task<bool> DeleteMenu(string id)
        {
            FilterDefinition<MenuResponseDTO> filter = Builders<MenuResponseDTO>.Filter.Eq(p => p.Id, id);
            var update = Builders<MenuResponseDTO>.Update.Set(nameof(MenuResponseDTO.IsDeleted), true);
            var deleteResult = await _context.Menu.UpdateOneAsync(filter, update);

            return deleteResult.IsAcknowledged
                && deleteResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Get Menu Detail
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns>Return Menu result</returns>
        public async Task<MenuResponseDTO> GetMenuById(string id)
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
        public async Task<IEnumerable<MenuResponseDTO>> GetMenu()
        {

            var builder = Builders<MenuResponseDTO>.Filter;
            FilterDefinition<MenuResponseDTO> filter = FilterDefinition<MenuResponseDTO>.Empty;
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
        public async Task<bool> UpdateMenu(MenuResponseDTO menu)
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
