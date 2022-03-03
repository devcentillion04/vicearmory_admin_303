using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Account;

namespace ViceArmory.DAL.Interface
{
    public interface IMenuRepository
    {
        /// <summary>
        /// Get all Menu
        /// </summary>
        /// <returns>Return all Menu</returns>
        // Task<IEnumerable<MenuResponseDTO>> GetMenu();
        Task<IEnumerable<MenuResponseDTO>> GetMenu();

        /// <summary>
        /// Get Menu Detail
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns>Return Menu result</returns>
        Task<MenuResponseDTO> GetMenuById(string id);

        /// <summary>
        /// Create Menu
        /// </summary>
        /// <param name="menu">Menu Object</param>
        /// <returns></returns>
        Task CreateMenu(MenuResponseDTO menu);

        /// <summary>
        /// Create Menu List
        /// </summary>
        /// <param name="menu">Menu List Object</param>
        /// <returns></returns>
        Task CreateMenuList(List<MenuResponseDTO> menu);

        /// <summary>
        /// Update Menu
        /// </summary>
        /// <param name="menu">Menu Object</param>
        /// <returns></returns>
        Task<bool> UpdateMenu(MenuResponseDTO menu);

        /// <summary>
        /// Delete Menu
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns></returns>
        Task<bool> DeleteMenu(string id);
    }
}
