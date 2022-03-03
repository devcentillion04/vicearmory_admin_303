using Account.DataContract.Entities;
using Account.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Account.API.Controllers
{
    /// <summary>
    ///  User controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Members

        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;

        #endregion

        #region Constructor

        /// <summary>
        ///  User Controller Constructor
        /// </summary>
        /// <param name="service">Inject IUserService</param>
        /// <param name="logger">Inject logger</param>
        public UserController(IUserService service, ILogger<UserController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
        [ProducesResponseType(typeof(IEnumerable<User>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var res = await _service.GetUsers();
            return Ok(res);
        }

        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Return User details</returns>
        [HttpGet("[action]", Name = "GetUserById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> GetUserById(string userId)
        {
            var res = await _service.GetUserById(userId);

            if (res == null)
            {
                _logger.LogError($"User with id: {userId}, not found.");
                return NotFound();
            }

            return Ok(res);
        }

        /// <summary>
        /// Create User 
        /// </summary>
        /// <param name="user">User Object</param>
        /// <returns>Return User</returns>
        [HttpPost("[action]", Name = "CreateUser")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<User>> CreateUser([FromBody] RequestUser User)
        {
            var users = new User()
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

            await _service.CreateUser(users);

            return Created(string.Empty, users);
        }

        /// <summary>
        /// Update User 
        /// </summary>
        /// <param name="user">User Object</param>
        /// <returns></returns>
        [HttpPut("[action]", Name = "UpdateUser")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            return Ok(await _service.UpdateUser(user));
        }

        /// <summary>
        /// Delete User 
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}", Name = "DeleteUser")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserById(string id)
        {
            return Ok(await _service.DeleteUser(id));
        }

        #endregion
    }
}