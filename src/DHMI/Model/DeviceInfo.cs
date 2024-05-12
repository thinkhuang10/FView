using System.ComponentModel;

namespace Model
{
    public class DeviceInfo
    {
        [DisplayName("设备类型"), ReadOnly(true), Category("设备基本信息"), Description("")]
        public DeviceTypeEnum DevType { set; get; } = DeviceTypeEnum.ModbusTCP;

        [DisplayName("设备名称"), Category("设备基本信息"), Description("")]
        public string DevName { set; get; } = "新建设备";

        [DisplayName("设备描述"), Category("设备基本信息"), Description("")]
        public string Description { set; get; } = "";
    }
}
