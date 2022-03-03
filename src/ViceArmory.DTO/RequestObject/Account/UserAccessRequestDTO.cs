using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ViceArmory.RequestObject.Account
{
    /// <summary>
    /// User Access class
    /// </summary>
    public class UserAccessRequestDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string RoleId { get; set; }
        public string MenuId { get; set; }
        public string ModuleId { get; set; }
        public string Access { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
