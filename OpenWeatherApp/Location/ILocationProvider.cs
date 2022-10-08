using System.Collections.Generic;

namespace OpenWeatherApp.Location
{
    public interface ILocationProvider
    {
        IEnumerable<Place> CachedLocations { get;  }
    }
}