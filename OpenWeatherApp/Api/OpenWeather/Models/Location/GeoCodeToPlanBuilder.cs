using OpenWeatherApp.Location;

namespace OpenWeatherApp.Api.OpenWeather.Models.Location
{
    public static class OpenWeatherGeoCodeToPlaceBuilder
    {
        public static Place ToPlace(this ZipCodeLocation zipCodeLocation)
        {
            return new Place(zipCodeLocation.Name, zipCodeLocation.Latitude, zipCodeLocation.Longitude,
                zipCodeLocation.Country);
        }

        public static Place ToPlace(this LatLonLocation latLonLocation)
        {
            return new Place(latLonLocation.Name, latLonLocation.Latitude, latLonLocation.Longitude,
                latLonLocation.Country);
        }

        public static Place ToPlace(this CityNameLocation cityNameLocation)
        {
            return new Place(cityNameLocation.Name, cityNameLocation.Latitude, cityNameLocation.Longitude,
                cityNameLocation.Country);
        }
    }
}