using Microsoft.Extensions.Options;
using Middleware.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.RequestObject.Newsletter;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Newsletter;
using ViceArmory.Middleware.Interface;

namespace ViceArmory.Middleware.Service
{
    public class NewsletterService : INewsletterService
    {
        #region Members

        private IOptions<APISettings> _options;

        #endregion

        #region Construction

        public NewsletterService(IOptions<APISettings> options)
        {
            _options = options;
        }
        #endregion

        #region Methods

        public async Task<NewsletterResponseDTO> CreateNewsletters(NewsletterRequestDTO req)
        {
            NewsletterResponseDTO res = new NewsletterResponseDTO();
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.CREATENEWSLETTERS, req, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<NewsletterResponseDTO>(responseString);
            }
            catch
            {
                throw;
            }
            return res;
        }

        public async Task<bool> DeleteNewsletterById(string id)
        {
            bool res = false;
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.DELETENEWSLETTERBYID, id, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<bool>(responseString);

            }
            catch
            {
                throw;
            }
            return res;
        }

        public async Task<NewsletterResponseDTO> GetNewsletterById(string id)
        {
            NewsletterResponseDTO res = new NewsletterResponseDTO();
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.GETNEWSLETTERBYID, id, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<NewsletterResponseDTO>(responseString);
            }
            catch
            {
                throw;
            }
            return res;
        }

        public async Task<IEnumerable<NewsletterResponseDTO>> GetNewsletters()
        {
            List<NewsletterResponseDTO> res = new List<NewsletterResponseDTO>();
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.GetResponseString(Constants.GETNEWSLETTERS, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<List<NewsletterResponseDTO>>(responseString);

            }
            catch
            {
                throw;
            }
            return res;
        }

        public async Task<bool> UpdatedNewsletters(NewsletterResponseDTO req)
        {
            bool res = false;
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.UPDATEMENU, req, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<bool>(responseString);

            }
            catch
            {
                throw;
            }
            return res;
        }

        #endregion
    }
}
