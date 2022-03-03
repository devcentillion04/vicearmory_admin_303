using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.RequestObject.ApiConfiguration;

namespace ViceArmory.DAL.Repository
{
    public class ApiConfigurationService : IApiConfigurationService
    {
        private ApiConfigToken token = new ApiConfigToken();
        private readonly IOptions<ApiConfigurationSetting> apiConfigurationSetting;
        public ApiConfigurationService(IOptions<ApiConfigurationSetting> apiConfigurationSetting)
        {
            this.apiConfigurationSetting = apiConfigurationSetting;
        }
        public async Task<ApiConfigToken> GetAccessToken()
        {
            RequestAccessToken requestAccessToken = new RequestAccessToken();
            requestAccessToken = requestAccessTokenData();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.apiConfigurationSetting.Value.AccessTokenUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.PostAsJsonAsync("", requestAccessToken).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ApiConfigToken>(response.Content.ReadAsStringAsync().Result);
                    return result;
                }
                return null;
            }
        }
        public async Task<ApiConfigToken> GetAccessTokenFromSession()
        {
            RequestAccessToken requestAccessToken = new RequestAccessToken();
            requestAccessToken = requestAccessTokenData();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.apiConfigurationSetting.Value.ProjectUrl+ "api/v1/Authenticate/GetAccessToken");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = new HttpResponseMessage();
                response = await client.GetAsync(client.BaseAddress).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var a = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<ApiConfigToken>(response.Content.ReadAsStringAsync().Result);
                    return result;
                }
                return null;
            }
        }

        public RequestAccessToken requestAccessTokenData()
        {
            RequestAccessToken requestAccessToken = new RequestAccessToken();
            requestAccessToken.refresh_token = this.apiConfigurationSetting.Value.refresh_token;
            requestAccessToken.client_id = this.apiConfigurationSetting.Value.client_id;
            requestAccessToken.client_secret = this.apiConfigurationSetting.Value.client_secret;
            requestAccessToken.grant_type = this.apiConfigurationSetting.Value.grant_type;
            return requestAccessToken;
        }
    }
}

