using System.Net;

namespace TflRoad.Application.Models.Api
{
    /// <summary>
    /// Base error response in the TfL API when there is an issue happen
    /// </summary>
    public class ApiError
    {
        ///<summary>
        /// Timestamp in UTC when the error occurred.
        ///</summary>
        public DateTime TimestampUtc { get; set; }

        ///<summary>
        /// Type of exception (e.g., EntityNotFoundException).
        ///</summary>
        public string ExceptionType { get; set; }

        ///<summary>
        /// HTTP status code (e.g., 404).
        ///</summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        ///<summary>
        /// HTTP status as a string (e.g., "NotFound").
        ///</summary>
        public string HttpStatus { get; set; }

        ///<summary>
        /// Relative URI which resulted in the error.
        ///</summary>
        public string RelativeUri { get; set; }

        ///<summary>
        /// Detailed message describing the error.
        ///</summary>
        public string Message { get; set; }
    }
}