using Account.DataContract.Entities;
using Authenticate.DataContract;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViceArmory.DAL.Interface;

namespace Authenticate.Data.Repository
{
    /// <summary>
    /// Authentication context class
    /// </summary>
    public class AuthenticateContext : IAuthenticateContext
    {
        /// <summary>
        /// Initiate AuthenticateContext to get connection string
        /// </summary>
        /// <param name="configuration">Object of configuration</param>
        public AuthenticateContext(IOptions<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
            UserLogins = database.GetCollection<UserLogin>("UserLogins");
            Users = database.GetCollection<User>("Users");
            //AuthenticateContextSeed.SeedData(UserLogins);
        }

        public IMongoCollection<UserLogin> UserLogins { get; }
        public IMongoCollection<User> Users { get; }
    }
}
