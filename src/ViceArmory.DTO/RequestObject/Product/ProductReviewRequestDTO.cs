using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using ViceArmory.DTO.RequestObject.BaseRequest;

namespace ViceArmory.RequestObject.Product
{
    public class ProductReviewRequestDTO : BaseRequestModel
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ParentId { get; set; }
        public string Title { get; set; }
        public int Rating { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime PublishedAt { get; set; }

    }
}
