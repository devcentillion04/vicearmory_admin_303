using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.MapObject;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.DTO.ResponseObject.Logs;
using ViceArmory.Utility;

namespace ViceArmory.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemImageController : Controller
    {
        #region Members

        private readonly IItemImageRepository _service;
        private readonly ILogger<ItemImageController> _logger;
        private readonly IApiConfigurationService _iApiConfigurationService;
        private readonly ILogContext _logs;
        private IOptions<ProjectSettings> _options;
        private readonly IBaseRepository _baseRepository;
        #endregion

        #region Constructor
        public ItemImageController(IItemImageRepository service, ILogger<ItemImageController> logger, IApiConfigurationService iApiConfigurationService, ILogContext logs, IOptions<ProjectSettings> options, IBaseRepository baseRepo)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _iApiConfigurationService = iApiConfigurationService;
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _options = options;
            _baseRepository = baseRepo ?? throw new ArgumentNullException(nameof(baseRepo));
        }
        #endregion



        [HttpPost("[action]", Name = "GetItemImagesByItemId")]
        [ProducesResponseType(typeof(List<ItemImageListMapDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ItemImageListMapDTO>> GetItemImagesByItemId(string id)
        { 
            var res = await _service.GetItemImagesByItemId(Convert.ToInt32(id));
            if (res == null)
            {
                await _baseRepository.AddLogs("Item Image", "GetItemImagesByItemId - not Successfull - Id " + id, _options.Value.UserNameForLog);
                return NotFound();
            }
            else
                {
                await _baseRepository.AddLogs("Item Image", "GetItemImagesByItemId - Successfull - Id " + id, _options.Value.UserNameForLog);
                return Ok(res);
            }
        }

        [HttpPost("[action]", Name = "UpdateItemImage")]
        [ProducesResponseType(typeof(Image), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateItemImage([FromBody] Image image)
        {
            await _baseRepository.AddLogs("Item Image", "UpdateItemImage - Successfull - image " + image.ToString(), _options.Value.UserNameForLog);
            return Ok(await _service.UpdateItemImage(image));
        }

        [HttpPost("[action]", Name = "InsertItemImage")]
        [ProducesResponseType(typeof(ItemImageMapDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ItemImageMapDTO>> InsertItemImage([FromBody] Image image)
        {
            await _baseRepository.AddLogs("Item Image", "InsertItemImage - Successfull - image " + image.ToString(), _options.Value.UserNameForLog);
            return Ok(await _service.UpdateItemImage(image));
        }

        [HttpDelete("[action]", Name = "DeleteItemImage")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteItemImage(string id)
        {
            await _baseRepository.AddLogs("Item Image", "DeleteItemImage - Successfull - id " + id, _options.Value.UserNameForLog);
            return Ok(await _service.DeleteItemImage(Convert.ToInt32(id)));
        }
    }
}
