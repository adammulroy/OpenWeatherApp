using System.Threading;
using System.Threading.Tasks;
using OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather;
using Refit;

namespace OpenWeatherApp.Api.OpenWeather.Endpoints
{
    public interface IOpenWeatherWeatherApi
    {
        [Get("/weather?lat={lat}&lon={lon}")]
        Task<ApiResponse<CurrentWeather>> GetCurrentWeatherAsync(string lat, string lon,
            CancellationToken cancellationToken = default);
    }
}