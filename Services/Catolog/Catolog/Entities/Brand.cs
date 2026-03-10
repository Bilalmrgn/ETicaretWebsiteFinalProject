using MongoDB.Bson.Serialization.Attributes;

namespace Catolog.Entities
{
    [BsonIgnoreExtraElements]
    public class Brand
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandImageUrl { get; set; }
    }
}
