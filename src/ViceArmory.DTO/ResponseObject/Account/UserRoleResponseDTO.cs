using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ViceArmory.DTO.ResponseObject.Account
{
    /// <summary>
    /// User Role class
    /// </summary>
    public class UserRoleResponseDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
