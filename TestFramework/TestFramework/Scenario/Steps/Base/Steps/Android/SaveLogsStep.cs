using System;
using System.IO;
using System.Threading.Tasks;
using SharpAdbClient;
using TestFramework.AndroidDeviceBridge;
using TestFramework.Scenario.Steps.Base.Dependencies;

namespace TestFramework.Scenario.Steps.Base.Steps.Android
{
    public class SaveLogsStep : BaseScenarioStep , IArtifactPath, IAndroidDevice, IStartTime
    {
        private DeviceData _deviceData;
        private DateTime _startTime;
        private const string ANDROID_LOGS_FILE = "logs";
        private const string UNITY_LOGS_FILE = "unityLogs";
        private string CurrentDirectory { get; set; }
        private string ScreenshotPath(string fileName) => $"{Path.Combine(CurrentDirectory, fileName)}.txt";

        protected override async Task Step()
        {
            if (_deviceData == null)
            {
                Console.WriteLine("Device null, logs are not stored");
                await Task.CompletedTask;
            }
            else
            {
                _deviceData.SaveLogs(_startTime, ScreenshotPath(ANDROID_LOGS_FILE));
                await Task.CompletedTask;
            }
            
        }

        public void SetArtifactPath(string path)
        {
            CurrentDirectory = path;
        }

        public void SetAndroidDevice(DeviceData deviceData)
        {
            _deviceData = deviceData;
        }

        public void SetStartTime(DateTime startTime)
        {
            _startTime = startTime;
        }
    }
}