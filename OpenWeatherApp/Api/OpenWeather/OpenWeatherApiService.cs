using System.Threading.Tasks;
using OpenWeatherApp.Api.OpenWeather.Endpoints;

namespace OpenWeatherApp.Api.OpenWeather
{
    public class OpenWeatherApiService : IWeatherApiService
    {
        private readonly IOpenWeatherMapApi _openWeatherMapApi;
        private readonly IOpenWeatherGeolocationApi _openWeatherGeolocationApi;

        public OpenWeatherApiService(IOpenWeatherMapApi openWeatherMapApi, IOpenWeatherGeolocationApi openWeatherGeolocationApi)
        {
            _openWeatherMapApi = openWeatherMapApi;
            _openWeatherGeolocationApi = openWeatherGeolocationApi;
        }
    }
}