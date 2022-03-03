using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.RequestObject.Newsletter;
using ViceArmory.DTO.ResponseObject.Newsletter;

namespace ViceArmory.Middleware.Interface
{
    public interface INewsletterService
    {
        /// <summary>
        /// Get all Newsletter
        /// </summary>
        /// <returns>Return all Newsletter</returns>
        Task<IEnumerable<NewsletterResponseDTO>> GetNewsletters();

        /// <summary>
        /// Get Newsletter By Id
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return Newsletter result</returns>
        Task<NewsletterResponseDTO> GetNewsletterById(string id);

        /// <summary>
        /// Delete Newsletter
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return delete result</returns>
        Task<bool> DeleteNewsletterById(string id);

        /// <summary>
        /// Create Newsletter
        /// </summary>
        /// <param name="req"></param>
        /// <returns>Return Create result</returns>

        Task<NewsletterResponseDTO> CreateNewsletters(NewsletterRequestDTO req);

        /// <summary>
        /// Updated Newsletter
        /// </summary>
        /// <param name="req"></param>
        /// <returns>Return updated result</returns>
        Task<bool> UpdatedNewsletters(NewsletterResponseDTO req);
    }
}
