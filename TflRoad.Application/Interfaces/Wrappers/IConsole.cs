namespace TflRoad.Application.Interfaces.Wrappers
{
    /// <summary>
    /// Wrapper interface for <see cref="Console"/> that provides basic console output functionality.
    /// </summary>
    public interface IConsole
    {
        /// <summary>
        /// Writes the specified message to the console output.
        /// This method is a wrapper for <see cref="Console.WriteLine(string)"/>.
        /// </summary>
        /// <param name="message">The message to be written to the console.</param>
        void WriteLine(string message);
    }
}
