namespace Marketplace.Application.LocationServices
{
    public class DistrictDto
    {

        public DistrictDto(string name, string countryName, string cityName, string townName)
        {
            Name = name;
            CountryName = countryName;
            CityName = cityName;
            TownName = townName;
        }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public string TownName { get; set; }
    }
}
