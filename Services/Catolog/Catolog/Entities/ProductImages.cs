using MongoDB.Bson.Serialization.Attributes;

namespace Catolog.Entities
{
    [BsonIgnoreExtraElements]
    public class ProductImages
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ProductImagesId { get; set; }
        public List<string> Images { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }

    }
}
