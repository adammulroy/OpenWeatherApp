using System;
using System.Collections.Generic;
using DynamicData;

namespace OpenWeatherApp.Location
{
    public interface ILocationProvider
    {
        ISourceCache<Place, string> CachedLocations { get; }
        IObservable<Place> SelectedPlaceUpdate { get; }

        void SetSelectedPlace(Place place);
    }
}