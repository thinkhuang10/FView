using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Events;
using Model;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment.DeviceManager
{
    public partial class DevicePropertyForm : DevExpress.XtraEditors.XtraForm
    {
        public object SelectedObject;

        private readonly DeviceModel deviceModel;

        private string deviceName;

        public DevicePropertyForm(DeviceModel deviceModel)
        {
            InitializeComponent();

            this.deviceModel = deviceModel;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            var deviceInfo = PropertyGridControl.SelectedObject as DeviceInfo;

            if (deviceModel.GetDevices().ContainsKey(deviceInfo.DevName))
            {
                MessageBox.Show("设备名称已经存在，请重新输入.");
                return;
            }

            SelectedObject = PropertyGridControl.SelectedObject;

            DialogResult = DialogResult.OK;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void DeviceProperty_Load(object sender, EventArgs e)
        {
            var rootNode = TreeView_Device.Nodes.Add("设备");
            var selectedNode = rootNode.Nodes.Add(DeviceTypeEnum.OPCDA.ToString(), DeviceTypeEnum.OPCDA.ToString());
            TreeView_Device.SelectedNode = selectedNode;
            rootNode.Nodes.Add(DeviceTypeEnum.ModbusTCP.ToString(), DeviceTypeEnum.ModbusTCP.ToString());
            TreeView_Device.ExpandAll();

            PropertyGridControl.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;

            deviceName = GetDeviceName();

            var deviceInfo = new OPCDADeviceInfo
            {
                DevType = DeviceTypeEnum.OPCDA,
                DevName = deviceName
            };
            PropertyGridControl.SelectedObject = deviceInfo;
        }

        private void PropertyGridControl_CustomPropertyDescriptors(object sender,CustomPropertyDescriptorsEventArgs e)
        {
            if (PropertyGridControl.SelectedObject is OPCDADeviceInfo)
            {
                e.Properties = e.Properties.Sort(new string[] { "DevType", "DevName", "Description", "IP", "Port" });
            }
            else if (PropertyGridControl.SelectedObject is ModbusTCPDeviceInfo)
            {
                e.Properties = e.Properties.Sort(new string[] { "DevType", "DevName", "Description", "IP", "Port" });
            }
        }

        private void TreeView_Device_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (DeviceTypeEnum.OPCDA.ToString() == e.Node.Text)
            {
                var deviceInfo = new OPCDADeviceInfo
                {
                    DevType = DeviceTypeEnum.OPCDA,
                    DevName = deviceName
                };
                PropertyGridControl.SelectedObject = deviceInfo;
            }
            else if (DeviceTypeEnum.ModbusTCP.ToString() == e.Node.Text)
            {
                var deviceInfo = new ModbusTCPDeviceInfo
                {
                    DevType = DeviceTypeEnum.ModbusTCP,
                    DevName = deviceName
                };
                PropertyGridControl.SelectedObject = deviceInfo;
            }
        }

        private string GetDeviceName()
        {
            var name = "新建设备";
            uint num = 0;
            var devices = deviceModel.GetDevices();
            foreach (var deviceName in devices.Keys)
            {
                if (!deviceName.StartsWith(name))
                    continue;

                var b = uint.TryParse(deviceName.Substring(name.Length),out uint count);
                if (!b && deviceName.Substring(name.Length) != "")
                    continue;

                if (count >= num)
                    num = count + 1;
            }

            if (0 != num)
                name += num;

            return name;
        }
    }
}