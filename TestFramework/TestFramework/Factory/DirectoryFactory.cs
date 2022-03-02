using System;
using System.Diagnostics;
using System.IO;

namespace TestFramework.Factory
{
    public static class DirectoryFactory
    {
        private static string CurrentDirectory => Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
        
        public static string GetArtifactFolder(string scenarioName, string deviceModel)
        {
            DateTime startTime = DateTime.Now;;
            string artifactsFolder = CreateTestArtifactsFolder(scenarioName,startTime);
            return GetDeviceTestArtifactFolder(deviceModel, artifactsFolder);
        }
        private static string CreateTestArtifactsFolder(string folderName, DateTime time)
        {
            string pathToFolder = Path.Combine(CurrentDirectory, $"{folderName} {time:dd.MM-HH.mm}");
            Directory.CreateDirectory(pathToFolder);
            return pathToFolder;
        }
        private static string GetDeviceTestArtifactFolder(string deviceModel, string artifactsFolder)
        {
            string deviceArtifactFolder = Path.Combine(artifactsFolder, deviceModel);
            Directory.CreateDirectory(deviceArtifactFolder);
            return deviceArtifactFolder;
        }
    }
}