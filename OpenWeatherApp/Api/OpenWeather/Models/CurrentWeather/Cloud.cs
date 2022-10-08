using System.Text.Json.Serialization;

namespace OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather
{
    public class Cloud
    {
        [JsonPropertyName("Name")]
        public string Description { get; set; }

        [JsonPropertyName("description")]
        public string Cloudiness { get; set; }
    }
}