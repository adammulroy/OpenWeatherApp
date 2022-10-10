using System.Text.Json.Serialization;

namespace OpenWeatherApp.Api.OpenWeather.Models.Location
{
    public class ZipCodeLocation
    {
        [JsonPropertyName("zip")] public string ZipCode { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("lat")] public double Latitude { get; set; }

        [JsonPropertyName("lon")] public double Longitude { get; set; }

        [JsonPropertyName("country")] public string Country { get; set; }
    }
}