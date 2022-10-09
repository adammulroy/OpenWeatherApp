using System.Collections.Generic;
using System.Net;
using OpenWeatherApp.Location;

namespace OpenWeatherApp.Api
{
    public class ApiPlaceResult : ApiResult
    {
        public IEnumerable<Place> Places { get; }

        public ApiPlaceResult(IEnumerable<Place> places, HttpStatusCode statusCode, string? httpMessage) : base(statusCode, httpMessage)
        {
            Places = places;
        }
    }
}