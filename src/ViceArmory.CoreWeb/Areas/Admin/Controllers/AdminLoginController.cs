using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViceArmory.CoreWeb.Models;
using ViceArmory.DTO.RequestObject.Authenticate;
using ViceArmory.DTO.ResponseObject.Account;
using ViceArmory.Middleware.Interface;
using ViceArmory.Middleware;
using Middleware.Infrastructure;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DAL.Interface;
using Microsoft.Extensions.Options;
using ViceArmory.DTO.ResponseObject.Logs;

namespace ViceArmory.CoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminLoginController : Controller
    {
        IAuthenticateService _IAuthenticateService;
        IAdminLoginService _IAdminLoginService;
        private IOptions<ProjectSettings> _options;
        //private readonly ILogContext _logs;
        public AdminLoginController(IAuthenticateService iAuthenticateService, IAdminLoginService iAdminLoginService, IOptions<ProjectSettings> options)
        {
            _IAuthenticateService = iAuthenticateService;
            _IAdminLoginService = iAdminLoginService;
           // _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _options = options;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(new AuthenticateRequest());
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult UserCreated()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(AuthenticateRequest req)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = _IAuthenticateService.Authenticate(req).Result;

                    HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(res));
                    
                    if (res == null)
                    {
                        var logs = new LogResponseDTO()
                        {
                            PageName = "Index",
                            Description = "Authenticate - not Successfull",
                            HostName = Utility.Functions.GetIpAddress().HostName,
                            IpAddress = Utility.Functions.GetIpAddress().Ip,
                            created_by = _options.Value.UserNameForLog,
                            Created_date = DateTime.Now
                        };
                        //await _logs.AddLogs.InsertOneAsync(logs);
                        ViewBag.Message = "Username or password not autheticated";
                        return View();
                    }
                    else
                    {
                        var logs = new LogResponseDTO()
                        {
                            PageName = "Index",
                            Description = "Authenticate - Successfull",
                            HostName = Utility.Functions.GetIpAddress().HostName,
                            IpAddress = Utility.Functions.GetIpAddress().Ip,
                            created_by = _options.Value.UserNameForLog,
                            Created_date = DateTime.Now
                        };
                        //await _logs.AddLogs.InsertOneAsync(logs);
                        return RedirectToActionPermanent("Index", "Product");
                    }
                }
                else
                {
                    var logs = new LogResponseDTO()
                    {
                        PageName = "Index",
                        Description = "Authenticate - Successfull",
                        HostName = Utility.Functions.GetIpAddress().HostName,
                        IpAddress = Utility.Functions.GetIpAddress().Ip,
                        created_by = _options.Value.UserNameForLog,
                        Created_date = DateTime.Now
                    };
                   // await _logs.AddLogs.InsertOneAsync(logs);
                    return RedirectToActionPermanent("Index");
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new UserResponseDTO());
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserResponseDTO req)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(req);    
                }
                var result= _IAdminLoginService.CreateUser(req).Result.ToString().Replace("\"","");

                if (result == Constants.CREATEUSERSUCCESS)
                {
                    var logs = new LogResponseDTO()
                    {
                        PageName = "Index",
                        Description = "Register  CREATEUSERSUCCESS - Successfull",
                        HostName = Utility.Functions.GetIpAddress().HostName,
                        IpAddress = Utility.Functions.GetIpAddress().Ip,
                        created_by = _options.Value.UserNameForLog,
                        Created_date = DateTime.Now
                    };
                  //  await _logs.AddLogs.InsertOneAsync(logs);
                    ViewBag.register = "user created please ask to admin for email verification";
                }
                else if(result == Constants.CREATEUSERMAILSENT)
                {
                    var logs = new LogResponseDTO()
                    {
                        PageName = "Index",
                        Description = "Register  CREATEUSERMAILSENT - Successfull",
                        HostName = Utility.Functions.GetIpAddress().HostName,
                        IpAddress = Utility.Functions.GetIpAddress().Ip,
                        created_by = _options.Value.UserNameForLog,
                        Created_date = DateTime.Now
                    };
                 //   await _logs.AddLogs.InsertOneAsync(logs);
                    return RedirectToAction("UserCreated");
                }
                else if(result == Constants.CREATEUSEREXIST)
                {
                    var logs = new LogResponseDTO()
                    {
                        PageName = "Index",
                        Description = "Register  CREATEUSEREXIST - Successfull",
                        HostName = Utility.Functions.GetIpAddress().HostName,
                        IpAddress = Utility.Functions.GetIpAddress().Ip,
                        created_by = _options.Value.UserNameForLog,
                        Created_date = DateTime.Now
                    };
                 //   await _logs.AddLogs.InsertOneAsync(logs);
                    ViewBag.register = result.ToString();
                }
                else
                {
                    ViewBag.register = "Error occur while register";
                }
            }
            catch (Exception ex)
            {
                var logs = new LogResponseDTO()
                {
                    PageName = "Index",
                    Description = "Register  Error occur while register - not Successfull",
                    HostName = Utility.Functions.GetIpAddress().HostName,
                    IpAddress = Utility.Functions.GetIpAddress().Ip,
                    created_by = _options.Value.UserNameForLog,
                    Created_date = DateTime.Now
                };
               // await _logs.AddLogs.InsertOneAsync(logs);
                ViewBag.register = "Error occur while register";
                return View();
            }
            return View(req);
        }

        [HttpGet]
        [CustomActionFilter]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserInfo");
            //HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(string.Empty));
            //await HttpContext.SignOutAsync();
            var logs = new LogResponseDTO()
            {
                PageName = "Index",
                Description = "Logout -  Successfull",
                HostName = Utility.Functions.GetIpAddress().HostName,
                IpAddress = Utility.Functions.GetIpAddress().Ip,
                created_by = _options.Value.UserNameForLog,
                Created_date = DateTime.Now
            };
          //  await _logs.AddLogs.InsertOneAsync(logs);
            return RedirectToActionPermanent("Index", "AdminLogin");
        }
    }
}
