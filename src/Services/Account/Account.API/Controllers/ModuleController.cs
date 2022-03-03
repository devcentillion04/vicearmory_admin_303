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
    ///  Module controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        #region Members

        private readonly IModuleService _service;
        private readonly ILogger<ModuleController> _logger;

        #endregion

        #region Constructor

        /// <summary>
        ///  Module Controller Constructor
        /// </summary>
        /// <param name="service">Inject IModuleService</param>
        /// <param name="logger">Inject logger</param>
        public ModuleController(IModuleService service, ILogger<ModuleController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Module
        /// </summary>
        /// <param></param>
        /// <returns>Return Module</returns>
        [HttpGet("[action]", Name = "GetModule")]
        [ProducesResponseType(typeof(IEnumerable<Module>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Module>>> GetModule()
        {
            var res = await _service.GetModules();
            return Ok(res);
        }

        /// <summary>
        /// Get Module
        /// </summary>
        /// <param name="id">Module id</param>
        /// <returns>Return Module details</returns>
        [HttpGet("[action]/{id}", Name = "GetModuleById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Module), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Module>> GetModuleById(string id)
        {
            var res = await _service.GetModuleById(id);

            if (res == null)
            {
                _logger.LogError($"Module with id: {id}, not found.");
                return NotFound();
            }

            return Ok(res);
        }

        /// <summary>
        /// Create Module 
        /// </summary>
        /// <param name="module">Module Object</param>
        /// <returns>Return Module</returns>
        [HttpPost("[action]", Name = "CreateModule")]
        [ProducesResponseType(typeof(Module), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Module>> CreateModule([FromBody] Module module)
        {
            await _service.CreateModule(module);

            return CreatedAtRoute("GetModule", new { id = module.Id }, module);
        }

        /// <summary>
        /// Update Module 
        /// </summary>
        /// <param name="module">Module Object</param>
        /// <returns></returns>
        [HttpPut("[action]", Name = "UpdateModule")]
        [ProducesResponseType(typeof(Module), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateModule([FromBody] Module module)
        {
            return Ok(await _service.UpdateModule(module));
        }

        /// <summary>
        /// Delete Module 
        /// </summary>
        /// <param name="id">Module id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}", Name = "DeleteModule")]
        [ProducesResponseType(typeof(Module), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteModuleById(string id)
        {
            return Ok(await _service.DeleteModule(id));
        }

        #endregion
    }
}
