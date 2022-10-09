namespace OpenWeatherApp.Weather
{
    public class WeatherViewModel
    {
        private readonly IWeatherProvider _weatherProvider;

        public WeatherViewModel(IWeatherProvider weatherProvider)
        {
            _weatherProvider = weatherProvider;
        }
    }
}