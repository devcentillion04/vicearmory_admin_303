using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.RequestObject.ApiConfiguration;
using ViceArmory.DTO.RequestObject.Authenticate;
using ViceArmory.DTO.RequestObject.OTP;
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
        #region APIs
        [AllowAnonymous]
        [HttpPost("[action]", Name = "UserAuthenticate")]
        public async Task<ActionResult<AuthenticateResponse>> UserAuthenticate([FromBody] AuthenticateRequest req)
        {
            var response = await _authenticateRepository.AuthenticateUser(req);
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
        #endregion

        [AllowAnonymous]
        [HttpGet("[action]", Name = "GetAccessToken")]
        public async Task<ActionResult<ApiConfigToken>> GetAccessToken()
        {
            var accessTokenExpireAt = Convert.ToDateTime(Constants.accessTokenExpireAt);
            ApiConfigToken _apiConfigToken = new ApiConfigToken();
            if (accessTokenExpireAt < DateTime.Now)
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

        [AllowAnonymous]
        [HttpGet("[action]", Name = "VerifyOTP")]
        public async Task<string> VerifyOTP()
        {
            try
            {
                Random rnd = new Random();
                int otp = rnd.Next(1000, 9999);
                _options.Value.OTPValue = otp.ToString();
                MailMessage mail = new MailMessage();
                mail.To.Add(_options.Value.UserNameForLog);
                mail.From = new MailAddress("Vicearmory.2022@gmail.com");
                mail.Subject = "OTP Verification - Vice Armory";
                mail.Body = "your otp from vicearmory is " + otp;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new System.Net.NetworkCredential("Vicearmory.2022@gmail.com", "Vice@_!2022_Armory");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(mail);
                var logs = new LogResponseDTO()
                {
                    PageName = "Index",
                    Description = "Authenticate - Successfull -mail send - " + _options.Value.UserNameForLog,
                    HostName = Utility.Functions.GetIpAddress().HostName,
                    IpAddress = Utility.Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                //await _logs.AddLogs.InsertOneAsync(logs);
                return "Success";
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]", Name = "OtpVerification")]
        public async Task<IActionResult> OtpVerification([FromBody] OTPRequestDTO req)
        {
            var data = req.OTPText;
            var OPTData = _options.Value.OTPValue;
            if (data == Convert.ToInt32(OPTData))
            {
            return Ok("Verify"); 
            }
            else
            {
                return BadRequest("NotVerify");
            }
        }
    }
}
