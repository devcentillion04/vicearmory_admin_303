using Account.DataContract.Entities;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Product.DataContract.Entities
{
    public class ProductImage : BaseRequestModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ImageFilePath { get; set; }
        public string ImageName { get; set; }
    }
    public class ProductImageRequest : BaseRequestModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ProductId { get; set; }
        //public string ImageFile { get; set; }
        //public string ImageName { get; set; }
        public IEnumerable<IFormFile> ProductImages { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
