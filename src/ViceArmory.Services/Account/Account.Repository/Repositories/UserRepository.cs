using Account.DataContract.Entities;
using Account.Repository.Data.Interfaces;
using Account.Repository.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Account.Repository.Repositories
{

    

    public class UserRepository : IUserRepository
    {
        private readonly IAccountContext _context;

        //Password encryptionKey Key
        private static object encryptionKey = "PRINTFULSECRETKEY";

        public UserRepository(IAccountContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User User)
        {
            User.CreatedDate = DateTime.UtcNow;
            User.Password = ToEncrypt(User.Password);
           //requestUser.Password = pwd;
            await _context.Users.InsertOneAsync(User);
        }

        ///// <summary>
        ///// Encrypt the Password.
        ///// </summary>
        ///// <param name="Password">string Password</param>
        ///// <returns>encoded string</returns>
        private static string ToEncrypt(string Password)
        {
            byte[] key = { };
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray;
            try
            {
                key = Encoding.UTF8.GetBytes(encryptionKey.ToString().Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(Password);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch //(System.Exception ex)
            {
                return (string.Empty);
            }
        }

        public async Task<bool> DeleteUser(string id)
        {
            FilterDefinition<User> filter = Builders<User>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context
                                               .Users
                                               .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<User> GetUserById(string id)
        {
            return await _context
                          .Users
                          .Find(p => p.Id == id)
                          .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context
                            .Users
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<bool> UpdateUser(User user)
        {
            user.UpdatedDate = DateTime.UtcNow;
            var updateResult = await _context
                                        .Users
                                        .ReplaceOneAsync(filter: g => g.Id == user.Id, replacement: user);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
