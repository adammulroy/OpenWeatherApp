using System.Text.Json.Serialization;

namespace OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather
{
    public class Wind
    {
        //REF - Default: meter/sec, Metric: meter/sec, Imperial: miles/hour.
        [JsonPropertyName("speed")] public double Speed { get; set; }

        //REF - https://forecast.weather.gov/glossary.php?word=wind%20direction
        //The true direction from which the wind is blowing at a given location (i.e., wind blowing from the north to the south is a north wind).
        //It is normally measured in tens of degrees from 10 degrees clockwise through 360 degrees. North is 360 degrees. A wind direction of 0 degrees
        //is only used when wind is calm. 
        [JsonPropertyName("deg")] public double Degree { get; set; }

        //REF Unit Default: meter/sec, Metric: meter/sec, Imperial: miles/hour
        [JsonPropertyName("gust")] public double Gust { get; set; }
    }
}