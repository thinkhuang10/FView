using System.ComponentModel;

namespace Model
{
    public class OPCDATagInfo
    {
        [DisplayName("变量名称"), Category("关联信息"), Description("")]
        public string Name { set; get; } = "新建变量";

        [DisplayName("所属设备"), Category("关联信息"), Description("")]
        public string DeviceName { set; get; } = "新建设备";

        [DisplayName("变量类型"), Category("关联信息"), Description("")]
        public string TagType { set; get; } = "float";

        [DisplayName("变量值"), Category("关联信息"), Description("")]
        public object Value { set; get; }
    }
}
