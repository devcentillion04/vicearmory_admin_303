using Account.DataContract.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ViceArmory.RequestObject.Product
{
    public class ProductRequestDTO : BaseRequestModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //public string UserId { get; set; }
        public string CategoryId { get; set; }
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("MetaTitle")]
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string Type { get; set; }
        public string SKU { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }
        public bool Shop { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }

        public bool IsDeleted { get; set; }
    }
}
