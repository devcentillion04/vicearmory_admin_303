using Account.DataContract.Entities;
using Account.Repository.Data.Interfaces;
using Account.Repository.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Repository.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly IAccountContext _context;
        public ModuleRepository(IAccountContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateModule(Module module)
        {
            module.CreatedDate = DateTime.UtcNow;
            await _context.Modules.InsertOneAsync(module);
        }

        public async Task<bool> DeleteModule(string id)
        {
            FilterDefinition<Module> filter = Builders<Module>.Filter.Eq(p => p.Id, id);
            var update = Builders<Module>.Update.Set(nameof(Module.IsDeleted), true);
            var deleteResult = await _context.Modules.UpdateOneAsync(filter, update);

            return deleteResult.IsAcknowledged
                && deleteResult.ModifiedCount > 0;
        }

        public async Task<Module> GetModuleById(string id)
        {
            return await _context
                          .Modules
                          .Find(p => p.Id == id)
                          .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Module>> GetModules()
        {
            var builder = Builders<Module>.Filter;
            FilterDefinition<Module> filter = FilterDefinition<Module>.Empty;
            filter = filter & builder.Eq(p => p.IsDeleted, false);
            return (await _context
                            .Modules
                            .FindAsync(filter))
                            .ToList();
        }

        public async Task<bool> UpdateModule(Module module)
        {
            module.UpdatedDate = DateTime.UtcNow;
            var updateResult = await _context
                                        .Modules
                                        .ReplaceOneAsync(filter: g => g.Id == module.Id, replacement: module);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
