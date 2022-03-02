using System;
using System.Threading.Tasks;

namespace TestFramework.Scenario.Steps.Base
{
    public interface IStep
    {
        Task Run();
        IStep ForEachResult<T>(Action<T> action);
    }
}