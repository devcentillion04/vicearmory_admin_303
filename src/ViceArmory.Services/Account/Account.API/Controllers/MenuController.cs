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
    ///  Menu controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        #region Members

        private readonly IMenuService _service;
        private readonly ILogger<MenuController> _logger;

        #endregion

        #region Constructor

        /// <summary>
        ///  Menu Controller Constructor
        /// </summary>
        /// <param name="service">Inject IMenuService</param>
        /// <param name="logger">Inject logger</param>
        public MenuController(IMenuService service, ILogger<MenuController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Menu
        /// </summary>
        /// <param></param>
        /// <returns>Return Menu</returns>
        [HttpGet("[action]", Name = "GetMenu")]
        [ProducesResponseType(typeof(IEnumerable<Menu>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenu()
        {
            var res = await _service.GetMenu();
            return Ok(res);
        }

        /// <summary>
        /// Get Menu
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns>Return Menu details</returns>
        [HttpGet("[action]/{id}", Name = "GetMenuById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Menu), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Menu>> GetMenuById(string id)
        {
            var res = await _service.GetMenuById(id);

            if (res == null)
            {
                _logger.LogError($"Menu with id: {id}, not found.");
                return NotFound();
            }

            return Ok(res);
        }

        /// <summary>
        /// Create Menu 
        /// </summary>
        /// <param name="menu">Menu Object</param>
        /// <returns>Return Menu</returns>
        [HttpPost("[action]", Name = "CreateMenu")]
        [ProducesResponseType(typeof(Menu), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Menu>> CreateMenu([FromBody] Menu menu)
        {
            await _service.CreateMenu(menu);

            return CreatedAtRoute("GetMenu", new { id = menu.Id }, menu);
        }

        /// <summary>
        /// Update Menu 
        /// </summary>
        /// <param name="menu">Menu Object</param>
        /// <returns></returns>
        [HttpPut("[action]", Name = "UpdateMenu")]
        [ProducesResponseType(typeof(Menu), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMenu([FromBody] Menu menu)
        {
            return Ok(await _service.UpdateMenu(menu));
        }

        /// <summary>
        /// Delete Menu 
        /// </summary>
        /// <param name="id">Menu id</param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}", Name = "DeleteMenu")]
        [ProducesResponseType(typeof(Menu), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteMenuById(string id)
        {
            return Ok(await _service.DeleteMenu(id));
        }

        #endregion
    }
}
