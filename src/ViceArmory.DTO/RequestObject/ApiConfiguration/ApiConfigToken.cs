using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory.DTO.RequestObject.ApiConfiguration
{
    public class ApiConfigToken
    {
        [JsonProperty(PropertyName = "access_token")]
        public string access_token { get; set; }
        [JsonProperty(PropertyName = "code")]
        public string ReuestCode { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Scope { get; set; }
    }
}
