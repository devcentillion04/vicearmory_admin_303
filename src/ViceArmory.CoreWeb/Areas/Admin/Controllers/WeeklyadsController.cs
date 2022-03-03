using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.WeeklyAds;
using ViceArmory.Middleware.Interface;
using ViceArmory.Utility;

namespace ViceArmory.CoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WeeklyadsController : Controller
    {
        IWeeklyAdsService _IWeeklyAdsService;
        private IOptions<ProjectSettings> _options;
        private readonly ILogger _logger;
        public WeeklyadsController(IWeeklyAdsService iWeeklyAdsService, IOptions<ProjectSettings> options, ILogger<ProductController> logger)
        {

            _IWeeklyAdsService = iWeeklyAdsService;
            _options = options;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _logger.LogInformation(String.Format("Web-Weeklyads-Index-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var result=_IWeeklyAdsService.GetAllPdf().Result;
            return View(result);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(IFormFile file)
        {
            try
            {
                _logger.LogInformation(String.Format("Web-Weeklyads-Create-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
                WeeklyAdsResponseDTO _weeklyads = new WeeklyAdsResponseDTO();
                var FileDic = "Files";
                var FilePath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot\\uploads");

                if (!Directory.Exists(FilePath))
                    Directory.CreateDirectory(FilePath);
                var stream = file.OpenReadStream();
                var fileName = file.FileName;
                var filePath = Path.Combine(FilePath, file.FileName);
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                }

                _weeklyads.Id = Guid.NewGuid().ToString();
                _weeklyads.Description = "Test";
                _weeklyads.FilePath = _options.Value.ProjectUrl + "uploads/" + file.FileName;
                _weeklyads.CreatedAt = DateTime.Now;
                _weeklyads.IsDeleted = false;
                _IWeeklyAdsService.AddPdf(_weeklyads);
                return RedirectToActionPermanent("index");
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }

        public ActionResult DeleteAds(string id)
        {
            _logger.LogInformation(String.Format("Web-Weeklyads-Delete-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var result = _IWeeklyAdsService.DeletePdf(id);
            return Json(result);
        }
        public ActionResult ActivateAds(string id)
        {
            _logger.LogInformation(String.Format("Web-ActivateAds-Create-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var result = _IWeeklyAdsService.ActivatePdf(id);
            return Json(result);
        }
    }
}
