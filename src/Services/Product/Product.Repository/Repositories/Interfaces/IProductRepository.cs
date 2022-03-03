using System.Collections.Generic;
using System.Threading.Tasks;
using Product.DataContract.Entities;
namespace Product.Repositories.Interfaces
{
    public interface IProductRepository
    {
        ///// <summary>
        ///// Get all Product
        ///// </summary>
        ///// <returns>Return all product</returns>
        //Task<IEnumerable<DataContract.Entities.Product>> GetProducts();
        /// <summary>
        /// Get product
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return product result</returns>
        Task<DataContract.Entities.Product> GetProduct(string id);
        /// <summary>
        /// Get Product by name
        /// </summary>
        /// <param name="name">Request Object</param>
        /// <returns>Return product result</returns>
        Task<IEnumerable<DataContract.Entities.Product>> GetProductByName(string name);
        /// <summary>
        /// Get Product by category
        /// </summary>
        /// <param name="categoryName">Request object</param>
        /// <returns>Return product result</returns>
        Task<IEnumerable<DataContract.Entities.Product>> GetProductByCategory(string categoryId);
        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns></returns>
        Task CreateProduct(DataContract.Entities.Product product);
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns>Return update result</returns>
        Task<bool> UpdateProduct(DataContract.Entities.Product product);
        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id">Request object</param>
        /// <returns>Return delete result</returns>
        Task<bool> DeleteProduct(string id);
        /// <summary>
        /// Get product attributes by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductAttribute result</returns>
        Task<IEnumerable<ProductAttribute>> GetProductAttributesByProductId(string productId);
        /// <summary>
        /// Get product review by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductReview</returns>
        Task<IEnumerable<ProductReview>> GetProductReviewsByProductId(string productId);

        /// <summary>
        /// Get product Images by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductImage result</returns>
        Task<IEnumerable<ProductImage>> GetProductImagesByProductId(string productId);

        /// <summary>
        /// Add Product Attributes(can be Inserted in aa bulk)
        /// </summary>
        /// <param name="productAttributes">Request object</param>
        /// <returns></returns>
        Task AddProductAttributes(IEnumerable<ProductAttribute> productAttributes);
        /// <summary>
        /// Add product review
        /// </summary>
        /// <param name="productReview">Request object</param>
        /// <returns></returns>
        Task AddProductReview(ProductReview productReview);
        /// <summary>
        /// Add product Images(can be Inserted in aa bulk)
        /// </summary>
        /// <param name="productImages">Request object</param>
        /// <returns></returns>
        Task AddProductImage(IEnumerable<ProductImage> productImages);

        /// <summary>
        /// Define 
        /// </summary>
        /// <param name="query">Product query</param>
        /// <returns></returns>
        Task<IEnumerable<DataContract.Entities.Product>> GetProducts(ProductQuery query);



    }
}
