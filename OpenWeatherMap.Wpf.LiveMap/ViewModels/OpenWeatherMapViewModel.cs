using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using OpenWeatherApp.Api;
using OpenWeatherApp.Location;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using static System.Collections.Specialized.NotifyCollectionChangedAction;

namespace OpenWeatherMap.Wpf.LiveMap.ViewModels;

public class AppViewModel : ReactiveObject, IActivatableViewModel
{
    private readonly ILocationApiService _apiService;
    private readonly ILocationProvider _locationProvider;

    public AppViewModel(ILocationProvider locationProvider, ILocationApiService apiService)
    {
        _locationProvider = locationProvider;
        _apiService = apiService;
        SearchResults = new ObservableCollection<Place>();
        Activator = new ViewModelActivator();
        SearchType = LocationSearchType.CityName;
        
        WaterMarkText = "Search for location by City Name..";

        var disp = new CompositeDisposable();
        
        SetupSelectedLocation(disp);
        SetupCommands(disp);
        SetupTextSearch(disp);

    }

    public ObservableCollection<Place> SearchResults { get; }
    [Reactive] public Place SelectedPlace { get; set; }
    [Reactive] public bool HasSearchResults { get; set; }
    [Reactive] public string WaterMarkText { get; set; }
    [Reactive] public string LocationSearchText { get; set; }
    [Reactive] public string SelectedLocationName { get; set; }
    [Reactive] public string SelectedLocationLatitude { get; set; }
    [Reactive] public string SelectedLocationLongitude { get; set; }
    [Reactive] public string SelectedLocationCountry { get; set; }
    public ReactiveCommand<Unit, Unit> ZipCodeSearch { get; private set; }
    public ReactiveCommand<Unit, Unit> CityNameSearch { get; private set; }
    public ReactiveCommand<Unit, Unit> LatitudeAndLongitude { get; private set; }
    [Reactive] public LocationSearchType SearchType { get; set; }

    public ViewModelActivator Activator { get; }

    private void SetupSelectedLocation(CompositeDisposable d)
    {
        var selectedPlaceChanged =
            this.WhenAnyValue(x => x.SelectedPlace)
                .Where(x => x != null);

        selectedPlaceChanged
            .ObserveOnDispatcher()
            .Subscribe(x =>
            {
                SelectedLocationName = x.DisplayName;
                SelectedLocationLatitude = x.Latitude.ToString(CultureInfo.CurrentCulture);
                SelectedLocationLongitude = x.Longitude.ToString(CultureInfo.CurrentCulture);
                SelectedLocationCountry = x.Country;
            })
            .DisposeWith(d);

        selectedPlaceChanged
            .Subscribe(x =>
            {
                SearchResults.Clear();
                _locationProvider.SetSelectedPlace(x);
            });
    }

    private void SetupCommands(CompositeDisposable d)
    {
        CityNameSearch = ReactiveCommand.Create(() =>
        {
            LocationSearchText = string.Empty;
            SearchType = LocationSearchType.CityName;
            WaterMarkText = "Search by City Name";
        }, Observable.Return(true), RxApp.MainThreadScheduler)
        .DisposeWith(d);

        LatitudeAndLongitude = ReactiveCommand.Create(() =>
        {
            LocationSearchText = string.Empty;
            SearchType = LocationSearchType.LatLon;
            WaterMarkText = "Search using latitude,longitude i.e. -41.5,59.2";
        }, Observable.Return(true), RxApp.MainThreadScheduler)
        .DisposeWith(d);

        ZipCodeSearch = ReactiveCommand.Create(() =>
        {
            LocationSearchText = string.Empty;
            SearchType = LocationSearchType.ZipCode;
            WaterMarkText = "Search for location by Zip Code";
        }, Observable.Return(true), RxApp.MainThreadScheduler)
        .DisposeWith(d);
    }

    private void SetupTextSearch(CompositeDisposable d)
    {
        SearchResults
            .ObserveCollectionChanges()
            .ObserveOnDispatcher()
            .Subscribe(x =>
            {
                if (x.EventArgs.Action is Add or Move or Replace)
                    HasSearchResults = true;
                else
                    HasSearchResults = false;
            });

        //Here we listen for changes to the search text box
        //we only accept new inputs every 500ms
        //we check that the input text has changed from the last value
        //we check the the value is not empty
        var searchText = this
            .WhenAnyValue(x => x.LocationSearchText)
            .Throttle(TimeSpan.FromMilliseconds(500))
            .Select(searchText => searchText?.Trim())
            .DistinctUntilChanged()
            .Where(searchText => !string.IsNullOrWhiteSpace(searchText));

        //this sets us up to know what the last search was
        //we start with empty
        var previousSearch =
            searchText.StartWith(string.Empty);

        //skip one emittance of the stream
        var newSearchText =
            previousSearch.Skip(1);

        //Clear search results if our new search is different
        //simple startswith check for now
        newSearchText
            .Zip(previousSearch, (l, p) => new { NewSearch = l, PreviousSearch = p })
            .ObserveOnDispatcher()
            .Do(x =>
            {
                if (!x.NewSearch.StartsWith(x.PreviousSearch))
                    SearchResults.Clear();
            })
            .Subscribe()
            .DisposeWith(d);

        //We validate the search text before searching
        //for each valid search text in the observable stream, we search, should be one
        //we merge the IEnumerable<Place> from API with our cached IEnumerable<Place>
        //we swap to the UI thread
        //ensure there are results from the API
        //Add the results if they are new
        //we capture any exceptions from the observable stream so the observable doesn't terminate
        //this is bad behavior without logging but logging will come later
        searchText
            .Where(searchTerm => ValidateSearch(SearchType, searchTerm))
            .SelectMany(search => Observable.FromAsync(() => TrySearch(SearchType, search)))
            .Catch<IEnumerable<Place>, Exception>(e =>
            {
                //TODO - Log the Exception e here

                return Observable.Return<IEnumerable<Place>>(new List<Place>());
            })
            .Where(x => x.Any())
            .ObserveOnDispatcher()
            .Do(places => { SearchResults.AddRange(places); })
            .Subscribe()
            .DisposeWith(d);
    }

    private async Task<IEnumerable<Place>> TrySearch(LocationSearchType searchType, string text)
    {
        var emptyPlaces = new List<Place>();
        switch (searchType)
        {
            case LocationSearchType.ZipCode:
                var zips = await _apiService.GetPlaceForZipCode(text);
                return zips?.Places ?? emptyPlaces;
            case LocationSearchType.CityName:
                var cities = await _apiService.GetPlaceForCityName(text);
                return cities?.Places ?? emptyPlaces;
            case LocationSearchType.LatLon:
                var latLon = GetLatLonFromText(text);

                if (double.IsNaN(latLon.lat) ||
                    double.IsNaN(latLon.lon))
                    return emptyPlaces;

                var res = await _apiService.GetPlaceForLatLon(latLon.lat.ToString(), latLon.lon.ToString());
                return res?.Places ?? emptyPlaces;

            default:
                return emptyPlaces;
        }
    }

    public bool ValidateSearch(LocationSearchType searchType, string text)
    {
        switch (searchType)
        {
            case LocationSearchType.ZipCode:
            {
                if (!ValidateTextForUsZipCode(text))
                    return false;
                break;
            }
            case LocationSearchType.CityName:
            {
                if (!ValidateTextForCityName(text))
                    return false;
                break;
            }
            case LocationSearchType.LatLon:
            {
                if (!ValidateTextForLatLon(text))
                    return false;
                break;
            }
            default:
                return false;
        }

        return true;
    }

    public bool ValidateTextForUsZipCode(string text)
    {
        if (string.IsNullOrEmpty(text))
            return false;

        var parsed = int.TryParse(text, out var zipCode);
        if (!parsed ||
            zipCode is < 1 or > 99950)
            return false;

        return true;
    }

    public bool ValidateTextForCityName(string text)
    {
        if (string.IsNullOrEmpty(text) ||
            text.Length < 4)
            return false;

        return text.All(chr => char.IsLetter(chr) || char.IsWhiteSpace(chr) || chr == '-');
    }

    public bool ValidateTextForLatLon(string text)
    {
        var res = GetLatLonFromText(text);

        if (double.IsNaN(res.lat) ||
            double.IsNaN(res.lon))
            return false;

        return true;
    }

    private (double lat, double lon) GetLatLonFromText(string text)
    {
        var nanLatLon = (double.NaN, double.NaN);

        if (string.IsNullOrEmpty(text) ||
            !text.Contains(','))
            return nanLatLon;

        var latLon = text.Split(',');
        var lat = latLon[0];
        var lon = latLon[1];

        var parsedLat = double.TryParse(lat, out var dblLat);
        var parsedLon = double.TryParse(lon, out var dblLon);

        if (!parsedLat ||
            dblLat is < -90 or > 90)
            return nanLatLon;

        if (!parsedLon ||
            dblLon is < -180 or > 180)
            return nanLatLon;

        return (dblLat, dblLon);
    }
}