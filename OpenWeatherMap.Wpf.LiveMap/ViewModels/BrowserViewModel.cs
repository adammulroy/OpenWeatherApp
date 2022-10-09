using System.Threading.Tasks;
using CefSharp;
using CefSharp.Wpf.HwndHost;

namespace OpenWeatherMap.Wpf.LiveMap.ViewModels
{
    public class BrowserViewModel
    {
        private readonly ChromiumWebBrowser _chromiumWebBrowser;

        public BrowserViewModel(ChromiumWebBrowser chromiumWebBrowser)
        {
            _chromiumWebBrowser = chromiumWebBrowser;
        }

        public void Init()
        {
            _chromiumWebBrowser.Address =
                "https://openweathermap.org/weathermap?basemap=map&cities=true&layer=precipitation&lat=33.2434&lon=-111.5525&zoom=10&units=imperial";

            _chromiumWebBrowser.FrameLoadEnd += async (sender, e) =>
            {
                if (!e.Frame.IsMain)
                    return;

                //ToDo - Look how to do this automagically later.  We need to when know the browser
                //is done loading javascript classes so we don't need this magic number wait here.
                //quick google says unlikely but worth looking into later.
                await Task.Delay(2000);

                _chromiumWebBrowser.ExecuteScriptAsync(@"
                    const remove = (sel) => document.querySelectorAll(sel).forEach(el => el.remove());
                    remove('.leaflet-bar');");
                _chromiumWebBrowser.ExecuteScriptAsync(@"document.getElementById('nav-website').remove()");
                _chromiumWebBrowser.ExecuteScriptAsync(@"
                    var mapStyle = document.querySelector('.global-map');
                    mapStyle.style.top=0;");
            };

            _chromiumWebBrowser.IsBrowserInitializedChanged += (sender, f) =>
            {
                if (!_chromiumWebBrowser.IsDisposed)
                {
#if DEBUG
                    _chromiumWebBrowser.ShowDevTools();
#endif
                }
            };
        }
    }
}