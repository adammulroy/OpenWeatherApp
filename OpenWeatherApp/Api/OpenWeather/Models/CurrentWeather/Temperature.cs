using System.Text.Json.Serialization;

namespace OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather
{
    public class Temperature
    {
        //REF - Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit. 
        [JsonPropertyName("temp")] public double CurrentTemperature { get; set; }

        [JsonPropertyName("feels_like")] public double FeelsLike { get; set; }

        //REF - hPa units
        [JsonPropertyName("pressure")] public double AtmosphericPressure { get; set; }

        [JsonPropertyName("humidity")] public double Humidity { get; set; }

        [JsonPropertyName("temp_min")] public double MinimumTemperature { get; set; }

        [JsonPropertyName("temp_max")] public double MaximumTemperature { get; set; }
    }
}