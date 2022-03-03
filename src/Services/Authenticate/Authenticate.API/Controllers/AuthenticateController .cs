using Authenticate.DataContract;
using Authenticate.Service.Helpers;
using Authenticate.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Authenticate.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        #region Members
        private IAuthenticateService _authenticateService;
        private readonly ILogger<AuthenticateController> _logger;
        #endregion

        #region Construnction

        public AuthenticateController(ILogger<AuthenticateController> logger, IAuthenticateService authenticateService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authenticateService = authenticateService ?? throw new ArgumentNullException(nameof(authenticateService));
        }

        #endregion
        #region APIs
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest req)
        {
            var response = await _authenticateService.Authenticate(req);

            if (response == null)
                return BadRequest(new { message = "Username or password not autheticated" });

            return Ok(response);
        }

        #endregion
    }
}
