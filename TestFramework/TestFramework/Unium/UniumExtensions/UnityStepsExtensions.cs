using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestFramework.Unium.Connection;
using TestFramework.Unium.QueryData;

namespace TestFramework.Unium.UniumExtensions
{
    public static class UnityStepsExtensions
    {
        public const string UNIUM_SCENE_SYSTEM_PATH = "UniumSceneSystem(Clone)";
        public const string UNIUM_SCENE_SYSTEM_NAME = "UniumSceneSystem";
        
        private const string SET_TIME_SCALE = "SetTimeScale";


        public static async Task<AboutData> GetAboutDataAsync(this UniumServer server)
        {
            return await server.Get<AboutData>("/about");
        }

        public static async Task SetTimeScale(this UniumServer server, float timeScale)
        {
            server.TimeScale = timeScale;
            await server.CallUniumSystemMethod(SET_TIME_SCALE, timeScale.ToString());
        }

        public static async Task CallUniumSystemMethod(this UniumServer server, string method, string value)
        {
            await server.InvokeMethod(UnityStepsExtensions.UNIUM_SCENE_SYSTEM_PATH, UNIUM_SCENE_SYSTEM_NAME, method, value);
        }

        public static async Task WaitWhileGameObjectWillBeActive(this UniumServer server, string path,
            bool activeInHierarchy, int delayInMS = 1000)
        {
            while (true)
            {
                if (await GameObjectIsActive(server, path, activeInHierarchy))
                    return;

                await Task.Delay(delayInMS);
            }
        }

        public static async Task WaitAsync(this UniumServer server, int ms)
        {
            await Task.Delay((int)(ms / server.TimeScale));
        }

        public static async Task<bool> GameObjectIsActive(this UniumServer server, string path, bool activeInHierarchy)
        {
            GameObjectData data = await GetGameObjectAsync(server, path, string.Empty);
            return data != null && data.ActiveInHierarchy == activeInHierarchy;
        }

        public static async Task<GameObjectData> GetGameObjectAsync(this UniumServer server, string path,
            string gameObjectName)
        {
            var datas = await GetGameObjectsAsync(server, path, gameObjectName);
            if (datas.Length >= 1)
                return datas[0];
            else
                return null;
        }

        public static async Task<GameObjectData[]> GetGameObjectsAsync(this UniumServer server, string path,
            string gameObjectName)
        {
            string responce = await server.GetByScenePath(Path.Combine(path, gameObjectName));
            JObject obj = JObject.Parse(responce);
            return JsonConvert.DeserializeObject<GameObjectData[]>(obj["data"].ToString());
        }

        public static async Task InvokeUIButton(this UniumServer server, string path)
        {
            await server.GetByScenePath(path + ".Button.onClick.Invoke()");
        }

        public static async Task InvokeMethod(this UniumServer server, string path, string typeName, string method,
            string param = "")
        {
            await server.GetByScenePath(path + $".{typeName}.{method}({param})");
        }

        public static async Task DownloadScreenshot(this UniumServer server, string path)
        {
            const string screenshotPath = "utils/appscreenshot";
            using (var client = new WebClient())
            {
                Console.Write(path);
                await client.DownloadFileTaskAsync(server.HttpIP + screenshotPath, path);
            }
        }
    }
}