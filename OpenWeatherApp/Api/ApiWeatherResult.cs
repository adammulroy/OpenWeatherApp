using System.Net;
using OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather;

namespace OpenWeatherApp.Api
{
    public class ApiWeatherResult : ApiResult
    {
        public ApiWeatherResult(CurrentWeather weather, HttpStatusCode statusCode, string? httpMessage) : base(
            statusCode, httpMessage)
        {
            Weather = weather;
        }

        public CurrentWeather Weather { get; }
    }
}