using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Middleware.Infrastructure;
using System;
using System.Threading.Tasks;
using ViceArmory.Middleware.Interface;

namespace ViceArmory.Web.Controllers
{
    public class RegisterController : Controller
    {
        #region members
        private IAdminLoginService _IAdminLoginService;
        #endregion
        #region construction
        public RegisterController(IAdminLoginService iAdminLoginService)
        {
            _IAdminLoginService = iAdminLoginService;
        }
        #endregion

        #region methods
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail(string Email, string Id)
        {
            try
            {
               var result = _IAdminLoginService.VerifyEmail(Email,Id);
                if(Constants.EMAILVERIFIED== result.Result.Replace("\"", ""))
                {
                    return View();
                }
                else
                {
                    RedirectToAction("EmailNotVerified");
                }
            }
            catch (Exception ex)
            {
                RedirectToAction("EmailNotVerified");
            }
            return null;
        }
        [AllowAnonymous]
        public async Task<IActionResult> EmailNotVerified(string Email, string Id)
        {
            return View();
        }
        #endregion

        
    }
}