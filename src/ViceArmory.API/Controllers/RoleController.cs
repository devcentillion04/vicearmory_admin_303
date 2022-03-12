using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.DTO.ResponseObject.Logs;
using ViceArmory.RequestObject.Account;
using ViceArmory.Utility;

namespace ViceArmory.API.Controllers
{
    /// <summary>
    ///  Role controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        #region Members

        private readonly IRoleRepository _service;
        private readonly ILogger<RoleController> _logger;
        private readonly ILogContext _logs;
        private IOptions<ProjectSettings> _options;
        private readonly IBaseRepository _baseRepository;
        #endregion

        #region Constructor

        /// <summary>
        ///  Role Controller Constructor
        /// </summary>
        /// <param name="service">Inject IRoleService</param>
        /// <param name="logger">Inject logger</param>
        public RoleController(IRoleRepository service, ILogger<RoleController> logger, ILogContext logs, IOptions<ProjectSettings> options, IBaseRepository baseRepo)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _options = options;
            _baseRepository = baseRepo ?? throw new ArgumentNullException(nameof(baseRepo));
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
        [ProducesResponseType(typeof(IEnumerable<RoleResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RoleResponseDTO>>> GetRole()
        {
            var res = await _service.GetRoles();
            if (res == null)
            {
                await _baseRepository.AddLogs("Role", "GetRole - not Successfull", _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("Role", "GetRole - Successfull", _options.Value.UserNameForLog);
            }
            return Ok(res);
        }

        /// <summary>
        /// Get Role
        /// </summary>
        /// <param name="id">Role id</param>
        /// <returns>Return Role details</returns>
        [HttpGet("[action]/{id}", Name = "GetRoleById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(RoleResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RoleResponseDTO>> GetRoleById(string id)
        {
            var res = await _service.GetRoleById(id);
            if (res == null)
            {
                await _baseRepository.AddLogs("Role", "GetRoleById - not Successfull - id :" + id, _options.Value.UserNameForLog);
                return NotFound();
            }
            else
            {
                await _baseRepository.AddLogs("Role", "GetRoleById - Successfull - id :" + id, _options.Value.UserNameForLog);
                return Ok(res);
            }
        }

        /// <summary>
        /// Create Role 
        /// </summary>
        /// <param name="role">Role Object</param>
        /// <returns>Return Role</returns>
        [HttpPost("[action]", Name = "CreateRole")]
        [ProducesResponseType(typeof(RoleRequestDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RoleResponseDTO>> CreateRole([FromBody] RoleRequestDTO role)
        {
            await _service.CreateRole(new RoleResponseDTO()
            {
                CreatedBy = role.CreatedBy,
                CreatedDate = role.CreatedDate,
                Id = "",
                IsDeleted = role.IsDeleted,
                UpdatedBy = role.UpdatedBy,
                Name = role.Name,
                UpdatedDate = role.UpdatedDate
            });
            if (role.Id != null)
            {
                await _baseRepository.AddLogs("Role", "CreateRole -  Successfull - id :" + role.Name + ":id" + role.Id, _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("Role", "CreateRole - not Successfull - id :" + role.Name + ":id" + role.Id, _options.Value.UserNameForLog);
            }
            return CreatedAtRoute("GetRole", new { id = role.Id }, role);
        }

        /// <summary>
        /// Update Role 
        /// </summary>
        /// <param name="role">Role Object</param>
        /// <returns></returns>
        [HttpPut("[action]", Name = "UpdateRole")]
        [ProducesResponseType(typeof(RoleResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateRole([FromBody] RoleResponseDTO role)
        {
            await _baseRepository.AddLogs("Role", "UpdateRole -  Successfull - id :" + role.Name + ":id" + role.Id, _options.Value.UserNameForLog);
            return Ok(await _service.UpdateRole(role));
        }

        /// <summary>
        /// Delete Role 
        /// </summary>
        /// <param name="id">Role id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}", Name = "DeleteRole")]
        [ProducesResponseType(typeof(RoleResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteRoleById(string id)
        {
            await _baseRepository.AddLogs("Role", "DeleteRole -  Successfull - id :" + id, _options.Value.UserNameForLog);
            return Ok(await _service.DeleteRole(id));
        }

        #endregion
    }
}