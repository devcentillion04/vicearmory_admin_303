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
        #endregion

        #region Constructor

        /// <summary>
        ///  Menu Controller Constructor
        /// </summary>
        /// <param name="service">Inject IMenuService</param>
        /// <param name="logger">Inject logger</param>
        public MenuController(IMenuRepository service, ILogger<MenuController> logger, IApiConfigurationService iApiConfigurationService, ILogContext logs, IOptions<ProjectSettings> options)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _iApiConfigurationService = iApiConfigurationService;
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _options = options;
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
            await _service.CreateMenuList(res.ToList());
          if(res == null)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "Menu",
                    Description = "GetMenu - not Successfull",
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
                    PageName = "Menu",
                    Description = "GetMenu - not Successfull",
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
        /// Get Menu
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns>Return Menu details</returns>
        [HttpPost("[action]", Name = "GetMenuById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(MenuResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MenuResponseDTO>> GetMenuById([FromBody]  string id)
        {
            var res = await _service.GetMenuById(id);
            if (res == null)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "Menu",
                    Description = "GetMenuById - not Successfull - id - " + id,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                _logger.LogError($"Menu with id: {id}, not found.");
                return NotFound();
            }
            else
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "Menu",
                    Description = "GetMenuById - Successfull - id - " + id,
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
                var logs = new LogResponseDTO()
                {
                    PageName = "Menu",
                    Description = "CreateMenu -not Successfull - id - " + menu.Id,
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
                    PageName = "Menu",
                    Description = "CreateMenu - Successfull - id - " + menu.Id,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
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
            var logs = new LogResponseDTO()
            {
                PageName = "Menu",
                Description = "UpdateMenu - Successfull - id - " + menu.Id,
                HostName = Functions.GetIpAddress().HostName,
                IpAddress = Functions.GetIpAddress().Ip,
                created_by = _options.Value.UserNameForLog,
                Created_date = DateTime.Now
            };
            await _logs.AddLogs.InsertOneAsync(logs);
            return Ok(await _service.UpdateMenu(menu));
        }

        /// <summary>
        /// Delete Menu 
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns></returns>
        [HttpPost("[action]", Name = "DeleteMenuById")]
        [ProducesResponseType(typeof(MenuResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMenuById([FromBody]  string id)
        {
            var logs = new LogResponseDTO()
            {
                PageName = "Menu",
                Description = "DeleteMenuById - Successfull - id - " + id,
                HostName = Functions.GetIpAddress().HostName,
                IpAddress = Functions.GetIpAddress().Ip,
                created_by = _options.Value.UserNameForLog,
                Created_date = DateTime.Now
            };
            await _logs.AddLogs.InsertOneAsync(logs);
            return Ok(await _service.DeleteMenu(id));
        }
        #endregion
    }
}
