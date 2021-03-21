using Marketplace.Domain.Location;
using MongoDB.Driver;

namespace Marketplace.Data
{
    public interface ILocationMongoDbContext
    {
        IMongoCollection<CityEntity> Cities { get; }
        IMongoCollection<CountryEntity> Countries { get; }
        IMongoCollection<DistrictEntity> Districts { get; }
        IMongoCollection<TownEntity> Towns { get; }
    }
}
