using System.Net;

namespace TflRoad.Application.Exceptions
{
    /// <summary>
    /// Represents an exception that occurs when an API call fails.
    /// </summary>
    public class ApiException : Exception
    {
        public ApiException(HttpStatusCode statusCode, Uri? requestUri, string responseContent, Exception internalException)
            : base($"API Issue: {statusCode}\r\n" +
                   $"Request Url:\r\n{requestUri?.AbsoluteUri}\r\n" +
                   $"Server Respond:\r\n{responseContent}",
                  internalException)
        {
            HttpStatusCode = statusCode;
            RequestUri = requestUri;
            ResponseContent = responseContent;
        }

        /// <summary>
        /// The HTTP status code returned by the API call.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// The URI of the request that caused the exception.
        /// </summary>
        public Uri? RequestUri { get; }

        /// <summary>
        /// The content of the server's response, useful for identifying error details.
        /// </summary>
        public string ResponseContent { get; }
    }

}
