using Account.DataContract.Entities;
using Account.Repository.Repositories.Interfaces;
using Account.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;
        public RoleService(IRoleRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task CreateRole(Role role)
        {
            await _repository.CreateRole(role);
        }

        public async Task<bool> DeleteRole(string id)
        {
            var result = await _repository.DeleteRole(id);
            return result;
        }

        public async Task<Role> GetRoleById(string id)
        {
            return await _repository.GetRoleById(id);
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {
            return await _repository.GetRoles();
        }

        public async Task<bool> UpdateRole(Role role)
        {
            var updateResult = await _repository.UpdateRole(role);
            return updateResult;
        }
    }
}
