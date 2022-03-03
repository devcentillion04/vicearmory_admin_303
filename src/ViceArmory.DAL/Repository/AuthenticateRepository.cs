using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.RequestObject.Authenticate;
using ViceArmory.DTO.RequestObject.User;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.DTO.ResponseObject.User;

namespace ViceArmory.DAL.Repository
{
    /// <summary>
    /// Implement IAuthenticateRepository interface
    /// </summary>
    public class AuthenticateRepository : IAuthenticateRepository
    {
        //Password encryptionKey Key
        private static object encryptionKey = "VICEARMORYSECRETKEY";
        private readonly AppSettings _appSettings;
        private readonly ILogger<AuthenticateRepository> _logger;

        private readonly IAuthenticateContext _context;
        /// <summary>
        /// Initiate class
        /// </summary>
        /// <param name="context"></param>
        public AuthenticateRepository(IOptions<AppSettings> appSettings, IAuthenticateContext context, ILogger<AuthenticateRepository> logger)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _logger = logger;
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
        public async Task<UserResponseDTO> GetUsers(AuthenticateRequest req)
        {
            //For user login
            //return await _context
            //              .Users
            //              .Find(p => (p.Email == req.Username || p.Username == req.Username) && p.Password == ToEncrypt(req.Password))
            //              .FirstOrDefaultAsync();

            //For adminLogin
            return await _context
                        .Users
                        .Find(p => (p.Email == req.Username || p.Username == req.Username) && p.Password == ToEncrypt(req.Password) && p.IsAdmin == true)
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


        #region Methods

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="req">AuthenticateRequest object</param>
        /// <returns>Return AuthenticateResponse</returns>
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest req)
        {
           
            var user = await GetUsers(req);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);
            var userLogin = new UserLogin()
            {
                _id = user.Id,
                UserName = user.Username,
                TokenId = token,
                IpAddress = req.IPAddress,
                UserLoggedinStartTime = DateTime.Now,
                UserLoggedinEndTime = DateTime.Now.AddMinutes(30),
                EmailId = user.Email,
                Password = user.Password
            };
            var loginUserExist = await this.GetLoginDetails(user.Username);
            if (loginUserExist != null)
            {
                await this.UpdateUserLogin(userLogin);
            }
            else
            {
                await this.CreateUserLogin(userLogin);
            }
            
            return new AuthenticateResponse()
            {
                Id = userLogin._id,
                IpAddress = userLogin.IpAddress,
                TokenId = userLogin.TokenId,
                UserLoggedinEndTime = userLogin.UserLoggedinEndTime,
                UserLoggedinStartTime = userLogin.UserLoggedinStartTime,
                UserName = userLogin.UserName,
                EmailId = userLogin.EmailId
            };
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Generate JWT tokens
        /// </summary>
        /// <param name="user">UserLogin object</param>
        /// <returns>Return Token</returns>
        private string GenerateJwtToken(UserResponseDTO user)
        {
            // generate token that is valid for 7 days

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Username), new Claim(ClaimTypes.NameIdentifier, user.Id) }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while Generating Token :" + ex.ToString());
            }
            return string.Empty;
        }
        #endregion
    }
}
