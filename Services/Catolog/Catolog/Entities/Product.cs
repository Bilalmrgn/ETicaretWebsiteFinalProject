using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catolog.Entities
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductDescription { get; set; }
        public DateTime CreatedDate { get; set; }

        [BsonElement("CategoryId")] // Veritabanındaki gerçek alan adıyla eşleşmeli
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }

        [BsonIgnore] // Bu alan veritabanında yok, sadece join için
        public Category Category { get; set; }

    }
}
