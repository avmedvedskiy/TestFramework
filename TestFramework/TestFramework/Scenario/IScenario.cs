using System;
using System.Threading.Tasks;

namespace TestFramework.Scenario
{
    public interface IScenario
    {
        string Name { get;}
        Task Run();

        void ForEach<T>(Action<T> action);
    }
}
