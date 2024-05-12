using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Model;
using System;
using System.Data;
using System.Windows.Forms;
using Util;

namespace HMIEditEnvironment.TagManager
{
    public partial class TagManagerForm : XtraForm
    {
        private DataTable dataTable;

        public TagManagerForm()
        {
            InitializeComponent();
        }

        private void TagManagerForm_Load(object sender, EventArgs e)
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("变量名称");
            dataTable.Columns.Add("所属设备");
            dataTable.Columns.Add("变量类型");
            dataTable.Columns.Add("变量值");
            GridControl.DataSource = dataTable;

            SetStyles(gridView);
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            var tag = new OPCDATagInfo();
            var form = new TagPropertyForm(tag);
            if (DialogResult.OK != form.ShowDialog())
                return;

            var tagInfo = form.SelectedObject as OPCDATagInfo;
            if (null != tagInfo)
            {
                dataTable.Rows.Add(tagInfo.Name, tagInfo.DeviceName, tagInfo.TagType, tagInfo.Value);
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

        private void Button_Export_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                FileName = "变量",
                Filter = "CSV文件(*.csv)|*.csv"
            };
            if (DialogResult.OK != dialog.ShowDialog())
                return;

            var dataTable = GridControl.DataSource as DataTable;
            if (null != dataTable)
            {
                CSVHelper.SaveToCSV(dataTable,dialog.FileName,true);
            }
        }

        private void Button_Import_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "CSV文件(*.csv)|*.csv"
            };
            if (DialogResult.OK != dialog.ShowDialog())
                return;

            GridControl.DataSource = CSVHelper.ReadFromCSV(dialog.FileName,true);
        }

        private void Button_Monitor_Click(object sender, EventArgs e)
        {
        }

        private void GridView_DoubleClick(object sender, EventArgs e)
        {
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridView view = sender as GridView;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                var currenRow = gridView.GetDataRow(gridView.FocusedRowHandle);
                var tag = new OPCDATagInfo
                {
                    Name = currenRow.ItemArray[0].ToString(),
                    DeviceName = currenRow.ItemArray[1].ToString(),
                    TagType = currenRow.ItemArray[2].ToString(),
                    Value = currenRow.ItemArray[3].ToString()
                };
                var form = new TagPropertyForm(tag);
                if (DialogResult.OK != form.ShowDialog())
                    return;

                var tagInfo = form.SelectedObject as OPCDATagInfo;
                if (null != tagInfo)
                {
                    gridView.SetRowCellValue(gridView.FocusedRowHandle, "变量名称", tagInfo.Name);
                }
            }
        }

    }
}