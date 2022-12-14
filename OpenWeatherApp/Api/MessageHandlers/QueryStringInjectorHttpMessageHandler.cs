using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace OpenWeatherApp.Api.MessageHandlers
{
    public class QueryStringInjectorHttpMessageHandler : DelegatingHandler
    {
        public QueryStringInjectorHttpMessageHandler(Dictionary<string, string>? parameters = null,
            HttpMessageHandler? innerHandler = null)
            : base(innerHandler ?? new HttpClientHandler())
        {
            Parameters = parameters ?? new Dictionary<string, string>();
        }

        public Dictionary<string, string> Parameters { get; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var queryStringParameters = HttpUtility.ParseQueryString(request.RequestUri.Query);
            foreach (var parameter in Parameters)
                queryStringParameters.Add(parameter.Key, parameter.Value);

            var uriBuilder = new UriBuilder(request.RequestUri)
            {
                Query = queryStringParameters.ToString()
            };

            request.RequestUri = new Uri(uriBuilder.ToString());
            return base.SendAsync(request, cancellationToken);
        }
    }
}