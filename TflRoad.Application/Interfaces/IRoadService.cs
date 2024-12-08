using TflRoad.Application.Responses;

namespace TflRoad.Application.Interfaces
{
    /// <summary>
    /// Provides methods for retrieving information about roads.
    /// </summary>
    public interface IRoadService
    {
        /// <summary>
        /// Asynchronously retrieves information about a road by its identifier.
        /// </summary>
        /// <param name="roadId">Road Id.</param>
        /// <returns>A result with road details.</returns>
        Task<Result<string>> GetRoadByIdAsync(string roadId);
    }
}