using Account.DataContract.Entities;
using Account.Repository.Data.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Account.Repository.Data
{
    public class AccountContext : IAccountContext
    {
        public AccountContext(IOptions<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
            Users = database.GetCollection<User>("Users");
            MerchantUsers = database.GetCollection<MerchantUser>("MerchantUsers");
            Roles = database.GetCollection<Role>("Roles");
            Menu = database.GetCollection<Menu>("Menu");
            Modules = database.GetCollection<Module>("Modules");
            UserAccess = database.GetCollection<UserAccess>("UserAccess");
            UserRoles = database.GetCollection<UserRole>("UserRoles");
            AuditLogs = database.GetCollection<AuditLog>("AuditLogs");
        }

        public IMongoCollection<User> Users { get; }
        public IMongoCollection<MerchantUser> MerchantUsers { get; }
        public IMongoCollection<Role> Roles { get; }
        public IMongoCollection<Menu> Menu { get; }
        public IMongoCollection<Module> Modules { get; }
        public IMongoCollection<UserAccess> UserAccess { get; }
        public IMongoCollection<UserRole> UserRoles { get; }
        public IMongoCollection<AuditLog> AuditLogs { get; }
    }
}
