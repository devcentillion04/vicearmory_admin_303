using Middleware.Infrastructure;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.Account;

namespace ViceArmory.DAL.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IAccountContext _context;

        //Password encryptionKey Key
        private static object encryptionKey = "VICEARMORYSECRETKEY";

        public UserRepository(IAccountContext context)
        {
            _context = context;
        }

        public async Task<string> CreateUser(UserResponseDTO User)
        {
            try
            {
                var res = await _context
                           .Users
                           .Find(p => p.Email == User.Email)
                           .ToListAsync();
                if (res.Count >= 1)
                {
                    return Constants.CREATEUSEREXIST;
                }
                User.IsUser = User.IsUser;
                User.IsEmailConfirm = User.IsEmailConfirm;
                User.CreatedDate = DateTime.UtcNow;
                User.UpdatedDate = DateTime.UtcNow;
                User.Password = ToEncrypt(User.Password);
                await _context.Users.InsertOneAsync(User);


                return Constants.CREATEUSERSUCCESS;
            }
            catch (Exception ex)
            {
                return Constants.CREATEUSERERROR;
                throw;
            }
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
            catch
            {
                return (string.Empty);
            }
        }

        public async Task<bool> DeleteUser(string id)
        {
            FilterDefinition<UserResponseDTO> filter = Builders<UserResponseDTO>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context
                                               .Users
                                               .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<UserResponseDTO> GetUserById(string id)
        {
            return await _context
                          .Users
                          .Find(p => p.Id == id)
                          .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<UserResponseDTO>> GetUsers()
        {
            return await _context
                            .Users
                            .Find(p => true)
                            .ToListAsync();
        }
        public async Task<IEnumerable<UserResponseDTO>> GetAdmin()
        {
            return await _context
                            .Users
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<bool> UpdateUser(UserResponseDTO user)
        {
            user.UpdatedDate = DateTime.UtcNow;
            var updateResult = await _context
                                        .Users
                                        .ReplaceOneAsync(filter: g => g.Id == user.Id, replacement: user);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<string> SendEmail(string smtpAddress, int portNumber, string userName, string password, string to, string from, string fromName, string subject, string body)
        {
            string msg = "";
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = smtpAddress;
                smtp.Credentials = new System.Net.NetworkCredential(from, password);
                smtp.Port = portNumber;
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return Constants.CREATEUSERMAILSENT;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return Constants.CREATEUSERERROR;
            }
        }
        public async Task<string> VerifyEmail(string Email, string Id)
        {
            try
            {
                var result = await _context
                    .Users
                    .Find(p => p.Email == Email && p.Id == Id)
                    .FirstOrDefaultAsync();
                if (result != null)
                {
                    //if(result.UpdatedDate.AddHours(24)<DateTime.Now)
                    //{
                    //    return Constants.EMAILEXPIRED;
                    //}
                    result.UpdatedDate = DateTime.UtcNow;
                    result.IsEmailConfirm = true;
                    var updateResult = await _context
                                                .Users
                                                .ReplaceOneAsync(filter: g => g.Id == result.Id, replacement: result);

                    if (updateResult.IsAcknowledged)
                    {

                    }
                    return Constants.EMAILVERIFIED;
                }
                return Constants.EMAILNOTVERIFIED;
            }
            catch (Exception ex)
            {
                return Constants.EMAILNOTVERIFIED;
                throw;
            }
        }
    }
}
