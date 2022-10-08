using OpenWeatherApp.Api.OpenWeather.Endpoints;

namespace OpenWeatherApp.Api.OpenWeather
{
    public class OpenWeatherApiService : IWeatherApiService
    {
        private readonly IOpenWeatherGeolocationApi _openWeatherGeolocationApi;
        private readonly IOpenWeatherMapApi _openWeatherMapApi;

        public OpenWeatherApiService(IOpenWeatherMapApi openWeatherMapApi,
            IOpenWeatherGeolocationApi openWeatherGeolocationApi)
        {
            _openWeatherMapApi = openWeatherMapApi;
            _openWeatherGeolocationApi = openWeatherGeolocationApi;
        }
    }
}