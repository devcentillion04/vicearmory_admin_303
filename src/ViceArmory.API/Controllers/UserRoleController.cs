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
    ///  UserRole controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        #region Members

        private IOptions<ProjectSettings> _options;
        private readonly IUserRoleRepository _service;
        private readonly ILogger<UserRoleController> _logger;
        private readonly ILogContext _logs;
        private readonly IBaseRepository _baseRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  UserRole Controller Constructor
        /// </summary>
        /// <param name="service">Inject IUserRoleService</param>
        /// <param name="logger">Inject logger</param>
        public UserRoleController(IUserRoleRepository service, ILogger<UserRoleController> logger, ILogContext logs, IOptions<ProjectSettings> options, IBaseRepository baseRepo)
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
        /// Get UserRoles
        /// </summary>
        /// <param></param>
        /// <returns>Return UserRole</returns>
        [HttpGet("[action]", Name = "GetUserRole")]
        [ProducesResponseType(typeof(IEnumerable<UserRoleResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserRoleResponseDTO>>> GetUserRole()
        {
            var res = await _service.GetUserRoles();
            if (res == null)
            {
                await _baseRepository.AddLogs("UserRole", "GetUserRole - not Successfull", _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("UserRole", "GetUserRole - Successfull", _options.Value.UserNameForLog);
            }
            return Ok(res);
        }

        /// <summary>
        /// Get UserRole
        /// </summary>
        /// <param name="id">UserRole id</param>
        /// <returns>Return UserRole details</returns>
        [HttpGet("[action]/{id}", Name = "GetUserRoleById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UserRoleResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserRoleResponseDTO>> GetUserRoleById(string id)
        {
            var res = await _service.GetUserRoleById(id);

            if (res == null)
            {
                await _baseRepository.AddLogs("UserRole", "GetUserRoleById - not Successfull - id : " + id, _options.Value.UserNameForLog);
                return NotFound();
            }
            else
            {
                await _baseRepository.AddLogs("UserRole", "GetUserRoleById - Successfull - id : " + id, _options.Value.UserNameForLog);
                return Ok(res);
            }
        }

        /// <summary>
        /// Create UserRole 
        /// </summary>
        /// <param name="userRole">UserRole Object</param>
        /// <returns>Return UserRole</returns>
        [HttpPost("[action]", Name = "CreateUserRole")]
        [ProducesResponseType(typeof(UserRoleRequestDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserRoleResponseDTO>> CreateUserRole([FromBody] UserRoleRequestDTO userRole)
        {
            await _service.CreateUserRole(new UserRoleResponseDTO()
            {
                CreatedBy = userRole.CreatedBy,
                CreatedDate = userRole.CreatedDate,
                Id = "",
                IsDeleted = userRole.IsDeleted,
                RoleId = userRole.RoleId,
                UpdatedBy = userRole.UpdatedBy,
                UpdatedDate = userRole.UpdatedDate,
                UserId = userRole.UserId
            });

            if (userRole.Id == null)
            {
                await _baseRepository.AddLogs("UserRole", "GetUserRoleById - not Successfull - UserId : " + userRole.Id, _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("UserRole", "GetUserRoleById - Successfull - UserId : " + userRole.Id, _options.Value.UserNameForLog);
            }
                return CreatedAtRoute("GetUserRole", new { id = userRole.Id }, userRole);
        }

        /// <summary>
        /// Update UserRole 
        /// </summary>
        /// <param name="userRole">UserRole Object</param>
        /// <returns></returns>
        [HttpPut("[action]", Name = "UpdateUserRole")]
        [ProducesResponseType(typeof(UserRoleResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateRole([FromBody] UserRoleResponseDTO userRole)
        {
            await _baseRepository.AddLogs("UserRole", "UpdateUserRole - Successfull - Id : " + userRole.Id, _options.Value.UserNameForLog);
            return Ok(await _service.UpdateUserRole(userRole));
        }

        /// <summary>
        /// Delete UserRole 
        /// </summary>
        /// <param name="id">UserRole id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}", Name = "DeleteUserRole")]
        [ProducesResponseType(typeof(UserRoleResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserRoleById(string id)
        {
            await _baseRepository.AddLogs("UserRole", "DeleteUserRole - Successfull - Id : " + id, _options.Value.UserNameForLog);
            return Ok(await _service.DeleteUserRole(id));
        }

        #endregion
    }
}
