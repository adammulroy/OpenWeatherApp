using System.Runtime.CompilerServices;

namespace OpenWeatherApp.Location
{
    public class Place
    {
        public Place(string name, string latitude, string longitude, string country)
        {
            DisplayName = name;
            Latitude = latitude;
            Longitude = longitude;
            Country = country;
        }

        public string DisplayName { get; }
        public string Latitude { get; }
        public string Longitude { get; }
        public string Country { get; }
    }
}