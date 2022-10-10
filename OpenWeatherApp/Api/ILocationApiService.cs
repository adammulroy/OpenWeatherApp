using System.Threading.Tasks;

namespace OpenWeatherApp.Api
{
    public interface ILocationApiService
    {
        Task<ApiPlaceResult> GetPlaceForLatLon(string latitude, string longitude);
        Task<ApiPlaceResult> GetPlaceForCityName(string cityName);
        Task<ApiPlaceResult> GetPlaceForZipCode(string zipCode);
    }
}