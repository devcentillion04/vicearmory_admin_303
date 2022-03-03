using Account.DataContract.Entities;
using Authenticate.DataContract;
using MongoDB.Driver;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;

namespace ViceArmory.DAL.Repository
{
    /// <summary>
    /// Implement IAuthenticateRepository interface
    /// </summary>
    public class AuthenticateRepository : IAuthenticateRepository
    {
        //Password encryptionKey Key
        private static object encryptionKey = "PRINTFULSECRETKEY";

        private readonly IAuthenticateContext _context;
        /// <summary>
        /// Initiate class
        /// </summary>
        /// <param name="context"></param>
        public AuthenticateRepository(IAuthenticateContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create UserLogin 
        /// </summary>
        /// <param name="req">UserLogin Object</param>
        /// <returns></returns>
        public async Task CreateUserLogin(UserLogin req)
        {
            await _context.UserLogins.InsertOneAsync(req);
        }

        /// <summary>
        /// Get User details
        /// </summary>
        /// <param name="req">User Credential object</param>
        /// <returns>Return User details</returns>
        public async Task<UserLogin> GetLoginDetails(string userName)
        {
            return await _context
                          .UserLogins
                          .Find(p => p.UserName == userName)
                          .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get User 
        /// </summary>
        /// <param name="req">User request object</param>
        /// <returns>Return User</returns>
        public async Task<User> GetUsers(AuthenticateRequest req)
        {
            return await _context
                          .Users
                          .Find(p => p.Username == req.Username && p.Password == ToEncrypt(req.Password))
                          .FirstOrDefaultAsync();
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

        /// <summary>
        /// Update User Login Details
        /// </summary>
        /// <param name="req">UserLogin object</param>
        /// <returns></returns>

        public async Task<bool> UpdateUserLogin(UserLogin req)
        {
            var updateResult = await _context
                                       .UserLogins
                                       .ReplaceOneAsync(filter: g => g._id == req._id, replacement: req);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
    }
}
