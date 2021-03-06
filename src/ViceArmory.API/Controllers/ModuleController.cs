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
    ///  Module controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        #region Members

        private readonly IModuleRepository _service;
        private readonly ILogger<ModuleController> _logger;
        private readonly ILogContext _logs;
        private IOptions<ProjectSettings> _options;
        private readonly IBaseRepository _baseRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Module Controller Constructor
        /// </summary>
        /// <param name="service">Inject IModuleService</param>
        /// <param name="logger">Inject logger</param>
        public ModuleController(IModuleRepository service, ILogger<ModuleController> logger, ILogContext logs, IOptions<ProjectSettings> options, IBaseRepository baseRepo)
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
        /// Get Module
        /// </summary>
        /// <param></param>
        /// <returns>Return Module</returns>
        [HttpGet("[action]", Name = "GetModule")]
        [ProducesResponseType(typeof(IEnumerable<ModuleResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ModuleResponseDTO>>> GetModule()
        {
            var res = await _service.GetModules();
            if (res == null)
            {
                await _baseRepository.AddLogs("Module", "GetModule - not Successfull", _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("Module", "GetModule - Successfull", _options.Value.UserNameForLog);
            }
                return Ok(res);
        }

        /// <summary>
        /// Get Module
        /// </summary>
        /// <param name="id">Module id</param>
        /// <returns>Return Module details</returns>
        [HttpGet("[action]/{id}", Name = "GetModuleById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModuleResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ModuleResponseDTO>> GetModuleById(string id)
        {
            var res = await _service.GetModuleById(id);
            if (res == null)
            {
                await _baseRepository.AddLogs("Module", "GetModule - not Successfull", _options.Value.UserNameForLog);
                return NotFound();
            }
            else
            {
                await _baseRepository.AddLogs("Module", "GetModule - Successfull", _options.Value.UserNameForLog);
                return Ok(res);
            }
        }

        /// <summary>
        /// Create Module 
        /// </summary>
        /// <param name="module">Module Object</param>
        /// <returns>Return Module</returns>
        [HttpPost("[action]", Name = "CreateModule")]
        [ProducesResponseType(typeof(ModuleRequestDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ModuleResponseDTO>> CreateModule([FromBody] ModuleRequestDTO module)
        {
            await _service.CreateModule(new ModuleResponseDTO()
            {
                UpdatedDate = module.UpdatedDate,
                Name = module.Name,
                UpdatedBy = module.UpdatedBy,
                IsDeleted = module.IsDeleted,
                Id = "",
                CreatedDate = module.CreatedDate,
                CreatedBy = module.CreatedBy,
                Description = module.Description,
                IsActive = module.IsActive,
                MenuId = module.MenuId,
                ParentId = module.ParentId
            });
            if (module.Id != null)
            {
                await _baseRepository.AddLogs("Module", "CreateModule -  Successfull", _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("Module", "CreateModule - not Successfull", _options.Value.UserNameForLog);
            }
                return CreatedAtRoute("GetModule", new { id = module.Id }, module);
        }

        /// <summary>
        /// Update Module 
        /// </summary>
        /// <param name="module">Module Object</param>
        /// <returns></returns>
        [HttpPut("[action]", Name = "UpdateModule")]
        [ProducesResponseType(typeof(ModuleResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateModule([FromBody] ModuleResponseDTO module)
        {
            await _baseRepository.AddLogs("Module", "UpdateModule -  Successfull", _options.Value.UserNameForLog);
            return Ok(await _service.UpdateModule(module));
        }

        /// <summary>
        /// Delete Module 
        /// </summary>
        /// <param name="id">Module id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}", Name = "DeleteModule")]
        [ProducesResponseType(typeof(ModuleResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteModuleById(string id)
        {
            await _baseRepository.AddLogs("Module", "DeleteModuleById -  Successfull", _options.Value.UserNameForLog);
            return Ok(await _service.DeleteModule(id));
        }

        #endregion
    }
}
