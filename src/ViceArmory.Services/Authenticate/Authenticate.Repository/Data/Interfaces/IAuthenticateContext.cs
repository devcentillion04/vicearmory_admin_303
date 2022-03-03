using Account.DataContract.Entities;
using Authenticate.DataContract;
using MongoDB.Driver;

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
        IMongoCollection<User> Users { get; }
        
    }
}
