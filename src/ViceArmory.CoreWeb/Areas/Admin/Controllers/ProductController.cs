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
using ViceArmory.CoreWeb.Models;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.DTO.ResponseObject.WeeklyAds;
using ViceArmory.Middleware.Interface;
using ViceArmory.RequestObject.Product;
using ViceArmory.ResponseObject.Product;
using ViceArmory.Utility;

namespace ViceArmory.CoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        IProductService _IProductService;
        IWeeklyAdsService _IWeeklyAdsService;
        private IOptions<ProjectSettings> _options;
        private readonly ILogger _logger;

        public ProductController(IProductService iProductService, IWeeklyAdsService iWeeklyAdsService, IOptions<ProjectSettings> options, ILogger<ProductController> logger)
        {
            _IProductService = iProductService;
            _IWeeklyAdsService = iWeeklyAdsService;
            _options = options;
            _logger = logger;
        }
        
        [HttpGet]
        [CustomActionFilter]
        public async Task<IActionResult> index()
        {

            var a = HttpContext.Connection;
            _logger.LogInformation(String.Format("Web-product-index-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var objSession = HttpContext.Session.GetString("UserInfo");
            if (!string.IsNullOrEmpty(objSession))
            {
                var authRes = JsonConvert.DeserializeObject<AuthenticateResponse>(HttpContext.Session.GetString("UserInfo"));
                _IProductService.SetSession(authRes);
            }
            var res = await _IProductService.GetProducts();

            return View(res);
        }
        
        [HttpGet]
        [CustomActionFilter]
        public IActionResult Create()
        {
            _logger.LogInformation(String.Format("Web-product-create-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            return View(new ProductRequestDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductRequestDTO req,IFormFile productImage)
        {
            if (!ModelState.IsValid)
                return View(req);

            if(productImage != null)
            {
                _IProductService.AddImage("wwwroot/uploads/productimages", productImage);

            req.ProductImage = _options.Value.ProjectUrl + "wwwroot/uploads/productimages" + productImage.FileName;
            }
            AuthenticateResponse authRes = null;
            var objSession = HttpContext.Session.GetString("UserInfo");
            if (!string.IsNullOrEmpty(objSession))
            {
                authRes = JsonConvert.DeserializeObject<AuthenticateResponse>(HttpContext.Session.GetString("UserInfo"));
                _IProductService.SetSession(authRes);
      

            }
           // req.UserId = objSession["UserName"];
            req.CreatedAt = DateTime.Now;
            var res = await _IProductService.CreateProduct(req);
            _logger.LogInformation(String.Format("Web-product-created-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            return RedirectToActionPermanent("index");
        }
        
        [HttpGet]
        public async Task<IActionResult> EditProduct(string id)
        {
            _logger.LogInformation(String.Format("Web-product-editproduct-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var res = _IProductService.GetProduct(id).Result;
            AuthenticateResponse authRes = null;
            var objSession = HttpContext.Session.GetString("UserInfo");
            if (!string.IsNullOrEmpty(objSession))
            {
                authRes = JsonConvert.DeserializeObject<AuthenticateResponse>(HttpContext.Session.GetString("UserInfo"));
                _IProductService.SetSession(authRes);
            }
            return View(new ProductRequestDTO()
            {
                Id = res.Id,
                CategoryId = res.CategoryId,
                Content = res.Content,
                CreatedAt = res.CreatedAt,
                Discount = res.Discount,
                EndsAt = res.EndsAt,
                IPAddress = res.IPAddress,
                IsDeleted = res.IsDeleted,
                IsWeeklyAdvertise = res.IsWeeklyAdvertise,
                MetaTitle = res.MetaTitle,
                Price = res.Price,
                PublishedAt = res.PublishedAt,
                Quantity = res.Quantity,
                Shop = res.Shop,
                SKU = res.SKU = res.SKU,
                Slug = res.Slug,
                StartsAt = res.StartsAt,
                Summary = res.Summary,
                Title = res.Title,
                Type = res.Type,
                //ProductImage=res.ProductImage,
                UpdatedAt = res.UpdatedAt,
                UserId =""
            });
        }
        
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductRequestDTO res,IFormFile productImagefile)
        {
           
            if (!ModelState.IsValid)
                return View(res);

            if(productImagefile!=null)
            {
            
                var FilePath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/uploads/productimages");

                if (!Directory.Exists(FilePath))
                    Directory.CreateDirectory(FilePath);
                var stream = productImagefile.OpenReadStream();
                var fileName = productImagefile.FileName;
                string extension = System.IO.Path.GetExtension(fileName);
                var newFileName = Guid.NewGuid()  + extension;
                var filePath = Path.Combine(FilePath, newFileName);
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    productImagefile.CopyTo(fs);
                }
                res.ProductImage = _options.Value.ProjectUrl + "wwwroot/uploads/productimages/" + newFileName;
            }            

            AuthenticateResponse authRes = null;
            var objSession = HttpContext.Session.GetString("UserInfo");
            if (!string.IsNullOrEmpty(objSession))
            {
                authRes = JsonConvert.DeserializeObject<AuthenticateResponse>(HttpContext.Session.GetString("UserInfo"));
                _IProductService.SetSession(authRes);
            }

            await _IProductService.UpdateProduct(new ProductResponseDTO()
            {
                Id = res.Id,
                CategoryId = res.CategoryId,
                Content = res.Content,
                CreatedAt = res.CreatedAt,
                Discount = res.Discount,
                EndsAt = res.EndsAt,
                //ProductImage=res.ProductImage,
                IPAddress = res.IPAddress,
                IsDeleted = res.IsDeleted,
                IsWeeklyAdvertise = res.IsWeeklyAdvertise,
                MetaTitle = res.MetaTitle,
                Price = res.Price,
                PublishedAt = res.PublishedAt,
                Quantity = res.Quantity,
                Shop = res.Shop,
                SKU = res.SKU = res.SKU,
                Slug = res.Slug,
                StartsAt = res.StartsAt,
                Summary = res.Summary,
                Title = res.Title,
                Type = res.Type,
                UpdatedAt = DateTime.Now,
                UserId = authRes?.Id
            });
            _logger.LogInformation(String.Format("Web-product-edited-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            return RedirectToActionPermanent("index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var objSession = HttpContext.Session.GetString("UserInfo");
            if (!string.IsNullOrEmpty(objSession))
            {
                var authRes = JsonConvert.DeserializeObject<AuthenticateResponse>(HttpContext.Session.GetString("UserInfo"));
                _IProductService.SetSession(authRes);
            }

            await _IProductService.DeleteProduct(id);
            return RedirectToActionPermanent("index");
        }


        [HttpGet]
        [CustomActionFilter]
        public IActionResult AddProductImages(string id)
        {

            var res = _IProductService.GetProduct(id).Result;
            AuthenticateResponse authRes = null;
            var objSession = HttpContext.Session.GetString("UserInfo");
            if (!string.IsNullOrEmpty(objSession))
            {
                authRes = JsonConvert.DeserializeObject<AuthenticateResponse>(HttpContext.Session.GetString("UserInfo"));
                _IProductService.SetSession(authRes);
            }
            //return View(new ProductImageRequest()
            //{
            //    ProductId = res.Id,
            //    UpdatedAt = res.UpdatedAt,
            //    UserId = authRes.Id
            //});
            return RedirectToActionPermanent("AddProductImages");


        }

        [HttpPost]
        [CustomActionFilter]
        public async Task<ActionResult<ProductImageResponseDTO>> AddProductImages([FromForm] ProductImageRequest productImagesReq)
        {
            AuthenticateResponse authRes = null;
            var objSession = HttpContext.Session.GetString("UserInfo");
            if (!string.IsNullOrEmpty(objSession))
            {
                authRes = JsonConvert.DeserializeObject<AuthenticateResponse>(HttpContext.Session.GetString("UserInfo"));
                _IProductService.SetSession(authRes);
            }
            productImagesReq.UserId = authRes.Id;
            productImagesReq.CreatedAt = DateTime.Now;
            productImagesReq.IPAddress = "";
            await _IProductService.AddProductImage(productImagesReq);
            return RedirectToActionPermanent("index");
        }

        public async Task<IActionResult> indexPDF()
        {
            ProductRequestDTO prodcuts = new ProductRequestDTO();
            var fixedPath = Path.Combine(Directory.GetCurrentDirectory());
            var relativeFolderName = Path.Combine(fixedPath, "Files");
            var absolutePath = relativeFolderName;
            string[] filePaths = Directory.Exists(absolutePath) ? Directory.GetFiles(absolutePath) : null;
            //prodcuts. = filePaths;
            if (filePaths == null)
                return null;
            return View(prodcuts);
        }
        [HttpGet]
        public async Task<IActionResult> uploadPDF()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadFileProduct(IFormFile file)
        {
            try
            {
                WeeklyAdsResponseDTO _weeklyads = new WeeklyAdsResponseDTO();
                var FileDic = "Files";
                var FilePath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/uploads");

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
                _weeklyads.Description = "Vice Armory Ads";
                _weeklyads.FilePath = _options.Value.ProjectUrl+ "wwwroot/uploads/" + file.FileName;
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
        
    }
}



    