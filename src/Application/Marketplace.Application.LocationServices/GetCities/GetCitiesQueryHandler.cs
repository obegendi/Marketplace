using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.LocationServices.GetCities
{
    public class GetCitiesQueryHandler : IQueryHandler<GetCitiesQuery, IEnumerable<CityDto>>
    {
        private readonly ILocationRepository _locationRepository;

        public GetCitiesQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<CityDto>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var cities = (await _locationRepository.GetCitiesAsync(request.CountryName))
                .Select(x => new CityDto(x.City, x.Country));

            return cities;
        }
    }
}
