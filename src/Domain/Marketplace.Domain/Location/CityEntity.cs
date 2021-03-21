using Marketplace.Domain.Seed;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Marketplace.Domain.Location
{
    public class CityEntity : Entity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("city")] public string City { get; set; }
        [BsonElement("country")] public string Country { get; set; }
    }
}
