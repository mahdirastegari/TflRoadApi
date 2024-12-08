namespace TflRoad.Infrastructure.Api
{
    /// <summary>
    /// A message handler that adds the required API ID and key as query parameters to the outgoing HTTP request.
    /// Used for authenticating with the TfL (Transport for London) API.
    /// </summary>
    /// <param name="appId">The application ID required for API authentication.</param>
    /// <param name="appKey">The application key required for API authentication.</param>
    public class TflApiIdKeyQueryParameterHandler(string appId, string appKey) : DelegatingHandler
    {
        private readonly string _appId = appId;
        private readonly string _appKey = appKey;

        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uriBuilder = new UriBuilder(request!.RequestUri);

            // Parse the existing query string and add API key parameters
            var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            query["app_id"] = _appId;
            query["app_key"] = _appKey;

            // Rebuild the URI with updated query parameters
            uriBuilder.Query = query.ToString();
            request.RequestUri = uriBuilder.Uri;

            // Send the request using the base handler
            return await base.SendAsync(request, cancellationToken);
        }
    }

}
