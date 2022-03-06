using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Middleware.Infrastructure;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.Middleware.Interface;
using ViceArmory.RequestObject.Product;
using ViceArmory.ResponseObject.Product;

namespace ViceArmory.Middleware.Service
{
    public class ProductService : IProductService
    {
        #region Members

        //private readonly IProductRepository _iProductRepository;
        //private readonly FileStorageConfig _fileStorageConfig;
        //private readonly IHostEnvironment _hostEnvironment;
        //private readonly ILogger<ProductRepository> _logger;
        private const string PRODUCT_IMAGE_LOCATION = "{0}/products/{1}/";
        private IOptions<APISettings> _options;
        private AuthenticateResponse _UserInfo;
        #endregion

        #region Construction
        public ProductService(IOptions<APISettings> options)
        {
            _options = options;
            //_iProductRepository = iProductRepository;
            //_fileStorageConfig = fileStorageConfig.Value;
            //_hostEnvironment = hostEnvironment;
            //_logger = logger;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns></returns>
        public async Task<bool> CreateProduct(ProductRequestDTO product)
        {
            try
            {
                product.CreatedBy= _UserInfo.UserName;
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.CREATEPRODUCT, product, _options.Value.APIUrl, _UserInfo);
                ProductResponseDTO res = JsonConvert.DeserializeObject<ProductResponseDTO>(responseString);
                if (responseString.ToLower() == "ok")
                    return true;
                return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return delete result</returns>
        public async Task<bool> DeleteProduct(string id)
        {
            bool res = false;
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.DELETEPRODUCTBYID, id, _options.Value.APIUrl, _UserInfo);

                //Convert the string in to desired object.
              return  res = JsonConvert.DeserializeObject<bool>(responseString);

            }
            catch
            {
                throw;
            }
           
        }

        /// <summary>
        /// Get product
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return product result</returns>
        public async Task<ProductResponseDTO> GetProduct(string id)
        {
            ProductResponseDTO res = new ProductResponseDTO();
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.GETPRODUCT, id, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<ProductResponseDTO>(responseString);

            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Get product attributes by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductAttribute result</returns>
        public async Task<IEnumerable<ProductAttributeResponseDTO>> GetProductAttributesByProductId(string productId)
        {
            List<ProductAttributeResponseDTO> res = null;
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.GETPRODUCTATTRIBUTESBYPRODUCTID, productId, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<List<ProductAttributeResponseDTO>>(responseString);

            }
            catch
            {
                throw;
            }
            return res;
        }

        /// <summary>
        /// Get Product by category
        /// </summary>
        /// <param name="categoryName">Request object</param>
        /// <returns>Return product result</returns>
        public async Task<IEnumerable<ProductResponseDTO>> GetProductByCategory(string categoryId)
        {
            List<ProductResponseDTO> res = null;
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.GETPRODUCTBYCATEGORYID, categoryId, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<List<ProductResponseDTO>>(responseString);

            }
            catch
            {
                throw;
            }
            return res;
        }


        /// <summary>
        /// Get product review by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductReview</returns>
        public async Task<IEnumerable<ProductReviewResponseDTO>> GetProductReviewsByProductId(string productId)
        {
            List<ProductReviewResponseDTO> res = null;
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.GETPRODUCTREVIEWSBYPRODUCTID, productId, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<List<ProductReviewResponseDTO>>(responseString);

            }
            catch
            {
                throw;
            }
            return res;
        }

        ///// <summary>
        ///// Get all Product
        ///// </summary>
        ///// <returns>Return all product</returns>
        //public async Task<IEnumerable<DataContract.Entities.Product>> GetProducts()
        //{
        //    return await _iProductRepository.GetProducts();
        //}

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns>Return update result</returns>
        public async Task<bool> UpdateProduct(ProductResponseDTO product)
        {
            try
            {
                product.UpdatedBy=_UserInfo.UserName;
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.UPDATEPRODUCT, product, _options.Value.APIUrl, _UserInfo);

                //Convert the string in to desired object.
                bool res = JsonConvert.DeserializeObject<bool>(responseString);

                return res;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add Product Attributes(can be Inserted in aa bulk)
        /// </summary>
        /// <param name="productAttributes">Request object</param>
        /// <returns></returns>
        public async Task<bool> AddProductAttributes(IEnumerable<ProductAttributeRequestDTO> productAttributes)
        {
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.UPDATEPRODUCT, productAttributes, _options.Value.APIUrl);

                //Convert the string in to desired object.
                ProductResponseDTO res = JsonConvert.DeserializeObject<ProductResponseDTO>(responseString);
                if (responseString.ToLower() == "ok")
                    return true;
                return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Add product Images(can be Inserted in aa bulk)
        /// </summary>
        /// <param name="productImages">Request object</param>
        /// <returns></returns>
        public async Task<bool> AddProductImage(ProductImageRequest productImagesReq)
        {
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostFileRequestGetResponseString(Constants.ADDPRODUCTIMAGES, productImagesReq, _options.Value.APIUrl, _UserInfo);

                //Convert the string in to desired object.
                //List<ProductImageResponseDTO> res = JsonConvert.DeserializeObject<List<ProductImageResponseDTO>>(responseString);
                if (!string.IsNullOrEmpty(responseString) && responseString.ToLower() == "ok")
                    return true;
                return false;
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Add product review
        /// </summary>
        /// <param name="productReview">Request object</param>
        /// <returns></returns>
        public async Task<bool> AddProductReview(ProductReviewRequestDTO productReview)
        {
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.ADDPRODUCTREVIEW, productReview, _options.Value.APIUrl);

                //Convert the string in to desired object.
                ProductResponseDTO res = JsonConvert.DeserializeObject<ProductResponseDTO>(responseString);
                if (responseString.ToLower() == "ok")
                    return true;
                return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get product review by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductReview</returns>
        public async Task<IEnumerable<ProductImageResponseDTO>> GetProductImagesByProductId(string productId)
        {
            List<ProductImageResponseDTO> res = new List<ProductImageResponseDTO>();
            try
            {

                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.PostRequestGetResponseString(Constants.GETPRODUCTIMAGESBYPRODUCTID, productId, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<List<ProductImageResponseDTO>>(responseString);
            }
            catch
            {
                throw;
            }
            return (IEnumerable<ProductImageResponseDTO>)res;
        }


        /// <summary>
        /// Get Product by query
        /// </summary>
        /// <param name="query">Request Object</param>
        /// <returns>Return product result</returns>
        public async Task<IEnumerable<ProductResponseDTO>> GetProducts()
        {
            List<ProductResponseDTO> res = new List<ProductResponseDTO>();
            try
            {
                // Make an api post call and get the response string.
                string responseString = InvokeCallRequest.GetResponseString(Constants.GETPRODUCTS, _options.Value.APIUrl);

                //Convert the string in to desired object.
                res = JsonConvert.DeserializeObject<List<ProductResponseDTO>>(responseString);

            }
            catch
            {
                throw;
            }
            return res;
        }

        public void SetSession(AuthenticateResponse req)
        {
            _UserInfo = req;
        }

        #endregion

        public async Task AddImage(string path, IFormFile productImage)
        {
            var FileDic = "Files";
            var FilePath = Path.Combine(
            Directory.GetCurrentDirectory(), path);

            if (!Directory.Exists(FilePath))
                Directory.CreateDirectory(FilePath);
            var stream = productImage.OpenReadStream();
            var fileName = productImage.FileName;
            var filePath = Path.Combine(FilePath, productImage.FileName);
            using (FileStream fs = System.IO.File.Create(filePath))
            {
                productImage.CopyTo(fs);
            }
        }
    }
}
