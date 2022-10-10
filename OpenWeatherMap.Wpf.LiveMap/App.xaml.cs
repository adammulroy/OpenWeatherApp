using System;
using System.IO;
using System.Windows;
using CefSharp;
using CefSharp.Wpf.HwndHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenWeatherApp;
using OpenWeatherApp.Api;
using OpenWeatherApp.Api.OpenWeather;
using OpenWeatherApp.Location;
using OpenWeatherApp.Weather;
using OpenWeatherMap.Wpf.LiveMap.ViewModels;
using OpenWeatherMap.Wpf.LiveMap.Views;

namespace OpenWeatherMap.Wpf.LiveMap;

public partial class App
{
    private readonly IHost _host;

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => { ConfigureServices(context.Configuration, services); })
            .Build();
    }

    private void ConfigureServices(IConfiguration configuration,
        IServiceCollection services)
    {
        var appSection = configuration.GetSection(nameof(AppSettings));
        var appSettings = appSection.Get<AppSettings>();

        services.AddOpenWeatherMap(appSettings);
        services.AddScoped<IWeatherApiService, OpenWeatherApiWeatherService>();
        services.AddScoped<IWeatherProvider, WeatherProvider>();
        services.AddScoped<ILocationApiService, OpenWeatherLocationApiService>();
        services.AddScoped<ILocationProvider, LocationProvider>();
        services.AddSingleton<CurrentWeatherViewModel>();
        services.AddSingleton<AppViewModel>();
        services.AddSingleton<MainWindow>();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        var settings = new CefSettings
        {
            CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "CefSharp\\Cache")
        };

        settings.CefCommandLineArgs.Add("enable-media-stream");
        settings.CefCommandLineArgs.Add("use-fake-ui-for-media-stream");
        settings.CefCommandLineArgs.Add("enable-usermedia-screen-capturing");

        if (!Cef.IsInitialized) Cef.Initialize(settings, true, browserProcessHandler: null);

        await _host.StartAsync();

        var window = _host.Services.GetRequiredService<MainWindow>();
        window.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        using (_host)
        {
            await _host.StopAsync(TimeSpan.FromSeconds(5));
        }

        base.OnExit(e);
    }
}