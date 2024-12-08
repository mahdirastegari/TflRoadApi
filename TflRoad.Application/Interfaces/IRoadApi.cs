using TflRoad.Application.Exceptions;
using TflRoad.Application.Models.Api;
using TflRoad.Application.Models.Api.RoadResults;

namespace TflRoad.Application.Interfaces
{
    /// <summary>
    /// Rest client for calling the TfL road API
    /// <see cref="https://api-portal.tfl.gov.uk/api-details#api=Road&operation=Road_GetByPathIds"/>
    /// </summary>
    public interface IRoadApi
    {
        /// <summary>
        /// Retrieves information about the specified road based on the given identifier.
        /// </summary>
        /// <param name="id">Road identifiers, e.g., "A406", A2"</param>
        /// <returns>
        /// A <see cref="Task{ApiResponse{RoadCorridor}}"/> representing the asynchronous operation, 
        /// which contains information about the requested road, including status and details.
        /// </returns>
        /// <remarks>
        /// This method sends a GET request to the Transport for London (TfL) API at:
        /// <c>https://api.tfl.gov.uk/Road/{ids}</c>
        /// </remarks>
        /// <exception cref="ApiException">Thrown when the API response result cannot deserialize to application models.</exception>
        Task<ApiResponse<RoadCorridor>> GetRoadByIdAsync(string id);
    }
}
