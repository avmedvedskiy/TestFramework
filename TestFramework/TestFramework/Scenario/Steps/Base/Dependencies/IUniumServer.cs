using TestFramework.Unium.Connection;

namespace TestFramework.Scenario.Steps.Base.Dependencies
{
    public interface IUniumServerStep
    {
        void SetServer(UniumServer server);
    }
}