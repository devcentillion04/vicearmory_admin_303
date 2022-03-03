using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViceArmory.RequestObject.Account
{
    /// <summary>
    /// Menu class
    /// </summary>
    public class MenuRequestDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Menu name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string ParentId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public List<MenuRequest> MenuRequestList { get; set; }
    }
    public class MenuRequest
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Menu name is required")]
        public string Name { get; set; }
    }
}
