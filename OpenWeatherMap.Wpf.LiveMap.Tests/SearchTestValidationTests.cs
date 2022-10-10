using System.Reactive.Concurrency;
using System.Windows.Threading;
using OpenWeatherApp.Api;
using OpenWeatherApp.Location;
using OpenWeatherApp.Weather;
using OpenWeatherMap.Wpf.LiveMap.ViewModels;
using ReactiveUI;

namespace OpenWeatherMap.Wpf.LiveMap.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        RxApp.MainThreadScheduler = new DispatcherScheduler(Dispatcher.CurrentDispatcher);
    }

    [TestCase("1234", false)]
    [TestCase("san", false)]
    [TestCase("san tan", true)]
    [TestCase("h2%j", false)]
    [TestCase("$$$", false)]
    [TestCase("#3hh uou", false)]
    [TestCase("chicago", true)]
    [TestCase("new york", true)]
    [Test]
    public void TestCityNames(string input, bool expected)
    {
        var mockLocation = new Mock<ILocationProvider>().Object;
        var mockLocationApi = new Mock<ILocationApiService>().Object;

        var appVm = new AppViewModel(mockLocation, mockLocationApi);
        var validated = appVm.ValidateTextForCityName(input);

        validated.Should().Be(expected);
    }

    [TestCase("$$$$$$", false)]
    [TestCase("$%", false)]
    [TestCase("!4567", false)]
    [TestCase("zip", false)]
    [TestCase("zipcode", false)]
    [TestCase("hundred", false)]
    [TestCase("hundred", false)]
    [TestCase("1234", true)]
    [TestCase("12", true)]
    [TestCase("85140", true)]
    [TestCase("100000", false)]
    [Test]
    public void TestZipCode(string input, bool expected)
    {
        var mockLocation = new Mock<ILocationProvider>().Object;
        var mockLocationApi = new Mock<ILocationApiService>().Object;

        var appVm = new AppViewModel(mockLocation, mockLocationApi);
        var validated = appVm.ValidateTextForUsZipCode(input);

        validated.Should().Be(expected);
    }

    [TestCase("44.56", false)]
    [TestCase("$%", false)]
    [TestCase("!4567", false)]
    [TestCase("4/5", false)]
    [TestCase("4\\5", false)]
    [TestCase("4,j", false)]
    [TestCase("-55.j,%.5", false)]
    [TestCase("-4,55", true)]
    [TestCase("55.4,-8.4", true)]
    [TestCase("85,44", true)]
    [TestCase("5,4", true)]
    [Test]
    public void TestLatLon(string input, bool expected)
    {
        var mockLocation = new Mock<ILocationProvider>().Object;
        var mockLocationApi = new Mock<ILocationApiService>().Object;

        var appVm = new AppViewModel(mockLocation, mockLocationApi);
        var validated = appVm.ValidateTextForLatLon(input);

        validated.Should().Be(expected);
    }
}