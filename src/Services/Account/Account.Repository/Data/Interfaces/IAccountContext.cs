using Account.DataContract.Entities;
using MongoDB.Driver;

namespace Account.Repository.Data.Interfaces
{
    public interface IAccountContext
    {
        IMongoCollection<User> Users { get; }
        IMongoCollection<MerchantUser> MerchantUsers { get; }
        IMongoCollection<Role> Roles { get; }
        IMongoCollection<Menu> Menu { get; }
        IMongoCollection<Module> Modules { get; }
        IMongoCollection<UserAccess> UserAccess { get; }
        IMongoCollection<UserRole> UserRoles { get; }
        IMongoCollection<AuditLog> AuditLogs { get; }
    }
}
