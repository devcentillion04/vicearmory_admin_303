using Product.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Product.DataContract.Entities;
using MongoDB.Driver;
using Product.Repositories.Data.Interfaces;
using Account.Repository.Repositories.Interfaces;
using Account.DataContract.Entities;
using Account.DataContract.Entities.Enum;
using MongoDB.Bson;

namespace Product.Repositories
{
    public class ProductRepository : IProductRepository
    {
        #region Members
        private readonly IProductContext _context;
        private readonly IAuditLogRepository _iAuditLogRepository;
        #endregion

        #region Construction
        public ProductRepository(IProductContext context, IAuditLogRepository iAuditLogRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _iAuditLogRepository = iAuditLogRepository;
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
        public async Task<DataContract.Entities.Product> GetProduct(string id)
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
        public async Task<IEnumerable<DataContract.Entities.Product>> GetProductByName(string name)
        {
            FilterDefinition<DataContract.Entities.Product> filter = Builders<DataContract.Entities.Product>.Filter.ElemMatch(p => p.Title, name);

            var res = await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();
            var auditLog = new AuditLog
            {
                ActivityID = name,
                AuditActivity = CommonEnums.AuditActivityEnum.ProductByName,
                IPAddress = "127.0.0.0",
                CreatedBy = "",
                CreatedDate = DateTime.Now,
                Description = "Get Product by name"
            };
            await _iAuditLogRepository.CreateAuditLog(auditLog);
            return res;
        }

        /// <summary>
        /// Get Product by category
        /// </summary>
        /// <param name="categoryName">Request object</param>
        /// <returns>Return product result</returns>
        public async Task<IEnumerable<DataContract.Entities.Product>> GetProductByCategory(string categoryId)
        {
            FilterDefinition<DataContract.Entities.Product> filter = Builders<DataContract.Entities.Product>.Filter.Eq(p => p.CategoryId, categoryId);

            var res = await _context
                            .Products
                            .Find(filter)
                            .ToListAsync();

            var item = _context.Products.Find(f => true).Sort("{ id: -1}").Limit(1).FirstOrDefault();
            var auditLog = new AuditLog
            {
                ActivityID = categoryId,
                AuditActivity = CommonEnums.AuditActivityEnum.ProductByCategory,
                IPAddress = "127.0.0.0",
                CreatedBy = "",
                CreatedDate = DateTime.Now,
                Description = "Get Product by category."
            };
            await _iAuditLogRepository.CreateAuditLog(auditLog);

            return res;

        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns></returns>
        public async Task CreateProduct(DataContract.Entities.Product product)
        {
            FilterDefinition<DataContract.Entities.Product> filter = Builders<DataContract.Entities.Product>.Filter.Eq(p => p.Title, product.Title);
            var item = await _context.Products.Find(filter).ToListAsync();
            if (item.Count <= 0)
            {
                await _context.Products.InsertOneAsync(product);
                //var item = _context.Products.Find(f => true).Sort("{ id: -1}").Limit(1).FirstOrDefault();
                var auditLog = new AuditLog
                {
                    ActivityID = product.Id,
                    AuditActivity = CommonEnums.AuditActivityEnum.Product,
                    IPAddress = product.IPAddress,
                    CreatedBy = product.UserId,
                    CreatedDate = DateTime.Now,
                    Description = "Add Product."
                };
                await _iAuditLogRepository.CreateAuditLog(auditLog);
            }
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns>Return update result</returns>
        public async Task<bool> UpdateProduct(DataContract.Entities.Product product)
        {
            var updateResult = await _context
                                        .Products
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            var auditLog = new AuditLog
            {
                ActivityID = product.Id,
                AuditActivity = CommonEnums.AuditActivityEnum.Product,
                IPAddress = product.IPAddress,
                CreatedBy = product.UserId,
                CreatedDate = DateTime.Now,
                Description = "Update Product."
            };
            await _iAuditLogRepository.CreateAuditLog(auditLog);

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

            FilterDefinition<DataContract.Entities.Product> filter = Builders<DataContract.Entities.Product>.Filter.Eq(p => p.Id, id);
            var update = Builders<DataContract.Entities.Product>.Update.Set(nameof(DataContract.Entities.Product.IsDeleted), true);
            var deleteResult = await _context.Products.UpdateOneAsync(filter, update);
            
            var item = GetProduct(id).Result;
            var auditLog = new AuditLog
            {
                ActivityID = id,
                AuditActivity = CommonEnums.AuditActivityEnum.Product,
                IPAddress = item.IPAddress,
                CreatedBy = item.UserId,
                CreatedDate = DateTime.Now,
                Description = "Update Product."
            };
            await _iAuditLogRepository.CreateAuditLog(auditLog);

            return deleteResult.IsAcknowledged
                && deleteResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Get product attributes by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductAttribute result</returns>
        public async Task<IEnumerable<ProductAttribute>> GetProductAttributesByProductId(string productId)
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
        public async Task<IEnumerable<ProductReview>> GetProductReviewsByProductId(string productId)
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
        public async Task AddProductAttributes(IEnumerable<ProductAttribute> productAttributes)
        {
            await _context.ProductAttributes.InsertManyAsync(productAttributes);
        }

        /// <summary>
        /// Add product review
        /// </summary>
        /// <param name="productReview">Request object</param>
        /// <returns></returns>
        public async Task AddProductReview(ProductReview productReview)
        {
            await _context.ProductReviews.InsertOneAsync(productReview);
        }

        /// <summary>
        /// Add product Images(can be Inserted in aa bulk)
        /// </summary>
        /// <param name="productImages">Request object</param>
        /// <returns></returns>
        public async Task AddProductImage(IEnumerable<ProductImage> productImages)
        {
            await _context.ProductImages.InsertManyAsync(productImages);
        }

        /// <summary>
        /// Get product Images by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductImage result</returns>
        public async Task<IEnumerable<ProductImage>> GetProductImagesByProductId(string productId)
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
        public async Task<IEnumerable<DataContract.Entities.Product>> GetProducts(ProductQuery query)
        {
            var builder = Builders<DataContract.Entities.Product>.Filter;
            FilterDefinition<DataContract.Entities.Product> filter = FilterDefinition<DataContract.Entities.Product>.Empty;
            filter = filter & builder.Eq(p => p.IsDeleted, false);
            var findOptions = new FindOptions<DataContract.Entities.Product, DataContract.Entities.Product>();
            List<SortDefinition<DataContract.Entities.Product>> definitions = new List<SortDefinition<DataContract.Entities.Product>>();
            var sortBuilder = Builders<DataContract.Entities.Product>.Sort;

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortDir == 0)
                    definitions.Add(sortBuilder.Ascending(query.SortBy));
                else
                    definitions.Add(sortBuilder.Descending(query.SortBy));
            }
            else
            {
                definitions.Add(sortBuilder.Ascending(nameof(DataContract.Entities.Product.Title)));
            }

            findOptions.Sort = sortBuilder.Combine(definitions);
            findOptions.Skip = (query.PageNumber - 1) * query.PageSize;
            findOptions.Limit = query.PageSize;
            return (await _context
                            .Products
                            .FindAsync(filter, findOptions))
                            .ToList();
        }
        #endregion
    }
}
