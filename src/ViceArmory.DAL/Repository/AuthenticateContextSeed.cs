using MongoDB.Driver;
using System;
using ViceArmory.DTO.RequestObject.User;

namespace ViceArmory.DAL.Repository
{
    /// <summary>
    /// Insert dummy data into database if data unavailable
    /// </summary>
    public class AuthenticateContextSeed
    {
        /// <summary>
        /// Insert Dummy data into the database if unavialable
        /// </summary>
        /// <param name="UserCollection">UserLogin collection object</param>
        public static void SeedData(IMongoCollection<UserLogin> UserCollection)
        {

            bool existUser = UserCollection.Find(p => true).Any();
            if (!existUser)
            {
                UserCollection.InsertOneAsync(GetUserLogin());
            }
        }

        private static UserLogin GetUserLogin()
        {
            return new UserLogin()
            {
                _id= "602d2149e773f2a3990b047f",
                UserName = "User0@email.com",
                UserLoggedinStartTime = DateTime.Now,
                UserLoggedinEndTime = DateTime.Now.AddMinutes(30),
                IpAddress = "172.1.1.1"
            };
        }
    }
}
