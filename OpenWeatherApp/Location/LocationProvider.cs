using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using OpenWeatherApp.Api;

namespace OpenWeatherApp.Location
{
    public class LocationProvider : ILocationProvider
    {
        private readonly ILocationApiService _locationApiService;
        private ISubject<Place> _selectedPlace = new Subject<Place>();
        
        public IEnumerable<Place> CachedLocations { get; } = new List<Place>();
        public IObservable<Place> SelectedPlace { get; }

        public LocationProvider(ILocationApiService locationApiService)
        {
            SelectedPlace = _selectedPlace.AsObservable();
            _locationApiService = locationApiService;
        }

        public void SetSelectedPlace(Place place)
        {
            _selectedPlace.OnNext(place);
        }
    }
}