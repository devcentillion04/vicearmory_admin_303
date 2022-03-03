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
    ///  Role controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        #region Members

        private readonly IRoleService _service;
        private readonly ILogger<RoleController> _logger;

        #endregion

        #region Constructor

        /// <summary>
        ///  Role Controller Constructor
        /// </summary>
        /// <param name="service">Inject IRoleService</param>
        /// <param name="logger">Inject logger</param>
        public RoleController(IRoleService service, ILogger<RoleController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Role
        /// </summary>
        /// <param></param>
        /// <returns>Return Module</returns>
        [Route("[action]", Name = "GetRole")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Role>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Role>>> GetRole()
        {
            var res = await _service.GetRoles();
            return Ok(res);
        }

        /// <summary>
        /// Get Role
        /// </summary>
        /// <param name="id">Role id</param>
        /// <returns>Return Role details</returns>
        [HttpGet("[action]/{id}", Name = "GetRoleById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Role), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Role>> GetRoleById(string id)
        {
            var res = await _service.GetRoleById(id);

            if (res == null)
            {
                _logger.LogError($"Role with id: {id}, not found.");
                return NotFound();
            }

            return Ok(res);
        }

        /// <summary>
        /// Create Role 
        /// </summary>
        /// <param name="role">Role Object</param>
        /// <returns>Return Role</returns>
        [HttpPost("[action]", Name = "CreateRole")]
        [ProducesResponseType(typeof(Role), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Role>> CreateRole([FromBody] Role role)
        {
            await _service.CreateRole(role);

            return CreatedAtRoute("GetRole", new { id = role.Id }, role);
        }

        /// <summary>
        /// Update Role 
        /// </summary>
        /// <param name="role">Role Object</param>
        /// <returns></returns>
        [HttpPut("[action]", Name = "UpdateRole")]
        [ProducesResponseType(typeof(Role), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateRole([FromBody] Role role)
        {
            return Ok(await _service.UpdateRole(role));
        }

        /// <summary>
        /// Delete Role 
        /// </summary>
        /// <param name="id">Role id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}", Name = "DeleteRole")]
        [ProducesResponseType(typeof(Role), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteRoleById(string id)
        {
            return Ok(await _service.DeleteRole(id));
        }

        #endregion
    }
}