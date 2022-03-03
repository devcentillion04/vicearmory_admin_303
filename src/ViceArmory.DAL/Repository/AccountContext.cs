using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Newsletter;
using ViceArmory.DTO.ResponseObject.WeeklyAds;

namespace ViceArmory.DAL.Repository
{
    public class AccountContext : IAccountContext
    {
        public AccountContext(IOptions<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
            Users = database.GetCollection<UserResponseDTO>("Users");
            MerchantUsers = database.GetCollection<MerchantUserResponseDTO>("MerchantUsers");
            Roles = database.GetCollection<RoleResponseDTO>("Roles");
            Menu = database.GetCollection<MenuResponseDTO>("Menu");
            Newsletter = database.GetCollection<NewsletterResponseDTO>("Newsletter");
            Modules = database.GetCollection<ModuleResponseDTO>("Modules");
            UserAccess = database.GetCollection<UserAccessResponseDTO>("UserAccess");
            UserRoles = database.GetCollection<UserRoleResponseDTO>("UserRoles");
            WeeklyAds = database.GetCollection<WeeklyAdsResponseDTO>("WeeklyAds");
        }

        public IMongoCollection<UserResponseDTO> Users { get; }
        public IMongoCollection<MerchantUserResponseDTO> MerchantUsers { get; }
        public IMongoCollection<RoleResponseDTO> Roles { get; }
        public IMongoCollection<MenuResponseDTO> Menu { get; }
        public IMongoCollection<NewsletterResponseDTO> Newsletter { get; }
        public IMongoCollection<ModuleResponseDTO> Modules { get; }
        public IMongoCollection<UserAccessResponseDTO> UserAccess { get; }
        public IMongoCollection<UserRoleResponseDTO> UserRoles { get; }
        public IMongoCollection<WeeklyAdsResponseDTO> WeeklyAds { get; }
    }
}
