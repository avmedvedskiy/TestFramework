using TestFramework.Scenario.Steps.Base;
using TestFramework.Scenario.Steps.Base.Steps;

namespace Example.Scenarios
{
    public class ExampleScenario: BaseScenario
    {
        protected override void SetUp()
        {
            Add(new EmptyScenarioStep());
            Add(new DownloadScreenshotStep());
        }
    }
}