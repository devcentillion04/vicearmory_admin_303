using Account.DataContract.Entities;
using Account.Repository.Repositories.Interfaces;
using Account.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _repository;
        public UserRoleService(IUserRoleRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task CreateUserRole(UserRole userRole)
        {
            await _repository.CreateUserRole(userRole);
        }

        public async Task<bool> DeleteUserRole(string id)
        {
            var result = await _repository.DeleteUserRole(id);
            return result;
        }

        public async Task<UserRole> GetUserRoleById(string id)
        {
            return await _repository.GetUserRoleById(id);
        }

        public async Task<IEnumerable<UserRole>> GetUserRoles()
        {
            return await _repository.GetUserRoles();
        }

        public async Task<bool> UpdateUserRole(UserRole userRole)
        {
            var updateResult = await _repository.UpdateUserRole(userRole);
            return updateResult;
        }
    }
}
