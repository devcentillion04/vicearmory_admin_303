using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.RequestObject.Product;
using ViceArmory.ResponseObject.Product;

namespace ViceArmory.Middleware.Interface
{
    public interface IProductService
    {

        void SetSession(AuthenticateResponse req);

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
        ///// <summary>
        ///// Get Product by name
        ///// </summary>
        ///// <param name="name">Request Object</param>
        ///// <returns>Return product result</returns>
        //Task<IEnumerable<ProductResponseDTO>> GetProductByName(string name);
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
        Task<bool> CreateProduct(ProductRequestDTO product);
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
        Task<bool> AddProductAttributes(IEnumerable<ProductAttributeRequestDTO> productAttributes);

        /// <summary>
        /// Add product review
        /// </summary>
        /// <param name="productReview">Request object</param>
        /// <returns></returns>
        Task<bool> AddProductReview(ProductReviewRequestDTO productReview);

        /// <summary>
        /// Add product Images(can be Inserted in aa bulk)
        /// </summary>
        /// <param name="productImages">Request object</param>
        /// <returns></returns>
        Task<bool> AddProductImage(ProductImageRequest productImagesReq);


        /// <summary>
        /// Get all Product
        /// </summary>
        /// <returns>Return all Product</returns>
        Task<IEnumerable<ProductResponseDTO>> GetProducts();
       Task AddImage(string path, IFormFile productImage);
    }
}
