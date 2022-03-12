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
        private readonly IBaseRepository _baseRepository;

        #endregion

        #region Construction
        public WeeklyAdsController(IWeeklyAdsRepository service, ILogger<WeeklyAdsController> logger, ILogContext logs, IOptions<ProjectSettings> options, IBaseRepository baseRepo)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _options = options;
            _baseRepository = baseRepo ?? throw new ArgumentNullException(nameof(baseRepo));
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
                await _baseRepository.AddLogs("WeeklyAds", "GetAllPdf - not Successfull", _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("WeeklyAds", "GetAllPdf - Successfull", _options.Value.UserNameForLog);
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
                await _baseRepository.AddLogs("WeeklyAds", "GetPdf - not Successfull", _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("WeeklyAds", "GetPdf - Successfull", _options.Value.UserNameForLog);
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
                await _baseRepository.AddLogs("WeeklyAds", "GetPdfById - not Successfull - id:" + id, _options.Value.UserNameForLog); 
                return NotFound();
            }
            else
            {
                await _baseRepository.AddLogs("WeeklyAds", "GetPdfById - Successfull - id:" + id, _options.Value.UserNameForLog);
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
                await _baseRepository.AddLogs("WeeklyAds", "AddPdf - not Successfull - id:" + res.Description, _options.Value.UserNameForLog); 
                return BadRequest();
            }
            else
            {
                await _baseRepository.AddLogs("WeeklyAds", "AddPdf - Successfull - id:" + res.Id, _options.Value.UserNameForLog);
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
                await _baseRepository.AddLogs("WeeklyAds", "UpdatePdf - not Successfull - id:" + weeklyads.Id, _options.Value.UserNameForLog);
                return BadRequest(new { message = "Pdf not Updated." });
            }
            else
            {
                await _baseRepository.AddLogs("WeeklyAds", "UpdatePdf - Successfull - id:" + weeklyads.Id, _options.Value.UserNameForLog);
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
                await _baseRepository.AddLogs("WeeklyAds", "DeletePdf - not Successfull - id:" + id, _options.Value.UserNameForLog);
                return BadRequest(new { message = "Pdf not deleted." });
            }
            else
            {
                await _baseRepository.AddLogs("WeeklyAds", "DeletePdf - Successfull - id:" + id, _options.Value.UserNameForLog);
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
                await _baseRepository.AddLogs("WeeklyAds", "ActivatePdf - not Successfull - id:" + id, _options.Value.UserNameForLog);
                return BadRequest(new { message = "Pdf not deleted." });
            }
            else
            {
                await _baseRepository.AddLogs("WeeklyAds", "ActivatePdf - Successfull - id:" + id, _options.Value.UserNameForLog);
                return Ok(res);
            }
        }
            #endregion
        }
}
