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
        #endregion

        #region Constructor
        public ItemImageController(IItemImageRepository service, ILogger<ItemImageController> logger, IApiConfigurationService iApiConfigurationService, ILogContext logs, IOptions<ProjectSettings> options)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _iApiConfigurationService = iApiConfigurationService;
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _options = options;
        }
        #endregion



        [HttpPost("[action]", Name = "GetItemImagesByItemId")]
        [ProducesResponseType(typeof(List<ItemImageListMapDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ItemImageListMapDTO>> GetItemImagesByItemId(string id)
        { 
            var res = await _service.GetItemImagesByItemId(Convert.ToInt32(id));
            if (res == null)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "ItemImage",
                    Description = "GetItemImagesByItemId - not Successfull - Id " + id,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);

                return NotFound();
            }
            else
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "ItemImage",
                    Description = "GetItemImagesByItemId - not Successfull - Id " + id,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                return Ok(res);

            }
        }

        [HttpPost("[action]", Name = "UpdateItemImage")]
        [ProducesResponseType(typeof(Image), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateItemImage([FromBody] Image image)
        {
            var logs = new LogResponseDTO()
            {
                PageName = "ItemImage",
                Description = "UpdateItemImage - Successfull - image " + image.ToString(),
                HostName = Functions.GetIpAddress().HostName,
                IpAddress = Functions.GetIpAddress().Ip,
                created_by = _options.Value.UserNameForLog,
                Created_date = DateTime.Now
            };
            await _logs.AddLogs.InsertOneAsync(logs);
            return Ok(await _service.UpdateItemImage(image));
        }

        [HttpPost("[action]", Name = "InsertItemImage")]
        [ProducesResponseType(typeof(ItemImageMapDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ItemImageMapDTO>> InsertItemImage([FromBody] Image image)
        {
            var logs = new LogResponseDTO()
            {
                PageName = "ItemImage",
                Description = "InsertItemImage - Successfull - image " + image.ToString(),
                HostName = Functions.GetIpAddress().HostName,
                IpAddress = Functions.GetIpAddress().Ip,
                created_by = _options.Value.UserNameForLog,
                Created_date = DateTime.Now
            };
            await _logs.AddLogs.InsertOneAsync(logs);
            return Ok(await _service.UpdateItemImage(image));
        }

        [HttpDelete("[action]", Name = "DeleteItemImage")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteItemImage(string id)
        {
            var logs = new LogResponseDTO()
            {
                PageName = "ItemImage",
                Description = "DeleteItemImage - Successfull - id " + id,
                HostName = Functions.GetIpAddress().HostName,
                IpAddress = Functions.GetIpAddress().Ip,
                created_by = _options.Value.UserNameForLog,
                Created_date = DateTime.Now
            };
            await _logs.AddLogs.InsertOneAsync(logs);
            return Ok(await _service.DeleteItemImage(Convert.ToInt32(id)));
        }
    }
}
