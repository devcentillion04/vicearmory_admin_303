using MongoDB.Driver;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.DTO.ResponseObject.Newsletter;
using ViceArmory.DTO.ResponseObject.WeeklyAds;

namespace ViceArmory.DAL.Interface
{
    public interface IAccountContext
    {
        IMongoCollection<UserResponseDTO> Users { get; }
        IMongoCollection<MerchantUserResponseDTO> MerchantUsers { get; }
        IMongoCollection<RoleResponseDTO> Roles { get; }
        IMongoCollection<MenuResponseDTO> Menu { get; }
        IMongoCollection<WeeklyAdsResponseDTO> WeeklyAds { get; }
        IMongoCollection<ModuleResponseDTO> Modules { get; }
        IMongoCollection<UserAccessResponseDTO> UserAccess { get; }
        IMongoCollection<UserRoleResponseDTO> UserRoles { get; }
        IMongoCollection<NewsletterResponseDTO> Newsletter { get; }
    }
}
