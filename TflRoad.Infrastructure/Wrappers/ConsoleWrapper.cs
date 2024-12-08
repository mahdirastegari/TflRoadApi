using TflRoad.Application.Interfaces.Wrappers;

namespace TflRoad.Infrastructure.Wrappers
{
    /// <inheritdoc cref="IConsole" />
    public class ConsoleWrapper : IConsole
    {
        /// <inheritdoc />
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}