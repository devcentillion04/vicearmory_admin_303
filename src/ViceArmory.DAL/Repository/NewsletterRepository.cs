using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.RequestObject.Newsletter;
using ViceArmory.DTO.ResponseObject.Newsletter;

namespace ViceArmory.DAL.Repository
{
    public class NewsletterRepository : INewsletterRepository
    {
        #region Members
        private readonly IAccountContext _context;
        #endregion

        #region Construction
        public NewsletterRepository(IAccountContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region Methods
        public async Task CreateNewsletters(NewsletterResponseDTO req)
        {
            FilterDefinition<NewsletterResponseDTO> filter = Builders<NewsletterResponseDTO>.Filter.Eq(p => p.Email, req.Email);
            var currdate = DateTime.UtcNow;
            var item = await _context.Newsletter.Find(filter).ToListAsync();
            if (item.Count > 0)
            {
                var update = Builders<NewsletterResponseDTO>.Update.Set(nameof(NewsletterResponseDTO.IsActive), true).Set(nameof(NewsletterResponseDTO.UpdatedAt), currdate);
                await _context.Newsletter.UpdateOneAsync(filter, update);
            }
            else
            {
                req.CreatedAt = currdate;
                await _context.Newsletter.InsertOneAsync(req);
            }
        }

        //Get all subscribe list
        public async Task<IEnumerable<NewsletterResponseDTO>> GetNewsletters()
        {
            var builder = Builders<NewsletterResponseDTO>.Filter;
            FilterDefinition<NewsletterResponseDTO> filter = FilterDefinition<NewsletterResponseDTO>.Empty;
            filter = filter & builder.Eq(p => p.IsActive, true);
            return (await _context
                            .Newsletter
                            .FindAsync(filter))
                            .ToList();
        }

        public async Task<bool> Unsubscribe(string id)
        {
            FilterDefinition<NewsletterResponseDTO> filter = Builders<NewsletterResponseDTO>.Filter.Eq(p => p.Id, id);
            var Unsubscribedate = DateTime.UtcNow;
            var update = Builders<NewsletterResponseDTO>.Update.Set(nameof(NewsletterResponseDTO.IsActive), false).Set(nameof(NewsletterResponseDTO.UnsubscribeAt), Unsubscribedate);
            var UnsubscribeResult = await _context.Newsletter.UpdateOneAsync(filter, update);

            return UnsubscribeResult.IsAcknowledged
                && UnsubscribeResult.ModifiedCount > 0;
        }
        #endregion
    }
}
