using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OpenWeatherApp.Api.OpenWeather.Models.Location;
using Refit;

namespace OpenWeatherApp.Api.OpenWeather.Endpoints
{
    public interface IOpenWeatherGeolocationApi
    {
        //http://api.openweathermap.org/geo/1.0/zip?zip={zip code},{country code}&appid={API key}
        [Get("/zip?zip={zipCode}," + "US")]
        Task<ApiResponse<ZipCodeLocation>> GetLocationByZipCode(string zipCode,
            CancellationToken cancellationToken = default);

        //http://api.openweathermap.org/geo/1.0/reverse?lat={lat}&lon={lon}&limit={limit}&appid={API key}
        [Get("/reverse?lat={latitude}&lon={longitude}")]
        Task<ApiResponse<IEnumerable<LatLonLocation>>> GetLocationByLatLon(string latitude, string longitude,
            CancellationToken cancellationToken = default);

        //http://api.openweathermap.org/geo/1.0/direct?q={city name},{state code},{country code}&limit={limit}&appid={API key}
        [Get("/direct?q={cityName}," + "US")]
        Task<ApiResponse<IEnumerable<CityNameLocation>>> GetLocationByCityName(string cityName,
            CancellationToken cancellationToken = default);
    }
}