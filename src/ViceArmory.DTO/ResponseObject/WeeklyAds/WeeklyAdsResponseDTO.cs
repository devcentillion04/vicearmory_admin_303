using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory.DTO.ResponseObject.WeeklyAds
{
    public class WeeklyAdsResponseDTO
    {
        [BsonId]
        public string Id { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
