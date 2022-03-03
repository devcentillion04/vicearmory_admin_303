using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ViceArmory.DAL.Interface;
using ViceArmory.DTO.ResponseObject.AppSettings;
using ViceArmory.ResponseObject.Product;

namespace ViceArmory.DAL.Repository
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IOptions<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
            Products = database.GetCollection<ProductResponseDTO>("Products");
            ProductAttributes = database.GetCollection<ProductAttributeResponseDTO>("ProductAttributes");
            ProductReviews = database.GetCollection<ProductReviewResponseDTO>("ProductReviews");
            ProductImages = database.GetCollection<ProductImageResponseDTO>("ProductImages");
        }

        public IMongoCollection<ProductResponseDTO> Products { get; }
        public IMongoCollection<ProductAttributeResponseDTO> ProductAttributes { get; }
        public IMongoCollection<ProductReviewResponseDTO> ProductReviews { get; }
        public IMongoCollection<ProductImageResponseDTO> ProductImages { get; }
    }
}
