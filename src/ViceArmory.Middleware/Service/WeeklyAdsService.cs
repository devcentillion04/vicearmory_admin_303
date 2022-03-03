using Microsoft.Extensions.Options;
using Middleware.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.DTO.ResponseObject.WeeklyAds;
using ViceArmory.Middleware.Interface;

namespace ViceArmory.Middleware.Service
{
    public class WeeklyAdsService : IWeeklyAdsService
    {
        #region Members
        private IOptions<APISettings> _options;
        private AuthenticateResponse _UserInfo;
        #endregion

        #region Construction
        public WeeklyAdsService(IOptions<APISettings> options)
        {
            _options = options;
        }
        #endregion
        /// <summary>
        /// Create Pdf
        /// </summary>
        /// <param name="weeklyads">Request Object</param>
        /// <returns></returns>
        public async Task<bool> AddPdf(WeeklyAdsResponseDTO weeklyads)
        {
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.ADDPDF, weeklyads, _options.Value.APIUrl, _UserInfo);
                //Convert the string in to desired object.
                WeeklyAdsResponseDTO res = JsonConvert.DeserializeObject<WeeklyAdsResponseDTO>(responseString);
                if (responseString.ToLower() == "ok")
                    return true;
                return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete Pdf
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return delete result</returns>
        public async Task<bool> DeletePdf(string id)
        {
            bool res = false;
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.DELETEPDF, id, _options.Value.APIUrl, _UserInfo);
                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// For add IsDeleted Eq False
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return delete result</returns>
        public async Task<bool> ActivatePdf(string id)
        {
            bool res = false;
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.ACTIVATEPDF, id, _options.Value.APIUrl, _UserInfo);
                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<bool>(responseString);
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Get all pdf 
        /// </summary>
        /// <param name="weeklyads">Request Object</param>
        /// <returns>Return weeklyads result</returns>  
        public async Task<IEnumerable<WeeklyAdsResponseDTO>> GetAllPdf()
        {
            List<WeeklyAdsResponseDTO> res = new List<WeeklyAdsResponseDTO>();
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.GetResponseString(Constants.GETALLPDF, _options.Value.APIUrl);
                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<List<WeeklyAdsResponseDTO>>(responseString).ToList();
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Get PDF If deleted false
        /// </summary>
        /// <param name="weeklyads">Request Object</param>
        /// <returns>Return weeklyads result</returns>  
        public async Task<IEnumerable<WeeklyAdsResponseDTO>> GetPdf()
        {
            List<WeeklyAdsResponseDTO> res = new List<WeeklyAdsResponseDTO>();
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.GetResponseString(Constants.GETALLPDF, _options.Value.APIUrl);
                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<List<WeeklyAdsResponseDTO>>(responseString);
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Get GetPdfById
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return Pdf result</returns>
        public async Task<WeeklyAdsResponseDTO> GetPdfById(string id)
        {
            WeeklyAdsResponseDTO res = new WeeklyAdsResponseDTO();
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.DELETEPDF, id, _options.Value.APIUrl);
                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<WeeklyAdsResponseDTO>(responseString);
            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Update Pdf
        /// </summary>
        /// <param name="weeklyads">Request Object</param>
        /// <returns></returns>
        public async Task<bool> UpdatePdf(WeeklyAdsResponseDTO weeklyads)
        {
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.UPDATEPDF, weeklyads, _options.Value.APIUrl, _UserInfo);
                //Convert the string in to desired object.
                bool res = JsonConvert.DeserializeObject<bool>(responseString);
                return res;
            }
            catch
            {
                throw;
            }
        }
    }
}
