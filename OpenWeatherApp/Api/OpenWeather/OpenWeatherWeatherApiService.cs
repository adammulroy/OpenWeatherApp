using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenWeatherApp.Api.OpenWeather.Endpoints;
using OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather;
using OpenWeatherApp.Api.OpenWeather.Models.Location;
using OpenWeatherApp.Location;

namespace OpenWeatherApp.Api.OpenWeather
{
    public class OpenWeatherApiWeatherService : IWeatherApiService
    {
        private readonly IOpenWeatherWeatherApi _openWeatherWeatherApi;
        private const int ApiTimeoutSeconds = 5;

        public OpenWeatherApiWeatherService(IOpenWeatherWeatherApi openWeatherWeatherApi,
            IOpenWeatherGeolocationApi openWeatherGeolocationApi)
        {
            _openWeatherWeatherApi = openWeatherWeatherApi;
        }
        
        public async Task<ApiWeatherResult> GetWeatherForLatLon(string latitude, string longitude)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(ApiTimeoutSeconds));
            var result = await _openWeatherWeatherApi.GetCurrentWeatherAsync(latitude, longitude, cts.Token);
            if (result.IsSuccessStatusCode &&
                result.Content != null)
            {
                return new ApiWeatherResult(result.Content, result.StatusCode, result.ReasonPhrase);
            }

            return new ApiWeatherResult(new CurrentWeather(), result.StatusCode, result.ReasonPhrase);
        }

    }
}