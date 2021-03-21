namespace Marketplace.Application.LocationServices
{
    public class CityDto
    {

        public CityDto(string name, string countryName)
        {
            Name = name;
            CountryName = countryName;
        }
        public string Name { get; set; }
        public string CountryName { get; set; }
    }
}
