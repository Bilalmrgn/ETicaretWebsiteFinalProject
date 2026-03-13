using MongoDB.Bson.Serialization.Attributes;

namespace Catolog.Entities
{
    [BsonIgnoreExtraElements]
    public class ProductWithCategory : Product
    {
        public List<Category> Categories { get; set; }
    }
}
