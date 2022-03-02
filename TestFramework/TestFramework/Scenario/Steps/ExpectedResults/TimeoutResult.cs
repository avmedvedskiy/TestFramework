using System;
using System.Threading.Tasks;
using TestFramework.Scenario.Steps.Base.Result;

namespace TestFramework.Scenario.Steps.ExpectedResults
{
    public class TimeoutResult : IParallelExpectedResult
    {
        private readonly TimeSpan _time;
        public TimeoutResult(int seconds)
        {
            _time = TimeSpan.FromSeconds(seconds);
        }
        
        public async Task Check()
        {
            await Task.Delay(_time);
            throw new TimeoutException();
        }

        public static TimeoutResult OneMinute => new TimeoutResult(60);
        public static TimeoutResult TwoMinutes => new TimeoutResult(60 * 2);
        public static TimeoutResult FiveMinutes => new TimeoutResult(60 * 5);

        public static TimeoutResult TenMinutes => new TimeoutResult(60 * 10);
        
    }
}