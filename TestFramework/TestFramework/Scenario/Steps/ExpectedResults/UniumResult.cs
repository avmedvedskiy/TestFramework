using System.Threading.Tasks;
using TestFramework.Scenario.Steps.Base.Dependencies;
using TestFramework.Scenario.Steps.Base.Result;
using TestFramework.Unium.Connection;

namespace TestFramework.Scenario.Steps.ExpectedResults
{
    public abstract class UniumResult : IExpectedResult, IUniumServerStep
    {
        protected UniumServer Server { get; set; }

        public abstract Task Check();

        public void SetServer(UniumServer server)
        {
            Server = server;
            
        }
    }
}