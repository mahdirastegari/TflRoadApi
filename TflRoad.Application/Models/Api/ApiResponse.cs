namespace TflRoad.Application.Models.Api
{
    /// <summary>
    /// Represents the response of an API call, which can either be a success or a failure.
    /// This class encapsulates the status, additional data related to success, or error data in case of failure.
    /// </summary>
    public class ApiResponse<T>
    {
        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess => SuccessData != null;

        /// <summary>
        /// Gets a value indicating whether the operation failed.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Gets the additional data related to a successful operation.
        /// This could contain information such as fetched data, metadata, or other success-related information.
        /// </summary>
        public T? SuccessData { get; private set; }

        /// <summary>
        /// Gets the additional data related to a failure operation.
        /// This could contain API error messages, error codes, or other failure-related information.
        /// </summary>
        public ApiError? FailureData { get; private set; }

        /// <summary>
        /// Creates a new successful response containing the success-related data.
        /// </summary>
        /// <param name="successData">The additional data related to the successful operation.</param>
        /// <returns>A successful <see cref="ApiResponse"/> containing the specified success data.</returns>
        public static ApiResponse<T> Success(T successData)
        {
            return new()
            {
                SuccessData = successData,
            };
        }

        /// <summary>
        /// Creates a new failure response with the specified failure-related data.
        /// </summary>
        /// <param name="failureData">The additional data related to the failure operation, such as error messages or error codes.</param>
        /// <returns>A failed <see cref="ApiResponse"/> containing the specified failure data.</returns>
        public static ApiResponse<T> Failure(ApiError failureData)
        {
            return new()
            {
                FailureData = failureData,
            };
        }
    }
}
