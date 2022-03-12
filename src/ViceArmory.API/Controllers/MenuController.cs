using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.RequestObject.Account;
using Microsoft.AspNetCore.Http;
using ViceArmory.DTO.ResponseObject.Logs;
using ViceArmory.Utility;
using Newtonsoft.Json;
using ViceArmory.DTO.ResponseObject.Authenticate;
using Microsoft.Extensions.Options;
using ViceArmory.DTO.ResponseObject.AppSettings;

namespace ViceArmory.API.Controllers
{
    /// <summary>
    ///  Menu controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        #region Members

        private readonly IMenuRepository _service;
        private readonly ILogger<MenuController> _logger;
        private readonly IApiConfigurationService _iApiConfigurationService;
        private readonly ILogContext _logs;
        private IOptions<ProjectSettings> _options;
        private readonly IBaseRepository _baseRepository;
        #endregion

        #region Constructor

        /// <summary>
        ///  Menu Controller Constructor
        /// </summary>
        /// <param name="service">Inject IMenuService</param>
        /// <param name="logger">Inject logger</param>
        public MenuController(IMenuRepository service, ILogger<MenuController> logger, IApiConfigurationService iApiConfigurationService, ILogContext logs, IOptions<ProjectSettings> options, IBaseRepository baseRepo)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _iApiConfigurationService = iApiConfigurationService;
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _options = options;
            _baseRepository = baseRepo ?? throw new ArgumentNullException(nameof(baseRepo));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Menu
        /// </summary>
        /// <param></param>
        /// <returns>Return Menu</returns>
        [HttpGet("[action]", Name = "GetMenu")]
        [ProducesResponseType(typeof(List<MenuResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<MenuResponseDTO>>> GetMenu()
        {
            var res = await _service.GetMenu();
            //  await _service.CreateMenuList(res.ToList());
            if (res == null)
            {
                await _baseRepository.AddLogs("Menu", "GetMenu - not Successfull", _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("Menu", "GetMenu - Successfull", _options.Value.UserNameForLog);
            }
            return Ok(res);
        }

        /// <summary>
        /// Get Menu
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns>Return Menu details</returns>
        [HttpPost("[action]", Name = "GetMenuById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(MenuResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MenuResponseDTO>> GetMenuById([FromBody] string id)
        {
            var res = await _service.GetMenuById(id);
            if (res == null)
            {
                await _baseRepository.AddLogs("Menu", "GetMenuById -not Successfull - id - " + id, _options.Value.UserNameForLog);
                return NotFound();
            }
            else
            {
                await _baseRepository.AddLogs("Menu", "GetMenuById - Successfull - id - " + id, _options.Value.UserNameForLog);
                return Ok(res);
            }
        }

        /// <summary>
        /// Create Menu 
        /// </summary>
        /// <param name="menu">Menu Object</param>
        /// <returns>Return Menu</returns>
        [HttpPost("[action]", Name = "CreateMenu")]
        [ProducesResponseType(typeof(MenuRequestDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MenuResponseDTO>> CreateMenu([FromBody] MenuRequestDTO menu)
        {
            await _service.CreateMenu(new MenuResponseDTO()
            {
                UpdatedDate = menu.UpdatedDate,
                CreatedBy = menu.CreatedBy,
                CreatedDate = menu.CreatedDate,
                Description = menu.Description,
                IsActive = menu.IsActive,
                IsDeleted = menu.IsDeleted,
                Name = menu.Name,
                ParentId = menu.ParentId,
                UpdatedBy = menu.UpdatedBy,

            });
            if (menu.Id == null)
            {
                await _baseRepository.AddLogs("Menu", "CreateMenu -Not Successfull - id - " + menu.Id, _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("Menu", "CreateMenu - Successfull - id - " + menu.Id, _options.Value.UserNameForLog);
            }
            return CreatedAtRoute("GetMenu", new { id = menu.Id }, menu);
        }

        /// <summary>
        /// Update Menu 
        /// </summary>
        /// <param name="menu">Menu Object</param>
        /// <returns></returns>
        [HttpPost("[action]", Name = "UpdateMenu")]
        [ProducesResponseType(typeof(MenuResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMenu([FromBody] MenuResponseDTO menu)
        {
            await _baseRepository.AddLogs("Menu", "UpdateMenu - Successfull - id - " + menu.Id, _options.Value.UserNameForLog);
            return Ok(await _service.UpdateMenu(menu));
        }

        /// <summary>
        /// Delete Menu 
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns></returns>
        [HttpPost("[action]", Name = "DeleteMenuById")]
        [ProducesResponseType(typeof(MenuResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMenuById([FromBody] string id)
        {
            await _baseRepository.AddLogs("Menu", "DeleteMenuById - Successfull - id - " + id, _options.Value.UserNameForLog);
            return Ok(await _service.DeleteMenu(id));
        }
        #endregion
    }
}
