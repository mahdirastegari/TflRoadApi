using Moq;
using System.Net;
using TflRoad.Application.Enums;
using TflRoad.Application.Exceptions;
using TflRoad.Application.Interfaces;
using TflRoad.Application.Interfaces.Wrappers;
using TflRoad.Application.Responses;
using TflRoad.Infrastructure.Services;

namespace TflRoad.UnitTests.Services
{
    /// <summary>
    /// Unit-tests for <see cref="MainService"/>
    /// </summary>
    public class MainServiceTests
    {
        private readonly Mock<IRoadService> _mockRoadService;
        private readonly Mock<IConsole> _mockConsole;
        private readonly Mock<IEnvironment> _mockEnvironment;
        private readonly MainService _mainService;

        public MainServiceTests()
        {
            _mockRoadService = new Mock<IRoadService>();
            _mockConsole = new Mock<IConsole>();
            _mockEnvironment = new Mock<IEnvironment>();

            _mainService = new MainService(
                _mockRoadService.Object,
                _mockConsole.Object,
                _mockEnvironment.Object);
        }

        [Fact]
        public async Task Run_ShouldExitWithInvalidArguments_WhenNoArgumentsProvided()
        {
            // Arrange
            var args = Array.Empty<string>();

            // Act
            await _mainService.Run(args);

            // Assert
            _mockConsole.Verify(console => console.WriteLine("Please provide Road Id."), Times.Once);
            _mockEnvironment.Verify(env => env.Exit((int)ExitCodeEnum.InvalidArguments), Times.Once);
        }

        [Fact]
        public async Task Run_ShouldExitWithInvalidArguments_WhenTooManyArgumentsProvided()
        {
            // Arrange
            var args = new[] { "arg1", "arg2" };

            // Act
            await _mainService.Run(args);

            // Assert
            _mockConsole.Verify(console => console.WriteLine("Too many arguments."), Times.Once);
            _mockEnvironment.Verify(env => env.Exit((int)ExitCodeEnum.InvalidArguments), Times.Once);
        }

        [Fact]
        public async Task Run_ShouldExitWithInvalidRoadId_WhenRoadServiceReturnsFailure()
        {
            // Arrange
            var args = new[] { "invalidRoadId" };
            _mockRoadService.Setup(service => service.GetRoadByIdAsync(args[0]))
                .ReturnsAsync(Result<string>.Failure("Invalid road ID."));

            // Act
            await _mainService.Run(args);

            // Assert
            _mockConsole.Verify(console => console.WriteLine("Invalid road ID."), Times.Once);
            _mockEnvironment.Verify(env => env.Exit((int)ExitCodeEnum.InvalidRoadId), Times.Once);
        }

        [Fact]
        public async Task Run_ShouldSerializeResult_WhenRoadServiceReturnsSuccess()
        {
            // Arrange
            var args = new[] { "validRoadId" };
            var roadResult = "Road result message";
            _mockRoadService.Setup(service => service.GetRoadByIdAsync(args[0]))
                .ReturnsAsync(Result<string>.Success(roadResult));

            // Act
            await _mainService.Run(args);

            // Assert
            _mockConsole.Verify(console => console.WriteLine(roadResult), Times.Once);
        }

        [Fact]
        public async Task Run_ShouldHandleApiException_WhenApiExceptionIsThrown()
        {
            // Arrange
            var args = new[] { "roadId" };
            _mockRoadService.Setup(service => service.GetRoadByIdAsync(args[0]))
                .ThrowsAsync(new ApiException(HttpStatusCode.BadRequest, new Uri("https://api.tfl.gov.uk/Road"), "Response content", null));

            // Act
            await _mainService.Run(args);

            // Assert
            const string expectedMessage =
                """
                API Issue: BadRequest
                Request Url:
                https://api.tfl.gov.uk/Road
                Server Respond:
                Response content
                """;
            _mockConsole.Verify(console => console.WriteLine(expectedMessage), Times.Once);
            _mockEnvironment.Verify(env => env.Exit((int)ExitCodeEnum.ApiError), Times.Once);
        }

        [Fact]
        public async Task Run_ShouldHandleUnhandledException_WhenUnexpectedExceptionIsThrown()
        {
            // Arrange
            var args = new[] { "roadId" };
            _mockRoadService.Setup(service => service.GetRoadByIdAsync(args[0]))
                .ThrowsAsync(new Exception("Unexpected error."));

            // Act
            await _mainService.Run(args);

            // Assert
            _mockConsole.Verify(console => console.WriteLine(It.Is<string>(s => s.Contains("Unexpected error."))), Times.Once);
            _mockEnvironment.Verify(env => env.Exit((int)ExitCodeEnum.UnhandledException), Times.Once);
        }
    }

}