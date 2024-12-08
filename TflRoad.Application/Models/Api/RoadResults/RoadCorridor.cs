using TflRoad.Application.Interfaces;

namespace TflRoad.Application.Models.Api.RoadResults
{
    /// <summary>
    /// Represents the Corridor with its related properties and metadata.
    /// <para>This is the result of <see cref="https://api.tfl.gov.uk/Road/{ids}"/> in <see cref="IRoadApi.GetRoadByIdAsync"/></para>
    /// </summary>
    public class RoadCorridor
    {
        /// <summary>
        /// The Id of the Corridor, e.g., "A406".
        /// </summary>
        public string Id { get; set; } = null!;

        /// <summary>
        /// The display name of the Corridor, e.g., "North Circular (A406)".
        /// This may be identical to the Id.
        /// </summary>
        public string DisplayName { get; set; } = null!;

        /// <summary>
        /// The group name of the Corridor, e.g., "Central London".
        /// Most corridors are not grouped; in such cases, this field can be null.
        /// </summary>
        public string Group { get; set; } = null!;

        /// <summary>
        /// Standard multi-mode status severity code.
        /// </summary>
        public string StatusSeverity { get; set; } = null!;

        /// <summary>
        /// Description of the status severity as applied to RoadCorridors.
        /// </summary>
        public string StatusSeverityDescription { get; set; } = null!;

        /// <summary>
        /// The Bounds of the Corridor, provided as a south-east followed by a 
        /// north-west coordinate pair in geoJSON format, e.g., 
        /// "[[-1.241531,51.242151],[1.641223,53.765721]]".
        /// </summary>
        public string Bounds { get; set; } = null!;

        /// <summary>
        /// The Envelope of the Corridor, represented by the corner coordinates 
        /// of a rectangular (four-point) polygon in geoJSON format, e.g., 
        /// "[[-1.241531,51.242151],[-1.241531,53.765721],[1.641223,53.765721],[1.641223,51.242151]]".
        /// </summary>
        public string Envelope { get; set; } = null!;

        /// <summary>
        /// The start of the period over which the status has been aggregated, 
        /// or null if this represents the current corridor status.
        /// </summary>
        public DateTime? StatusAggregationStartDate { get; set; }

        /// <summary>
        /// The end of the period over which the status has been aggregated, 
        /// or null if this represents the current corridor status.
        /// </summary>
        public DateTime? StatusAggregationEndDate { get; set; }

        /// <summary>
        /// URL to retrieve this Corridor.
        /// </summary>
        public string Url { get; set; } = null!;
    }
}
