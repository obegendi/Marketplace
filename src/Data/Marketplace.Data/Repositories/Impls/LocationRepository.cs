using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Data.Repositories.Interfaces;
using Marketplace.Domain.Location;
using MongoDB.Driver;

namespace Marketplace.Data.Repositories.Impls
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ILocationMongoDbContext _mongoDbContext;

        public LocationRepository(ILocationMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }

        public async Task<ICollection<CityEntity>> GetCitiesAsync(string countryName)
        {
            var cities = await _mongoDbContext.Cities.FindAsync(x => x.Country == countryName);

            return cities.ToList();
        }

        public async Task<ICollection<CountryEntity>> GetCountriesAsync()
        {
            var countries = await _mongoDbContext.Countries.FindAsync(x => x.Country != string.Empty);

            return countries.ToList();
        }

        public async Task<ICollection<DistrictEntity>> GetDistrictsAsync(string cityName, string townName)
        {
            var districts = await _mongoDbContext.Districts.FindAsync(x => x.City == cityName && x.Town == townName);

            return districts.ToList();
        }

        public async Task<ICollection<TownEntity>> GetTownsAsync(string cityName)
        {
            var towns = await _mongoDbContext.Towns.FindAsync(x => x.City == cityName);
            return towns.ToList();
        }
    }
}
