using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.RequestObject.BaseRequest;

namespace ViceArmory.DTO.ResponseObject.Newsletter
{
    public class NewsletterResponseDTO : BaseRequestModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Email { get; set; }

        //Subscribe flag
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public string UpdatedBy { get; set; }

        public DateTime UnsubscribeAt { get; set; }

    }
}
