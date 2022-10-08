namespace OpenWeatherApp.Location
{
    public class Place
    {
        public Place(string name, string latitude, string longitude)
        {
            DisplayName = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string DisplayName { get; }
        public string Latitude { get; }
        public string Longitude { get; }
    }
}