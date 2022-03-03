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
    ///  UserAccess controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserAccessController : ControllerBase
    {
        #region Members

        private readonly IUserAccessService _service;
        private readonly ILogger<UserAccessController> _logger;

        #endregion

        #region Constructor

        /// <summary>
        ///  UserAccess Controller Constructor
        /// </summary>
        /// <param name="service">Inject IUserAccessService</param>
        /// <param name="logger">Inject logger</param>
        public UserAccessController(IUserAccessService service, ILogger<UserAccessController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get UserAccess
        /// </summary>
        /// <param></param>
        /// <returns>Return UserAccess</returns>
        [HttpGet("[action]", Name = "GetUserAccess")]
        [ProducesResponseType(typeof(IEnumerable<UserAccess>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserAccess>>> GetUserAccess()
        {
            var res = await _service.GetUserAccess();
            return Ok(res);
        }

        /// <summary>
        /// Get UserAccess
        /// </summary>
        /// <param name="id">UserAccess id</param>
        /// <returns>Return UserAccess details</returns>
        [HttpGet("[action]/{id}", Name = "GetUserAccessById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UserAccess), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserAccess>> GetUserAccessById(string id)
        {
            var res = await _service.GetUserAccessById(id);

            if (res == null)
            {
                _logger.LogError($"User Access with id: {id}, not found.");
                return NotFound();
            }

            return Ok(res);
        }

        /// <summary>
        /// Create UserAccess 
        /// </summary>
        /// <param name="userAccess">UserAccess Object</param>
        /// <returns>Return UserAccess</returns>
        [HttpPost("[action]", Name = "CreateUserAccess")]
        [ProducesResponseType(typeof(UserAccess), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserAccess>> CreateUserAccess([FromBody] UserAccess userAccess)
        {
            await _service.CreateUserAccess(userAccess);

            return CreatedAtRoute("GetUserAccess", new { id = userAccess.Id }, userAccess);
        }

        /// <summary>
        /// Update UserAccess 
        /// </summary>
        /// <param name="userAccess">UserAccess Object</param>
        /// <returns></returns>
        [HttpPut("[action]", Name = "UpdateUserAccess")]
        [ProducesResponseType(typeof(UserAccess), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUserAccess([FromBody] UserAccess userAccess)
        {
            return Ok(await _service.UpdateUserAccess(userAccess));
        }

        /// <summary>
        /// Delete UserAccess 
        /// </summary>
        /// <param name="id">UserAccess id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}", Name = "DeleteUserAccess")]
        [ProducesResponseType(typeof(UserAccess), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserAccessById(string id)
        {
            return Ok(await _service.DeleteUserAccess(id));
        }

        #endregion
    }
}
