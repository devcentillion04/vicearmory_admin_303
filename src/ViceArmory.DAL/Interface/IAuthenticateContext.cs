using MongoDB.Driver;
using ViceArmory.DTO.RequestObject.User;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.DTO.ResponseObject.User;

namespace ViceArmory.DAL.Interface
{
    /// <summary>
    /// IAuthenticateContext inserface
    /// </summary>
    public interface IAuthenticateContext
    {
        /// <summary>
        /// Define UserLogin property
        /// </summary>
        IMongoCollection<UserLogin> UserLogins { get; }
        IMongoCollection<UserResponseDTO> Users { get; }
        
    }
}
