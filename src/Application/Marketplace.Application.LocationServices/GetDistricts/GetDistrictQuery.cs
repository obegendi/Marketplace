using System.Collections.Generic;
using Marketplace.Common.Application.Queries;

namespace Marketplace.Application.LocationServices.GetDistricts
{
    public class GetDistrictQuery : IQuery<IEnumerable<DistrictDto>>
    {
        public GetDistrictQuery(string townName, string cityName)
        {
            TownName = townName;
            CityName = cityName;
        }

        public string TownName { get; set; }
        public string CityName { get; set; }
    }
}
