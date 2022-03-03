using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.WeeklyAds;

namespace ViceArmory.Middleware.Interface
{
    public interface IWeeklyAdsService
    {
        /// <summary>
        /// Get PDF by id
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return PDF result</returns>
        Task<WeeklyAdsResponseDTO> GetPdfById(string id);

        /// <summary>
        /// Get PDF 
        /// </summary>
        /// <param name="">Request Object</param>
        /// <returns>Return PDF result</returns>
        Task<IEnumerable<WeeklyAdsResponseDTO>> GetAllPdf();

        /// <summary>
        /// Get PDF If deleted false
        /// </summary>
        /// <param name="">Request Object</param>
        /// <returns>Return PDF result</returns>
        Task<IEnumerable<WeeklyAdsResponseDTO>> GetPdf();

        /// <summary>
        /// Delete PDF
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return delete result</returns>
        Task<bool> DeletePdf(string id);

        /// <summary>
        /// For add IsDeleted Eq False
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return delete result</returns>
        Task<bool> ActivatePdf(string id);

        /// <summary>
        /// Update PDF
        /// </summary>
        /// <param name="weeklyads">Request Object</param>
        /// <returns>Return update result</returns>
        Task<bool> UpdatePdf(WeeklyAdsResponseDTO weeklyads);

        /// <summary>
        /// Create PDF
        /// </summary>
        /// <param name="weeklyads">Request Object</param>
        /// <returns></returns>
        Task<bool> AddPdf(WeeklyAdsResponseDTO weeklyads);
    }
}
