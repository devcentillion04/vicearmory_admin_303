using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ViceArmory.API.Helpers;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.RequestObject.User;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.DTO.ResponseObject.Logs;
using ViceArmory.DTO.ResponseObject.WeeklyAds;
using ViceArmory.Utility;

namespace ViceArmory.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WeeklyAdsController:ControllerBase
    {
        #region Members
        private readonly IWeeklyAdsRepository _service;
        private readonly ILogger<WeeklyAdsController> _logger;
        private readonly ILogContext _logs;
        private IOptions<ProjectSettings> _options;

        #endregion

        #region Construction
        public WeeklyAdsController(IWeeklyAdsRepository service, ILogger<WeeklyAdsController> logger, ILogContext logs, IOptions<ProjectSettings> options)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _options = options;
        }
        #endregion

        #region APIs
        /// <summary>
        /// Get All Pdf
        /// </summary>
        /// <param name="">Request object</param>
        /// <returns>Return Pdf List</returns>
        [AllowAnonymous]
        [HttpGet("[action]", Name = "GetAllPdf")]
        [ProducesResponseType(typeof(WeeklyAdsResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WeeklyAdsResponseDTO>> GetAllPdf()
        {
            _logger.LogInformation(String.Format("Api-GetAllPdf-Index-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var res = await _service.GetAllPdf();
            if (res == null)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "WeeklyAds",
                    Description = "GetAllPdf - not Successfull",
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
                    PageName = "WeeklyAds",
                    Description = "GetAllPdf - Successfull",
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
        /// Get PDF If deleted false
        /// </summary>
        /// <param name="">Request object</param>
        /// <returns>Return Pdf List</returns>
        [AllowAnonymous]
        [HttpGet("[action]", Name = "GetPdf")]
        [ProducesResponseType(typeof(WeeklyAdsResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WeeklyAdsResponseDTO>> GetPdf()
        {
            _logger.LogInformation(String.Format("Api-GetPdf-Index-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var res = await _service.GetPdf();
            if (res == null)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "WeeklyAds",
                    Description = "GetPdf - not Successfull",
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
                    PageName = "WeeklyAds",
                    Description = "GetPdf - Successfull",
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
        /// Get WeeklyAds
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return WeeklyAds</returns>
        [HttpPost("[action]/{id}", Name = "GetPdfById")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(WeeklyAdsResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WeeklyAdsResponseDTO>> GetPdfById([FromBody] int id)
        {
            _logger.LogInformation(String.Format("Api-GetPdfById-Index-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var product = await _service.GetPdfById(id.ToString());
            if (product == null)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "WeeklyAds",
                    Description = "GetPdfById - not Successfull - id:" + id,
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
                    PageName = "WeeklyAds",
                    Description = "GetPdfById - Successfull - id:" + id,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                return Ok(product);
            }
        }

        /// <summary>
        /// Create Pdf
        /// </summary>
        /// <param name="WeeklyAdsResponseDTO">Request object</param>
        /// <returns></returns>
   
        [HttpPost("[action]", Name = "AddPdf")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(WeeklyAdsResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<WeeklyAdsResponseDTO>> AddPdf([FromBody] WeeklyAdsResponseDTO weeklyads)
        {
            _logger.LogInformation(String.Format("Api-AddPdf-Index-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
          
            weeklyads.CreatedAt = DateTime.Now;
            weeklyads.UpdatedAt = DateTime.Now;
            weeklyads.IsDeleted = false;
            var res = new WeeklyAdsResponseDTO()
            {
                Id = weeklyads.Id,
                Description = weeklyads.Description,
                FilePath = weeklyads.FilePath,
                UpdatedAt = weeklyads.UpdatedAt,
                IsDeleted = weeklyads.IsDeleted,
                CreatedAt = weeklyads.CreatedAt
            };
            await _service.AddPdf(res);
            if (string.IsNullOrEmpty(res.Id))
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "WeeklyAds",
                    Description = "AddPdf - not Successfull - id:" + res.Description,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                return BadRequest();
            }
            else
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "WeeklyAds",
                    Description = "AddPdf - Successfull - id:" + res.Id,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                return CreatedAtRoute("GetAllPdf", new { id = res.Id }, weeklyads);
            }
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product">Request object</param>
        /// <returns>retunrn result</returns>
        [HttpPost("[action]", Name = "UpdatePdf")]
        [ProducesResponseType(typeof(WeeklyAdsResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePdf([FromBody] WeeklyAdsResponseDTO weeklyads)
        {
            _logger.LogInformation(String.Format("Api-UpdatePdf-Index-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            weeklyads.UpdatedAt = DateTime.Now;
            weeklyads.IsDeleted = false;
            var res = await _service.UpdatePdf(new WeeklyAdsResponseDTO()
            {
                Id = weeklyads.Id,
                Description = weeklyads.Description,
                FilePath = weeklyads.FilePath,
                UpdatedAt = weeklyads.UpdatedAt,
                IsDeleted = weeklyads.IsDeleted,
                CreatedAt = weeklyads.CreatedAt
            }); 

            if (!res)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "WeeklyAds",
                    Description = "UpdatePdf - not Successfull - id:" + weeklyads.Id,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                _logger.LogError($"Pdf not Updated.");
                return BadRequest(new { message = "Pdf not Updated." });
            }
            else
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "WeeklyAds",
                    Description = "UpdatePdf - Successfull - id:" + weeklyads.Id,
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
        /// Delete Pdf
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return True/False</returns>
        [HttpPost("[action]", Name = "DeletePdf")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletePdf(object id)
        {
            _logger.LogInformation(String.Format("Api-DeletePdf-Index-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var res = await _service.DeletePdf(id.ToString());
            if (!res)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "WeeklyAds",
                    Description = "DeletePdf - not Successfull - id:" + id,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                _logger.LogError($"Pdf not deleted.");
                return BadRequest(new { message = "Pdf not deleted." });
            }
            else
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "WeeklyAds",
                    Description = "DeletePdf - Successfull - id:" + id,
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
        /// For add IsDeleted Eq False
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return True/False</returns>
        [HttpPost("[action]", Name = "ActivatePdf")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ActivatePdf(object id)
        {
            _logger.LogInformation(String.Format("Api-ActivatePdf-Index-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var res = await _service.ActivatePdf(id.ToString());
            if (!res)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "WeeklyAds",
                    Description = "ActivatePdf - not Successfull - id:" + id,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                _logger.LogError($"Pdf not deleted.");
                return BadRequest(new { message = "Pdf not deleted." });
            }
            else
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "WeeklyAds",
                    Description = "ActivatePdf - Successfull - id:" + id,
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
                return Ok(res);
            }
        }
        #endregion
    }
}
