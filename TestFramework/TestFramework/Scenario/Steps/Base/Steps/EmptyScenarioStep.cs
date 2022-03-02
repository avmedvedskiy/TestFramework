using System;
using System.Threading.Tasks;

namespace TestFramework.Scenario.Steps.Base.Steps
{
    public class EmptyScenarioStep : BaseScenarioStep
    {

        protected override async Task Step()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}