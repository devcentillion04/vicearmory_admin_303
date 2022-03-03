using Authenticate.DataContract;
using DnsClient.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Product.API.Helpers;
using Product.DataContract.Entities;
using Product.Repositories.Interfaces;
using Product.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Members

        private readonly IProductService _service;
        private readonly ILogger<ProductController> _logger;

        #endregion

        #region Construction

        public ProductController(IProductService service, ILogger<ProductController> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
        [HttpGet("[action]/{id}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(DataContract.Entities.Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DataContract.Entities.Product>> GetProductById(string id)
        {
            var product = await _service.GetProduct(id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, not found.");
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Get product by category
        /// </summary>
        /// <param name="category">Request object</param>
        /// <returns>Return product list</returns>
        [AllowAnonymous]
        [HttpGet("[action]/{categoryId}", Name = "GetProductByCategoryId")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(IEnumerable<DataContract.Entities.Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DataContract.Entities.Product>>> GetProductByCategoryId(string categoryId)
        {
            var products = await _service.GetProductByCategory(categoryId);
            if (products == null)
            {
                _logger.LogError($"Product with category: {categoryId}, not found.");
                return NotFound();
            }
            return Ok(products);
        }

        /// <summary>
        /// Create product
        /// </summary>
        /// <param name="product">Request object</param>
        /// <returns></returns>
        [CustomAuthorize]
        [HttpPost("[action]", Name = "CreateProduct")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(DataContract.Entities.Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DataContract.Entities.Product>> CreateProduct([FromBody] DataContract.Entities.Product product)
        {
            var user = (UserLogin)HttpContext.Items["UserLogin"];
            product.UserId = user._id;
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            product.IsDeleted = false;
            await _service.CreateProduct(product);
            if (string.IsNullOrEmpty(product.Id))
                return BadRequest();

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product">Request object</param>
        /// <returns>retunrn result</returns>
        [CustomAuthorize]
        [HttpPut("[action]", Name = "UpdateProduct")]
        [ProducesResponseType(typeof(DataContract.Entities.Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] DataContract.Entities.Product product)
        {
            var user = (UserLogin)HttpContext.Items["UserLogin"];
            product.UpdatedAt = DateTime.Now;
            product.UserId = user._id;
            product.IsDeleted = false;
            var res = await _service.UpdateProduct(product);
            if (!res)
            {
                _logger.LogError($"Products not Updated.");
                return BadRequest(new { message = "Products not Updated." });
            }
            return Ok(res);
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>return result</returns>
        [CustomAuthorize]
        [HttpDelete("[action]/{id}", Name = "DeleteProductById")]
        [ProducesResponseType(typeof(DataContract.Entities.Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            var res = await _service.DeleteProduct(id);
            if (!res)
            {
                _logger.LogError($"Products not deleted.");
                return BadRequest(new { message = "Products not deleted." });
            }
            return Ok(res);
        }

        /// <summary>
        /// Get product attributes by product id
        /// </summary>
        /// <param name="productId">request object</param>
        /// <returns>return list of ProductAttribute</returns>
        [AllowAnonymous]
        [HttpGet("[action]/{productId}", Name = "GetProductAttributesByProductId")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductAttribute), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductAttribute>> GetProductAttributesByProductId(string productId)
        {
            var res = await _service.GetProductAttributesByProductId(productId);

            if (res == null)
            {
                _logger.LogError($"Product with id: {productId}, not found.");
                return NotFound();
            }

            return Ok(res);
        }

        /// <summary>
        /// Get product review by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return List of ProductReview</returns>
        [AllowAnonymous]
        [HttpGet("[action]/{productId}", Name = "GetProductReviewsByProductId")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductReview), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductReview>> GetProductReviewsByProductId(string productId)
        {
            var res = await _service.GetProductReviewsByProductId(productId);

            if (res == null)
            {
                _logger.LogError($"Product with id: {productId}, not found.");
                return NotFound();
            }

            return Ok(res);
        }

        /// <summary>
        /// Get Product images
        /// </summary>
        /// <param name="productId">Request Object</param>
        /// <returns>Return list of product image</returns>
        [AllowAnonymous]
        [HttpGet("[action]/{productId}", Name = "GetProductImagesByProductId")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ProductImage), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductImage>> GetProductImagesByProductId(string productId)
        {
            var res = await _service.GetProductImagesByProductId(productId);

            if (res == null)
            {
                _logger.LogError($"Product with id: {productId}, not found.");
                return NotFound();
            }

            return Ok(res);
        }

        /// <summary>
        /// Add product attributes
        /// </summary>
        /// <param name="productAttributes">Request object</param>
        /// <returns></returns>
        [CustomAuthorize]
        [HttpPost("[action]", Name = "AddProductAttributes")]
        [ProducesResponseType(typeof(ProductAttribute), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductAttribute>> AddProductAttributes([FromBody] List<ProductAttribute> productAttributes)
        {
            productAttributes.ForEach(l => l.CreatedAt = l.UpdatedAt = DateTime.Now);
            await _service.AddProductAttributes(productAttributes);

            return CreatedAtRoute("GetProductAttributesByProductId", new { productId = productAttributes[0].ProductId }, productAttributes);
        }

        /// <summary>
        /// Add product review
        /// </summary>
        /// <param name="productReview">Request object</param>
        /// <returns></returns>
        [CustomAuthorize]
        [HttpPost("[action]", Name = "AddProductReview")]
        [ProducesResponseType(typeof(ProductReview), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductReview>> AddProductReview([FromBody] ProductReview productReview)
        {
            var user = (UserLogin)HttpContext.Items["UserLogin"];
            productReview.UserId = user._id;
            productReview.CreatedAt = DateTime.Now;
            await _service.AddProductReview(productReview);

            return CreatedAtRoute("GetProductReviewsByProductId", new { productId = productReview.ProductId }, productReview);
        }

        /// <summary>
        /// Add product images
        /// </summary>
        /// <param name="productImages">Request object</param>
        /// <returns></returns>
        [CustomAuthorize]
        [HttpPost("[action]", Name = "AddProductImages")]
        [ProducesResponseType(typeof(ProductImage), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductImage>> AddProductImages([FromForm] ProductImageRequest productImagesReq)
        {
            var user = (UserLogin)HttpContext.Items["UserLogin"];
            productImagesReq.UserId = user._id;
            await _service.AddProductImage(productImagesReq);

            return Ok();
        }


        /// <summary>
        /// Get Product
        /// </summary>
        /// <param name="query">Request object</param>
        /// <returns>Return Product</returns>
        [AllowAnonymous]
        [HttpGet("[action]", Name = "GetProducts")]
        [ProducesResponseType(typeof(IEnumerable<DataContract.Entities.Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DataContract.Entities.Product>>> GetProducts([FromQuery] ProductQuery query)
        {
            var res = await _service.GetProducts(query);
            return Ok(res);
        }

        #endregion
    }
}
