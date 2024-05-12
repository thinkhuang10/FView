using System.ComponentModel;

namespace Model
{
    public class ModbusTCPDeviceInfo : DeviceInfo
    {
        [DisplayName("IP地址"), Category("通信接口信息"), Description("")]
        public string IP { set; get; } = "127.0.0.1";

        [DisplayName("端口号"), Category("通信接口信息"), Description("")]
        public int Port { set; get; } = 11000;
    }
}
