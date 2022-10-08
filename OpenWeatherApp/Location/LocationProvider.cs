using System.Collections.Generic;

namespace OpenWeatherApp.Location
{
    public class LocationProvider : ILocationProvider
    {
        public IEnumerable<Place> CachedLocations { get; } = new List<Place>();
    }
}