using DnsClient.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ViceArmory.API.Helpers;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.RequestObject.User;
using ViceArmory.RequestObject.Product;
using ViceArmory.ResponseObject.Product;
using System.Web;
using Microsoft.AspNetCore.Http;
using ViceArmory.DTO.ResponseObject.AppSettings;
using Microsoft.Extensions.Options;
using ViceArmory.Utility;
using ViceArmory.DTO.ResponseObject.Logs;
using ViceArmory.DTO.ResponseObject.Authenticate;
using Newtonsoft.Json;

namespace ViceArmory.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Members
        private IOptions<ProjectSettings> _options;
        private readonly IProductRepository _service;
        private readonly ILogger<ProductController> _logger;
        private readonly ILogContext _logs;
        private readonly IBaseRepository _baseRepository;
        #endregion

        #region Construction

        public ProductController(IProductRepository service, ILogger<ProductController> logger, IOptions<ProjectSettings> options, ILogContext logs, IBaseRepository baseRepo)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options;
            _logs = logs ?? throw new ArgumentNullException(nameof(logs));
            _baseRepository = baseRepo ?? throw new ArgumentNullException(nameof(baseRepo));
        }

        #endregion

        #region APIs

        ///// <summary>
        ///// Get Product
        ///// </summary>
        ///// <returns>Return Products result</returns>
        //[CustomAuthorize]
        //[HttpGet("[action]", Name = "GetProducts")]
        //[ProducesResponseType(typeof(IEnumerable<DataContract.Entities.Product>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<DataContract.Entities.Product>>> GetProducts()
        //{
        //    var response = await _service.GetProducts();
        //    if (response == null)
        //    {
        //        _logger.LogError($"Products not found.");
        //        return BadRequest(new { message = "Products not available" });
        //    }
        //    return Ok(response);
        //}

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">request object</param>
        /// <returns>Return product result</returns>
        [AllowAnonymous]
        [HttpPost("[action]", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponseDTO>> GetProduct([FromBody] string id)
        {
            _logger.LogInformation(String.Format("API-product-GetProduct-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var product = await _service.GetProduct(id);
            if (product == null)
            {
                await _baseRepository.AddLogs("Product", "get products - Not Successfull -ProductId :  " + id, _options.Value.UserNameForLog);
                return NotFound();
            }
            else
            {
                await _baseRepository.AddLogs("Product", "get products - Successfull - ProductId :  " + id, _options.Value.UserNameForLog);
                return Ok(product);

            }
        }

        /// <summary>
        /// Get product by category
        /// </summary>
        /// <param name="category">Request object</param>
        /// <returns>Return product list</returns>
        [AllowAnonymous]
        [HttpGet("[action]/{categoryId}", Name = "GetProductByCategoryId")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<ProductResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetProductByCategoryId(string categoryId)
        {
            _logger.LogInformation(String.Format("API-product-GetProductByCategoryId-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var products = await _service.GetProductByCategory(categoryId);

            if (products == null)
            {
                await _baseRepository.AddLogs("Product", "GetProductByCategoryId - Not Successfull - ProductId : " + categoryId, _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("Product", "GetProductByCategoryId - Successfull - ProductId : " + categoryId, _options.Value.UserNameForLog);
            }
            return Ok(products);
        }

        /// <summary>
        /// Create product
        /// </summary>
        /// <param name="product">Request object</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("[action]", Name = "CreateProduct")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProductRequestDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponseDTO>> CreateProduct([FromBody] ProductRequestDTO product)
        {
            _logger.LogInformation(String.Format("API-product-CreateProduct-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var user = (UserLogin)HttpContext.Items["UserLogin"];
            product.UserId = "1";// user._id;
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            product.IsDeleted = false;
            var res = new ProductResponseDTO()
            {
                //CategoryId = product.CategoryId,
                Content = product.Content,
                CreatedAt = product.CreatedAt,
                Discount = product.Discount,
                EndsAt = product.EndsAt,
                IPAddress = product.IPAddress,
                IsDeleted = product.IsDeleted,
                MetaTitle = product.MetaTitle,
                Price = product.Price,
                PublishedAt = product.PublishedAt,
                Quantity = product.Quantity,
                Shop = product.Shop,
                SKU = product.SKU,
                Slug = product.Slug,
                StartsAt = product.StartsAt,
                Title = product.Title,
                Summary = product.Title,
                Type = product.Type,
                UpdatedAt = product.UpdatedAt,
                UserId = product.UserId,
                IsWeeklyAdvertise = product.IsWeeklyAdvertise,
                //ProductImage = product.ProductImage,
                CreatedBy = product.CreatedBy,
                UpdatedBy = product.UpdatedBy
            };
            await _service.CreateProduct(res);
            if (string.IsNullOrEmpty(res.Id))
            {
                await _baseRepository.AddLogs("Product", "create products - Not Successfull - " + product.Title, _options.Value.UserNameForLog);
                return BadRequest();
            }
            else
            {
                await _baseRepository.AddLogs("Product", "create products - Successfull - " + product.Title, _options.Value.UserNameForLog);
                return CreatedAtRoute("GetProduct", new { id = res.Id }, product);
            }
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product">Request object</param>
        /// <returns>retunrn result</returns>
        [AllowAnonymous]
        [HttpPost("[action]", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(ProductResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductResponseDTO product)

        {
            _logger.LogInformation(String.Format("API-product-UpdateProduct-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var user = (UserLogin)HttpContext.Items["UserLogin"];
            product.UserId = "1";// user._id;

            product.UpdatedAt = DateTime.Now;
            product.UserId = user != null ? user._id : "";
            product.IsDeleted = false;
            var res = await _service.UpdateProduct(new ProductResponseDTO()
            {
                //CategoryId = product.CategoryId,
                Content = product.Content,
                CreatedAt = product.CreatedAt,
                Discount = product.Discount,
                EndsAt = product.EndsAt,
                ProductImage = product.ProductImage,
                IPAddress = product.IPAddress,
                IsDeleted = product.IsDeleted,
                MetaTitle = product.MetaTitle,
                Price = product.Price,
                PublishedAt = product.PublishedAt,
                Quantity = product.Quantity,
                Shop = product.Shop,
                SKU = product.SKU,
                Slug = product.Slug,
                StartsAt = product.StartsAt,
                Title = product.Title,
                Summary = product.Title,
                Type = product.Type,
                UpdatedAt = product.UpdatedAt,
                UserId = product.UserId,
                Id = product.Id,
                IsWeeklyAdvertise = product.IsWeeklyAdvertise,
            });
            if (!res)
            {
                await _baseRepository.AddLogs("Product", "update products - Not Successfull - " + product.Title + ", " + product.Id, _options.Value.UserNameForLog);
                return BadRequest(new { message = "Products not Updated." });
            }
            else
            {
                await _baseRepository.AddLogs("Product", "update products - Successfull - " + product.Title + ", " + product.Id, _options.Value.UserNameForLog);
                return Ok(res);
            }
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>return result</returns>
        [AllowAnonymous]
        [HttpPost("[action]", Name = "DeleteProductById")]
        [ProducesResponseType(typeof(ProductResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById([FromBody] string id)
        {
            _logger.LogInformation(String.Format("API-product-DeleteProductById-Name : {0} and ip : {1}", Functions.GetIpAddress().HostName, Functions.GetIpAddress().Ip));
            var res = await _service.DeleteProduct(id);
            if (!res)
            {
                await _baseRepository.AddLogs("Product", "delete products - Not Successfull -ProductId :  " + id, _options.Value.UserNameForLog);
                return BadRequest(new { message = "Products not Updated." });
            }
            else
            {
                await _baseRepository.AddLogs("Product", "delete products - Successfull -ProductId :  " + id, _options.Value.UserNameForLog);
                return Ok(res);
            }
        }

        /// <summary>
        /// Get product attributes by product id
        /// </summary>
        /// <param name="productId">request object</param>
        /// <returns>return list of ProductAttribute</returns>
        [AllowAnonymous]
        [HttpGet("[action]/{productId}", Name = "GetProductAttributesByProductId")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductAttributeResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductAttributeResponseDTO>> GetProductAttributesByProductId(string productId)
        {
            var res = await _service.GetProductAttributesByProductId(productId);

            if (res == null)
            {
                await _baseRepository.AddLogs("Product", "GetProductAttributesByProductId - Not Successfull -ProductId :  " + productId, _options.Value.UserNameForLog);
                return NotFound();
            }
            else
            {
                await _baseRepository.AddLogs("Product", "GetProductAttributesByProductId - Successfull -ProductId :  " + productId, _options.Value.UserNameForLog);
                return Ok(res);
            }
        }

        /// <summary>
        /// Get product review by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return List of ProductReview</returns>
        [AllowAnonymous]
        [HttpGet("[action]/{productId}", Name = "GetProductReviewsByProductId")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductReviewResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductReviewResponseDTO>> GetProductReviewsByProductId(string productId)
        {
            var res = await _service.GetProductReviewsByProductId(productId);
            if (res == null)
            {
                await _baseRepository.AddLogs("Product", "GetProductReviewsByProductId - Not Successfull -ProductId :  " + productId, _options.Value.UserNameForLog);
                return NotFound();
            }
            else
            {
                await _baseRepository.AddLogs("Product", "GetProductReviewsByProductId - Successfull -ProductId :  " + productId, _options.Value.UserNameForLog);
                return Ok(res);
            }
        }

        /// <summary>
        /// Get Product images
        /// </summary>
        /// <param name="productId">Request Object</param>
        /// <returns>Return list of product image</returns>
        [AllowAnonymous]
        [HttpGet("[action]/{productId}", Name = "GetProductImagesByProductId")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductImageResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductImageResponseDTO>> GetProductImagesByProductId(string productId)
        {
            var res = await _service.GetProductImagesByProductId(productId);
            if (res == null)
            {
                await _baseRepository.AddLogs("Product", "GetProductImagesByProductId - Not Successfull -ProductId :  " + productId, _options.Value.UserNameForLog);
                return NotFound();
            }
            else
            {
                await _baseRepository.AddLogs("Product", "GetProductImagesByProductId - Successfull -ProductId :  " + productId, _options.Value.UserNameForLog);
                return Ok(res);
            }
        }

        /// <summary>
        /// Add product attributes
        /// </summary>
        /// <param name="productAttributes">Request object</param>
        /// <returns></returns>
        [CustomAuthorize]
        [HttpPost("[action]", Name = "AddProductAttributes")]
        [ProducesResponseType(typeof(ProductAttributeRequestDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductAttributeRequestDTO>> AddProductAttributes([FromBody] List<ProductAttributeRequestDTO> req)
        {
            List<ProductAttributeResponseDTO> list = new List<ProductAttributeResponseDTO>();
            foreach (var item in req)
            {
                list.Add(new ProductAttributeResponseDTO()
                {
                    CreatedAt = DateTime.Now,
                    Key = item.Key,
                    Name = item.Name,
                    ProductId = item.ProductId,
                    UpdatedAt = DateTime.Now,
                    Id = ""
                });
            }
            await _service.AddProductAttributes(list);
            if (list.Count > 0)
            {
                await _baseRepository.AddLogs("Product", "AddProductAttributes - Successfull - ProductId : " + req[0].ProductId, _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("Product", "AddProductAttributes -Not Successfull - ProductId : " + req[0].ProductId, _options.Value.UserNameForLog);
            }
            return CreatedAtRoute("GetProductAttributesByProductId", new { productId = req[0].ProductId }, req);
        }

        /// <summary>
        /// Add product review
        /// </summary>
        /// <param name="productReview">Request object</param>
        /// <returns></returns>
        [CustomAuthorize]
        [HttpPost("[action]", Name = "AddProductReview")]
        [ProducesResponseType(typeof(ProductReviewRequestDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductReviewResponseDTO>> AddProductReview([FromBody] ProductReviewRequestDTO productReview)
        {
            var user = (UserLogin)HttpContext.Items["UserLogin"];
            productReview.UserId = user._id;
            productReview.CreatedAt = DateTime.Now;
            await _service.AddProductReview(new ProductReviewResponseDTO()
            {
                IPAddress = productReview.IPAddress,
                CreatedAt = productReview.CreatedAt,
                ParentId = productReview.ParentId,
                ProductId = productReview.ProductId,
                Published = productReview.Published,
                PublishedAt = productReview.PublishedAt,
                Rating = productReview.Rating,
                Title = productReview.Title,
                UserId = productReview.UserId,
                Id = ""
            });
            if (productReview != null)
            {
                await _baseRepository.AddLogs("Product", "AddProductReview - Successfull - ProductReviewId : " + productReview.Id, _options.Value.UserNameForLog);
            }
            else
            {
                await _baseRepository.AddLogs("Product", "AddProductReview - Not Successfull - ProductReviewId : " + productReview.Id, _options.Value.UserNameForLog);
            }
            return CreatedAtRoute("GetProductReviewsByProductId", new { productId = productReview.ProductId }, productReview);
        }

        /// <summary>
        /// Add product images
        /// </summary>
        /// <param name="productImages">Request object</param>
        /// <returns></returns>
        [CustomAuthorize]
        [HttpPost("[action]", Name = "AddProductImages")]
        [ProducesResponseType(typeof(ProductImageRequest), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductImageResponseDTO>> AddProductImages([FromForm] ProductImageRequest productImagesReq)
        {
            var user = (UserLogin)HttpContext.Items["UserLogin"];
            productImagesReq.UserId = user._id;
            productImagesReq.CreatedAt = DateTime.Now;
            productImagesReq.UpdatedAt = DateTime.Now;
            await _service.AddProductImage(productImagesReq);
            if (productImagesReq.UserId != "")
            {
                await _baseRepository.AddLogs("Product", "AddProductImages products -Not Successfull - productImageId : " + productImagesReq.Id, _options.Value.UserNameForLog);
                return BadRequest(new { message = "Products not deleted." });
            }
            else
            {
                await _baseRepository.AddLogs("Product", "AddProductImages products - Successfull - productImageId : " + productImagesReq.Id, _options.Value.UserNameForLog);
                return Ok("ok");
            }
        }

        /// <summary>
        /// Get Product
        /// </summary>
        /// <param name="query">Request object</param>
        /// <returns>Return Products</returns>
        [AllowAnonymous]
        [HttpGet("[action]", Name = "GetProducts")]
        [ProducesResponseType(typeof(ProductResponseDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponseDTO>> GetProducts()
        {
            try
            {
                var res = await _service.GetProducts();

                string HostName = Dns.GetHostName();
                IPAddress[] add = Dns.GetHostAddresses(HostName);
                string IpAddress = "";
                foreach (IPAddress ip in add)
                {
                    IpAddress = ip.ToString();
                }
                if (res != null)
                {
                    await _baseRepository.AddLogs("Product", "get products - Successfull", "TEst");
                }
                else
                {
                    await _baseRepository.AddLogs("Product", "get products - Not Successfull", _options.Value.UserNameForLog);
                }
                #region for createproduct
                //await _service.CreateProductList(res.ToList());
                #endregion


                return Ok(res);
            }
            catch (Exception ex)
            {
                await _baseRepository.AddLogs("Product", "get products - Not Successfull", _options.Value.UserNameForLog);
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Get Product
        /// </summary>
        /// <param name="query">Request object</param>
        /// <returns>Return Product</returns>
        [AllowAnonymous]
        [HttpGet("[action]", Name = "GetProductsWithImage")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponseDTO>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ProductResponseDTO>>> GetProductsWithImage([FromQuery] ProductQueryRequestDTO query)
        {
            var res = await _service.GetProductsWithImage(query);   
            await _baseRepository.AddLogs("Product", "GetProductsWithImage - Successfull ", _options.Value.UserNameForLog);
            return Ok(res);
        }
        /// <summary>
        /// Create product
        /// </summary>
        /// <param name="product">Request object</param>
        /// <returns></returns>
        [CustomAuthorize]
        [HttpPost("[action]", Name = "UploadPDF")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ProductRequestDTO), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponseDTO>> UploadPDF([FromBody] ProductRequestDTO product)
        {
            var user = (UserLogin)HttpContext.Items["UserLogin"];
            product.UserId = "1";//user._id;
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            product.IsDeleted = false;
            var res = new ProductResponseDTO()
            {
                //CategoryId = product.CategoryId,
                Content = product.Content,
                CreatedAt = product.CreatedAt,
                Discount = product.Discount,
                EndsAt = product.EndsAt,
                IPAddress = product.IPAddress,
                IsDeleted = product.IsDeleted,
                MetaTitle = product.MetaTitle,
                Price = product.Price,
                PublishedAt = product.PublishedAt,
                Quantity = product.Quantity,
                Shop = product.Shop,
                SKU = product.SKU,
                Slug = product.Slug,
                StartsAt = product.StartsAt,
                Title = product.Title,
                Summary = product.Title,
                Type = product.Type,
                UpdatedAt = product.UpdatedAt,
                UserId = product.UserId,
                IsWeeklyAdvertise = product.IsWeeklyAdvertise
            };
            await _service.CreateProduct(res);
            if (string.IsNullOrEmpty(res.Id))
            {
                await _baseRepository.AddLogs("Product", "UploadPDF - not Successfull ", _options.Value.UserNameForLog);
                return BadRequest();
            }
            else
            {
                await _baseRepository.AddLogs("Product", "UploadPDF - Successfull ", _options.Value.UserNameForLog);
                return CreatedAtRoute("GetProduct", new { id = res.Id }, product);
            }
        }


        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product">Request object</param>
        /// <returns>retunrn result</returns>
        [AllowAnonymous]
        [HttpPost("[action]", Name = "UpdateProductImage")]
        [ProducesResponseType(typeof(ProductResponseImgUploadDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProductImage([FromForm] ProductResponseImgUploadDTO product)
        {
            try
            {
                var file = product.UploadedProductImages;
                var id = Request.Form["guid"];
                var type = Request.Form["type"];
                var docType = file.FileName;//docType = "userPhoto" for user photo
                var fileName = "";
                var fPath = "";
                var FileDic = "ProductImage";
                string pathToSave = Path.Combine("D:/projects/Vice_Armory/vicearmory_front/ProductImage", FileDic);
                product.ProductImages[0] = "D:/projects/Vice_Armory/vicearmory_front/ProductImage/" + product.Title + '/' + file.FileName;
                if (file.Length > 0)
                {
                    fileName = docType;
                    pathToSave = Path.Combine(pathToSave, product.Title);
                    if (!Directory.Exists(pathToSave))
                    {
                        Directory.CreateDirectory(pathToSave);
                    }
                    string[] array1 = Directory.GetFiles(pathToSave);
                    foreach (string arr in array1)
                    {
                        if (Path.GetFileNameWithoutExtension(arr) == docType)
                        {
                            System.IO.File.Delete(arr);
                        }
                    }
                    fPath = Path.Combine(pathToSave, fileName);
                    using (var stream = new FileStream(fPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                var user = (UserLogin)HttpContext.Items["UserLogin"];
                product.UpdatedAt = DateTime.Now;
                product.UserId = "1";//user._id;
                product.IsDeleted = false;
                var res = await _service.UpdateProduct(new ProductResponseDTO()
                {
                    UpdatedAt = product.UpdatedAt,
                    Id = product.Id,
                    Content = product.Content,
                    CreatedAt = product.CreatedAt,
                    Discount = product.Discount,
                    EndsAt = product.EndsAt,
                    IPAddress = product.IPAddress,
                    IsDeleted = product.IsDeleted,
                    MetaTitle = product.MetaTitle,
                    Price = product.Price,
                    PublishedAt = product.PublishedAt,
                    Quantity = product.Quantity,
                    Shop = product.Shop,
                    SKU = product.SKU,
                    Slug = product.Slug,
                    Title = product.Title,
                    Summary = product.Title,
                    Type = product.Type,
                    UserId = product.UserId,
                    IsWeeklyAdvertise = product.IsWeeklyAdvertise,
                    //ProductImage = product.ProductImages
                }); ;
                if (!res)
                {
                    await _baseRepository.AddLogs("Product", "UpdateProductImage - not Successfull " + product.ProductImages, _options.Value.UserNameForLog);
                    return BadRequest(new { message = "Products not Updated." });
                }
                else
                {
                    await _baseRepository.AddLogs("Product", "UpdateProductImage - Successfull " + product.ProductImages, _options.Value.UserNameForLog);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]", Name = "ChangeProductImage")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<bool> ChangeProductImage(string id)
        {
            var httpRequest = this.HttpContext.Request.Form;
            if (httpRequest.Files.Count > 0)
            {
                foreach (IFormFile file in httpRequest.Files)
                {
                    string newFileName = "";
                    if (file != null)
                    {
                        var FilePath = _options.Value.ProductImagePath;
                        if (!Directory.Exists(FilePath))
                            Directory.CreateDirectory(FilePath);
                        var stream = file.OpenReadStream();
                        var fileName = file.FileName;
                        string extension = System.IO.Path.GetExtension(fileName);
                        newFileName = Guid.NewGuid() + extension;
                        var filePath = Path.Combine(FilePath, newFileName);
                        using (FileStream fs = System.IO.File.Create(filePath))
                        {
                            try
                            {
                                file.CopyTo(fs);
                            }
                            catch (Exception e)
                            {

                                throw;
                            }
                        }
                    }
                    var product = await _service.GetProduct(id);
                    //product.ProductImage = _options.Value.ProjectUrl + "wwwroot/uploads/productimages/" + newFileName;
                    var result = await _service.UpdateProduct(product);
                    //if (!result)
                    //{
                    //    var logs = new LogResponseDTO()
                    //    {
                    //        PageName = "Product",
                    //        Description = "ChangeProductImage - not Successfull " + product.ProductImage,
                    //        HostName = Functions.GetIpAddress().HostName,
                    //        IpAddress = Functions.GetIpAddress().Ip,
                    //        created_by = _options.Value.UserNameForLog,
                    //        Created_date = DateTime.Now
                    //    };
                    //    await _logs.AddLogs.InsertOneAsync(logs);

                    //}
                    //else
                    //{
                    //    var logs = new LogResponseDTO()
                    //    {
                    //        PageName = "Product",
                    //        Description = "ChangeProductImage -  Successfull " + product.ProductImage,
                    //        HostName = Functions.GetIpAddress().HostName,
                    //        IpAddress = Functions.GetIpAddress().Ip,
                    //        created_by = _options.Value.UserNameForLog,
                    //        Created_date = DateTime.Now
                    //    };
                    //    await _logs.AddLogs.InsertOneAsync(logs);
                    //    
                    //}
                    return result;
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
