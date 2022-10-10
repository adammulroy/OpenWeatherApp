using System.Reactive.Disposables;
using OpenWeatherMap.Wpf.LiveMap.ViewModels;
using ReactiveUI;

namespace OpenWeatherMap.Wpf.LiveMap.Views;

public partial class CurrentWeatherView : IViewFor<CurrentWeatherViewModel>
{
    public CurrentWeatherView()
    {
        InitializeComponent();

        this.WhenActivated(disp =>
        {
            this.OneWayBind(ViewModel,
                    viewModel => viewModel.CurrentTemperature,
                    view => view.CurrentTemperature.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.FeelsLike,
                    view => view.FeelsLike.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.MinimumTemperature,
                    view => view.TodaysLow.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.MaximumTemperature,
                    view => view.TodaysHigh.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.Humidity,
                    view => view.Humidity.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.WeatherConditions,
                    view => view.WeatherConditions.ItemsSource)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.VisibilityInMeters,
                    view => view.Visibilty.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.HowCloudy,
                    view => view.Cloudiness.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.CloudDescription,
                    view => view.CloudDescription.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.TimeCollected,
                    view => view.DateOfCollection.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.WindSpeed,
                    view => view.WindSpeed.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.WindDirection,
                    view => view.WindDirection.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.WindGustMph,
                    view => view.WindGusts.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.SunriseTime,
                    view => view.Sunrise.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.SunsetTime,
                    view => view.Sunset.Text)
                .DisposeWith(disp);
        });
    }

    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (CurrentWeatherViewModel)value;
    }

    public CurrentWeatherViewModel ViewModel { get; set; }
}