using Account.DataContract.Entities;
using Account.Repository.Repositories.Interfaces;
using Account.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _repository;
        public ModuleService(IModuleRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task CreateModule(Module module)
        {
            await _repository.CreateModule(module);
        }

        public async Task<bool> DeleteModule(string id)
        {
            var result = await _repository.DeleteModule(id);
            return result;
        }

        public async Task<Module> GetModuleById(string id)
        {
            return await _repository.GetModuleById(id);
        }

        public async Task<IEnumerable<Module>> GetModules()
        {
            return await _repository.GetModules();
        }

        public async Task<bool> UpdateModule(Module module)
        {
            var updateResult = await _repository.UpdateModule(module);
            return updateResult;
        }
    }
}
