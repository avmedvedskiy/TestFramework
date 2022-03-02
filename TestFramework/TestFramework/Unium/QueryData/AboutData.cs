using System;

namespace TestFramework.Unium.QueryData
{
    [Serializable]
    public class AboutData
    {
        public string Unium { get; set; }
        public string Unity { get; set; }
        public string Mono { get; set; }
        public bool IsEditor { get; set; }
        public string Product { get; set; }
        public string Company { get; set; }
        public string Version { get; set; }
        public string IPAddress { get; set; }
        public float FPS { get; set; }
        public float RunningTime { get; set; }
        public string Scene { get; set; }
    }
}