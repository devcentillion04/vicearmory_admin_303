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
            var token = await _iApiConfigurationService.GetAccessTokenFromSession();
            using (var client = new HttpClient())
            {
                MenuResponseDTO _menuResponseDTO = new MenuResponseDTO();
                client.BaseAddress = new Uri(_apiConfigurationSetting.Value.api_url+ _apiConfigurationSetting.Value.account_id+"/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token.ToString());
                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.GetAsync("Category.json").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var builder = Builders<MenuResponseDTO>.Filter;
                    MenuMapDTO _menuMapDTO = JsonConvert.DeserializeObject<MenuMapDTO>(result);
                    var configuration = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Category, MenuResponseDTO>().ForMember(s=>s.Id,m=>m.MapFrom(s=>s.categoryID))
                        .ForMember(s => s.Name, m => m.MapFrom(s => s.name))
                        .ForMember(s => s.Description, m => m.MapFrom(s => s.name))
                        .ForMember(s => s.ParentId, m => m.MapFrom(s => s.parentID))
                        .ForMember(s => s.CreatedDate, m => m.MapFrom(s => s.createTime));
                    });
                    var mapper = configuration.CreateMapper();
                    var data = mapper.Map<List<Category>,List<MenuResponseDTO>>(_menuMapDTO.Category);
                    return data;
                }
            }
            return new List<MenuResponseDTO>();
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
