using System;
using System.Collections.Generic;

namespace OpenWeatherApp.Location
{
    public interface ILocationProvider
    {
        IEnumerable<Place> CachedLocations { get; }
        IObservable<Place> SelectedPlace { get; }

        void SetSelectedPlace(Place place);
    }
}