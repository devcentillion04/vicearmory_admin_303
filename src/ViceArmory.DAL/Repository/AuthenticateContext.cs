using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.RequestObject.User;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.User;

namespace ViceArmory.DAL.Repository
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
            Users = database.GetCollection<UserResponseDTO>("Users");
            //AuthenticateContextSeed.SeedData(UserLogins);
        }

        public IMongoCollection<UserLogin> UserLogins { get; }
        public IMongoCollection<UserResponseDTO> Users { get; }
    }
}
