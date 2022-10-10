using System;
using System.Net;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using OpenWeatherApp.Api;
using OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather;
using OpenWeatherApp.Location;

namespace OpenWeatherApp.Weather
{
    public class WeatherProvider : IWeatherProvider
    {
        private readonly ILocationProvider _locationProvider;
        private readonly IWeatherApiService _weatherApiService;

        private readonly ISubject<CurrentWeather> _currentWeather = new Subject<CurrentWeather>();

        public WeatherProvider(IWeatherApiService weatherApiService, ILocationProvider locationProvider)
        {
            _weatherApiService = weatherApiService;
            _locationProvider = locationProvider;

            CurrentWeatherUpdate = _currentWeather.AsObservable();

            locationProvider.SelectedPlaceUpdate
                .Subscribe(async x =>
                {
                    var weather =
                        await weatherApiService.GetWeatherForLatLon(x.Latitude.ToString(), x.Longitude.ToString());
                    if (weather.StatusCode == HttpStatusCode.OK) _currentWeather.OnNext(weather.Weather);
                });
        }

        public IObservable<CurrentWeather> CurrentWeatherUpdate { get; }
    }
}