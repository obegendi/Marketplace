using System.Collections.Generic;
using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.LocationServices.GetCities
{
    public class GetCitiesQuery : IQuery<IEnumerable<CityDto>>
    {
        public GetCitiesQuery(string countryName)
        {
            CountryName = countryName;
        }

        public string CountryName { get; }
    }
}
