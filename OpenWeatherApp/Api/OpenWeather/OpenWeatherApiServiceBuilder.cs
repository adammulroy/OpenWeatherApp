using System;
using Microsoft.Extensions.DependencyInjection;
using OpenWeatherApp.Api.MessageHandlers;
using OpenWeatherApp.Api.OpenWeather.Endpoints;
using Refit;

namespace OpenWeatherApp.Api.OpenWeather
{
    public static class OpenWeatherApiServiceBuilder
    {
        public static IServiceCollection AddOpenWeatherMap(this IServiceCollection services, AppSettings settings)
        {
            services.AddRefitClient<IOpenWeatherGeolocationApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = new Uri(settings.OpenWeatherMapGeoCodeApiUrl); })
                .ConfigurePrimaryHttpMessageHandler(_ =>
                {
                    var handler = new QueryStringInjectorHttpMessageHandler();
                    handler.Parameters.Add("units", "imperial");
                    handler.Parameters.Add("APPID", settings.OpenWeatherMapApiKey);
                    return handler;
                });

            services.AddRefitClient<IOpenWeatherWeatherApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = new Uri(settings.OpenWeatherMapWeatherApiUrl); })
                .ConfigureHttpMessageHandlerBuilder(_ =>
                {
                    var handler = new QueryStringInjectorHttpMessageHandler();
                    handler.Parameters.Add("units", "imperial");
                    handler.Parameters.Add("APPID", settings.OpenWeatherMapApiKey);
                });

            return services;
        }
    }
}