using System.Diagnostics;

namespace TflRoad.AcceptanceTests.Drivers
{
    public class ConsoleTflAppDriver
    {
        private string? _output;
        private int _exitCode;

        /// <summary>
        /// Executes the console application with the given arguments.
        /// </summary>
        /// <param name="args">The arguments to pass to the console application.</param>
        public void Execute(string args)
        {
#if DEBUG
            const string configFolder = "Debug";
#else
            const string configFolder = "Release";
#endif
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var appPath = Path.Combine(baseDir, $@"..\..\..\..\TflRoad\bin\{configFolder}\net8.0");
            appPath = Path.GetFullPath(appPath);
            using var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(appPath, "RoadStatus.exe"),
                    WorkingDirectory = appPath,
                    Arguments = args,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            process.WaitForExit();
            _output = process.StandardOutput.ReadToEnd();
            _exitCode = process.ExitCode;
        }

        /// <summary>
        /// Gets the output from the console application.
        /// </summary>
        /// <returns>The standard output as a string.</returns>
        public string GetOutput()
        {
            return _output.Trim();
        }

        /// <summary>
        /// Gets the exit code from the console application.
        /// </summary>
        /// <returns>The exit code as an integer.</returns>
        public int GetExitCode()
        {
            return _exitCode;
        }
    }
}