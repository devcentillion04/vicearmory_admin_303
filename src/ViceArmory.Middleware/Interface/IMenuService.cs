using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.RequestObject.Account;

namespace ViceArmory.Middleware.Interface
{
    public interface IMenuService
    {
        void SetSession(AuthenticateResponse req);
        Task<IEnumerable<MenuResponseDTO>> GetMenu();
        Task<MenuResponseDTO> GetMenuById(string id);
        Task<bool> DeleteMenuById(string id);
        Task<MenuResponseDTO> CreateMenu(MenuRequestDTO req);
        Task<bool> UpdateMenu(MenuRequestDTO req);
    }
}
