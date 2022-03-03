using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.RequestObject.Newsletter;
using ViceArmory.DTO.ResponseObject.Newsletter;

namespace ViceArmory.DAL.Interface
{
    public interface INewsletterRepository
    {
        /// <summary>
        /// Get all Newsletter
        /// </summary>
        /// <returns>Return all Newsletter</returns>
        Task<IEnumerable<NewsletterResponseDTO>> GetNewsletters();

        /// <summary>
        /// Create Newsletter
        /// </summary>
        /// <param name="req"></param>
        /// <returns>Return Create result</returns>

        Task CreateNewsletters(NewsletterResponseDTO req);

        /// <summary>
        /// UnSubscibe Result
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return UnSubscibe result</returns>
        Task<bool> Unsubscribe(string id);
    }
}
