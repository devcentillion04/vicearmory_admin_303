using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.RequestObject.ApiConfiguration;
using ViceArmory.DTO.RequestObject.Authenticate;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.DTO.ResponseObject.Logs;
using ViceArmory.Utility;

namespace ViceArmory.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        #region Members
        private IOptions<ProjectSettings> _options;
        private IAuthenticateRepository _authenticateRepository;
        private IApiConfigurationService _iApiConfigurationService;
        private readonly ILogger<AuthenticateController> _logger;
        private readonly ILogContext _logs;
        #endregion

        #region Construnction

        public AuthenticateController(ILogger<AuthenticateController> logger, IAuthenticateRepository authenticateRepository, IApiConfigurationService iApiConfigurationService, IOptions<ProjectSettings> options, ILogContext logs)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authenticateRepository = authenticateRepository ?? throw new ArgumentNullException(nameof(authenticateRepository));
            _iApiConfigurationService = iApiConfigurationService;
            _options = options;
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
        }
        #endregion

        #region APIs
        [AllowAnonymous]
        [HttpPost("[action]", Name = "Authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Authenticate([FromBody] AuthenticateRequest req)
        {
            var response = await _authenticateRepository.Authenticate(req);
            HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(req));
            _options.Value.UserNameForLog = req.Username;
            if (response == null)
            {
                var logs = new LogResponseDTO()
                {

                    PageName = "Authenticate",
                    Description = "Authenticate - Not Successfull - Id : " + req.UserId,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                return BadRequest(new { message = "Username or password not autheticated" });
            }
            else
            {
                var logs = new LogResponseDTO()
                {

                    PageName = "Authenticate",
                    Description = "Authenticate - Not Successfull - - Id : " + req.UserId,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                return Ok(response);
            }
        }

        [AllowAnonymous]
        [HttpGet("[action]", Name = "GetAccessToken")]
        public async Task<ActionResult<ApiConfigToken>> GetAccessToken()
        {
            var accessTokenExpireAt = Convert.ToDateTime(Constants.accessTokenExpireAt);
            ApiConfigToken _apiConfigToken = new ApiConfigToken();
            if (accessTokenExpireAt <DateTime.Now)
            {
                _apiConfigToken = await _iApiConfigurationService.GetAccessToken();
                Constants.accessTokenSessionKey = _apiConfigToken.access_token;
                Constants.accessTokenExpireAt = DateTime.Now.AddSeconds(_apiConfigToken.ExpiresIn).ToString();
            }
            else
            {
                _apiConfigToken.access_token = Constants.accessTokenSessionKey;
            }
            if (String.IsNullOrEmpty(_apiConfigToken.access_token))
            {

                var logs = new LogResponseDTO()
                {

                    PageName = "Authenticate",
                    Description = "Authenticate - Not Successfull - access_token : " + _apiConfigToken.access_token,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                return BadRequest(new { message = "Username or password not autheticated" });
            }
            else
            {
                var logs = new LogResponseDTO()
                {

                    PageName = "Authenticate",
                    Description = "Authenticate - Not Successfull - access_token : " + _apiConfigToken.access_token,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                return Ok(_apiConfigToken);
            }
        }
        #endregion
    }
}
