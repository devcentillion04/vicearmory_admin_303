using Account.DataContract.Entities;
using Account.Service.Services;
using Account.Service.Services.Interfaces;
using Amazon.S3;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Product.DataContract.Entities;
using Product.Repositories;
using Product.Repositories.Interfaces;
using Product.Service.Services.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Product.Service.Services
{
    public class ProductService : IProductService
    {
        #region Members

        private readonly IProductRepository _iProductRepository;
        private readonly FileStorageConfig _fileStorageConfig;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ILogger<ProductRepository> _logger;
        private const string PRODUCT_IMAGE_LOCATION = "{0}/products/{1}/";
        #endregion

        #region Construction

        public ProductService(IProductRepository iProductRepository, ILogger<ProductRepository> logger, IOptions<FileStorageConfig> fileStorageConfig, IHostEnvironment hostEnvironment)
        {
            _iProductRepository = iProductRepository;
            _fileStorageConfig = fileStorageConfig.Value;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns></returns>
        public async Task CreateProduct(DataContract.Entities.Product product)
        {
            await _iProductRepository.CreateProduct(product);
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return delete result</returns>
        public async Task<bool> DeleteProduct(string id)
        {
            return await _iProductRepository.DeleteProduct(id);
        }

        /// <summary>
        /// Get product
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return product result</returns>
        public async Task<DataContract.Entities.Product> GetProduct(string id)
        {
            return await _iProductRepository.GetProduct(id);
        }

        /// <summary>
        /// Get product attributes by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductAttribute result</returns>
        public async Task<IEnumerable<ProductAttribute>> GetProductAttributesByProductId(string productId)
        {
            return await _iProductRepository.GetProductAttributesByProductId(productId);
        }

        /// <summary>
        /// Get Product by category
        /// </summary>
        /// <param name="categoryName">Request object</param>
        /// <returns>Return product result</returns>
        public async Task<IEnumerable<DataContract.Entities.Product>> GetProductByCategory(string categoryId)
        {
            return await _iProductRepository.GetProductByCategory(categoryId);
        }

        /// <summary>
        /// Get Product by name
        /// </summary>
        /// <param name="name">Request Object</param>
        /// <returns>Return product result</returns>
        public async Task<IEnumerable<DataContract.Entities.Product>> GetProductByName(string name)
        {
            return await _iProductRepository.GetProductByName(name);
        }

        /// <summary>
        /// Get product review by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductReview</returns>
        public async Task<IEnumerable<ProductReview>> GetProductReviewsByProductId(string productId)
        {
            return await _iProductRepository.GetProductReviewsByProductId(productId);
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
        public async Task<bool> UpdateProduct(DataContract.Entities.Product product)
        {
            return await _iProductRepository.UpdateProduct(product);
        }

        /// <summary>
        /// Add Product Attributes(can be Inserted in aa bulk)
        /// </summary>
        /// <param name="productAttributes">Request object</param>
        /// <returns></returns>
        public async Task AddProductAttributes(IEnumerable<ProductAttribute> productAttributes)
        {
            await _iProductRepository.AddProductAttributes(productAttributes);
        }

        /// <summary>
        /// Add product Images(can be Inserted in aa bulk)
        /// </summary>
        /// <param name="productImages">Request object</param>
        /// <returns></returns>
        public async Task AddProductImage(ProductImageRequest productImagesReq)
        {
            List<ProductImage> productImages = new List<ProductImage>();

            var s3Storage = GetS3Storage();
            var container = string.Format(PRODUCT_IMAGE_LOCATION, _hostEnvironment.EnvironmentName.ToLower(), productImagesReq.ProductId);
            var downloadUrlBasePath = $"{_fileStorageConfig.DownloadDomainUrl}{container}";

            foreach (var item in productImagesReq.ProductImages)
            {
                var stream = item.OpenReadStream();
                var fileName = item.FileName;
                var StoredFilePath = string.Empty;
                if (stream != null && stream.Length > 0)
                {
                    var personImage = await s3Storage.SaveAsync(container, fileName, stream, "product", true, true);
                    StoredFilePath = $"{downloadUrlBasePath}{personImage}";
                }
                productImages.Add(new ProductImage()
                {
                    Id = productImagesReq.Id,
                    ImageFilePath = StoredFilePath,
                    ProductId = productImagesReq.ProductId,
                    ImageName = item.FileName,
                    UserId = productImagesReq.UserId
                });

            }

            await _iProductRepository.AddProductImage(productImages);
        }

        /// <summary>
        /// Add product review
        /// </summary>
        /// <param name="productReview">Request object</param>
        /// <returns></returns>
        public async Task AddProductReview(ProductReview productReview)
        {
            await _iProductRepository.AddProductReview(productReview);
        }

        /// <summary>
        /// Get product review by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductReview</returns>
        public async Task<IEnumerable<ProductImage>> GetProductImagesByProductId(string productId)
        {
            return await _iProductRepository.GetProductImagesByProductId(productId);
        }


        /// <summary>
        /// Get Product by query
        /// </summary>
        /// <param name="query">Request Object</param>
        /// <returns>Return product result</returns>
        public async Task<IEnumerable<DataContract.Entities.Product>> GetProducts(ProductQuery query)
        {
            return await _iProductRepository.GetProducts(query);
        }

        #endregion

        #region Private method

        private FileStorageService GetS3Storage()
        {
            var amazonS3Client = new AmazonS3Client(_fileStorageConfig.AccessKey, _fileStorageConfig.SecretKey, new AmazonS3Config
            {
                ServiceURL = _fileStorageConfig.ServiceURL
            });

            return new FileStorageService(amazonS3Client, _fileStorageConfig.SpaceName);
        }
        #endregion
    }
}
