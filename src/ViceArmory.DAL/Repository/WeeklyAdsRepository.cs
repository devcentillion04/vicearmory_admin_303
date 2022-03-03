using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.RequestObject.ApiConfiguration;
using ViceArmory.DTO.ResponseObject.WeeklyAds;
using ViceArmory.ResponseObject.Product;

namespace ViceArmory.DAL.Repository
{
    public class WeeklyAdsRepository : IWeeklyAdsRepository
    {
        #region Members
        private readonly IAccountContext _context;
        private readonly IApiConfigurationService _iApiConfigurationService;
        private readonly IOptions<ApiConfigurationSetting> _apiConfigurationSetting;
        #endregion

        #region Construction
        public WeeklyAdsRepository(IAccountContext context, IApiConfigurationService iApiConfigurationService, IOptions<ApiConfigurationSetting> apiConfigurationSetting)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _iApiConfigurationService = iApiConfigurationService;
            this._apiConfigurationSetting = apiConfigurationSetting;
        }
        #endregion

        /// <summary>
        /// Create PDF
        /// </summary>
        /// <param name="weeklyads">Request Object</param>
        /// <returns></returns>
        public async Task AddPdf(WeeklyAdsResponseDTO weeklyads)
        {
            FilterDefinition<WeeklyAdsResponseDTO> filter = Builders<WeeklyAdsResponseDTO>.Filter.Eq(p => p.Id, weeklyads.Id);
            var item = await _context.WeeklyAds.Find(filter).ToListAsync();
            if (item.Count <= 0)
            {
                await _context.WeeklyAds.InsertOneAsync(weeklyads);
            }
        }

        /// <summary>
        /// Delete PDF
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return delete result</returns>
        public async Task<bool> DeletePdf(string id)
        {
            FilterDefinition<WeeklyAdsResponseDTO> filter = Builders<WeeklyAdsResponseDTO>.Filter.Eq(p => p.Id, id);
            var update = Builders<WeeklyAdsResponseDTO>.Update.Set(nameof(WeeklyAdsResponseDTO.IsDeleted), true);
            var deleteResult = await _context.WeeklyAds.UpdateOneAsync(filter, update);
            return deleteResult.IsAcknowledged
                && deleteResult.ModifiedCount > 0;
        }

        /// <summary>
        /// For add IsDeleted Eq False
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return delete result</returns>
        public async Task<bool> ActivatePdf(string id)
        {
            FilterDefinition<WeeklyAdsResponseDTO> filter = Builders<WeeklyAdsResponseDTO>.Filter.Eq(p => p.Id, id);
            var update = Builders<WeeklyAdsResponseDTO>.Update.Set(nameof(WeeklyAdsResponseDTO.IsDeleted), false);
            var deleteResult = await _context.WeeklyAds.UpdateOneAsync(filter, update);
            return deleteResult.IsAcknowledged
                && deleteResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Get PDF 
        /// </summary>
        /// <param name="">Request Object</param>
        /// <returns>Return PDF result</returns>
        public async Task<IEnumerable<WeeklyAdsResponseDTO>> GetAllPdf()
        {
            var builder = Builders<WeeklyAdsResponseDTO>.Filter;
            FilterDefinition<WeeklyAdsResponseDTO> filter = FilterDefinition<WeeklyAdsResponseDTO>.Empty;
            return (await _context
                            .WeeklyAds
                            .FindAsync(filter))
                            .ToList();
        }
        /// <summary>
        /// Get PDF If deleted false
        /// </summary>
        /// <param name="">Request Object</param>
        /// <returns>Return PDF result</returns>
        public async Task<IEnumerable<WeeklyAdsResponseDTO>> GetPdf()
        {
            var builder = Builders<WeeklyAdsResponseDTO>.Filter;
            FilterDefinition<WeeklyAdsResponseDTO> filter = FilterDefinition<WeeklyAdsResponseDTO>.Empty;
            filter = filter & builder.Eq(p => p.IsDeleted, false);
            return (await _context
                            .WeeklyAds
                            .FindAsync(filter))
                            .ToList();
        }

        /// <summary>
        /// Get PDF by id
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return PDF result</returns>
        public async Task<WeeklyAdsResponseDTO> GetPdfById(string id)
        {
            return await _context
                          .WeeklyAds
                          .Find(p => p.Id == id)
                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Update PDF
        /// </summary>
        /// <param name="weeklyads">Request Object</param>
        /// <returns>Return update result</returns>
        public async Task<bool> UpdatePdf(WeeklyAdsResponseDTO weeklyads)
        {
            var updateResult = await _context
                                       .WeeklyAds
                                       .ReplaceOneAsync(filter: g => g.Id == weeklyads.Id, replacement: weeklyads);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
