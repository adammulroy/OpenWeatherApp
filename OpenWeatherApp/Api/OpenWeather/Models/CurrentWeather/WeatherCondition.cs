using System.Text.Json.Serialization;

namespace OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather
{
    //REF - https://openweathermap.org/weather-conditions
    //REF - http://openweathermap.org/img/wn/10d@2x.png
    //where 10d@2x = Icon string below

    public class WeatherCondition
    {
        [JsonPropertyName("main")] public string Condition { get; set; }

        [JsonPropertyName("description")] public string Description { get; set; }

        [JsonPropertyName("icon")] public string Icon { get; set; }
    }
}