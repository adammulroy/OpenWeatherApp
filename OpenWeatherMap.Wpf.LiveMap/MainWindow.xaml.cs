using OpenWeatherApp.Location;
using OpenWeatherApp.Weather;
using OpenWeatherMap.Wpf.LiveMap.ViewModels;

namespace OpenWeatherMap.Wpf.LiveMap
{
    public partial class MainWindow
    {
        public WeatherViewModel WeatherViewModel { get; }
        public LocationViewModel LocationViewModel { get; }
        public BrowserViewModel BrowserViewModel { get; }
        public MainWindow(WeatherViewModel weatherViewModel, LocationViewModel locationViewModel)
        {
            InitializeComponent();

            WeatherViewModel = weatherViewModel;
            LocationViewModel = locationViewModel;
            BrowserViewModel = new BrowserViewModel(Browser);
            BrowserViewModel.Init();
            
            
        }
    }
}