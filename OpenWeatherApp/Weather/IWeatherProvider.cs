using System;
using OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather;

namespace OpenWeatherApp.Weather
{
    public interface IWeatherProvider
    {
        IObservable<CurrentWeather> CurrentWeatherUpdate { get; }
    }
}