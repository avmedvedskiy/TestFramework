using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestFramework.Scenario.Steps.Base
{
    public abstract class BaseScenario : IScenario
    {
        public string Name => GetType().Name;
        private readonly List<IStep> _steps = new List<IStep>();

        protected BaseScenario()
        {
            SetUp();
        }

        protected abstract void SetUp();

        protected void Add(IStep step)
        {
            _steps.Add(step);
        }

        public async Task Run()
        {
            int i = 0;
            try
            {
                for (i = 0; i < _steps.Count; i++)
                {
                    await _steps[i].Run();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Step {i} failed"); // add to string in step
                Console.WriteLine(e);
            }
        }

        public void ForEach<T>(Action<T> action)
        {
            foreach (var step in _steps)
            {
                if (step is T cast)
                {
                    action(cast);
                }

                step.ForEachResult(action);
            }
        }
    }
}