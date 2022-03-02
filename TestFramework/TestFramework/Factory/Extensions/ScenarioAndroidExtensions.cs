using SharpAdbClient;
using TestFramework.AndroidDeviceBridge;
using TestFramework.Scenario;
using TestFramework.Scenario.Steps.Base.Dependencies;

namespace TestFramework.Factory.Extensions
{
    public static class ScenarioAndroidExtensions
    {
        public static IScenario AddAndroidDeviceDependency(this IScenario scenario, DeviceData deviceData)
        {
            scenario.ForEach<IAndroidDevice>(x=>x.SetAndroidDevice(deviceData));
            return scenario;
        }
    }
}