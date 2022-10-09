namespace OpenWeatherApp.Location
{
    public class LocationViewModel
    {
        private readonly ILocationProvider _locationProvider;

        public LocationViewModel(ILocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
        }
    }
}