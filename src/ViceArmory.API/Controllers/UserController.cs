using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Middleware.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Logs;
using ViceArmory.RequestObject.Account;

namespace ViceArmory.API.Controllers
{
    /// <summary>
    ///  User controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Members
        private IOptions<ProjectSettings> _options;
        private readonly IUserRepository _service;
        private readonly ILogger<UserController> _logger;
        private readonly ILogContext _logs;

        #endregion

        #region Constructor

        /// <summary>
        ///  User Controller Constructor
        /// </summary>
        /// <param name="service">Inject IUserService</param>
        /// <param name="logger">Inject logger</param>
        public UserController(IUserRepository service, ILogger<UserController> logger, IOptions<ProjectSettings> options, ILogContext logs)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _options = options;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Users
        /// </summary>
        /// <param></param>
        /// <returns>Return User</returns>
        [Route("[action]", Name = "GetUsers")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetUsers()
        {
            var res = await _service.GetUsers();

           if(res == null)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "User",
                    Description = "GetUsers - not Successfull",
                    HostName = Utility.Functions.GetIpAddress().HostName,
                    IpAddress = Utility.Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
            }
            else
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "User",
                    Description = "GetUsers -  Successfull",
                    HostName = Utility.Functions.GetIpAddress().HostName,
                    IpAddress = Utility.Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
            }
            return Ok(res);
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Return User details</returns>
        [HttpGet("[action]", Name = "GetUserById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UserResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserResponseDTO>> GetUserById(string userId)
        {
            var res = await _service.GetUserById(userId);

            if (res == null)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "User",
                    Description = "GetUserById - not Successfull",
                    HostName = Utility.Functions.GetIpAddress().HostName,
                    IpAddress = Utility.Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                _logger.LogError($"User with id: {userId}, not found.");
                return NotFound();
            }
            else
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "User",
                    Description = "GetUserById - Successfull",
                    HostName = Utility.Functions.GetIpAddress().HostName,
                    IpAddress = Utility.Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
            }
            return Ok(res);
        }

        /// <summary>
        /// Create User 
        /// </summary>
        /// <param name="user">User Object</param>
        /// <returns>Return User</returns>
        [HttpPost("[action]", Name = "CreateUser")]
        [ProducesResponseType(typeof(UserRequestDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> CreateUser([FromBody] RequestUserRequestDTO User)
        {
            try
            {
                var users = new UserResponseDTO()
                {
                    Id = User.Id,
                    Username = User.Username,
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    MiddleName = User.MiddleName,
                    Mobile = User.Mobile,
                    Email = User.Email,
                    IsAdmin = User.IsAdmin,
                    IsVendor = User.IsVendor,
                    IsEmailConfirm = User.IsEmailConfirm,
                    IsUser = User.IsUser,
                    RegisterAt = User.RegisterAt,
                    LastLogin = User.LastLogin,
                    Intro = User.Intro,
                    Profile = User.Profile,
                    Password = User.Password,
                    CreatedDate = User.CreatedDate,
                    CreatedBy = User.CreatedBy,
                    UpdatedDate = User.UpdatedDate,
                    UpdatedBy = User.UpdatedBy
                };

                var result = await _service.CreateUser(users);

                if (result == Constants.CREATEUSERSUCCESS)
                {
                    string body = "";
                    string to = "";
                    string link = _options.Value.ProjectUrl + "Register/VerifyEmail?Email=" + users.Email + "&Id=" + users.Id;
                    string FilePath = "D:/Project/ViceArmory/ViceArmory/src/ViceArmory.API/wwwroot/Templates/welcomepage.html";
                    StreamReader str = new StreamReader(FilePath);
                    string MailText = str.ReadToEnd();
                    MailText = MailText.Replace("UserName", User.FirstName).Replace("link_verification", link).Replace("urlPathFrontEnd", _options.Value.urlPathFrontEnd);
                    body = MailText;
                    body = MailText;
                    string subject = "";
                    subject = "Vice Armory Registration Confirmation";
                    if (User.Email != "")
                    {
                        to = User.Email;
                    }
                    var mail = await _service.SendEmail(_options.Value.smtpAddress, _options.Value.portNumber, _options.Value.userName, _options.Value.passWord, User.Email, _options.Value.from, _options.Value.fromName, subject, body);
                    if (mail == Constants.CREATEUSERMAILSENT)
                    {
                        var logs = new LogResponseDTO()
                        {
                            PageName = "User",
                            Description = "CreateUser CREATEUSERMAILSENT- Successfull" + User.Email,
                            HostName = Utility.Functions.GetIpAddress().HostName,
                            IpAddress = Utility.Functions.GetIpAddress().Ip,
                            created_by = _options.Value.UserNameForLog,
                            Created_date = DateTime.Now
                        };
                        await _logs.AddLogs.InsertOneAsync(logs);
                        return Created(string.Empty, Constants.CREATEUSERMAILSENT);
                    }
                    else
                    {
                        var logs = new LogResponseDTO()
                        {
                            PageName = "User",
                            Description = "CreateUser CREATEUSERERROR - Successfull" + User.Email,
                            HostName = Utility.Functions.GetIpAddress().HostName,
                            IpAddress = Utility.Functions.GetIpAddress().Ip,
                            created_by = _options.Value.UserNameForLog,
                            Created_date = DateTime.Now
                        };
                        await _logs.AddLogs.InsertOneAsync(logs);
                        return Constants.CREATEUSERERROR;
                    }
                }
                else if (result == Constants.CREATEUSEREXIST)
                {
                    var logs = new LogResponseDTO()
                    {
                        PageName = "User",
                        Description = "CreateUser CREATEUSEREXIST - Successfull" + User.Email,
                        HostName = Utility.Functions.GetIpAddress().HostName,
                        IpAddress = Utility.Functions.GetIpAddress().Ip,
                        created_by = _options.Value.UserNameForLog,
                        Created_date = DateTime.Now
                    };
                    await _logs.AddLogs.InsertOneAsync(logs);
                    return Constants.CREATEUSEREXIST;
                }
                else
                {
                    var logs = new LogResponseDTO()
                    {
                        PageName = "User",
                        Description = "CreateUser CREATEUSERERROR - Successfull" + User.Email,
                        HostName = Utility.Functions.GetIpAddress().HostName,
                        IpAddress = Utility.Functions.GetIpAddress().Ip,
                        created_by = _options.Value.UserNameForLog,
                        Created_date = DateTime.Now
                    };
                    await _logs.AddLogs.InsertOneAsync(logs);
                    return Constants.CREATEUSERERROR;
                }

            }
            catch (Exception ex)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "User",
                    Description = "CreateUser CREATEUSERERROR - Successfull ex" + User.Email,
                    HostName = Utility.Functions.GetIpAddress().HostName,
                    IpAddress = Utility.Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                return Constants.CREATEUSERERROR;
            }
        }

        /// <summary>
        /// Update User 
        /// </summary>
        /// <param name="user">User Object</param>
        /// <returns></returns>
        [HttpPut("[action]", Name = "UpdateUser")]
        [ProducesResponseType(typeof(UserResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser([FromBody] UserResponseDTO user)
        {
            var logs = new LogResponseDTO()
            {
                PageName = "User",
                Description = "UpdateUser - Successfull ex" + user.Email,
                HostName = Utility.Functions.GetIpAddress().HostName,
                IpAddress = Utility.Functions.GetIpAddress().Ip,
                created_by = _options.Value.UserNameForLog,
                Created_date = DateTime.Now
            };
            await _logs.AddLogs.InsertOneAsync(logs);
            return Ok(await _service.UpdateUser(user));
        }

        /// <summary>
        /// Delete User 
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}", Name = "DeleteUser")]
        [ProducesResponseType(typeof(UserResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserById(string id)
        {
            var logs = new LogResponseDTO()
            {
                PageName = "User",
                Description = "DeleteUserById - Successfull id" + id,
                HostName = Utility.Functions.GetIpAddress().HostName,
                IpAddress = Utility.Functions.GetIpAddress().Ip,
                created_by = _options.Value.UserNameForLog,
                Created_date = DateTime.Now
            };
            await _logs.AddLogs.InsertOneAsync(logs);
            return Ok(await _service.DeleteUser(id));
        }

        /// <summary>
        /// Verify Email
        /// </summary>
        /// <param name="id">User id</param>
        [HttpPost("[action]", Name = "VerifyUser")]
        [ProducesResponseType(typeof(UserResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> VerifyUser([FromBody] UserResponseDTO User)
        {
            var logs = new LogResponseDTO()
            {
                PageName = "User",
                Description = "VerifyUser - Successfull id" + User.Email,
                HostName = Utility.Functions.GetIpAddress().HostName,
                IpAddress = Utility.Functions.GetIpAddress().Ip,
                created_by = _options.Value.UserNameForLog,
                Created_date = DateTime.Now
            };
            await _logs.AddLogs.InsertOneAsync(logs);
            return Ok(await _service.VerifyEmail(User.Email,User.Id));
        }

        #endregion 
    }
}
