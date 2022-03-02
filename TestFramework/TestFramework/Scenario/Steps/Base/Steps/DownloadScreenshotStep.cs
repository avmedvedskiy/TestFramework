using System;
using System.IO;
using System.Threading.Tasks;
using TestFramework.Scenario.Steps.Base.Dependencies;
using TestFramework.Unium.UniumExtensions;

namespace TestFramework.Scenario.Steps.Base.Steps
{
    public class DownloadScreenshotStep : UniumBaseScenarioStep, IArtifactPath
    {        
        private const string TIME_FORMAT = "HH_mm_ss";
        private string CurrentDirectory { get; set; }
        private string ScreenshotPath(string fileName) => $"{Path.Combine(CurrentDirectory, fileName)}.png";

        protected override async Task Step()
        {
            await Server.DownloadScreenshot(ScreenshotPath(DateTime.Now.ToString(TIME_FORMAT)));
        }

        public void SetArtifactPath(string path)
        {
            CurrentDirectory = path;
        }
    }
}