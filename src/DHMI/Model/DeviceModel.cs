using System.Collections.Generic;

namespace Model
{
    public class DeviceModel
    {
        private readonly Dictionary<string, DeviceInfo> Devices;

        public DeviceModel() 
        {
            Devices = new Dictionary<string, DeviceInfo>();
        }

        public bool ContainDevice(string name)
        {
            return Devices.ContainsKey(name);
        }

        public Dictionary<string, DeviceInfo> GetDevices()
        {
            return Devices;
        }

        public void AddDevice(string name, DeviceInfo devInfo)
        {
            if (!Devices.ContainsKey(name))
            { 
                Devices.Add(name, devInfo);
            }
        }

        public void UpdateDevice()
        { 
        
        }

        public void DeleteDevice(string name)
        {
            if (Devices.ContainsKey(name))
            {
                Devices.Remove(name);
            }
        }
    }
}
