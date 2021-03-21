using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.LocationServices.GetDistricts
{
    public class GetDistrictQueryHandler : IQueryHandler<GetDistrictQuery, IEnumerable<DistrictDto>>
    {
        private readonly ILocationRepository _locationRepository;

        public GetDistrictQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<DistrictDto>> Handle(GetDistrictQuery request, CancellationToken cancellationToken)
        {
            var districts = (await _locationRepository.GetDistrictsAsync(request.CityName, request.TownName))
                .Select(x => new DistrictDto(x.District, x.Country, x.City, x.Town));

            return districts;
        }
    }
}
