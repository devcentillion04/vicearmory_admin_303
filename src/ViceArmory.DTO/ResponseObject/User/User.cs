using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace ViceArmory.DTO.ResponseObject.User
{
    /// <summary>
    /// User Details class
    /// </summary>
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsVendor { get; set; }
        public DateTime RegisterAt { get; set; }
        public DateTime LastLogin { get; set; }
        public string Intro { get; set; }//The brief introduction of the Vendor User to be displayed on the Product Page.
        public string Profile { get; set; }//The vendor details to be displayed on the Product Page.
        [JsonIgnore]
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
