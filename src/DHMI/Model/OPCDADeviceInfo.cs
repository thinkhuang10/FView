using System.ComponentModel;

namespace Model
{
    public class OPCDADeviceInfo
    {
        [DisplayName("设备名称"), Category("关联信息"), Description("")]
        public string Name { set; get; } = "新建设备";

        [DisplayName("IP地址"), Category("关联信息"), Description("")]
        public string IP { set; get; } = "127.0.0.1";

        [DisplayName("端口号"), Category("关联信息"), Description("")]
        public int Port { set; get; } = 12000;
    }
}
