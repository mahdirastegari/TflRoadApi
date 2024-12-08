namespace TflRoad.Application.Configurations
{
    /// <summary>
    /// Represents the application settings used for configuration purposes.
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the base address for the API used by the application.
        /// </summary>
        public required string BaseApiAddress { get; set; }

        /// <summary>
        /// Gets or sets the application identifier.
        /// <para>Get your app id from https://api-portal.tfl.gov.uk/</para>
        /// </summary>
        public required string AppId { get; set; }

        /// <summary>
        /// Gets or sets the developer key used for authentication or API access.
        /// <para>Get your developer key from https://api-portal.tfl.gov.uk/</para>
        /// </summary>
        public required string DeveloperKey { get; set; }
    }
}
