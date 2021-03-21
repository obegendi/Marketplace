namespace Marketplace.Application.LocationServices
{
    public class TownDto
    {

        public TownDto(string name, string countryName, string cityName)
        {
            Name = name;
            CountryName = countryName;
            CityName = cityName;
        }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
    }
}
