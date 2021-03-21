using System.Collections.Generic;
using System.Threading.Tasks;
using Marketplace.Domain.Location;

namespace Marketplace.Data.Repositories.Interfaces
{
    public interface ILocationRepository
    {
        Task<ICollection<CountryEntity>> GetCountriesAsync();
        Task<ICollection<CityEntity>> GetCitiesAsync(string countryName);
        Task<ICollection<TownEntity>> GetTownsAsync(string cityName);
        Task<ICollection<DistrictEntity>> GetDistrictsAsync(string cityName, string townName);
    }
}
