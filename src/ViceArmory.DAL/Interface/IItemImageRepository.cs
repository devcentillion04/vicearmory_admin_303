using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.MapObject;

namespace ViceArmory.DAL.Interface
{
    public interface IItemImageRepository
    {
        Task<ItemImageListMapDTO> GetItemImagesByItemId(int id);
        Task<bool> DeleteItemImage(int id);
        Task<Image> InsertItemImage(Image image);
        Task<Image> UpdateItemImage(Image image);
    }
}
