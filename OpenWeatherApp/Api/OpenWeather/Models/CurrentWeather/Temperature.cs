using System.Text.Json.Serialization;

namespace OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather
{
    public class Temperature
    {
        //REF - Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit. 
        [JsonPropertyName("temp")]
        public string CurrentTemperature { get; set; }
        
        [JsonPropertyName("feels_like")]
        public string FeelsLike { get; set; }
        
        //REF - hPa units
        [JsonPropertyName("pressure")]
        public string AtmosphericPressure { get; set; }

        [JsonPropertyName("humidity")]
        public string Humidity { get; set; }

        [JsonPropertyName("temp_min")]
        public string MinimumTemperature { get; set; }

        [JsonPropertyName("temp_max")]
        public string MaximumTemperature { get; set; }
    }
}