using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using ViceArmory.DTO.RequestObject.BaseRequest;

namespace ViceArmory.RequestObject.Product
{
    public class ProductImageRequestDTO : BaseRequestModel
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ImageFilePath { get; set; }
        public string ImageName { get; set; }
    }
    public class ProductImageRequest : BaseRequestModel
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ProductId { get; set; }
        //public string ImageFile { get; set; }
        //public string ImageName { get; set; }
        public IEnumerable<IFormFile> ProductImages { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
