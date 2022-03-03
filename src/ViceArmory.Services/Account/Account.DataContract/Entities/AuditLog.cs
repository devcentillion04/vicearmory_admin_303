using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using static Account.DataContract.Entities.Enum.CommonEnums;

namespace Account.DataContract.Entities
{
    /// <summary>
    /// Audit log class
    /// </summary>
    public class AuditLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string EventID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public AuditActivityEnum AuditActivity { get; set; }
        public string ActivityID { get; set; }
        public string IPAddress { get; set; }
        public string Description { get; set; }
    }
}



