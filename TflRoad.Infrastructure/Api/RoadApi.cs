using System.Net;
using System.Net.Http.Json;
using TflRoad.Application.Exceptions;
using TflRoad.Application.Interfaces;
using TflRoad.Application.Models.Api;
using TflRoad.Application.Models.Api.RoadResults;

namespace TflRoad.Infrastructure.Api
{
    /// <inheritdoc cref="IRoadApi"/>
    public class RoadApi(HttpClient httpClient) : IRoadApi
    {
        /// <inheritdoc />
        public async Task<ApiResponse<RoadCorridor>> GetRoadByIdAsync(string id)
        {
            var response = await httpClient.GetAsync($"/Road/{id}");
            try
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<RoadCorridor[]>();
                    return ApiResponse<RoadCorridor>.Success(responseContent[0]);
                }
                else
                {
                    var responseContent = await response.Content.ReadFromJsonAsync<ApiError>();
                    return ApiResponse<RoadCorridor>.Failure(responseContent);
                }
            }
            catch (Exception ex)
            {
                // This try-catch block handles exceptions that may occur during JSON deserialization of the response.
                // Typically, these exceptions indicate that the API response format has changed.
                // In such cases, it is advisable to log the full response content by throwing the exception to the higher level for better debugging context.
                throw new ApiException(response.StatusCode, response.RequestMessage?.RequestUri, await response.Content.ReadAsStringAsync(), ex);
            }
        }
    }
}
