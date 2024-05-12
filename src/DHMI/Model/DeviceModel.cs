using System.Collections.Generic;

namespace Model
{
    public class DeviceModel
    {
        private readonly Dictionary<string, object> Devices;

        public DeviceModel() 
        {
            Devices = new Dictionary<string, object>();
        }

        public bool ContainDevice(string name)
        {
            return Devices.ContainsKey(name);
        }

        public Dictionary<string, object> GetDevices()
        {
            return Devices;
        }

        public void AddDevice(string name, object devInfo)
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
