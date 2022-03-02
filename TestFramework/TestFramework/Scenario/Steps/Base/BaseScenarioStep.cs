using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestFramework.Scenario.Steps.Base.Result;

namespace TestFramework.Scenario.Steps.Base
{
    public abstract class BaseScenarioStep : IStep
    {
        private readonly List<IExpectedResult> _expectedResults = new List<IExpectedResult>();
        private readonly List<IExpectedResult> _expectedParallelResults = new List<IExpectedResult>();

        public BaseScenarioStep Expected(IExpectedResult expected)
        {
            _expectedResults.Add(expected);
            return this;
        }

        /// <summary>
        /// Parallel step and expected results, like timeout
        /// </summary>
        public BaseScenarioStep ExpectedParallel(IExpectedResult result)
        {
            _expectedParallelResults.Add(result);
            return this;
        }

        protected abstract Task Step();

        public async Task Run()
        {
            List<Task> tasks = new List<Task>();
            foreach (var result in _expectedParallelResults)
            {
                tasks.Add(result.Check());
            }

            Console.WriteLine($"Step Running {this}");
            tasks.Add(Step());

            var doneTask = await Task.WhenAny(tasks);
            if (doneTask.IsFaulted)
            {
                Console.WriteLine($"Step failure {this}");
                throw doneTask.Exception;
            }

            Console.WriteLine($"Step {this} start expected");

            foreach (var result in _expectedResults)
            {
                await result.Check();
            }

            Console.WriteLine($"Expected Result is Done {this}");
        }

        public IStep ForEachResult<T>(Action<T> action)
        {
            foreach (var expectedResult in _expectedResults)
            {
                if(expectedResult is T cast)
                    action.Invoke(cast);
            }
            foreach (var expectedResult in _expectedParallelResults)
            {
                if(expectedResult is T cast)
                    action.Invoke(cast);
            }
            return this;
        }
    }
}