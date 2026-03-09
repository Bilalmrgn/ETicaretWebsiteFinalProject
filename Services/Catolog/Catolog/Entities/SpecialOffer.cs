using MongoDB.Bson.Serialization.Attributes;

namespace Catolog.Entities
{
    [BsonIgnoreExtraElements]
    public class SpecialOffer
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string SpecialOfferId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string ImageUrl { get; set; }
    }
}
