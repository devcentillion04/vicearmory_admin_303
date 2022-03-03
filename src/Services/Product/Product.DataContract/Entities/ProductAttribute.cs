using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Product.DataContract.Entities
{
    public class ProductAttribute
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}