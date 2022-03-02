using SharpAdbClient;

namespace TestFramework.Scenario.Steps.Base.Dependencies
{
    public interface IAndroidDevice
    {
        void SetAndroidDevice(DeviceData deviceData);
    }
}