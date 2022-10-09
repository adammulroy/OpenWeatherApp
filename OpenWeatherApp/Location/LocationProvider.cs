using System.Collections.Generic;
using OpenWeatherApp.Api;

namespace OpenWeatherApp.Location
{
    public class LocationProvider : ILocationProvider
    {
        private readonly ILocationApiService _locationApiService;
        public IEnumerable<Place> CachedLocations { get; } = new List<Place>();

        public LocationProvider(ILocationApiService locationApiService)
        {
            _locationApiService = locationApiService;
        }
    }
}