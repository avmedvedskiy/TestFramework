using System;
using System.IO;
using System.Linq;
using SharpAdbClient;

namespace TestFramework.AndroidDeviceBridge
{
    public static class DeviceExtensions
    {
        //private const string ADB_LOGCAT_COMMAND_FORMAT =  "logcat -t \"{0}\"  > \"{1}\"";
        //need to set time of test start and path to file
        //Console.WriteLine(DateTime.Now.ToString( "MM-dd hh:mm:ss.000"));
        //private const string ADB_LOGCAT_COMMAND = "logcat -t \"02-16 11:20:00.000\"";
        //private const string ADB_LOGCAT_SHORT_COMMAND_FORMAT =  "adb logcat -t \"{0}\"  > \"{1}\"";
        
        private const string ADB_IP_ADDRESS_SHOW = "ip addr show  | grep 'inet ' | cut -d ' ' -f 6 | cut -d / -f 1";
        public static string GetIp(this DeviceData device)
        {
            var client = new AdbClient();
            var receiver = new ConsoleOutputReceiver();
            
            client.ExecuteRemoteCommand(ADB_IP_ADDRESS_SHOW, device, receiver);
            var addresses = receiver.ToString().Split(
                new[] { Environment.NewLine },
                StringSplitOptions.RemoveEmptyEntries);

            return addresses.Last();
        }
        
        
        private const string ADB_LOGCAT_COMMAND_FORMAT = "logcat -t \"{0}\"";
        private const string ANDROID_LOGS_FILENAME = "androidLogs.txt";
        private const string TIME_FORMAT = "MM-dd HH:mm:ss.000";

        public static void SaveLogs(this DeviceData device, DateTime startTime, string folder)
        {
            var client = new AdbClient();
            var receiver = new ConsoleOutputReceiver();
            string fullLogsFilePath = Path.Combine(folder, ANDROID_LOGS_FILENAME);
            
            string query = String.Format(
                ADB_LOGCAT_COMMAND_FORMAT,
                startTime.ToString(TIME_FORMAT));
            
            client.ExecuteRemoteCommand(
                query, 
                device, 
                receiver);
            
            using(var sw = File.CreateText(fullLogsFilePath))
            {
                sw.WriteLine($"{device.Name} {device.Model}");
                sw.Write(receiver.ToString());
            }
        }
    }
}