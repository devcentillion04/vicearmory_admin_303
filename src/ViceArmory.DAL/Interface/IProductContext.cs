using MongoDB.Driver;
using ViceArmory.ResponseObject.Product;

namespace ViceArmory.DAL.Interface
{
    public interface IProductContext
    {
        IMongoCollection<ProductResponseDTO> Products { get; }
        IMongoCollection<ProductAttributeResponseDTO> ProductAttributes { get; }
        IMongoCollection<ProductReviewResponseDTO> ProductReviews { get; }
        IMongoCollection<ProductImageResponseDTO> ProductImages { get; }
    }
}
