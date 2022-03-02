using TestFramework.Scenario.Steps.Base;
using TestFramework.Scenario.Steps.Base.Steps;
using TestFramework.Scenario.Steps.ExpectedResults;

namespace TestFramework.Scenario
{
    public class TestScenario : BaseScenario
    {
        protected override void SetUp()
        {
            Add(new EmptyScenarioStep()
                .Expected(new EmptyResult()));
            
            Add(new EmptyScenarioStep()
                .Expected(TimeoutResult.FiveMinutes));
        }
    }
}