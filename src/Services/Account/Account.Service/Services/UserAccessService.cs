using Account.DataContract.Entities;
using Account.Repository.Repositories.Interfaces;
using Account.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services
{
    public class UserAccessService : IUserAccessService
    {
        private readonly IUserAccessRepository _repository;
        public UserAccessService(IUserAccessRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task CreateUserAccess(UserAccess userAccess)
        {
            await _repository.CreateUserAccess(userAccess);
        }

        public async Task<bool> DeleteUserAccess(string id)
        {
            var result = await _repository.DeleteUserAccess(id);
            return result;
        }

        public async Task<UserAccess> GetUserAccessById(string id)
        {
            return await _repository.GetUserAccessById(id);
        }

        public async Task<IEnumerable<UserAccess>> GetUserAccess()
        {
            return await _repository.GetUserAccess();
        }

        public async Task<bool> UpdateUserAccess(UserAccess userAccess)
        {
            var updateResult = await _repository.UpdateUserAccess(userAccess);
            return updateResult;
        }
    }
}
