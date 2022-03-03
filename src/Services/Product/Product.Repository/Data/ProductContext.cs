using Account.DataContract.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Product.DataContract.Entities;
using Product.Repositories.Data.Interfaces;

namespace Product.Repositories.Data
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IOptions<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
            Products = database.GetCollection<DataContract.Entities.Product>("Products");
            ProductAttributes = database.GetCollection<ProductAttribute>("ProductAttributes");
            ProductReviews = database.GetCollection<ProductReview>("ProductReviews");
            ProductImages = database.GetCollection<ProductImage>("ProductImages");
        }

        public IMongoCollection<Product.DataContract.Entities.Product> Products { get; }
        public IMongoCollection<ProductAttribute> ProductAttributes { get; }
        public IMongoCollection<ProductReview> ProductReviews { get; }
        public IMongoCollection<ProductImage> ProductImages { get; }
    }
}
