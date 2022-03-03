using Account.DataContract.Entities;
using Account.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Account.API.Controllers
{
    /// <summary>
    ///  UserRole controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        #region Members

        private readonly IUserRoleService _service;
        private readonly ILogger<UserRoleController> _logger;

        #endregion

        #region Constructor

        /// <summary>
        ///  UserRole Controller Constructor
        /// </summary>
        /// <param name="service">Inject IUserRoleService</param>
        /// <param name="logger">Inject logger</param>
        public UserRoleController(IUserRoleService service, ILogger<UserRoleController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get UserRoles
        /// </summary>
        /// <param></param>
        /// <returns>Return UserRole</returns>
        [HttpGet("[action]", Name = "GetUserRole")]
        [ProducesResponseType(typeof(IEnumerable<UserRole>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserRole>>> GetUserRole()
        {
            var res = await _service.GetUserRoles();
            return Ok(res);
        }

        /// <summary>
        /// Get UserRole
        /// </summary>
        /// <param name="id">UserRole id</param>
        /// <returns>Return UserRole details</returns>
        [HttpGet("[action]/{id}", Name = "GetUserRoleById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UserRole), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserRole>> GetUserRoleById(string id)
        {
            var res = await _service.GetUserRoleById(id);

            if (res == null)
            {
                _logger.LogError($"User Role with id: {id}, not found.");
                return NotFound();
            }

            return Ok(res);
        }

        /// <summary>
        /// Create UserRole 
        /// </summary>
        /// <param name="userRole">UserRole Object</param>
        /// <returns>Return UserRole</returns>
        [HttpPost("[action]", Name = "CreateUserRole")]
        [ProducesResponseType(typeof(UserRole), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserRole>> CreateUserRole([FromBody] UserRole userRole)
        {
            await _service.CreateUserRole(userRole);

            return CreatedAtRoute("GetUserRole", new { id = userRole.Id }, userRole);
        }

        /// <summary>
        /// Update UserRole 
        /// </summary>
        /// <param name="userRole">UserRole Object</param>
        /// <returns></returns>
        [HttpPut("[action]", Name = "UpdateUserRole")]
        [ProducesResponseType(typeof(UserRole), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateRole([FromBody] UserRole userRole)
        {
            return Ok(await _service.UpdateUserRole(userRole));
        }

        /// <summary>
        /// Delete UserRole 
        /// </summary>
        /// <param name="id">UserRole id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}", Name = "DeleteUserRole")]
        [ProducesResponseType(typeof(UserRole), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserRoleById(string id)
        {
            return Ok(await _service.DeleteUserRole(id));
        }

        #endregion
    }
}
