using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using OpenWeatherApp.Api.Converters;

namespace OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather
{
    public class CurrentWeather
    {
        [JsonPropertyName("coord")]
        public Coordinates Coordinates { get; set; }
        
        [JsonPropertyName("weather")]
        public IList<WeatherCondition> WeatherConditions { get; set; }

        [JsonPropertyName("main")]
        public WeatherDetail Weather { get; set; }

        //REF - Visibility in meters
        [JsonPropertyName("visibility")]
        public string Visibility { get; set; }
        
        [JsonPropertyName("main")]
        public Temperature Temperature { get; set; }

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }

        [JsonPropertyName("clouds")]
        public Cloud Clouds { get; set; }

        [JsonPropertyName("sys")]
        public Sun Sun { get; set; }

        [JsonPropertyName("dt")]
        [JsonConverter(typeof(UnixToNullableUtcDateTimeConverter))]
        public DateTime? TimeOfDataCollectionUtc { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cod")]
        public int Code { get; set; }
    }

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