using MongoDB.Driver;
using Product.DataContract.Entities;

namespace Product.Repositories.Data.Interfaces
{
    public interface IProductContext
    {
        IMongoCollection<DataContract.Entities.Product> Products { get; }
        IMongoCollection<ProductAttribute> ProductAttributes { get; }
        IMongoCollection<ProductReview> ProductReviews { get; }
        IMongoCollection<ProductImage> ProductImages { get; }
    }
}
