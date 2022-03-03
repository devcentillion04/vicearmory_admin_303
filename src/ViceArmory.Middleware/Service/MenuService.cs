using Microsoft.Extensions.Options;
using Middleware.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.Middleware.Interface;
using ViceArmory.RequestObject.Account;

namespace ViceArmory.Middleware.Service
{
    public class MenuService : IMenuService
    {
        private const string PRODUCT_IMAGE_LOCATION = "{0}/products/{1}/";
        private IOptions<APISettings> _options;
        private AuthenticateResponse _UserInfo;

        public MenuService(IOptions<APISettings> options)
        {
            _options = options;
        }

        public async Task<MenuResponseDTO> CreateMenu(MenuRequestDTO req)
        {
            MenuResponseDTO res = new MenuResponseDTO();
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.CREATEMENU, req, _options.Value.APIUrl, _UserInfo);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<MenuResponseDTO>(responseString);

            }
            catch
            {
                throw;
            }
            return res;
        }

        public async Task<bool> DeleteMenuById(string id)
        {
            bool res = false;
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.DELETEMENUBYID, id, _options.Value.APIUrl, _UserInfo);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<bool>(responseString);

            }
            catch
            {
                throw;
            }
            return res;
        }

        public async Task<IEnumerable<MenuResponseDTO>> GetMenu()
        {
            List<MenuResponseDTO> res = new List<MenuResponseDTO>();
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.GetResponseString(Constants.GETMENU, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<List<MenuResponseDTO>>(responseString);

            }
            catch
            {
                throw;
            }
            return res;
        }

        public async Task<MenuResponseDTO> GetMenuById(string id)
        {
            MenuResponseDTO res = new MenuResponseDTO();
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.GETMENUBYID, id, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<MenuResponseDTO>(responseString);

            }
            catch
            {
                throw;
            }
            return res;
        }

        public async Task<bool> UpdateMenu(MenuRequestDTO req)
        {
            bool res = false;
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.UPDATEMENU, req, _options.Value.APIUrl, _UserInfo);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<bool>(responseString);

            }
            catch
            {
                throw;
            }
            return res;
        }

        public void SetSession(AuthenticateResponse req)
        {
            _UserInfo = req;
        }
    }
}
