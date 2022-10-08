using System.Text.Json.Serialization;

namespace OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather
{
    public class Coordinates
    {
        [JsonPropertyName("lon")]
        public string Longitude { get; set; }
        
        [JsonPropertyName("lat")]
        public string Latitude { get; set; }
    }
}