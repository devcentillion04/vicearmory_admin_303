using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.RequestObject.BaseRequest;

namespace ViceArmory.DTO.ResponseObject.Logs
{
    public class LogResponseDTO : BaseRequestModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PageName { get; set; }
        public string Description { get; set; }
        public string HostName { get; set; }
        public string IpAddress { get; set; }
        public string   created_by { get; set; }
        public DateTime Created_date { get; set; }
    }
}
