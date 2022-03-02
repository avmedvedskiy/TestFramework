using System.Threading.Tasks;

namespace TestFramework.Scenario.Steps.Base.Steps
{
    public class ConnectServerStep : UniumBaseScenarioStep
    {
        protected override async Task Step()
        {
            await Server.ConnectAsync();
        }
    }
}