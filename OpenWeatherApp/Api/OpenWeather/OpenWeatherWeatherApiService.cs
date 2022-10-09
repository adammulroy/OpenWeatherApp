using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenWeatherApp.Api.OpenWeather.Endpoints;
using OpenWeatherApp.Api.OpenWeather.Models.Location;
using OpenWeatherApp.Location;

namespace OpenWeatherApp.Api.OpenWeather
{
    public class OpenWeatherApiWeatherService : IWeatherApiService
    {
        private readonly IOpenWeatherWeatherApi _openWeatherWeatherApi;
        private const int ApiTimeoutSeconds = 5;

        public OpenWeatherApiWeatherService(IOpenWeatherWeatherApi openWeatherWeatherApi,
            IOpenWeatherGeolocationApi openWeatherGeolocationApi)
        {
            _openWeatherWeatherApi = openWeatherWeatherApi;
        }
    }
}