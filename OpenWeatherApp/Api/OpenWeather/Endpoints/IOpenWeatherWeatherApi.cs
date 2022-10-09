using System.Threading;
using System.Threading.Tasks;
using Refit;

namespace OpenWeatherApp.Api.OpenWeather.Endpoints
{
    public interface IOpenWeatherWeatherApi
    {
        [Get("/weather")]
        Task<ApiResponse<string>> GetCurrentWeatherAsync([AliasAs("q")] string lat, string lon, string appId,
            CancellationToken cancellationToken = default);
    }
}