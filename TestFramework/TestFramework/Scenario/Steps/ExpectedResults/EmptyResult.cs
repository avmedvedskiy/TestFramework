using System.Threading.Tasks;
using TestFramework.Scenario.Steps.Base.Result;

namespace TestFramework.Scenario.Steps.ExpectedResults
{
    public class EmptyResult : IParallelExpectedResult
    {
        public async Task Check()
        { 
            await Task.Delay(1);
        }
    }
}