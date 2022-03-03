using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.MapObject;
using ViceArmory.DTO.RequestObject.ApiConfiguration;
using ViceArmory.DTO.ResponseObject.Account;
using RestSharp;

namespace ViceArmory.DAL.Repository
{
    public class ItemImageRepository : IItemImageRepository
    {
        #region Members
        private readonly IAccountContext _context;
        private readonly IApiConfigurationService _iApiConfigurationService;
        private readonly IOptions<ApiConfigurationSetting> _apiConfigurationSetting;
        #endregion

        #region Construction
        public ItemImageRepository(IAccountContext context, IApiConfigurationService iApiConfigurationService, IOptions<ApiConfigurationSetting> apiConfigurationSetting)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _iApiConfigurationService = iApiConfigurationService;
            this._apiConfigurationSetting = apiConfigurationSetting;
        }
        #endregion
        public async Task<bool> DeleteItemImage(int id)
        {
            var token = await _iApiConfigurationService.GetAccessTokenFromSession();
            using (var client = new HttpClient())
            {
                ItemImageMapDTO _itemImageMapDTO = new ItemImageMapDTO();
                client.BaseAddress = new Uri(_apiConfigurationSetting.Value.api_url + _apiConfigurationSetting.Value.account_id + "/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token.ToString());
                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.DeleteAsync("Image/"+id+".json").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    return true;
                }
            }
            return false;
        }

        public async Task<ItemImageListMapDTO> GetItemImagesByItemId(int id)
        {
            var token = await _iApiConfigurationService.GetAccessTokenFromSession();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiConfigurationSetting.Value.api_url + _apiConfigurationSetting.Value.account_id + "/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token.ToString());
                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.GetAsync("Item/"+id+"/Image.json").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var builder = Builders<ItemImageListMapDTO>.Filter;
                    var _attr= Newtonsoft.Json.JsonConvert.DeserializeObject<RootMapDTO>(result); 
                    if(Convert.ToInt32(_attr.Attributes.count)<=1)
                    {
                        ItemImageListMapDTO _itemImageListMapDTO = new ItemImageListMapDTO();
                        var itemimage= Newtonsoft.Json.JsonConvert.DeserializeObject<ItemImageMapDTO>(result);
                        _itemImageListMapDTO.Attributes = itemimage.Attributes;
                        _itemImageListMapDTO.Image.Add(itemimage.Image);
                        return _itemImageListMapDTO;
                    }
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<ItemImageListMapDTO>(result);
                }
            }
            return new ItemImageListMapDTO();
        }

        public async Task<Image> InsertItemImage(Image image)
        {
            var token = await _iApiConfigurationService.GetAccessTokenFromSession();
            using (var client = new HttpClient())
            {
                Image _image = new Image();
                var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(image), UnicodeEncoding.UTF8, "application/json");
                client.BaseAddress = new Uri(_apiConfigurationSetting.Value.api_url + _apiConfigurationSetting.Value.account_id + "/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token.ToString());
                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.PutAsync("Item/" + image.imageID + "/Image.json", stringContent).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var builder = Builders<ItemImageMapDTO>.Filter;
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Image>(result);
                }
            }
            return new Image();
        }

        public async Task<Image> UpdateItemImage(Image image)
        {
            var token = await _iApiConfigurationService.GetAccessTokenFromSession();
            using (var client = new HttpClient())
            {
                Image _image = new Image();
                var stringContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(image), UnicodeEncoding.UTF8, "application/json");
                client.BaseAddress = new Uri(_apiConfigurationSetting.Value.api_url + _apiConfigurationSetting.Value.account_id + "/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token.ToString());
                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.PutAsync("Item/" + image.imageID + "/Image.json", stringContent).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var builder = Builders<ItemImageMapDTO>.Filter;
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<Image>(result);
                }
            }
            return new Image();
        }
    }
}
