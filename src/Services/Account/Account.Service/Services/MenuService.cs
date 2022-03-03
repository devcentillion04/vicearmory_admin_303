using Account.DataContract.Entities;
using Account.Repository.Repositories.Interfaces;
using Account.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repository;
        public MenuService(IMenuRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task CreateMenu(Menu menu)
        {
            await _repository.CreateMenu(menu);
        }

        public async Task<bool> DeleteMenu(string id)
        {
            var result = await _repository.DeleteMenu(id);
            return result;
        }

        public async Task<Menu> GetMenuById(string id)
        {
            return await _repository.GetMenuById(id);
        }

        public async Task<IEnumerable<Menu>> GetMenu()
        {
            return await _repository.GetMenu();
        }

        public async Task<bool> UpdateMenu(Menu menu)
        {
            var updateResult = await _repository.UpdateMenu(menu);
            return updateResult;
        }
    }
}
