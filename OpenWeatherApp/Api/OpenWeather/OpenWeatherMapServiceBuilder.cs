using System;
using Microsoft.Extensions.DependencyInjection;
using OpenWeatherApp.Api.MessageHandlers;
using OpenWeatherApp.Api.OpenWeather.Endpoints;
using Refit;

namespace OpenWeatherApp.Api.OpenWeather
{
    public static class OpenWeatherMapServiceBuilder
    {
        public static IServiceCollection AddOpenWeatherMap(this IServiceCollection services, AppSettings settings)
        {
            services.AddRefitClient<IOpenWeatherGeolocationApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = new Uri(settings.OpenWeatherMapApiTileUrl); })
                .ConfigurePrimaryHttpMessageHandler(_ =>
                {
                    var handler = new QueryStringInjectorHttpMessageHandler();
                    handler.Parameters.Add("units", "imperial");
                    handler.Parameters.Add("APPID", settings.OpenWeatherMapApiKey);
                    return handler;
                });

            services.AddRefitClient<IOpenWeatherMapApi>()
                .ConfigureHttpClient(client => { client.BaseAddress = new Uri(settings.OpenWeatherMapApiUrl); })
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