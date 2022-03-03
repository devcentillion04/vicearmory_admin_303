using Account.DataContract.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Repository.Repositories.Interfaces
{
    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> GetModules();
        Task<Module> GetModuleById(string id);
        Task CreateModule(Module module);
        Task<bool> UpdateModule(Module module);
        Task<bool> DeleteModule(string id);
    }
}
