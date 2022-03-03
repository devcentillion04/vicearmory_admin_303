using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory.DTO.MapObject
{
    public class Attributes
    {
        public string count { get; set; }
        public string offset { get; set; }
        public string limit { get; set; }
    }

    public class Category
    {
        public string categoryID { get; set; }
        public string name { get; set; }
        public string nodeDepth { get; set; }
        public string fullPathName { get; set; }
        public string leftNode { get; set; }
        public string rightNode { get; set; }
        public string parentID { get; set; }
        public DateTime createTime { get; set; }
        public DateTime timeStamp { get; set; }
    }

    public class MenuMapDTO
    {
        public MenuMapDTO()
        {
            Attributes = new Attributes();
            Category = new List<Category>();
        }
        [JsonProperty("@attributes")]
        public Attributes Attributes { get; set; }
        public List<Category> Category { get; set; }
    }
}
