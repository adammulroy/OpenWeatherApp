using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenWeatherApp.Api.OpenWeather.Endpoints;
using OpenWeatherApp.Api.OpenWeather.Models.Location;
using OpenWeatherApp.Location;

namespace OpenWeatherApp.Api.OpenWeather
{
    public class OpenWeatherLocationApiService : ILocationApiService
    {
        private const int ApiTimeoutSeconds = 5;
        private readonly IOpenWeatherGeolocationApi _geolocationApi;

        public OpenWeatherLocationApiService(IOpenWeatherGeolocationApi geolocationApi)
        {
            _geolocationApi = geolocationApi;
        }

        public async Task<ApiPlaceResult> GetPlaceForLatLon(string latitude, string longitude)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(ApiTimeoutSeconds));
            var result = await _geolocationApi.GetLocationByLatLon(latitude, longitude, cts.Token);
            if (result.IsSuccessStatusCode &&
                result.Content != null)
            {
                var places = result.Content.Select(location => location.ToPlace()).ToList();
                return new ApiPlaceResult(places, result.StatusCode, result.ReasonPhrase);
            }

            return new ApiPlaceResult(new List<Place>(), result.StatusCode, result.ReasonPhrase);
        }

        public async Task<ApiPlaceResult> GetPlaceForCityName(string cityName)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(ApiTimeoutSeconds));
            var result = await _geolocationApi.GetLocationByCityName(cityName, cts.Token);
            if (result.IsSuccessStatusCode &&
                result.Content != null)
            {
                var places = result.Content.Select(cityNameLocation => cityNameLocation.ToPlace()).ToList();
                return new ApiPlaceResult(places, result.StatusCode, result.ReasonPhrase);
            }

            return new ApiPlaceResult(new List<Place>(), result.StatusCode, result.ReasonPhrase);
        }

        public async Task<ApiPlaceResult> GetPlaceForZipCode(string zipCode)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(ApiTimeoutSeconds));
            var result = await _geolocationApi.GetLocationByZipCode(zipCode, cts.Token);
            if (result.IsSuccessStatusCode &&
                result.Content != null)
            {
                var places = new List<Place> { result.Content.ToPlace() };
                return new ApiPlaceResult(places, result.StatusCode, result.ReasonPhrase);
            }

            return new ApiPlaceResult(new List<Place>(), result.StatusCode, result.ReasonPhrase);
        }
    }
}