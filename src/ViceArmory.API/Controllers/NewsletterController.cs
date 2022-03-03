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
using ViceArmory.DTO.RequestObject.Newsletter;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.DTO.ResponseObject.Logs;
using ViceArmory.DTO.ResponseObject.Newsletter;
using ViceArmory.Utility;

namespace ViceArmory.API.Controllers
{
    /// <summary>
    ///  Newsletter controller
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class NewsletterController : Controller
    {
        #region Members

        private readonly INewsletterRepository _service;
        private readonly ILogger<NewsletterController> _logger;
        private readonly ILogContext _logs;
        private IOptions<ProjectSettings> _options;

        #endregion

        #region Constructor

        /// <summary>
        ///  Menu Controller Constructor
        /// </summary>
        /// <param name="service">Inject IMenuService</param>
        /// <param name="logger">Inject logger</param>
        public NewsletterController(INewsletterRepository service, ILogger<NewsletterController> logger, ILogContext logs, IOptions<ProjectSettings> options)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _options = options;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Newsletter
        /// </summary>
        /// <param></param>
        /// <returns>Return Newsletter</returns>
        [HttpGet("[action]", Name = "GetNewsletters")]
        [ProducesResponseType(typeof(IEnumerable<NewsletterResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<NewsletterResponseDTO>>> GetNewsletters()
        {
            var res = await _service.GetNewsletters(); 
            if (res == null)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "Newsletter",
                    Description = "GetNewsletters - not Successfull",
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
                    PageName = "Newsletter",
                    Description = "GetNewsletters - Successfull",
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
        /// Create Newsletter  
        /// </summary>
        /// <param name="newsletterResponse">Newsleter Object</param>
        /// <returns>Return Menu</returns>
        [HttpPost("[action]", Name = "CreateNewsletter")]
        [ProducesResponseType(typeof(NewsletterRequestDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<NewsletterRequestDTO>> CreateNewsletters([FromBody] NewsletterRequestDTO newsletterResponse)
        {
            await _service.CreateNewsletters(new NewsletterResponseDTO()
            {
                
                UpdatedAt = newsletterResponse.UpdatedAt,
                UpdatedBy = newsletterResponse.UpdatedBy,
                CreatedAt = newsletterResponse.CreatedAt,
                CreatedBy = newsletterResponse.CreatedBy,
                Email = newsletterResponse.Email,
                Id = "",
                IsActive = newsletterResponse.IsActive,
                UnsubscribeAt = newsletterResponse.UnsubscribeAt,
                UserId = newsletterResponse.UserId,
                IPAddress = newsletterResponse.IPAddress

            });
            if(newsletterResponse != null)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "Newsletter",
                    Description = "CreateNewsletters - Successfull",
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
                    PageName = "Newsletter",
                    Description = "CreateNewsletters - not Successfull",
                    HostName = Functions.GetIpAddress().HostName,
                    IpAddress = Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
                await _logs.AddLogs.InsertOneAsync(logs);
            }
            return CreatedAtRoute("GetNewsletters", new { id = newsletterResponse.Id }, newsletterResponse);
        }

        /// <summary>
        /// Update Unsubscribe 
        /// </summary>
        /// <param name="id">Unsubscribe Object</param>
        /// <returns></returns>
        [HttpPost("[action]", Name = "Unsubscribe")]
        [ProducesResponseType(typeof(NewsletterResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Unsubscribe(string id)
        {
            var logs = new LogResponseDTO()
            {
                PageName = "Newsletter",
                Description = "Unsubscribe - Successfull",
                HostName = Functions.GetIpAddress().HostName,
                IpAddress = Functions.GetIpAddress().Ip,
                created_by = _options.Value.UserNameForLog,
                Created_date = DateTime.Now
            };
            await _logs.AddLogs.InsertOneAsync(logs);
            return Ok(await _service.Unsubscribe(id));
        }

        #endregion
    }
}

