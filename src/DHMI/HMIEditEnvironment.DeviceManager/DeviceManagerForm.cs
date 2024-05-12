using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using LogHelper;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Util;

namespace HMIEditEnvironment.DeviceManager
{
    public partial class DeviceManagerForm : XtraForm
    {
        private DataTable deviceTable;

        private DeviceModel deviceModel;

        public DeviceManagerForm()
        {
            InitializeComponent();
        }

        private void DeviceManagerForm_Load(object sender, EventArgs e)
        {
            deviceTable = new DataTable();
            deviceTable.Columns.Add("名称");
            deviceTable.Columns.Add("类型");
            deviceTable.Columns.Add("IP地址");
            deviceTable.Columns.Add("端口号");
            GridControl_Device.DataSource = deviceTable;

            SetStyles(gridView);

            try
            {
                deviceModel = new DeviceModel();
                var devices = JSONHelper.ReadFile<Dictionary<string, object>>(ConstantHelper.DeviceFileName);
                foreach (var key in devices.Keys)
                {
                    if (deviceModel.ContainDevice(key))
                        continue;

                    AddDeviceByFile(devices[key]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取设备表出现异常:" + ex.Message);
                LogUtil.Error("读取设备表出现异常:" + ex);
            }
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            var form = new DevicePropertyForm(deviceModel);
            if (DialogResult.OK != form.ShowDialog())
                return;

            if (form.SelectedObject is OPCDADeviceInfo)
            {
                if (form.SelectedObject is OPCDADeviceInfo deviceInfo)
                {
                    deviceModel.AddDevice(deviceInfo.DevName, deviceInfo);
                    deviceTable.Rows.Add(deviceInfo.DevName, deviceInfo.DevType, deviceInfo.IP, deviceInfo.Port);
                }
            }
            else if (form.SelectedObject is ModbusTCPDeviceInfo)
                {
                if (form.SelectedObject is ModbusTCPDeviceInfo deviceInfo)
                {
                    deviceModel.AddDevice(deviceInfo.DevName, deviceInfo);
                    deviceTable.Rows.Add(deviceInfo.DevName, deviceInfo.DevType, deviceInfo.IP, deviceInfo.Port);
                }
            }

            JSONHelper.WriteFile(deviceModel.GetDevices(), ConstantHelper.DeviceFileName);
        }

        private void Button_Delete_Click(object sender, EventArgs e)
        {
            var view = GridControl_Device.FocusedView as ColumnView;
            if (view == null) 
                return;

            if (view.OptionsBehavior.AllowDeleteRows != DefaultBoolean.False && view.SelectedRowsCount > 0)
            {
                var dialogResult = XtraMessageBox.Show($"确定删除所选{view.SelectedRowsCount}行？", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == dialogResult)
                {
                    foreach (var rowIndex in view.GetSelectedRows())
                    {
                        var name = view.GetRowCellValue(rowIndex, "名称").ToString();
                        deviceModel.DeleteDevice(name);
                    }

                    view.DeleteSelectedRows();
                }
            }

            JSONHelper.WriteFile(deviceModel.GetDevices(), ConstantHelper.DeviceFileName);
        }

        private void AddDeviceByFile(object obj)
        {
            var info = JsonConvert.DeserializeObject<DeviceInfo>(obj.ToString());
            if(null == info)
                return;

            if (info.DevType == DeviceTypeEnum.OPCDA)
            {
                var deviceInfo = JsonConvert.DeserializeObject<OPCDADeviceInfo>(obj.ToString());
                if (null != deviceInfo)
                {
                    deviceModel.AddDevice(deviceInfo.DevName, deviceInfo);
                    deviceTable.Rows.Add(deviceInfo.DevName, deviceInfo.DevType, deviceInfo.IP, deviceInfo.Port);
                }
            }
            else if (info.DevType == DeviceTypeEnum.ModbusTCP)
            {
                var deviceInfo = JsonConvert.DeserializeObject<ModbusTCPDeviceInfo>(obj.ToString());
                if (null != deviceInfo)
                {
                    deviceModel.AddDevice(deviceInfo.DevName, deviceInfo);
                    deviceTable.Rows.Add(deviceInfo.DevName, deviceInfo.DevType, deviceInfo.IP, deviceInfo.Port);
                }
            }
        }

        private static void SetStyles(GridView gridView)
        {
            gridView.OptionsBehavior.ReadOnly = true;
            gridView.OptionsBehavior.AllowAddRows = DefaultBoolean.False;

            gridView.OptionsView.ShowGroupPanel = false;  //隐藏最上面的GroupPanel
            gridView.OptionsView.ShowIndicator = false;

            gridView.OptionsView.ShowAutoFilterRow = true; //隐藏指示列

            gridView.OptionsSelection.MultiSelect = true;
            gridView.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

            gridView.FocusRectStyle = DrawFocusRectStyle.None;  //设置焦点框为整行
            gridView.OptionsSelection.EnableAppearanceFocusedCell = false; //禁用单元格焦点
            gridView.OptionsSelection.EnableAppearanceFocusedRow = true; //启用整行焦点
            gridView.OptionsSelection.EnableAppearanceFocusedRow = true; //启用整行焦点
            gridView.OptionsSelection.EnableAppearanceHideSelection = false;

            gridView.OptionsView.EnableAppearanceEvenRow = true; //启用偶数行背景色
            gridView.OptionsView.EnableAppearanceOddRow = true; //启用奇数行背景色

            gridView.OptionsCustomization.AllowFilter = false;  
            gridView.OptionsCustomization.AllowSort = true;
            gridView.OptionsMenu.EnableColumnMenu = false; //屏蔽右键菜单

            gridView.OptionsBehavior.Editable = false;

            gridView.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(150, 237, 243, 254); //设置偶数行背景色
            gridView.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(150, 199, 237, 204); //设置奇数行背景色
        }

    }
}