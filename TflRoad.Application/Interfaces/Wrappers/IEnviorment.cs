namespace TflRoad.Application.Interfaces.Wrappers
{
    /// <summary>
    /// Wrapper interface for <see cref="Environment"/>
    /// </summary>
    public interface IEnvironment
    {
        /// <summary>
        /// Wrap function for <see cref="Environment.Exit(int)"/>
        /// <para><inheritdoc cref="Environment.Exit(int)"/></para>
        /// </summary>
        /// <param name="exitCode">Exit code</param>
        void Exit(int exitCode);
    }
}
