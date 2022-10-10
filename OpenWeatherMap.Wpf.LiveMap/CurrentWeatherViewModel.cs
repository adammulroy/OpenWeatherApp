using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using OpenWeatherApp.Api;
using OpenWeatherApp.Api.OpenWeather.Models.CurrentWeather;
using OpenWeatherApp.Location;
using OpenWeatherApp.Weather;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using static System.Collections.Specialized.NotifyCollectionChangedAction;

namespace OpenWeatherMap.Wpf.LiveMap;

public class CurrentWeatherViewModel : ReactiveObject
{
    private readonly IWeatherProvider _weatherProvider;

    public ObservableCollection<WeatherCondition> WeatherConditions { get; }
    [Reactive] public string VisibilityInMeters { get; set; }
    [Reactive] public string TimeCollected { get; set; }
    [Reactive] public string CurrentTemperature { get; set; }
    [Reactive] public string FeelsLike { get; set; }
    [Reactive] public string AtmosphericPressure { get; set; }
    [Reactive] public string Humidity { get; set; }
    [Reactive] public string MinimumTemperature { get; set; }
    [Reactive] public string MaximumTemperature { get; set; }
    [Reactive] public string WindSpeed { get; set; }
    [Reactive] public string WindDirection { get; set; }
    [Reactive] public string WindGustMph { get; set; }
    [Reactive] public string CloudDescription { get; set; }
    [Reactive] public string HowCloudy { get; set; }
    [Reactive] public string SunriseTime { get; set; }
    [Reactive] public string SunsetTime { get; set; }

    public CurrentWeatherViewModel(IWeatherProvider weatherProvider)
    {
        _weatherProvider = weatherProvider;
        Activator = new ViewModelActivator();
        WeatherConditions = new ObservableCollection<WeatherCondition>();

        
        _weatherProvider.CurrentWeatherUpdate
            .Subscribe(UpdateWeatherFromCurrentWeather);
    }

    private void UpdateWeatherFromCurrentWeather(CurrentWeather currentWeather)
    {
        var weather = currentWeather;
        var culture = CultureInfo.CurrentCulture;
        
        if (weather.TimeOfDataCollectionUtc != null)
            TimeCollected = weather.TimeOfDataCollectionUtc.Value.ToLocalTime().ToString("g");
        if (weather.Sun.SunriseTimeUtc != null)
            SunriseTime = weather.Sun.SunriseTimeUtc.Value.ToLocalTime().ToString("g");
        if (weather.Sun.SunsetTimeUtc != null)
            SunsetTime = weather.Sun.SunsetTimeUtc.Value.ToLocalTime().ToString("g");
        
        WeatherConditions.Clear();
        WeatherConditions.AddRange(weather.WeatherConditions);
        VisibilityInMeters = $"{weather.Visibility} Meters";
        CurrentTemperature = weather.Temperature.CurrentTemperature.ToString(culture) +"°F";
        FeelsLike = weather.Temperature.FeelsLike.ToString(culture)+ "°F";
        Humidity = weather.Temperature.Humidity.ToString(culture);
        MinimumTemperature = weather.Temperature.MinimumTemperature.ToString(culture) + "°F";
        MaximumTemperature = weather.Temperature.MaximumTemperature.ToString(culture) + "°F";
        WindSpeed = weather.Wind.Speed.ToString(culture) + "MPH";
        WindDirection = weather.Wind.Degree.ToString(culture) + "°";
        WindGustMph = weather.Wind.Gust.ToString(culture) + "MPH";
        CloudDescription = weather.Clouds.Description;
        HowCloudy = weather.Clouds.Cloudiness.ToString(culture);
    }

    public ViewModelActivator Activator { get; }
}