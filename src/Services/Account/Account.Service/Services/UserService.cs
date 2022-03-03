using Account.DataContract.Entities;
using Account.Repository.Repositories.Interfaces;
using Account.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task CreateUser(User User)
        {
            await _repository.CreateUser(User);
        }

        public async Task<bool> DeleteUser(string id)
        {
            var result = await _repository.DeleteUser(id);
            return result;
        }

        public async Task<User> GetUserById(string id)
        {
            return await _repository.GetUserById(id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _repository.GetUsers();
        }

        public async Task<bool> UpdateUser(User user)
        {
            var updateResult = await _repository.UpdateUser(user);
            return updateResult;
        }
    }
}
