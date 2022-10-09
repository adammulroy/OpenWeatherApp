using OpenWeatherApp.Location;
using OpenWeatherApp.Weather;

namespace OpenWeatherMap.Wpf.LiveMap.ViewModels;

public class AppViewModel 
{
    public BrowserViewModel BrowserViewModel { get; }
    public LocationViewModel LocationViewModel { get; }
    public WeatherViewModel WeatherViewModel { get; }

    public AppViewModel(BrowserViewModel browserViewModel, LocationViewModel locationViewModel, WeatherViewModel weatherViewModel)
    {
        BrowserViewModel = browserViewModel;
        LocationViewModel = locationViewModel;
        WeatherViewModel = weatherViewModel;
    }
}