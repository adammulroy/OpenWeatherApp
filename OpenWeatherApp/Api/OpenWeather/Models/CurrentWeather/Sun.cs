using System;
using System.Text.Json.Serialization;
using OpenWeatherApp.Api.Converters;

namespace OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather
{
    public class Sun
    {
        [JsonPropertyName("sunrise")]
        [JsonConverter(typeof(UnixToNullableUtcDateTimeConverter))]
        public DateTime? SunriseTimeUtc { get; set; }

        [JsonPropertyName("sunset")]
        [JsonConverter(typeof(UnixToNullableUtcDateTimeConverter))]
        public DateTime? SunsetTimeUtc { get; set; }
    }
}