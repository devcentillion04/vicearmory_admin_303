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
    ///  MerchantUser controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MerchantUserController : ControllerBase
    {
        #region Members

        private readonly IMerchantUserRepository _service;
        private readonly ILogger<MerchantUserController> _logger;
        private readonly ILogContext _logs;
        private IOptions<ProjectSettings> _options;
        private readonly IBaseRepository _baseRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  MerchantUser Controller Constructor
        /// </summary>
        /// <param name="service">Inject IMerchantUserService</param>
        /// <param name="logger">Inject logger</param>
        public MerchantUserController(IMerchantUserRepository service, ILogger<MerchantUserController> logger, ILogContext logs, IOptions<ProjectSettings> options, IBaseRepository baseRepo)
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
        /// Get MerchantUser
        /// </summary>
        /// <param></param>
        /// <returns>Return MerchantUser</returns>
        [HttpGet("[action]", Name = "GetMerchantUsers")]
        [ProducesResponseType(typeof(IEnumerable<MerchantUserResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MerchantUserResponseDTO>>> GetMerchantUsers()
        {
            var res = await _service.GetMerchantUsers();
            if (res == null)
            {
                await _baseRepository.AddLogs("MerchantUser", "GetMerchantUsers - not Successfull", _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("MerchantUser", "GetMerchantUsers - Successfull", _options.Value.UserNameForLog);
            }
            return Ok(res);
        }
        /// <summary>
        /// Get MerchantUser
        /// </summary>
        /// <param name="id">MerchantUser id</param>
        /// <returns>Return MerchantUser details</returns>
        [HttpGet("[action]/{id}", Name = "GetMerchantUserById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(MerchantUserResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MerchantUserResponseDTO>> GetMerchantUserById(string id)
        {
            var res = await _service.GetMerchantUserById(id);

            if (res == null)
            {
                await _baseRepository.AddLogs("MerchantUser", "GetMerchantUsers - not Successfull - id:" + id, _options.Value.UserNameForLog);
                return NotFound();
            }
            else
            {
                await _baseRepository.AddLogs("MerchantUser", "GetMerchantUsers - Successfull - id:" + id, _options.Value.UserNameForLog);
                return Ok(res);
            }
        }

        /// <summary>
        /// Create MerchantUser 
        /// </summary>
        /// <param name="merchantUser">MerchantUser Object</param>
        /// <returns>Return MerchantUser</returns>
        [HttpPost("[action]", Name = "CreateMerchantUser")]
        [ProducesResponseType(typeof(MerchantUserRequestDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MerchantUserResponseDTO>> CreateMerchantUser([FromBody] MerchantUserRequestDTO merchantUser)
        {
            await _service.CreateMerchantUser(new MerchantUserResponseDTO()
            {
                CreatedBy = merchantUser.CreatedBy,
                CreatedDate = merchantUser.CreatedDate,
                Id = "",
                UpdatedBy = merchantUser.UpdatedBy,
                Email = merchantUser.Email,
                FirstName = merchantUser.FirstName,
                Intro = merchantUser.Intro,
                IsAdmin = merchantUser.IsAdmin,
                IsVendor = merchantUser.IsVendor,
                LastLogin = merchantUser.LastLogin,
                LastName = merchantUser.LastName,
                MiddleName = merchantUser.MiddleName,
                Mobile = merchantUser.Mobile,
                Profile = merchantUser.Profile,
                Password = merchantUser.Password,
                RegisterAt = merchantUser.RegisterAt,
                UpdatedDate = merchantUser.UpdatedDate,
                Username = merchantUser.Username
            });
            if (merchantUser.Id == null)
            {
                await _baseRepository.AddLogs("MerchantUser", "CreateMerchantUser - not Successfull - merchantUserId:" + merchantUser.Id, _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("MerchantUser", "CreateMerchantUser - Successfull - merchantUserId:" + merchantUser.Id, _options.Value.UserNameForLog);
            }
            return CreatedAtRoute("GetMerchantUsers", new { id = merchantUser.Id }, merchantUser);
        }

        /// <summary>
        /// Update MerchantUser 
        /// </summary>
        /// <param name="merchantUser">MerchantUser Object</param>
        /// <returns></returns>
        [HttpPut("[action]", Name = "UpdateMerchantUser")]
        [ProducesResponseType(typeof(MerchantUserResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMerchantUser([FromBody] MerchantUserResponseDTO merchantUser)
        {
            await _baseRepository.AddLogs("MerchantUser", "UpdateMerchantUser - Successfull - merchantUserId:" + merchantUser.Id, _options.Value.UserNameForLog);
            return Ok(await _service.UpdateMerchantUser(merchantUser));
        }

        /// <summary>
        /// Delete MerchantUser 
        /// </summary>
        /// <param name="id">MerchantUser id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}", Name = "DeleteMerchantUser")]
        [ProducesResponseType(typeof(MerchantUserResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMerchantUserById(string id)
        {            
            await _baseRepository.AddLogs("MerchantUser", "DeleteMerchantUserById - Successfull - merchantUserId:" + id, _options.Value.UserNameForLog);
            return Ok(await _service.DeleteMerchantUser(id));
        }

        #endregion
    }
}