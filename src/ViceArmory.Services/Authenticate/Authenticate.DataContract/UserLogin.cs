using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Authenticate.DataContract
{
    /// <summary>
    /// User Login Details class
    /// </summary>
    public class UserLogin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime UserLoggedinStartTime { get; set; }
        public DateTime UserLoggedinEndTime { get; set; }
        public string TokenId { get; set; }
        public string IpAddress { get; set; }
    }
}
