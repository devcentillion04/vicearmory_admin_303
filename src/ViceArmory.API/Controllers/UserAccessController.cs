using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
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
    ///  UserAccess controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserAccessController : ControllerBase
    {
        #region Members

        private readonly IUserAccessRepository _service;
        private readonly ILogger<UserAccessController> _logger;
        private readonly ILogContext _logs;
        private IOptions<ProjectSettings> _options;
        private readonly IBaseRepository _baseRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  UserAccess Controller Constructor
        /// </summary>
        /// <param name="service">Inject IUserAccessService</param>
        /// <param name="logger">Inject logger</param>
        public UserAccessController(IUserAccessRepository service, ILogger<UserAccessController> logger, ILogContext logs, IOptions<ProjectSettings> options, IBaseRepository baseRepo)
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
        /// Get UserAccess
        /// </summary>
        /// <param></param>
        /// <returns>Return UserAccess</returns>
        [HttpGet("[action]", Name = "GetUserAccess")]
        [ProducesResponseType(typeof(IEnumerable<UserAccessResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserAccessResponseDTO>>> GetUserAccess()
        {
            var res = await _service.GetUserAccess(); 
            if (res == null)
            {
                await _baseRepository.AddLogs("UserAccess", "GetUserAccess - not Successfull", _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("UserAccess", "GetUserAccess - Successfull", _options.Value.UserNameForLog);
            }
            return Ok(res);
        }

        /// <summary>
        /// Get UserAccess
        /// </summary>
        /// <param name="id">UserAccess id</param>
        /// <returns>Return UserAccess details</returns>
        [HttpGet("[action]/{id}", Name = "GetUserAccessById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UserAccessResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserAccessResponseDTO>> GetUserAccessById(string id)
        {
            var res = await _service.GetUserAccessById(id);

            if (res == null)
            {
                await _baseRepository.AddLogs("UserAccess", "GetUserAccessById - not Successfull - id : " + id, _options.Value.UserNameForLog);
                return NotFound();
            }
            else
            {
                await _baseRepository.AddLogs("UserAccess", "GetUserAccessById - Successfull - id : " + id, _options.Value.UserNameForLog);
                return Ok(res);
            }
        }

        /// <summary>
        /// Create UserAccess 
        /// </summary>
        /// <param name="userAccess">UserAccess Object</param>
        /// <returns>Return UserAccess</returns>
        [HttpPost("[action]", Name = "CreateUserAccess")]
        [ProducesResponseType(typeof(UserAccessRequestDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserAccessResponseDTO>> CreateUserAccess([FromBody] UserAccessRequestDTO userAccess)
        {
            await _service.CreateUserAccess(new UserAccessResponseDTO()
            {
                UpdatedDate = userAccess.UpdatedDate,
                UpdatedBy = userAccess.UpdatedBy,
                RoleId = userAccess.RoleId,
                IsDeleted = userAccess.IsDeleted,
                Id = "",
                Access = userAccess.Access,
                CreatedBy = userAccess.CreatedBy,
                CreatedDate = userAccess.CreatedDate,
                MenuId = userAccess.MenuId,
                ModuleId = userAccess.ModuleId
            });

            if (userAccess.Id == null)
            {
                await _baseRepository.AddLogs("UserAccess", "CreateUserAccess - not Successfull - moduleid : " + userAccess.ModuleId, _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("UserAccess", "CreateUserAccess - Successfull - moduleid : " + userAccess.ModuleId, _options.Value.UserNameForLog);
            }
            return CreatedAtRoute("GetUserAccess", new { id = userAccess.Id }, userAccess);
        }

        /// <summary>
        /// Update UserAccess 
        /// </summary>
        /// <param name="userAccess">UserAccess Object</param>
        /// <returns></returns>
        [HttpPut("[action]", Name = "UpdateUserAccess")]
        [ProducesResponseType(typeof(UserAccessResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUserAccess([FromBody] UserAccessResponseDTO userAccess)
        {
            await _baseRepository.AddLogs("UserAccess", "UpdateUserAccess - Successfull - id : " + userAccess.Id, _options.Value.UserNameForLog);
            return Ok(await _service.UpdateUserAccess(userAccess));
        }

        /// <summary>
        /// Delete UserAccess 
        /// </summary>
        /// <param name="id">UserAccess id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}", Name = "DeleteUserAccess")]
        [ProducesResponseType(typeof(UserAccessResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserAccessById(string id)
        {
            await _baseRepository.AddLogs("UserAccess", "DeleteUserAccess - Successfull - id : " + id, _options.Value.UserNameForLog);
            return Ok(await _service.DeleteUserAccess(id));
        }

        #endregion
      
        }
}
