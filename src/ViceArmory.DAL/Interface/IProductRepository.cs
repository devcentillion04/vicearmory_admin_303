using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.RequestObject.Product;
using ViceArmory.ResponseObject.Product;

namespace ViceArmory.DAL.Interface
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
        Task<ProductResponseDTO> GetProduct(string id);
        /// <summary>
        /// Get Product by name
        /// </summary>
        /// <param name="name">Request Object</param>
        /// <returns>Return product result</returns>
        Task<IEnumerable<ProductResponseDTO>> GetProductByName(string name);
        /// <summary>
        /// Get Product by category
        /// </summary>
        /// <param name="categoryName">Request object</param>
        /// <returns>Return product result</returns>
        Task<IEnumerable<ProductResponseDTO>> GetProductByCategory(string categoryId);
        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns></returns>
        Task CreateProduct(ProductResponseDTO product);

        /// <summary>
        /// Create Product List
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns></returns>
        Task CreateProductList(List<ProductResponseDTO> product);

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="product">Request Object</param>
        /// <returns>Return update result</returns>
        Task<bool> UpdateProduct(ProductResponseDTO product);
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
        Task<IEnumerable<ProductAttributeResponseDTO>> GetProductAttributesByProductId(string productId);
        /// <summary>
        /// Get product review by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductReview</returns>
        Task<IEnumerable<ProductReviewResponseDTO>> GetProductReviewsByProductId(string productId);

        /// <summary>
        /// Get product Images by product id
        /// </summary>
        /// <param name="productId">Request object</param>
        /// <returns>Return list of ProductImage result</returns>
        Task<IEnumerable<ProductImageResponseDTO>> GetProductImagesByProductId(string productId);

        /// <summary>
        /// Add Product Attributes(can be Inserted in aa bulk)
        /// </summary>
        /// <param name="productAttributes">Request object</param>
        /// <returns></returns>
        Task AddProductAttributes(IEnumerable<ProductAttributeResponseDTO> productAttributes);
        /// <summary>
        /// Add product review
        /// </summary>
        /// <param name="productReview">Request object</param>
        /// <returns></returns>
        Task AddProductReview(ProductReviewResponseDTO productReview);
        /// <summary>
        /// Add product Images(can be Inserted in aa bulk)
        /// </summary>
        /// <param name="productImages">Request object</param>
        /// <returns></returns>
        Task AddProductImage(ProductImageRequest productImages);

        /// <summary>
        /// Define 
        /// </summary>
        /// <param name="query">Product query</param>
        /// <returns></returns>
        Task<IEnumerable<ProductResponseDTO>> GetProducts(ProductQueryRequestDTO query);

        /// <summary>
        /// Define 
        /// </summary>
        /// <param name="query">Get Product</param>
        /// <returns></returns>
        Task<IEnumerable<ProductResponseDTO>> GetProducts();

        /// <summary>
        /// Define
        /// </summary>
        /// <param name="query">Product query</param>
        /// <returns></returns>
        Task<IEnumerable<ProductResponseDTO>> GetProductsWithImage(ProductQueryRequestDTO query);

    }
}
