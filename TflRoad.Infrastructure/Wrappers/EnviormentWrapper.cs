using TflRoad.Application.Interfaces.Wrappers;

namespace TflRoad.Infrastructure.Wrappers
{
    /// <inheritdoc cref="IEnvironment"/>
    public class EnvironmentWrapper : IEnvironment
    {
        /// <inheritdoc />
        public void Exit(int exitCode)
        {
            Environment.Exit(exitCode);
        }
    }
}
