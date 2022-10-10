﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DynamicData;
using OpenWeatherApp.Api;

namespace OpenWeatherApp.Location
{
    public class LocationProvider : ILocationProvider
    {
        private ISubject<Place> _selectedPlace = new Subject<Place>();

        private readonly SourceCache<Place, string> _cachedLocation = new(x => $"{x.Latitude},{x.Longitude}");
        public ISourceCache<Place, string> CachedLocations => _cachedLocation;
        public IObservable<Place> SelectedPlaceUpdate { get; }

        public LocationProvider(ILocationApiService locationApiService)
        {
            SelectedPlaceUpdate = _selectedPlace.AsObservable();
        }

        public void SetSelectedPlace(Place place)
        {
            _cachedLocation.AddOrUpdate(place);
            _selectedPlace.OnNext(place);
        }
    }
}