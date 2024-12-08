using System.Net;
using TflRoad.Application.Interfaces;
using TflRoad.Application.Responses;

namespace TflRoad.Infrastructure.Services
{
    /// <inheritdoc cref="IRoadService"/>
    public class RoadService(IRoadApi roadApi) : IRoadService
    {
        const string SuccessResponseFormat =
            """
            The status of the {0} is as follows
                Road Status is {1}
                Road Status Description is {2}
            """;

        const string FailureResponseFormat =
            """
            {0} is not a valid road
            """;

        /// <inheritdoc />
        public async Task<Result<string>> GetRoadByIdAsync(string roadId)
        {
            var result = await roadApi.GetRoadByIdAsync(roadId);
            if (result.IsSuccess)
            {
                return Result<string>.Success(string.Format(SuccessResponseFormat,
                    result.SuccessData!.DisplayName,
                    result.SuccessData.StatusSeverity,
                    result.SuccessData.StatusSeverityDescription));
            }
            else
            {
                return Result<string>.Failure(result.FailureData!.HttpStatusCode == HttpStatusCode.NotFound
                    ? string.Format(FailureResponseFormat, roadId)
                    : result.FailureData.Message);
            }
        }
    }
}
