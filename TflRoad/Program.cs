using Microsoft.Extensions.DependencyInjection;
using Polly.Extensions.Http;
using Polly;
using TflRoad.Application.Interfaces;
using Polly.Retry;
using TflRoad.Infrastructure.Services;
using TflRoad.Application.Interfaces.Wrappers;
using TflRoad.Infrastructure.Wrappers;
using TflRoad.Infrastructure.Api;
using Microsoft.Extensions.Configuration;
using TflRoad.Application.Configurations;

namespace RoadStatus
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var appConfig = configuration.GetSection("AppConfig").Get<AppSettings>()
                ?? throw new Exception("App config is not read correctly");

            var serviceProvider = RegisterIoc(appConfig);
            using var scope = serviceProvider.CreateScope();
            var mainService = scope.ServiceProvider.GetRequiredService<MainService>();
            await mainService.Run(args);
        }

        private static IServiceProvider RegisterIoc(AppSettings appConfig)
        {
            // Set up a ServiceCollection
            var services = new ServiceCollection();

            //Register wrappers
            services.AddSingleton<IConsole, ConsoleWrapper>();
            services.AddTransient<IEnvironment, EnvironmentWrapper>();
            services.AddTransient(_ => new TflApiIdKeyQueryParameterHandler(appConfig.AppId, appConfig.DeveloperKey));

            // Register HttpClient with Polly policies
            services
                .AddHttpClient<IRoadApi, RoadApi>(client => client.BaseAddress = new Uri(appConfig.BaseApiAddress))
                .AddHttpMessageHandler<TflApiIdKeyQueryParameterHandler>()
                .AddPolicyHandler(GetRetryPolicy());

            services.AddTransient<IRoadService, RoadService>();
            services.AddSingleton<MainService>();

            return services.BuildServiceProvider();
        }

        // Retry policy
        private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                    retryCount: 3,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)),
                    onRetry: (outcome, timespan, retryAttempt, context) => Console.WriteLine($"Retry {retryAttempt}: Waiting {timespan.TotalSeconds} seconds..."));
        }
    }
}
