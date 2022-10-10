using System.Text.Json.Serialization;

namespace OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather
{
    public class Coordinates
    {
        [JsonPropertyName("lon")] public double Longitude { get; set; }

        [JsonPropertyName("lat")] public double Latitude { get; set; }
    }
}