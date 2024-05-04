using DevExpress.Pdf.Native;
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

        public DevicePropertyForm()
        {
            InitializeComponent();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            SelectedObject = propertyGridControl.SelectedObject;

            DialogResult = DialogResult.OK;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void DeviceProperty_Load(object sender, EventArgs e)
        {
            propertyGridControl.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;

            var deviceInfo = new OPCDADeviceInfo();
            propertyGridControl.SelectedObject = deviceInfo;
        }

        private void PropertyGridControl_CustomPropertyDescriptors(object sender,CustomPropertyDescriptorsEventArgs e)
        {
            if (propertyGridControl.SelectedObject is OPCDADeviceInfo)
            {
                e.Properties = e.Properties.Sort(new string[] { "Name", "IP", "Port" });
            }
        }
    }
}