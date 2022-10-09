using System.Net;

namespace OpenWeatherApp.Api
{
    public class ApiResult
    {
        public ApiResult(HttpStatusCode statusCode, string? httpMessage = "")
        {
            StatusCode = statusCode;
            HttpMessage = httpMessage;
        }

        public HttpStatusCode StatusCode { get; }
        public string? HttpMessage { get; }
    }
}