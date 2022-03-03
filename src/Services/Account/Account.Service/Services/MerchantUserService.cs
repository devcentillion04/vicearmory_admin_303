using Account.DataContract.Entities;
using Account.Repository.Repositories.Interfaces;
using Account.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services
{
    public class MerchantUserService : IMerchantUserService
    {
        private readonly IMerchantUserRepository _repository;
        public MerchantUserService(IMerchantUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task CreateMerchantUser(MerchantUser user)
        {
            await _repository.CreateMerchantUser(user);
        }

        public async Task<bool> DeleteMerchantUser(string id)
        {
            var result = await _repository.DeleteMerchantUser(id);
            return result;
        }

        public async Task<MerchantUser> GetMerchantUserById(string id)
        {
            return await _repository.GetMerchantUserById(id);
        }

        public async Task<IEnumerable<MerchantUser>> GetMerchantUsers()
        {
            return await _repository.GetMerchantUsers();
        }

        public async Task<bool> UpdateMerchantUser(MerchantUser user)
        {
            var updateResult = await _repository.UpdateMerchantUser(user);
            return updateResult;
        }
    }
}
