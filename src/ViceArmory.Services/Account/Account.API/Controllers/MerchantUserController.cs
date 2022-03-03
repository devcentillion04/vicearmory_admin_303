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
    ///  MerchantUser controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MerchantUserController : ControllerBase
    {
        #region Members

        private readonly IMerchantUserService _service;
        private readonly ILogger<MerchantUserController> _logger;

        #endregion

        #region Constructor

        /// <summary>
        ///  MerchantUser Controller Constructor
        /// </summary>
        /// <param name="service">Inject IMerchantUserService</param>
        /// <param name="logger">Inject logger</param>
        public MerchantUserController(IMerchantUserService service, ILogger<MerchantUserController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get MerchantUser
        /// </summary>
        /// <param></param>
        /// <returns>Return MerchantUser</returns>
        [HttpGet("[action]", Name = "GetMerchantUsers")]
        [ProducesResponseType(typeof(IEnumerable<MerchantUser>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MerchantUser>>> GetMerchantUsers()
        {
            var res = await _service.GetMerchantUsers();
            return Ok(res);
        }
        /// <summary>
        /// Get MerchantUser
        /// </summary>
        /// <param name="id">MerchantUser id</param>
        /// <returns>Return MerchantUser details</returns>
        [HttpGet("[action]/{id}", Name = "GetMerchantUserById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(MerchantUser), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MerchantUser>> GetMerchantUserById(string id)
        {
            var res = await _service.GetMerchantUserById(id);

            if (res == null)
            {
                _logger.LogError($"Merchant user with id: {id}, not found.");
                return NotFound();
            }

            return Ok(res);
        }

        /// <summary>
        /// Create MerchantUser 
        /// </summary>
        /// <param name="merchantUser">MerchantUser Object</param>
        /// <returns>Return MerchantUser</returns>
        [HttpPost("[action]", Name = "CreateMerchantUser")]
        [ProducesResponseType(typeof(MerchantUser), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MerchantUser>> CreateMerchantUser([FromBody] MerchantUser merchantUser)
        {
            await _service.CreateMerchantUser(merchantUser);

            return CreatedAtRoute("GetMerchantUsers", new { id = merchantUser.Id }, merchantUser);
        }

        /// <summary>
        /// Update MerchantUser 
        /// </summary>
        /// <param name="merchantUser">MerchantUser Object</param>
        /// <returns></returns>
        [HttpPut("[action]", Name = "UpdateMerchantUser")]
        [ProducesResponseType(typeof(MerchantUser), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMerchantUser([FromBody] MerchantUser merchantUser)
        {
            return Ok(await _service.UpdateMerchantUser(merchantUser));
        }

        /// <summary>
        /// Delete MerchantUser 
        /// </summary>
        /// <param name="id">MerchantUser id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}", Name = "DeleteMerchantUser")]
        [ProducesResponseType(typeof(MerchantUser), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMerchantUserById(string id)
        {
            return Ok(await _service.DeleteMerchantUser(id));
        }

        #endregion
    }
}