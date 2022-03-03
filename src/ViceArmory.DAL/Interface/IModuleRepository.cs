using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Account;

namespace ViceArmory.DAL.Interface
{
    public interface IModuleRepository
    {
        Task<IEnumerable<ModuleResponseDTO>> GetModules();
        Task<ModuleResponseDTO> GetModuleById(string id);
        Task CreateModule(ModuleResponseDTO module);
        Task<bool> UpdateModule(ModuleResponseDTO module);
        Task<bool> DeleteModule(string id);
    }
}
