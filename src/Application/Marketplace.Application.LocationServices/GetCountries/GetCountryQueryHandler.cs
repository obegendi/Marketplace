using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.LocationServices.GetCountries
{
    public class GetCountryQueryHandler : IQueryHandler<GetCountryQuery, IEnumerable<CountryDto>>
    {
        private readonly ILocationRepository _locationRepository;

        public GetCountryQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public async Task<IEnumerable<CountryDto>> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            var countries = (await _locationRepository.GetCountriesAsync())
                .Select(x => new CountryDto(x.Country));

            return countries;
        }
    }
}
