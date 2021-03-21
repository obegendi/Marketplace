using System.Collections.Generic;
using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.LocationServices.GetTowns
{
    public class GetTownQuery : IQuery<IEnumerable<TownDto>>
    {
        public GetTownQuery(string cityName)
        {
            CityName = cityName;
        }

        public string CityName { get; }
    }
}
