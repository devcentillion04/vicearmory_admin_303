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
        #endregion

        #region Constructor

        /// <summary>
        ///  Role Controller Constructor
        /// </summary>
        /// <param name="service">Inject IRoleService</param>
        /// <param name="logger">Inject logger</param>
        public RoleController(IRoleRepository service, ILogger<RoleController> logger, ILogContext logs, IOptions<ProjectSettings> options)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _options = options;
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
            if(res == null)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "Role",
                    Description = "GetRole - not Successfull",
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
            }
            else
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "Role",
                    Description = "GetRole - Successfull",
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
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
                var logs = new LogResponseDTO()
                {
                    PageName = "Role",
                    Description = "GetRoleById - not Successfull - id :" + id,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                return NotFound();
            }
            else
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "Role",
                    Description = "GetRoleById - Successfull - id :" + id,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);

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
                var logs = new LogResponseDTO()
                {
                    PageName = "Role",
                    Description = "CreateRole -  Successfull - id :" + role.Name + ":id" + role.Id,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
            }
            else
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "Role",
                    Description = "CreateRole - not Successfull - id :" + role.Name,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
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
            var logs = new LogResponseDTO()
            {
                PageName = "Role",
                Description = "UpdateRole -  Successfull - id :" + role.Name + ":id" + role.Id,
                HostName = Functions.GetIpAddress().HostName,
                IpAddress = Functions.GetIpAddress().Ip,
                created_by = _options.Value.UserNameForLog,
                Created_date = DateTime.Now
            };
            await _logs.AddLogs.InsertOneAsync(logs);
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
            var logs = new LogResponseDTO()
            {
                PageName = "Role",
                Description = "DeleteRole -  Successfull - id :" + id,
                HostName = Functions.GetIpAddress().HostName,
                IpAddress = Functions.GetIpAddress().Ip,
                created_by = _options.Value.UserNameForLog,
                Created_date = DateTime.Now
            };
            await _logs.AddLogs.InsertOneAsync(logs);
            return Ok(await _service.DeleteRole(id));
        }

        #endregion
    }
}