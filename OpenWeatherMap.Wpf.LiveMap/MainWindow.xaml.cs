using System.Threading.Tasks;
using CefSharp;

namespace OpenWeatherMap.Wpf.LiveMap
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Browser.Address =
                "https://openweathermap.org/weathermap?basemap=map&cities=true&layer=precipitation&lat=33.2434&lon=-111.5525&zoom=10&units=imperial";

            Browser.FrameLoadEnd += async (sender, e) =>
            {
                if (!e.Frame.IsMain)
                    return;

                //ToDo - Look how to do this automagically later.  We need to when know the browser
                //is done loading javascript classes so we don't need this magic number wait here.
                //quick google says unlikely but worth looking into later.
                await Task.Delay(2000);

                Browser.ExecuteScriptAsync(@"
                    const remove = (sel) => document.querySelectorAll(sel).forEach(el => el.remove());
                    remove('.leaflet-bar');");
                Browser.ExecuteScriptAsync(@"document.getElementById('nav-website').remove()");
                Browser.ExecuteScriptAsync(@"
                    var mapStyle = document.querySelector('.global-map');
                    mapStyle.style.top=0;");
            };

            Browser.IsBrowserInitializedChanged += (sender, f) =>
            {
                if (!Browser.IsDisposed)
                {
#if DEBUG
                    Browser.ShowDevTools();
#endif
                }
            };
        }
    }
}