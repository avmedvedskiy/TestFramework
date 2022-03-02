using System;

namespace TestFramework.Unium.QueryData
{
    [Serializable]
    public class GameObjectData
    {
        public string name;
        public string tag;
        public string activeInHierarchy;
        public string[] components;

        public bool ActiveInHierarchy => bool.Parse(activeInHierarchy);
    }
}