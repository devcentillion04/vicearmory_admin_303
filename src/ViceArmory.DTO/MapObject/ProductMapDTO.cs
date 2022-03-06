using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViceArmory.DTO.MapObject
{
    public class Attr
    {
        public string count { get; set; }
        public string offset { get; set; }
        public string limit { get; set; }
    }

    public class ItemPrice
    {
        public string amount { get; set; }
        public string useTypeID { get; set; }
        public string useType { get; set; }
    }

    public class Prices
    {
        public Prices()
        {
            ItemPrice = new List<ItemPrice>();
        }
        public List<ItemPrice> ItemPrice { get; set; }
    }

    public class Item
    {
        public Item()
        {
            Prices = new Prices();
        }
        public string itemID { get; set; }
        public string systemSku { get; set; }
        public string defaultCost { get; set; }
        public string avgCost { get; set; }
        public string discountable { get; set; }
        public string tax { get; set; }
        public string archived { get; set; }
        public string itemType { get; set; }
        public string serialized { get; set; }
        public string description { get; set; }
        public string modelYear { get; set; }
        public string upc { get; set; }
        public string ean { get; set; }
        public string customSku { get; set; }
        public string manufacturerSku { get; set; }
        public DateTime createTime { get; set; }
        public DateTime timeStamp { get; set; }
        public string categoryID { get; set; }
        public string taxClassID { get; set; }
        public string departmentID { get; set; }
        public string itemMatrixID { get; set; }
        public string manufacturerID { get; set; }
        public string seasonID { get; set; }
        public string defaultVendorID { get; set; }
        public Prices Prices { get; set; }
    }

    public class ProductMapDTO
    {
        public ProductMapDTO()
        {
            Attributes = new Attr();
            Item = new List<Item>();
        }
        [JsonProperty("@attributes")]
        public Attr Attributes { get; set; }
        public List<Item> Item { get; set; }
    }

}
