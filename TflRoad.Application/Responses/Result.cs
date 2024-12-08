namespace TflRoad.Application.Responses
{
    /// <summary>
    /// Represents the result of an operation, which can either be a success or a failure.
    /// This class encapsulates the status, the value if successful, and an error message if failed.
    /// </summary>
    /// <typeparam name="T">The type of the value returned in case of a successful operation.</typeparam>
    public sealed class Result<T>
    {
        /// <summary>
        /// Gets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Gets a value indicating whether the operation failed.
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// Gets the error message if the operation failed. If the operation was successful, this will be null.
        /// </summary>
        public string? ErrorMessage { get; }

        /// <summary>
        /// Gets the value returned by the operation if it was successful. If the operation failed, this will be the default value of type <typeparamref name="T"/>.
        /// </summary>
        public T? Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Result{T}"/> class.
        /// This constructor is private to enforce the use of the Success and Failure factory methods.
        /// </summary>
        /// <param name="isSuccess">Indicates if the operation was successful.</param>
        /// <param name="value">The value returned if the operation was successful.</param>
        /// <param name="errorMessage">The error message if the operation failed.</param>
        private Result(bool isSuccess, T? value, string? errorMessage)
        {
            IsSuccess = isSuccess;
            Value = value;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Creates a new successful result containing the given value.
        /// </summary>
        /// <param name="value">The value to be returned by the successful operation.</param>
        /// <returns>A successful <see cref="Result{T}"/> containing the specified value.</returns>
        public static Result<T> Success(T value)
        {
            return new Result<T>(true, value, default);
        }

        /// <summary>
        /// Creates a new failure result with the specified error message.
        /// </summary>
        /// <param name="errorMessage">The error message describing the failure.</param>
        /// <returns>A failed <see cref="Result{T}"/> containing the specified error message.</returns>
        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>(false, default, errorMessage);
        }
    }
}
