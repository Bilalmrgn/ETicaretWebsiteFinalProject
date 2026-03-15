using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catolog.Entities
{
    [BsonIgnoreExtraElements]
    public class ProductDetail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductDetailId { get; set; }

        public string? ProductDetailInformation { get; set; }
        public string? ProductDetailDescription { get; set; }

        // ÖNEMLİ: MongoDB'deki koleksiyonunuzda bu alanın tam adını kontrol edin. 
        // Eğer veritabanında "ProductId" ise "productId" yazmak null döndürür.
        [BsonElement("ProductId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }

        [BsonIgnore]
        public Product Product { get; set; }
    }
}
