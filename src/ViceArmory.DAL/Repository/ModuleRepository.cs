using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.Account;

namespace ViceArmory.DAL.Repository
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly IAccountContext _context;
        public ModuleRepository(IAccountContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateModule(ModuleResponseDTO module)
        {
            module.CreatedDate = DateTime.UtcNow;
            await _context.Modules.InsertOneAsync(module);
        }

        public async Task<bool> DeleteModule(string id)
        {
            FilterDefinition<ModuleResponseDTO> filter = Builders<ModuleResponseDTO>.Filter.Eq(p => p.Id, id);
            var update = Builders<ModuleResponseDTO>.Update.Set(nameof(ModuleResponseDTO.IsDeleted), true);
            var deleteResult = await _context.Modules.UpdateOneAsync(filter, update);

            return deleteResult.IsAcknowledged
                && deleteResult.ModifiedCount > 0;
        }

        public async Task<ModuleResponseDTO> GetModuleById(string id)
        {
            return await _context
                          .Modules
                          .Find(p => p.Id == id)
                          .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ModuleResponseDTO>> GetModules()
        {
            var builder = Builders<ModuleResponseDTO>.Filter;
            FilterDefinition<ModuleResponseDTO> filter = FilterDefinition<ModuleResponseDTO>.Empty;
            filter = filter & builder.Eq(p => p.IsDeleted, false);
            return (await _context
                            .Modules
                            .FindAsync(filter))
                            .ToList();
        }

        public async Task<bool> UpdateModule(ModuleResponseDTO module)
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
