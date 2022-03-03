using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory.DTO.RequestObject.ApiConfiguration
{
    public class ApiConfigurationSetting
    {
        public string code { get; set; }
        public string api_url { get; set; }
        public string grant_type { get; set; }
        public string LightSpeedAccessUrl { get; set; }
        public string AccessTokenUrl { get; set; }
        public string ProjectUrl { get; set; }
        public string response_type { get; set; }
        public string client_id { get; set; }
        public string refresh_token { get; set; }
        public string client_secret { get; set; }
        public string scope { get; set; }
        public string state { get; set; }
        public string account_id { get; set; }
    }
}
