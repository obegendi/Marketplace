using Marketplace.Domain.Seed;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Marketplace.Domain.Location
{
    public class DistrictEntity : Entity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("district")] public string District { get; set; }
        [BsonElement("country")] public string Country { get; set; }
        [BsonElement("city")] public string City { get; set; }
        [BsonElement("town")] public string Town { get; set; }
        [BsonElement("neighborhood")] public string Neighborhood { get; set; }
        [BsonElement("zip")] public string Zip { get; set; }
    }
}
