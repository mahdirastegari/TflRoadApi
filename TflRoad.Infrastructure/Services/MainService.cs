using TflRoad.Application.Enums;
using TflRoad.Application.Exceptions;
using TflRoad.Application.Interfaces;
using TflRoad.Application.Interfaces.Wrappers;

namespace TflRoad.Infrastructure.Services
{
    /// <summary>
    /// Main service to run in the program
    /// </summary>
    /// <param name="roadService">Road service to fetch road information</param>
    /// <param name="console">Console to write output</param>
    /// <param name="environment">Environment to exit the application</param>
    public class MainService(IRoadService roadService,
        IConsole console,
        IEnvironment environment)
    {
        public async Task Run(string[] args)
        {
            try
            {
                Validate(args);
                var result = await roadService.GetRoadByIdAsync(args[0]);
                if (result.IsFailure)
                {
                    console.WriteLine(result.ErrorMessage!);
                    environment.Exit((int)ExitCodeEnum.InvalidRoadId);
                    return;
                }
                else
                {
                    console.WriteLine(result.Value!);
                }
            }
            catch (ApiException ex)
            {
                console.WriteLine(ex.Message);
                environment.Exit((int)ExitCodeEnum.ApiError);
            }
            catch (ArgumentException ex)
            {
                console.WriteLine(ex.Message);
                environment.Exit((int)ExitCodeEnum.InvalidArguments);
            }
            catch (Exception ex)
            {
                console.WriteLine(ex.ToString());
                environment.Exit((int)ExitCodeEnum.UnhandledException);
            }
        }

        /// <summary>
        /// Validate the arguments
        /// </summary>
        /// <param name="args">Console arguments</param>
        /// <exception cref="ArgumentException">Argument exception</exception>
        private void Validate(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                throw new ArgumentException("Please provide Road Id.");
            }

            if (args.Length > 1)
            {
                throw new ArgumentException("Too many arguments.");
            }
        }
    }
}
