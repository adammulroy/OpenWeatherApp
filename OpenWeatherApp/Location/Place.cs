namespace OpenWeatherApp.Location
{
    public class Place
    {
        public Place(string name, double latitude, double longitude, string country)
        {
            DisplayName = name;
            Latitude = latitude;
            Longitude = longitude;
            Country = country;
        }

        public string DisplayName { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        public string Country { get; }
    }
}