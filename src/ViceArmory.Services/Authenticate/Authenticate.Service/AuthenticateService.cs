using Authenticate.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Authenticate.DataContract;
using System.Threading.Tasks;
using Authenticate.Repository.Interfaces;
using Account.DataContract.Entities;

namespace Authenticate.Services
{
    /// <summary>
    /// Implement IAuthenticateService interface
    /// </summary>
    public class AuthenticateService : IAuthenticateService
    {
        #region Members

        private readonly AppSettings _appSettings;
        private readonly IAuthenticateRepository _iAuthenticateRepository;
        private readonly ILogger<AuthenticateService> _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Initiate class
        /// </summary>
        /// <param name="appSettings">app setting object</param>
        /// <param name="iAuthenticateRepository">Inject iAuthenticateRepository</param>
        /// <param name="logger">Inject logger</param>
        public AuthenticateService(IOptions<AppSettings> appSettings, IAuthenticateRepository iAuthenticateRepository, ILogger<AuthenticateService> logger)
        {
            _appSettings = appSettings.Value;
            _iAuthenticateRepository = iAuthenticateRepository;
            _logger = logger;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="req">AuthenticateRequest object</param>
        /// <returns>Return AuthenticateResponse</returns>
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest req)
        {
            var user = await _iAuthenticateRepository.GetUsers(req);

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
                UserLoggedinEndTime = DateTime.Now.AddMinutes(30)
            };
            var loginUserExist = await this._iAuthenticateRepository.GetLoginDetails(user.Username);
            if (loginUserExist != null)
            {
                await this._iAuthenticateRepository.UpdateUserLogin(userLogin);
            }
            else
            {
                await this._iAuthenticateRepository.CreateUserLogin(userLogin);
            }
            return new AuthenticateResponse(userLogin);
        }
        /// <summary>
        /// Method to get Login user details
        /// </summary>
        /// <param name="req">Authentication request object</param>
        /// <returns>Return user login details</returns>
        public async Task<UserLogin> GetLoginDetails(string userName)
        {
            return await _iAuthenticateRepository.GetLoginDetails(userName);
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Generate JWT tokens
        /// </summary>
        /// <param name="user">UserLogin object</param>
        /// <returns>Return Token</returns>
        private string GenerateJwtToken(User user)
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