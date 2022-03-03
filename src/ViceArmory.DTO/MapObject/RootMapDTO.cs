using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory.DTO.MapObject
{
    public class CommonAttributes
    {
        public string count { get; set; }
        public string offset { get; set; }
        public string limit { get; set; }
    }
    public class RootMapDTO
    {
        public RootMapDTO()
        {
            Attributes = new CommonAttributes();
        }
        [JsonProperty("@attributes")]
        public CommonAttributes Attributes { get; set; }
    }
}
