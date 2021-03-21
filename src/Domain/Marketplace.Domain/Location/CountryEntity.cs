using Marketplace.Domain.Seed;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Marketplace.Domain.Location
{
    public class CountryEntity : Entity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("country")] public string Country { get; set; }
        [BsonElement("currency")] public string Currency { get; set; }
        [BsonElement("language")] public string Language { get; set; }
        [BsonElement("timezone")] public string TimeZone { get; set; }
    }
}
