using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Model;
using System;
using System.Data;
using System.Windows.Forms;

namespace HMIEditEnvironment.DeviceManager
{
    public partial class DeviceManagerForm : XtraForm
    {
        private DataTable dataTable;

        public DeviceManagerForm()
        {
            InitializeComponent();
        }

        private void DeviceManagerForm_Load(object sender, EventArgs e)
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("名称");
            dataTable.Columns.Add("IP地址");
            dataTable.Columns.Add("端口号");
            GridControl.DataSource = dataTable;

            SetStyles(gridView);
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            var form = new DevicePropertyForm();
            if (DialogResult.OK != form.ShowDialog())
                return;

            var deviceInfo = form.SelectedObject as OPCDADeviceInfo;
            if (null != deviceInfo)
            {
                dataTable.Rows.Add(deviceInfo.Name, deviceInfo.IP, deviceInfo.Port);
            }
        }

        public static void SetStyles(GridView gridView)
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