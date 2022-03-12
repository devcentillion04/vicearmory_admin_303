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
        private readonly IBaseRepository _baseRepository;

        #endregion

        #region Constructor

        /// <summary>
        ///  Menu Controller Constructor
        /// </summary>
        /// <param name="service">Inject IMenuService</param>
        /// <param name="logger">Inject logger</param>
        public NewsletterController(INewsletterRepository service, ILogger<NewsletterController> logger, ILogContext logs, IOptions<ProjectSettings> options, IBaseRepository baseRepo)
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
                await _baseRepository.AddLogs("Newsletter", "GetNewsletters - not Successfull", _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("Newsletter", "GetNewsletters - Successfull", _options.Value.UserNameForLog);
            }
            return Ok(res);
        }

        /// <summary>
        /// Create Newsletter  
        /// </summary>
        /// <param name="newsletterResponse">Newsleter Object</param>
        /// <returns>Return Menu</returns>
        [HttpPost("[action]", Name = "CreateNewsletters")]
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
                await _baseRepository.AddLogs("Newsletter", "CreateNewsletters - Successfull", _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("Newsletter", "CreateNewsletters -not Successfull", _options.Value.UserNameForLog);
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
            await _baseRepository.AddLogs("Newsletter", "Unsubscribe - Successfull", _options.Value.UserNameForLog);
            return Ok(await _service.Unsubscribe(id));
        }

        #endregion

        [HttpPost("[action]", Name = "SendNewsLetter")]
        [ProducesResponseType(typeof(NewsletterRequestDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<NewsletterRequestDTO>> SendNewsLetter(string email)
        {
            string body = "";
            string to = "";
            var subject = "Subcription from Vice Armory";
           body = $@"<section>
                                    <div id='sevenwest_main'>
                                    <div style='width: 100%;text-align: center;background-color: black; padding:10px;'>
                                       <h1 style='color:#ffffff;'>Vice Armory</h1>
                                    </div>
                                    <div style='width: 100%;overflow: auto;display: block; text-align: center;margin-top: 30px;margin-bottom: 20px;'>
                                        <label style='float: left;clear: both; text-align: right;font-weight:800;'>Hello! </div>
                                        <br>
                                        <label style='float: left;clear: both; text-align: right;'>Thanks for subscription.</div>
                                    </div>
                                    <br />
                                    <div><p>If you received this email by mistake,simply delete it.</p>
                                    </div>
                                    <br />
                                    <div><p>For questions about this list,please contact:
                                    <br />
                                    <a link href='mailto:info@vicearmory.com'>info@vicearmory.com</a></p>
                                    </div>
                                    </div>
                                    </section>
                                    <br /><br /> Regards, 
                               <br /> Vice Armory.";            
            if (email != "")
            {
                to = email;
            }
            var mail = await _service.SendEmail(_options.Value.smtpAddress, _options.Value.portNumber, _options.Value.userName, _options.Value.passWord, email, _options.Value.from, _options.Value.fromName, subject, body);

            await _service.CreateNewsletters(new NewsletterResponseDTO()
            {

                UpdatedAt = DateTime.Now,
                UpdatedBy = email,
                CreatedAt = DateTime.Now,
                CreatedBy = email,
                Email = email,
                Id = "",
                IsActive = true,
                UnsubscribeAt = DateTime.Now,
                UserId = "",
                IPAddress = ""

            });
            if (mail == "OK")
            {
                await _baseRepository.AddLogs("Newsletter", "CreateNewsletters - Successfull", _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("Newsletter", "CreateNewsletters - not Successfull", _options.Value.UserNameForLog);
            }
            return Ok("Mail sent successfully.");

        }

    }
}

