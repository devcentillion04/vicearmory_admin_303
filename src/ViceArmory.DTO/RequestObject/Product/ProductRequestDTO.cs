using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using ViceArmory.DTO.RequestObject.BaseRequest;

namespace ViceArmory.RequestObject.Product
{
    public class ProductRequestDTO : BaseRequestModel
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //public string UserId { get; set; }
        public string CategoryId { get; set; }
        [BsonElement("Title")]
        [Required(ErrorMessage = "Product Title name is required")]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }
        [BsonElement("MetaTitle")]
        [Required(ErrorMessage = "MetaTitle name is required")]
        public string MetaTitle { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        [Required(ErrorMessage = "Summary must be specified")]
        public string Summary { get; set; }
        [Required(ErrorMessage = "Product Type is required")]
        public string Type { get; set; }
        public string SKU { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price { get; set; }
        public float Discount { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public int Quantity { get; set; }
        public bool Shop { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "IsWeeklyAdvertise must be specified")]
        public bool IsWeeklyAdvertise { get; set; }
        public string ProductImage { get; set; }
  
    }
    public class ProductRequestImgUploadDTO : BaseRequestModel
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
        public double Price { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }
        public bool Shop { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsWeeklyAdvertise { get; set; }
        public string ProductImages { get; set; }
        public IFormFile UploadedProductImages { get; set; }
        public string[] file { get; set; }
    }
}
