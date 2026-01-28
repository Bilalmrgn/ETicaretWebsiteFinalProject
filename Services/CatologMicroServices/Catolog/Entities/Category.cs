using MongoDB.Bson.Serialization.Attributes;

namespace Catolog.Entities
{
    public class Category
    {
        [BsonId] //mongodb de id olduğunu belirtmek için kullanılır
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CategoryId { get; set; }//mongodb de id ler string ile tutulur
        public string CategoryName { get; set; }
    }
}
