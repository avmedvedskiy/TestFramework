using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestFramework.Scenario.Steps.Base
{
    public class MultipleScenario : IScenario
    {
        public string Name => GetType().Name;
        private readonly List<IScenario> _scenarios = new List<IScenario>();

        public void Add(IScenario scenario)
        {
            _scenarios.Add(scenario);
        }

        public async Task Run()
        {
            foreach (var scenario in _scenarios)
            {
                await scenario.Run();
            }
        }

        public void ForEach<T>(Action<T> action)
        {
            foreach (var scenario in _scenarios)
            {
                scenario.ForEach(action);
            }
        }
    }
}