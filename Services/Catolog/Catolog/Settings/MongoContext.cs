using Catolog.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catolog.Settings
{
    // Bu sınıf MongoDB bağlantısını yönetir ve koleksiyonlara erişim sağlar
    public class MongoContext
    {
        public IMongoDatabase Database { get; }
        public IMongoCollection<Category> Categories { get; }
        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<ProductImages> ProductImages { get; }
        public IMongoCollection<ProductDetail> ProductDetails { get; }

        public MongoContext(IOptions<DatabaseSettings> opt)
        {
            var s = opt.Value;
            var client = new MongoClient(s.ConnectionString); // tek client
            Database = client.GetDatabase(s.DatabaseName);

            Categories = Database.GetCollection<Category>(s.CategoryCollectionName);
            Products = Database.GetCollection<Product>(s.ProductCollectionName);
            ProductImages = Database.GetCollection<ProductImages>(s.ProductImagesCollectionName);
            ProductDetails = Database.GetCollection<ProductDetail>(s.ProductDetailConnectionName);
        }
    }
}
