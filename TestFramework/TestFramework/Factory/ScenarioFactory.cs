using System;
using System.Threading;
using TestFramework;
using TestFramework.Scenario;
using TestFramework.Scenario.Steps.Base;
using TestFramework.Scenario.Steps.Base.Dependencies;
using TestFramework.Unium.Connection;

namespace TestFramework.Factory
{
    public static class ScenarioFactory
    {
        public static IScenario CreateMultipleScenario(string address, string port, string deviceModel,
            params Type[] scenariosTypes)
        {
            var scenario = new MultipleScenario();
            foreach (var scenarioType in scenariosTypes)
            {
                scenario.Add(Activator.CreateInstance(scenarioType) as IScenario);
            }
            InitScenario(scenario,address, port, deviceModel);
            return scenario;
        }

        public static IScenario CreateScenario<T>(string address, string port, string deviceModel)
            where T : IScenario, new()
        {
            IScenario scenario = new T();
            return InitScenario(scenario, address, port, deviceModel);
        }

        public static IScenario CreateRepeatScenario<T>(string address, string port, string deviceModel, int count)
            where T : IScenario, new()
        {
            var scenario = new MultipleScenario();
            for (int i = 0; i < count; i++)
            {
                scenario.Add(new T());
            }
            InitScenario(scenario,address, port, deviceModel);
            return scenario;
        }

        private static IScenario InitScenario(IScenario scenario, string address, string port, string deviceModel)
        {
            UniumServer server = new UniumServer(address, port, new CancellationTokenSource());
            string path = DirectoryFactory.GetArtifactFolder(scenario.Name, deviceModel);

            //set dependencies
            scenario.ForEach<IUniumServerStep>(step => step.SetServer(server));
            scenario.ForEach<IArtifactPath>(step => step.SetArtifactPath(path));
            scenario.ForEach<IStartTime>(step => step.SetStartTime(DateTime.Now));
            return scenario;
        }
    }
}