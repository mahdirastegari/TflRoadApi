using TflRoad.AcceptanceTests.Drivers;

namespace TflRoad.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class ConsoleOutputStepDefinitions
    {
        private readonly ConsoleTflAppDriver _driver;

        public ConsoleOutputStepDefinitions(ConsoleTflAppDriver driver)
        {
            _driver = driver;
        }

        [Given(@"I have entered the road ID ""(.*)""")]
        public void GivenIHaveEnteredTheRoadID(string roadId)
        {
            _driver.Execute(roadId);
        }

        [Then(@"the application should display:")]
        public void ThenTheApplicationShouldDisplay(string message)
        {
            Assert.Equal(message, _driver.GetOutput());
        }

        [Then(@"it should exit with code (.*)")]
        public void ThenItShouldExitWithCode(int expectedExitCode)
        {
            Assert.Equal(expectedExitCode, _driver.GetExitCode());
        }
    }
}
