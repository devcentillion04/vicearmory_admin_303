using Account.DataContract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Repository.Repositories.Interfaces
{
    public interface IMenuRepository
    {
        /// <summary>
        /// Get all Menu
        /// </summary>
        /// <returns>Return all Menu</returns>
        Task<IEnumerable<Menu>> GetMenu();

        /// <summary>
        /// Get Menu Detail
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns>Return Menu result</returns>
        Task<Menu> GetMenuById(string id);

        /// <summary>
        /// Create Menu
        /// </summary>
        /// <param name="menu">Menu Object</param>
        /// <returns></returns>
        Task CreateMenu(Menu menu);

        /// <summary>
        /// Update Menu
        /// </summary>
        /// <param name="menu">Menu Object</param>
        /// <returns></returns>
        Task<bool> UpdateMenu(Menu menu);

        /// <summary>
        /// Delete Menu
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns></returns>
        Task<bool> DeleteMenu(string id);
    }
}
