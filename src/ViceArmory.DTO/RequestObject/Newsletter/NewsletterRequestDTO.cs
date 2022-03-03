using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.RequestObject.BaseRequest;

namespace ViceArmory.DTO.RequestObject.Newsletter
{
    public class NewsletterRequestDTO : BaseRequestModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email address is required")]
        [MaxLength(254, ErrorMessage = "Email address cannot be longer than 254 characters.")]
        public string Email { get; set; }

        //Subscribe flag
        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UnsubscribeAt { get; set; }



    }
}
