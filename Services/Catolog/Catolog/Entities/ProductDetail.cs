using MongoDB.Bson.Serialization.Attributes;

namespace Catolog.Entities
{
    public class ProductDetail
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ProductDetailId { get; set; }
        public string ProductDetailInformation { get; set; }
        public string ProductDetailDescription { get; set;}
        public string ProductId { get; set; }
        public Product Product { get; set; }



    }
}
