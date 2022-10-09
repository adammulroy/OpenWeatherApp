using System.Linq;
using System.Threading.Tasks;
using OpenWeatherApp.Api;

namespace OpenWeatherApp.Location
{
    public class LocationViewModel
    {
        private readonly ILocationProvider _locationProvider;
        private readonly ILocationApiService _apiService;

        public LocationViewModel(ILocationProvider locationProvider, ILocationApiService apiService)
        {
            _locationProvider = locationProvider;
            _apiService = apiService;
        }

        public async Task<bool> TrySearch(string text, LocationSearchType searchType = LocationSearchType.CityName)
        {
            if (string.IsNullOrEmpty(text) ||
                text.Length <= 4)
                return false;

            var res = await _apiService.GetPlaceForCityName(text);
            if (!res.Places.Any()) return false;
            
            var firstPlace = res.Places.First();
            _locationProvider.SetSelectedPlace(firstPlace);

            return false;
        }
    }

    public enum LocationSearchType
    {
        ZipCode,
        CityName,
        LatLon
    }
}