using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory.DTO.MapObject
{
    public class ItemImageAttributes
    {
        public string count { get; set; }
        public string offset { get; set; }
        public string limit { get; set; }
    }
    public class Image
    {
        public string imageID { get; set; }
        public string description { get; set; }
        public string filename { get; set; }
        public string ordering { get; set; }
        public string publicID { get; set; }
        public string baseImageURL { get; set; }
        public string size { get; set; }
        public DateTime createTime { get; set; }
        public DateTime timeStamp { get; set; }
        public string itemID { get; set; }
        public string itemMatrixID { get; set; }
    }

    public class ItemImageListMapDTO
    {
        public ItemImageListMapDTO()
        {
            Attributes = new ItemImageAttributes();
            Image = new List<Image>();
        }
        [JsonProperty("@attributes")]
        public ItemImageAttributes Attributes { get; set; }
        public List<Image> Image { get; set; }
    }
    public class ItemImageMapDTO
    {
        public ItemImageMapDTO()
        {
            Attributes = new ItemImageAttributes();
            Image = new Image();
        }
        [JsonProperty("@attributes")]
        public ItemImageAttributes Attributes { get; set; }
        public Image Image { get; set; }
    }

}
