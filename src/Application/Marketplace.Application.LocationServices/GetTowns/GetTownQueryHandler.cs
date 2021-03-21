using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Marketplace.Common.Application.Queries;
using Marketplace.Data.Repositories.Interfaces;

namespace Marketplace.Application.LocationServices.GetTowns
{
    public class GetTownQueryHandler : IQueryHandler<GetTownQuery, IEnumerable<TownDto>>
    {
        private readonly ILocationRepository _locationRepository;

        public GetTownQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<TownDto>> Handle(GetTownQuery request, CancellationToken cancellationToken)
        {
            var towns = (await _locationRepository.GetTownsAsync(request.CityName))
                .Select(x => new TownDto(x.Town, x.Country, x.City));

            return towns;
        }
    }
}
