using System.Threading.Tasks;

namespace TestFramework.Scenario.Steps.Base.Result
{
    public interface IExpectedResult
    {
        Task Check();
    }
}