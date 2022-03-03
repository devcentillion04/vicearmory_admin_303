using Account.DataContract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services.Interfaces
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetMenu();
        Task<Menu> GetMenuById(string id);
        Task CreateMenu(Menu menu);
        Task<bool> UpdateMenu(Menu menu);
        Task<bool> DeleteMenu(string id);
    }
}
