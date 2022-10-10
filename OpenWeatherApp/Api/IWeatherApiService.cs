using System.Threading.Tasks;

namespace OpenWeatherApp.Api
{
    public interface IWeatherApiService
    {
        Task<ApiWeatherResult> GetWeatherForLatLon(string latitude, string longitude);
    }
}