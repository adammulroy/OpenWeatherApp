using System.Text.Json.Serialization;

namespace OpenWeatherApp.Api.OpenWeather.Models.Location
{
    public class LatLonLocation
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("lat")]
        public string Latitude { get; set; }
        
        [JsonPropertyName("lon")]
        public string Longitude { get; set; }
        
        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}