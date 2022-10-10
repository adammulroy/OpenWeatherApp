using System;
using System.Threading;
using System.Threading.Tasks;
using OpenWeatherApp.Api.OpenWeather.Endpoints;
using OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather;

namespace OpenWeatherApp.Api.OpenWeather
{
    public class OpenWeatherApiWeatherService : IWeatherApiService
    {
        private const int ApiTimeoutSeconds = 5;
        private readonly IOpenWeatherWeatherApi _openWeatherWeatherApi;

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
                return new ApiWeatherResult(result.Content, result.StatusCode, result.ReasonPhrase);

            return new ApiWeatherResult(new CurrentWeather(), result.StatusCode, result.ReasonPhrase);
        }
    }
}