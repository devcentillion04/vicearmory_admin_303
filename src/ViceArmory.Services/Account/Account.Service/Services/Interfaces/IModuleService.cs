using Account.DataContract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services.Interfaces
{
    public interface IModuleService
    {
        Task<IEnumerable<Module>> GetModules();
        Task<Module> GetModuleById(string id);
        Task CreateModule(Module module);
        Task<bool> UpdateModule(Module module);
        Task<bool> DeleteModule(string id);
    }
}
