using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Events;
using Model;
using System;
using System.Linq;
using System.Windows.Forms;
using Util;

namespace HMIEditEnvironment.DeviceManager
{
    public partial class DevicePropertyForm : DevExpress.XtraEditors.XtraForm
    {
        public object SelectedObject;

        private readonly DeviceModel deviceModel;

        private string devName = "新建设备";

        private int devID;

        public DevicePropertyForm(DeviceModel deviceModel)
        {
            InitializeComponent();

            this.deviceModel = deviceModel;
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            var deviceInfo = PropertyGridControl.SelectedObject as DeviceInfo;

            if (string.IsNullOrEmpty(deviceInfo.DevName))
            {
                MessageBox.Show("请输入设备名称.");
                return;
            }

            if (deviceModel.GetDevices().ContainsKey(deviceInfo.DevName))
            {
                MessageBox.Show("设备名称已经存在，请重新输入.");
                return;
            }

            if (DeviceTypeEnum.OPCDA == deviceInfo.DevType && !ValidateOPCDA())
                return;

            if (DeviceTypeEnum.OPCDA == deviceInfo.DevType && !ValidateModbusTCP())
               return;

            SelectedObject = PropertyGridControl.SelectedObject;

            DialogResult = DialogResult.OK;
        }

        private bool ValidateOPCDA()
        {
            var deviceInfo = PropertyGridControl.SelectedObject as OPCDADeviceInfo;

            if (!RegexHelper.IsIPAddress(deviceInfo.IP.ToString()))
            {
                MessageBox.Show("请输入正确IP地址.");
                return false;
            }

            if (!RegexHelper.IsIntergerNonZero(deviceInfo.Port.ToString()))
            {
                MessageBox.Show("请输入正确的端口号.");
                return false;
            }

            return true;
        }

        private bool ValidateModbusTCP()
        {

            return true;
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

            devName = GetDeviceName();
            devID = GetDeviceID();

            var deviceInfo = new OPCDADeviceInfo
            {
                DevID = devID,
                DevName = devName,
                DevType = DeviceTypeEnum.OPCDA
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
                    DevID = devID,
                    DevName = devName,
                    DevType = DeviceTypeEnum.OPCDA
                };
                PropertyGridControl.SelectedObject = deviceInfo;
            }
            else if (DeviceTypeEnum.ModbusTCP.ToString() == e.Node.Text)
            {
                var deviceInfo = new ModbusTCPDeviceInfo
                {
                    DevID = devID,
                    DevName = devName,
                    DevType = DeviceTypeEnum.ModbusTCP
                };
                PropertyGridControl.SelectedObject = deviceInfo;
            }
        }

        private int GetDeviceID()
        {
            var devices = deviceModel.GetDevices().Values;
            if (0 == devices.Count)
                return 0;

            var maxID = devices.Max(p => p.DevID);

            return ++maxID;
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