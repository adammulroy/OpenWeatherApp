using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls;
using OpenWeatherApp.Location;
using OpenWeatherMap.Wpf.LiveMap.ViewModels;
using System;
using ReactiveUI;

namespace OpenWeatherMap.Wpf.LiveMap.Views;

public partial class MainWindow : IViewFor<AppViewModel>
{
    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register("ViewModel",
            typeof(AppViewModel), typeof(MainWindow),
            new PropertyMetadata(null));

    public MainWindow(ILocationProvider locationProvider, AppViewModel vm, CurrentWeatherViewModel weatherViewModel)
    {
        InitializeComponent();

        ViewModel = vm;
        WeatherView.ViewModel = weatherViewModel;

        BrowserAdapter = new CefSharpBrowserAdapter(Browser, locationProvider);
        BrowserAdapter.Init();

        this.WhenActivated(disp =>
        {
            this.Bind(ViewModel,
                    viewModel => viewModel.LocationSearchText,
                    view => view.SearchTextBox.Text)
                .DisposeWith(disp);

            this.Bind(ViewModel,
                    viewModel => viewModel.SelectedPlace,
                    view => view.SearchResultsComboBox.SelectedItem)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.SearchResults,
                    view => view.SearchResultsComboBox.ItemsSource)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.SelectedLocationName,
                    view => view.SelectedLocationName.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.SelectedLocationLatitude,
                    view => view.SelectedLocationLatitude.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.SelectedLocationLongitude,
                    view => view.SelectedLocationLongitude.Text)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.SelectedLocationCountry,
                    view => view.SelectedLocationCountry.Text)
                .DisposeWith(disp);

            this.Bind(ViewModel,
                    viewModel => viewModel.HasSearchResults,
                    view => view.SearchResultsComboBox.IsDropDownOpen)
                .DisposeWith(disp);

            this.BindCommand(ViewModel,
                    viewModel => viewModel.CityNameSearch,
                    view => view.CityNameButton)
                .DisposeWith(disp);

            this.BindCommand(ViewModel,
                    viewModel => viewModel.LatitudeAndLongitude,
                    view => view.LatLonButton)
                .DisposeWith(disp);

            this.BindCommand(ViewModel,
                    viewModel => viewModel.ZipCodeSearch,
                    view => view.ZipCodeButton)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.SearchType,
                    view => view.ZipCodeButton.Foreground,
                    x => x == LocationSearchType.ZipCode ? Brushes.Green : Brushes.White)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.SearchType,
                    view => view.CityNameButton.Foreground,
                    x => x == LocationSearchType.CityName ? Brushes.Green : Brushes.White)
                .DisposeWith(disp);

            this.OneWayBind(ViewModel,
                    viewModel => viewModel.SearchType,
                    view => view.LatLonButton.Foreground,
                    x => x == LocationSearchType.LatLon ? Brushes.Green : Brushes.White)
                .DisposeWith(disp);

            this.WhenAnyValue(v => v.ViewModel.WaterMarkText)
                .Subscribe(waterMarkText =>
                {
                    SearchTextBox.SetValue(TextBoxHelper.WatermarkProperty, waterMarkText);
                })
                .DisposeWith(disp);
        });
    }

    public CefSharpBrowserAdapter BrowserAdapter { get; }

    public AppViewModel ViewModel
    {
        get => (AppViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = (AppViewModel)value;
    }
}