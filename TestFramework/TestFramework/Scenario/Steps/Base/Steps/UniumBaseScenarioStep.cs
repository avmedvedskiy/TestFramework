using TestFramework.Scenario.Steps.Base.Dependencies;
using TestFramework.Unium.Connection;

namespace TestFramework.Scenario.Steps.Base.Steps
{
    public abstract class UniumBaseScenarioStep : BaseScenarioStep,IUniumServerStep
    {
        protected UniumServer Server { get; private set; }

        public void SetServer(UniumServer server)
        {
            Server = server;
        }
        
    }
}