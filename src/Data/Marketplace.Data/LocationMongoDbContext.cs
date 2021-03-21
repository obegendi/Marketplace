using Marketplace.Common;
using Marketplace.Domain.Location;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Marketplace.Data
{
    public class LocationMongoDbContext : ILocationMongoDbContext
    {
        private readonly MongoClient _client;
        public IMongoDatabase _db;
        public LocationMongoDbContext(IOptions<Appsettings> options)
        {
            var internalIdentity = new MongoInternalIdentity("admin", options.Value.LocationMongoDbOptions.Username);
            var passwordEvidence = new PasswordEvidence(options.Value.LocationMongoDbOptions.Password);
            var mongoCredential = new MongoCredential(options.Value.LocationMongoDbOptions.AuthMechanism, internalIdentity, passwordEvidence);
            if (_client == null)
                _client = new MongoClient(options.Value.LocationMongoDbOptions.DbConnectionString);

            if (_client != null && _db == null)
                _db = _client.GetDatabase(options.Value.LocationMongoDbOptions.Database);
        }

        public IMongoCollection<CityEntity> Cities => _db.GetCollection<CityEntity>("cities");

        public IMongoCollection<TownEntity> Towns => _db.GetCollection<TownEntity>("towns");

        public IMongoCollection<DistrictEntity> Districts => _db.GetCollection<DistrictEntity>("districts");


        public IMongoCollection<CountryEntity> Countries => _db.GetCollection<CountryEntity>("countries");
    }
}
