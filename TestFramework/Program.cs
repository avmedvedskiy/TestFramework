using System;
using System.Threading.Tasks;
using Example.Scenarios;
using SharpAdbClient;
using TestFramework.Factory;
using TestFramework.Factory.Extensions;

class Program
{
    //finish result must be like this https://habr.com/ru/company/vk/blog/579210/
    //maybe need use this for mac or linus OS https://habr.com/ru/post/447236/
    //sharp adb examples https://www.codeproject.com/Tips/1041232/Manipulating-Android-Devices-from-a-Windows-Csharp
    //Appium + c# + specflow https://www.youtube.com/watch?v=BXzFeMtFRCU
    //https://www.udemy.com/course/automation-framework-development-with-appium/
        
    private const string PATH_TO_ADB_EXE = @"D:\Program Files\Nox\bin\adb.exe"; //need to be changed to find adb via system variables
    private const string DEFAULT_PORT = "8342";
    private const string LOCALHOST = "localhost";

    static async Task Main()
    {
        var scenario =
            ScenarioFactory.CreateScenario<ExampleScenario>(LOCALHOST, DEFAULT_PORT, "Editor");
        await scenario.Run();
    }

    static async Task Example()
    {
        //single scenario for one device
        var scenario = ScenarioFactory.CreateScenario<ExampleScenario>(LOCALHOST,DEFAULT_PORT,"Editor");
        await scenario.Run();
            
        //example for many scenarios in one, maybe it will be help for long sequences
        //like attack, stole etc
        var multipleScenario = ScenarioFactory.CreateMultipleScenario(LOCALHOST,DEFAULT_PORT,"Editor",
            new[]
            {
                typeof(ExampleScenario),
                typeof(ExampleScenario)
            });
            
        await multipleScenario.Run();
            
        //Create repeatable scenario like build town 10 times
        var repeatScenario = ScenarioFactory.CreateRepeatScenario<ExampleScenario>(LOCALHOST,DEFAULT_PORT,"Editor",3);
        await repeatScenario.Run();
            
            
        //for connect with adb - add new dependency, get deviceData from adb
        var androidScenario = ScenarioFactory
            .CreateScenario<ExampleScenario>(LOCALHOST,DEFAULT_PORT,"Editor")
            .AddAndroidDeviceDependency(null);
        await scenario.Run();
    }
        
/*
        static async Task Main()
        {
            //string testName = "BuildTownTest";
            //Console.WriteLine($"Start {testName}");
            
            //StartAdbServer();
            
            //DateTime startTime = DateTime.Now;;
            //string artifactsFolder = CreateTestArtifactsFolder(testName,startTime);
            //var client = new AdbClient();
            //var tasks = new List<Task>();
            
            //for editor
            //tasks.Add(BuildTownTest("Editor", 1,LOCALHOST, DEFAULT_PORT));
            
            //foreach (var device in client.GetDevices())
            //{
            //    Console.WriteLine($"Device connected {device.Model}");
            //    tasks.Add(BuildTownScenario(
            //        testName,
            //        GetDeviceTestArtifactFolder(device,artifactsFolder),
            //        startTime,
            //        device));
            //        
            //}
            //await Task.WhenAll(tasks);
        }
*/
    /*
        private static async Task BuildTownScenario(string testName, string artifactFolder, DateTime startTime, DeviceData device)
        {
            CancellationTokenSource cancellationToken = new CancellationTokenSource();
            UniumServer server = await UniumServer.CreateAndConnect(device.GetIp(),DEFAULT_PORT,cancellationToken);
            BuildTownScenario buildTownScenario = new BuildTownScenario(server, cancellationToken,10f);
            buildTownScenario.SetArtifactFolder(artifactFolder);

            for (int i = 0; i < 1; i++)
            {
                buildTownScenario.Name = testName;
                await buildTownScenario.RunScenario();
            }

            device.SaveLogs(startTime, artifactFolder);
        }
        */

    static StartServerResult StartAdbServer()
    {
        AdbServer server = new AdbServer();
        return server.StartServer(PATH_TO_ADB_EXE, restartServerIfNewer: false);
    }

    static void EchoTest(AdbClient client, DeviceData device)
    {
        const string ADB_IP_ADDR_SHOW = "ip addr show  | grep 'inet ' | cut -d ' ' -f 6 | cut -d / -f 1";
        var receiver = new ConsoleOutputReceiver();
        client.ExecuteRemoteCommand(ADB_IP_ADDR_SHOW, device, receiver);
        Console.WriteLine("The device responded:");
        Console.WriteLine(receiver.ToString());
    }
}