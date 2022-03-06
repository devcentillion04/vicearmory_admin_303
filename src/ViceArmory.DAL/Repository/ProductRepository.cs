using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using ViceArmory.DAL.Interface;
using ViceArmory.ResponseObject.Product;
using ViceArmory.RequestObject.Product;
using Amazon.S3;
using Microsoft.Extensions.Hosting;
using ViceArmory.DTO.ResponseObject.Account;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Newtonsoft.Json;
using ViceArmory.DTO.RequestObject.ApiConfiguration;
using System.Net.Http.Headers;
using AutoMapper;
using ViceArmory.DTO.MapObject;

namespace ViceArmory.DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        #region Members
        private readonly IProductContext _context;
        //private readonly IAuditLogRepository _iAuditLogRepository;
        private const string PRODUCT_IMAGE_LOCATION = "{0}/products/{1}/";
        private readonly FileStorageConfigResponseDTO _fileStorageConfig;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IApiConfigurationService _iApiConfigurationService;
        private readonly IOptions<ApiConfigurationSetting> _apiConfigurationSetting;
        #endregion

        #region Construction
        public ProductRepository(IProductContext context, IOptions<FileStorageConfigResponseDTO> fileStorageConfig, IHostEnvironment hostEnvironment, IOptions<ApiConfigurationSetting> apiConfigurationSetting, IApiConfigurationService iApiConfigurationService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _fileStorageConfig = fileStorageConfig.Value;
            _iApiConfigurationService = iApiConfigurationService;
            _hostEnvironment = hostEnvironment;
            this._apiConfigurationSetting = apiConfigurationSetting;
            //_iAuditLogRepository = iAuditLogRepository;
        }
        #endregion

        #region Methods
        //public async Task<IEnumerable<DataContract.Entities.Product>> GetProducts()
        //{
        //    var res = await _context
        //                    .Products
        //                    .Find(p => true)
        //                    .ToListAsync();
        //    var auditLog = new AuditLog
        //    {
        //        ActivityID = "*",
        //        AuditActivity = CommonEnums.AuditActivityEnum.ProductAll,
        //        IPAddress = "127.0.0.0",
        //        CreatedBy = "",
        //        CreatedDate = DateTime.Now,
        //        Description = "Get Product by name"
        //    };
        //    await _iAuditLogRepository.CreateAuditLog(auditLog);
        //    return res;
        //}

        /// <summary>
        /// Get product
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return product result</returns>
        public async Task<ProductResponseDTO> GetProduct(string id)
        {
            return await _context
                           .Products
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get Product by name
        /// </summary>
        /// <param name="name">Request Object</param>
        /// <returns>Return product result</returns>
        public async Task<IEnumerable<ProductResponseDTO>> GetProductByName(string name)
        {
            FilterDefinition<ProductResponseDTO> filter = Builders<ProductResponseDTO>.Filter.ElemMatch(p => p.Title, name);

            var res = await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
            //var auditLog = new AuditLog
            //{
            //    ActivityID = name,
            //    AuditActivity = CommonEnums.AuditActivityEnum.ProductByName,
            //    IPAddress = "127.0.0.0",
            //    CreatedBy = "",
            //    CreatedDate = DateTime.Now,
            //    Description = "Get Product by name"
            //};
            //await _iAuditLogRepository.CreateAuditLog(auditLog);
            return res;
        }

        /// <summary>
        /// Get Product by category
        /// </summary>
        /// <param name="categoryName">Request object</param>
        /// <returns>Return product result</returns>
        public async Task<IEnumerable<ProductResponseDTO>> GetProductByCategory(string categoryId)
        {
            FilterDefinition<ProductResponseDTO> filter = Builders<ProductResponseDTO>.Filter.Eq(p => p.CategoryId, categoryId);
            var res = await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();

            var item = _context.Products.Find(f => true).Sort("{ id: -1}").Limit(1).FirstOrDefault();
            //var auditLog = new AuditLog
            //{
            //    ActivityID = categoryId,
            //    AuditActivity = CommonEnums.AuditActivityEnum.ProductByCategory,
            //    IPAddress = "127.0.0.0",
            //    CreatedBy = "",
            //    CreatedDate = DateTime.Now,
            //    Description = "Get Product by category."
            //};
            //await _iAuditLogRepository.CreateAuditLog(auditLog);

            return res;

        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns></returns>
        public async Task CreateProduct(ProductResponseDTO product)
        {
            FilterDefinition<ProductResponseDTO> filter = Builders<ProductResponseDTO>.Filter.Eq(p => p.Title, product.Title) & Builders<ProductResponseDTO>.Filter.Eq(p => p.IsDeleted, true);
            var item = await _context.Products.Find(filter).ToListAsync();
            if (item.Count <= 0)
            {
                await _context.Products.InsertOneAsync(product);
                //var item = _context.Products.Find(f => true).Sort("{ id: -1}").Limit(1).FirstOrDefault();
                //var auditLog = new AuditLog
                //{
                //    ActivityID = product.Id,
                //    AuditActivity = CommonEnums.AuditActivityEnum.Product,
                //    IPAddress = product.IPAddress,
                //    CreatedBy = product.UserId,
                //    CreatedDate = DateTime.Now,
                //    Description = "Add Product."
                //};
                //await _iAuditLogRepository.CreateAuditLog(auditLog);
            }
        }

        /// <summary>
        /// Create Product List
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns></returns>
        public async Task CreateProductList(List<ProductResponseDTO> product)
        {
            foreach (var item in product)
            {
                var _existingData = await _context
                          .Products
                          .Find(p => p.Id == item.Id)
                          .FirstOrDefaultAsync();
                if (_existingData != null)
                {
                    await _context.Products.ReplaceOneAsync(filter: g => g.Id == item.Id, replacement: item);
                }
                else
                {
                    await _context.Products.InsertOneAsync(item);
                }
            }
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns>Return update result</returns>
        public async Task<bool> UpdateProduct(ProductResponseDTO product)
        {
            var updateResult = await _context
                                        .Products
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            //var auditLog = new AuditLog
            //{
            //    ActivityID = product.Id,
            //    AuditActivity = CommonEnums.AuditActivityEnum.Product,
            //    IPAddress = product.IPAddress,
            //    CreatedBy = product.UserId,
            //    CreatedDate = DateTime.Now,
            //    Description = "Update Product."
            //};
            //await _iAuditLogRepository.CreateAuditLog(auditLog);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return delete result</returns>
        public async Task<bool> DeleteProduct(string id)
        {
            //FilterDefinition<DataContract.Entities.Product> filter = Builders<DataContract.Entities.Product>.Filter.Eq(p => p.Id, id);
            //var item = GetProduct(id).Result;
            //DeleteResult deleteResult = await _context
            //                                    .Products
            //                                    .DeleteOneAsync(filter);

            FilterDefinition<ProductResponseDTO> filter = Builders<ProductResponseDTO>.Filter.Eq(p => p.Id, id);
            var update = Builders<ProductResponseDTO>.Update.Set(nameof(ProductResponseDTO.IsDeleted), true);
            var deleteResult = await _context.Products.UpdateOneAsync(filter, update);

            var item = GetProduct(id).Result;
            //var auditLog = new AuditLog
            //{
            //    ActivityID = id,
            //    AuditActivity = CommonEnums.AuditActivityEnum.Product,
            //    IPAddress = item.IPAddress,
            //    CreatedBy = item.UserId,
            //    CreatedDate = DateTime.Now,
            //    Description = "Update Product."
            //};
            //await _iAuditLogRepository.CreateAuditLog(auditLog);

            return deleteResult.IsAcknowledged
                && deleteResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Get product attributes by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductAttribute result</returns>
        public async Task<IEnumerable<ProductAttributeResponseDTO>> GetProductAttributesByProductId(string productId)
        {
            return await _context
                           .ProductAttributes
                           .Find(p => p.ProductId == productId)
                           .ToListAsync();
        }

        /// <summary>
        /// Get product review by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductReview</returns>
        public async Task<IEnumerable<ProductReviewResponseDTO>> GetProductReviewsByProductId(string productId)
        {
            var res = await _context
                           .ProductReviews
                           .Find(p => p.ProductId == productId)
                           .ToListAsync();
            return res;
        }

        /// <summary>
        /// Add Product Attributes(can be Inserted in aa bulk)
        /// </summary>
        /// <param name="productAttributes">Request object</param>
        /// <returns></returns>
        public async Task AddProductAttributes(IEnumerable<ProductAttributeResponseDTO> productAttributes)
        {
            await _context.ProductAttributes.InsertManyAsync(productAttributes);
        }

        /// <summary>
        /// Add product review
        /// </summary>
        /// <param name="productReview">Request object</param>
        /// <returns></returns>
        public async Task AddProductReview(ProductReviewResponseDTO productReview)
        {
            await _context.ProductReviews.InsertOneAsync(productReview);
        }

        /// <summary>
        /// Add product Images(can be Inserted in aa bulk)
        /// </summary>
        /// <param name="productImages">Request object</param>
        /// <returns></returns>
        public async Task AddProductImage(ProductImageRequest productImagesReq)
        {
            List<ProductImageResponseDTO> productImages = new List<ProductImageResponseDTO>();

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
                productImages.Add(new ProductImageResponseDTO()
                {
                    Id = productImagesReq.Id,
                    ImageFilePath = StoredFilePath,
                    ProductId = productImagesReq.ProductId,
                    ImageName = item.FileName,
                    UserId = productImagesReq.UserId
                });

            }

            await _context.ProductImages.InsertManyAsync(productImages);
        }

        /// <summary>
        /// Get product Images by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductImage result</returns>
        public async Task<IEnumerable<ProductImageResponseDTO>> GetProductImagesByProductId(string productId)
        {
            return await _context
                           .ProductImages
                           .Find(p => p.ProductId == productId)
                           .ToListAsync();
        }

        /// <summary>
        /// Get all Product
        /// </summary>
        /// <returns>Return all Product</returns>
        public async Task<IEnumerable<ProductResponseDTO>> GetProducts(ProductQueryRequestDTO query)
        {
            var builder = Builders<ProductResponseDTO>.Filter;
            FilterDefinition<ProductResponseDTO> filter = FilterDefinition<ProductResponseDTO>.Empty;
            filter = filter & builder.Eq(p => p.IsDeleted, false);
            var findOptions = new FindOptions<ProductResponseDTO, ProductResponseDTO>();
            List<SortDefinition<ProductResponseDTO>> definitions = new List<SortDefinition<ProductResponseDTO>>();
            var sortBuilder = Builders<ProductResponseDTO>.Sort;

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortDir == 0)
                    definitions.Add(sortBuilder.Ascending(query.SortBy));
                else
                    definitions.Add(sortBuilder.Descending(query.SortBy));
            }
            else
            {
                definitions.Add(sortBuilder.Ascending(nameof(ProductResponseDTO.Title)));
            }

            findOptions.Sort = sortBuilder.Combine(definitions);
            findOptions.Skip = (query.PageNumber - 1) * query.PageSize;
            findOptions.Limit = query.PageSize;
            return (await _context
                            .Products
                            .FindAsync(filter, findOptions))
                            .ToList();
        }

        /// <summary>
        /// Get all Product
        /// </summary>
        /// <returns>Return all Product</returns>
        public async Task<IEnumerable<ProductResponseDTO>> GetProducts()
        {
            //var builder = Builders<ProductResponseDTO>.Filter;
            //FilterDefinition<ProductResponseDTO> filter = FilterDefinition<ProductResponseDTO>.Empty;
            //filter = filter & builder.Eq(p => p.IsDeleted, false);
            //return (await _context
            //                .Products
            //                .FindAsync(filter))
            //                .ToList();
            try
            {
                var token = await _iApiConfigurationService.GetAccessTokenFromSession();
                using (var client = new HttpClient())
                {
                    ProductResponseDTO _productResponseDTO = new ProductResponseDTO();
                    client.BaseAddress = new Uri(_apiConfigurationSetting.Value.api_url + _apiConfigurationSetting.Value.account_id + "/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token.ToString());
                    HttpResponseMessage response = new HttpResponseMessage();
                    response = await client.GetAsync("Item.json").ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        var builder = Builders<ProductResponseDTO>.Filter;
                        ProductMapDTO _productMapDTO = JsonConvert.DeserializeObject<ProductMapDTO>(result);
                        var configuration = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<Item, ProductResponseDTO>().ForMember(s => s.Id, m => m.MapFrom(s => s.itemID))
                            .ForMember(s => s.CategoryId, m => m.MapFrom(s => s.categoryID))
                            .ForMember(s => s.Title, m => m.MapFrom(s => s.description))
                            .ForMember(s => s.MetaTitle, m => m.MapFrom(s => s.description))
                            .ForMember(s => s.Slug, m => m.MapFrom(s => s.upc))
                            .ForMember(s => s.Type, m => m.MapFrom(s => s.itemType))
                            .ForMember(s => s.SKU, m => m.MapFrom(s => s.customSku))
                            .ForMember(s => s.CreatedAt, m => m.MapFrom(s => s.createTime))
                            .ForMember(s => s.IsDeleted, m => m.MapFrom(s => Convert.ToBoolean(s.archived)));
                        });
                        var mapper = configuration.CreateMapper();
                        var data = mapper.Map<List<Item>, List<ProductResponseDTO>>(_productMapDTO.Item);
                        foreach (var img in data)
                        {
                            var token1 = await _iApiConfigurationService.GetAccessTokenFromSession();
                            using (var client1 = new HttpClient())
                            {
                                ItemImageReponceDTO _itemImageresponseDTO = new ItemImageReponceDTO();
                                client1.BaseAddress = new Uri(_apiConfigurationSetting.Value.api_url + _apiConfigurationSetting.Value.account_id + "/Item/" + img.Id + "/Image.json");
                                client1.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                client1.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token1.access_token.ToString());
                                HttpResponseMessage response1 = new HttpResponseMessage();
                                response1 = await client1.GetAsync("Image.json").ConfigureAwait(false);
                                if (response1.IsSuccessStatusCode)
                                {
                                    string result1 = response1.Content.ReadAsStringAsync().Result;
                                    var builder1 = Builders<ItemImageListMapDTO>.Filter;
                                    var _attr = Newtonsoft.Json.JsonConvert.DeserializeObject<RootMapDTO>(result1);
                                    if (Convert.ToInt32(_attr.Attributes.count) >0)
                                    {
                                        ItemImageListMapDTO _itemImageListMapDTO = new ItemImageListMapDTO();
                                        var itemimage = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemImageMapDTO>(result1);
                                        _itemImageListMapDTO.Attributes = itemimage.Attributes;
                                        _itemImageListMapDTO.Image.Add(itemimage.Image);
                                        var data1 = _itemImageListMapDTO;
                                        img.ProductImage = data1.Image[0].baseImageURL + '/' + data1.Image[0].publicID + ".jpg";
                                    }
                                }
                            }
                        }

                        return data;
                    }
                }
            }
            catch(Exception ex)
            {

            }            
            #region get product from lightspeed api
          
            return new List<ProductResponseDTO>();
            #endregion
        }
        #endregion

        #region Private method
        private FileStorageRepository GetS3Storage()
        {
            var amazonS3Client = new AmazonS3Client(_fileStorageConfig.AccessKey, _fileStorageConfig.SecretKey, new AmazonS3Config
            {
                ServiceURL = _fileStorageConfig.ServiceURL
            });

            return new FileStorageRepository(amazonS3Client, _fileStorageConfig.SpaceName);
        }

        /// <summary>
        /// Get all Product
        /// </summary>
        /// <returns>Return all Product</returns>
        public async Task<IEnumerable<ProductResponseDTO>> GetProductsWithImage(ProductQueryRequestDTO query)
        {
            var builder = Builders<ProductResponseDTO>.Filter;
            FilterDefinition<ProductResponseDTO> filter = FilterDefinition<ProductResponseDTO>.Empty;
            filter = filter & builder.Eq(p => p.IsDeleted, false);
            var findOptions = new FindOptions<ProductResponseDTO, ProductResponseDTO>();
            List<SortDefinition<ProductResponseDTO>> definitions = new List<SortDefinition<ProductResponseDTO>>();
            var sortBuilder = Builders<ProductResponseDTO>.Sort;
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortDir == 0)
                    definitions.Add(sortBuilder.Ascending(query.SortBy));
                else
                    definitions.Add(sortBuilder.Descending(query.SortBy));
            }
            else
            {
                definitions.Add(sortBuilder.Ascending(nameof(ProductResponseDTO.Title)));
            }
            findOptions.Sort = sortBuilder.Combine(definitions);
            findOptions.Skip = (query.PageNumber - 1) * query.PageSize;
            findOptions.Limit = 3;
            var resp = (await _context
                              .Products
                              .FindAsync(filter, findOptions))
                              .ToListAsync();
            //foreach (var item in resp.Result)
            //{
            //    var Image = await _context.ProductImages.Find(p => p.ProductId == item.Id).ToListAsync();
            //    var ProductImage = await _context.ProductImages.Find(p => p.ProductId == item.Id).ToListAsync();
            //    if (ProductImage.Count > 0)
            //    {
            //        item.ProductImages = ProductImage[0].ImageFilePath;
            //    }
            //    //item.ProductImages = Image[0].ImageFilePath;
            //}
            return resp.Result;
        }
        #endregion

    }
}
