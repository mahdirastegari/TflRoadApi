namespace TflRoad.Application.Enums
{
    /// <summary>
    /// Represents the various exit codes that the application can return.
    /// These codes are used to indicate the status or result of the application's execution.
    /// </summary>
    public enum ExitCodeEnum
    {
        /// <summary>
        /// Exit code 0: indicates that the application completed with no issues.
        /// </summary>
        NoIssue = 0,

        /// <summary>
        /// Exit code 1: indicates that the argument passed to the application is invalid.
        /// </summary>
        InvalidArguments = 1,

        /// <summary>
        /// Exit code 2: indicates that the provided Road Id is invalid when checked with the API.
        /// </summary>
        InvalidRoadId = 2,

        /// <summary>
        /// Exit code 3: indicates that there was a problem fetching the API results.
        /// </summary>
        ApiError = 3,

        /// <summary>
        /// Exit code 4: indicates that an unhandled exception occurred during execution.
        /// </summary>
        UnhandledException = 4
    }
}
