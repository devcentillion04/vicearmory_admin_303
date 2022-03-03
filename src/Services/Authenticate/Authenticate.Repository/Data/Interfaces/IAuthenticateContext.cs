using Account.DataContract.Entities;
using Authenticate.DataContract;
using MongoDB.Driver;

namespace Authenticate.Repository.Data.Interfaces
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
