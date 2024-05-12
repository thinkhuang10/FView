using CommonSnappableTypes;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraNavBar;
using HMIEditEnvironment.DeviceManager;
using HMIEditEnvironment.TagManager;
using LogHelper;
using Model;
using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Util;

namespace HMIEditEnvironment;

public partial class MDIParent1 : XtraForm
{
    private readonly DataSet PixieLibraryControlDataSet = new();

    private readonly ImageList PixieLibraryControlImageList = new();

    private readonly DataSet DCCEControlLibraryControlDataSet = new();

    private readonly ImageList DCCEControlLibraryControlImageList = new();

    private readonly ImageList ActiveXControlImageList = new();

    private int childFormNumber;

    private int pageGroupNumber;

    private readonly CGlobal nullcglobal = new();

    public UserCommandControl2 userCommandControl21 = new();

    private int m_MouseClicks;

    private readonly DataSet CommonMetaDataInfo = new();

    private const string DockConfigFileName = "DockConfig.xml";

    private ProjectPathSaveHandler projectPathSave;

    public MDIParent1()
    {
        InitializeComponent();
    }

    private void Panel_横向标尺_Paint(object sender, PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        new Font("黑体", 7f, FontStyle.Regular);
        for (int i = 0; i < 200; i++)
        {
            graphics.DrawLine(Pens.Gray, 15 * i, 15, 15 * i, 35);
        }
        for (int j = 0; j < 100; j++)
        {
            graphics.DrawLine(Pens.Gray, 30 * j, 0, 30 * j, 30);
        }
        for (int k = 0; k < 1000; k++)
        {
            graphics.DrawLine(Pens.Gray, 3 * k, 30, 3 * k, 17);
        }
        graphics.DrawRectangle(Pens.Black, e.ClipRectangle);
    }

    private void Panel_纵向标尺_Paint(object sender, PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        new StringFormat(StringFormatFlags.DirectionVertical);
        new Font("黑体", 7f, FontStyle.Regular);
        for (int i = 0; i < 200; i++)
        {
            graphics.DrawLine(Pens.Gray, 15, 15 * (i + 2), 30, 15 * (i + 2));
        }
        for (int j = 0; j < 100; j++)
        {
            graphics.DrawLine(Pens.Gray, 0, 30 * (j + 1), 17, 30 * (j + 1));
        }
        for (int k = 0; k < 1000; k++)
        {
            graphics.DrawLine(Pens.Gray, 30, 3 * (k + 10), 17, 3 * (k + 10));
        }
        graphics.DrawRectangle(Pens.Black, e.ClipRectangle);
    }

    private void PropertyGrid_Attrubute_Enter(object sender, EventArgs e)
    {
        if (PropertyGrid_Attrubute.SelectedObject is not CShape)
            return;

        foreach (CShape selectedShape in userCommandControl21.theglobal.SelectedShapeList)
        {
            userCommandControl21.theglobal.OldShapes.Add(selectedShape.Copy());
        }
    }

    private void MyPropertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
    {
        if (e.ChangedItem != null && e.OldValue != e.ChangedItem.Value)
        {
            PropertySelectJudge(e);
            PropertyNameJudge(e);
            userCommandControl21.theglobal.uc2.RefreshGraphics();
            PropertyPageNameJudge(e);
            PropertyScriptNameJudge(e);
            PropertyShapeJudge();
            userCommandControl21.theglobal.OldShapes.Clear();
        }
    }

    private void MenubarCheckItem_工具栏_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        if (menubarCheckItem_工具栏.Checked)
        {
            LoadBarLayout();
            return;
        }

        SaveBarLayout();
        foreach (var bar in barManager1.Bars.Cast<Bar>())
        {
            if (!bar.BarName.Equals("菜单栏") && !bar.BarName.Equals("状态栏"))
            {
                bar.Visible = false;
            }
        }
    }

    private void MenubarCheckItem_状态栏_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        statusbar_状态栏.Visible = menubarCheckItem_状态栏.Checked;
    }

    private void MenubarCheckItem_导航栏_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        dockPanel_导航栏.Visibility = ((!menubarCheckItem_导航栏.Checked) ? DockVisibility.Hidden : DockVisibility.Visible);
    }

    private void MenubarCheckItem_输出栏_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        dockPanel_输出栏.Visibility = ((!menubarCheckItem_输出栏.Checked) ? DockVisibility.Hidden : DockVisibility.Visible);
    }

    private void MenubarCheckItem_对象浏览器_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        dockPanel_对象浏览器.Visibility = ((!menubarCheckItem_对象浏览器.Checked) ? DockVisibility.Hidden : DockVisibility.Visible);
    }

    private void MenubarCheckItem_变量浏览器_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        dockPanel_变量浏览器.Visibility = ((!menubarCheckItem_变量浏览器.Checked) ? DockVisibility.Hidden : DockVisibility.Visible);
    }

    private void MenubarCheckItem_属性_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        dockPanel_属性.Visibility = ((!menubarCheckItem_属性.Checked) ? DockVisibility.Hidden : DockVisibility.Visible);
    }

    private void MenubarCheckItem_动画_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        dockPanel_动画.Visibility = ((!menubarCheckItem_动画.Checked) ? DockVisibility.Hidden : DockVisibility.Visible);
    }

    private void BarButtonItem137_ItemClick(object sender, ItemClickEventArgs e)
    {
        CEditEnvironmentGlobal.scriptUnitForm.Init();
        CEditEnvironmentGlobal.scriptUnitForm.Show();
        CEditEnvironmentGlobal.scriptUnitForm.BringToFront();
    }

    private void BarButtonItem_保存页面_ItemClick(object sender, ItemClickEventArgs e)
    {
        SaveOnePage(base.ActiveMdiChild as ChildForm);
    }

    private void BarButtonItem62_ItemClick(object sender, ItemClickEventArgs e)
    {
        ControlInfoSetting controlInfoSetting = new();
        controlInfoSetting.ShowDialog();
    }

    private void BarButtonItem115_ItemClick(object sender, ItemClickEventArgs e)
    {
        RotateShape(90f);
    }

    private void BarButtonItem116_ItemClick(object sender, ItemClickEventArgs e)
    {
        RotateShape(-90f);
    }

    private void BarCheckItem17_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        barCheckItem10.Checked = barCheckItem_LockElements.Checked;
    }

    private void BarButtonItem100_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem25_ItemClick(sender, e);
    }

    private void BarButtonItem101_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem26_ItemClick(sender, e);
    }

    private void BarButtonItem102_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem23_ItemClick(sender, e);
    }

    private void BarButtonItem103_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem24_ItemClick(sender, e);
    }

    private void BarButtonItem104_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem29_ItemClick(sender, e);
    }

    private void BarButtonItem105_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem30_ItemClick(sender, e);
    }

    private void BarButtonItem106_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem48_ItemClick(sender, e);
    }

    private void BarButtonItem107_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem49_ItemClick(sender, e);
    }

    private void BarButtonItem108_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem50_ItemClick(sender, e);
    }

    private void BarButtonItem109_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem51_ItemClick(sender, e);
    }

    private void BarButtonItem110_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem52_ItemClick(sender, e);
    }

    private void BarButtonItem111_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem53_ItemClick(sender, e);
    }

    private void BarButtonItem117_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem27_ItemClick(sender, e);
    }

    private void BarButtonItem118_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem28_ItemClick(sender, e);
    }

    private void BarButtonItem112_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem54_ItemClick(sender, e);
    }

    private void BarButtonItem113_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem55_ItemClick(sender, e);
    }

    private void BarButtonItem114_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem56_ItemClick(sender, e);
    }

    private void BarButtonItem72_ItemClick(object sender, ItemClickEventArgs e)
    {
        AddWindowsControl("ShapeRuntime.CButton");
    }

    private void BarButtonItem73_ItemClick(object sender, ItemClickEventArgs e)
    {
        AddWindowsControl("ShapeRuntime.CCheckBox");
    }

    private void BarButtonItem74_ItemClick(object sender, ItemClickEventArgs e)
    {
        AddWindowsControl("ShapeRuntime.CLabel");
    }

    private void BarButtonItem75_ItemClick(object sender, ItemClickEventArgs e)
    {
        AddWindowsControl("ShapeRuntime.CTextBox");
    }

    private void BarButtonItem76_ItemClick(object sender, ItemClickEventArgs e)
    {
        AddWindowsControl("ShapeRuntime.CComboBox");
    }

    private void BarButtonItem77_ItemClick(object sender, ItemClickEventArgs e)
    {
        AddWindowsControl("ShapeRuntime.CDateTimePicker");
    }

    private void BarButtonItem78_ItemClick(object sender, ItemClickEventArgs e)
    {
        AddWindowsControl("ShapeRuntime.CPictureBox");
    }

    private void BarButtonItem79_ItemClick(object sender, ItemClickEventArgs e)
    {
        AddWindowsControl("ShapeRuntime.CGroupBox");
    }

    private void BarButtonItem80_ItemClick(object sender, ItemClickEventArgs e)
    {
        AddWindowsControl("ShapeRuntime.CDataGridView");
    }

    private void BarButtonItem_about_ItemClick(object sender, ItemClickEventArgs e)
    {
        var aboutBox = new AboutBox();
        aboutBox.ShowDialog();
    }

    private void BarButtonItem85_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem14_ItemClick(sender, e);
    }

    private void BarButtonItem86_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem13_ItemClick(sender, e);
    }

    private void BarButtonItem87_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem15_ItemClick(sender, e);
    }

    private void BarButtonItem88_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem17_ItemClick(sender, e);
    }

    private void BarButtonItem89_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem10_ItemClick(sender, e);
    }

    private void BarButtonItem90_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem11_ItemClick(sender, e);
    }

    private void BarButtonItem_Debug_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarButtonItem_Debug.ItemClick -= BarButtonItem_Debug_ItemClick;

        if (CEditEnvironmentGlobal.HMIPath != null && !(CEditEnvironmentGlobal.HMIPath == ""))
        {
            string key = CEditEnvironmentGlobal.dhp.key;
            CEditEnvironmentGlobal.dhp.key = "";
            CEditEnvironmentGlobal.dhp.PageGroup = treeView_工程导航.Nodes[0].Nodes[0].Tag as HmiPageGroup;
            var projectCompiler = new ProjectCompiler();
            projectCompiler.Compile();
            projectCompiler.ExecuteHMIRun();
            CEditEnvironmentGlobal.dhp.key = key;
        }

        BarButtonItem_Debug.ItemClick += BarButtonItem_Debug_ItemClick;
    }

    private void ToolbarButtonItem_Windows控件_ItemClick(object sender, ItemClickEventArgs e)
    {
        dockPanel_基本控件.Show();
    }

    private void ToolbarButtonItem_DCCE工控组件_ItemClick(object sender, ItemClickEventArgs e)
    {
        dockPanel_DCCE工控组件.Show();
    }

    private void ToolbarButtonItem_ActiveX控件_ItemClick(object sender, ItemClickEventArgs e)
    {
        dockPanel_ActiveX控件.Show();
    }

    private void ToolbarButtonItem_精灵控件_ItemClick(object sender, ItemClickEventArgs e)
    {
        dockPanel_精灵控件.Show();
    }

    private void BarEditItem4_EditValueChanged(object sender, EventArgs e)
    {
        if (CEditEnvironmentGlobal.NotEditValue)
        {
            return;
        }
        var list = new List<CShape>();
        var list2 = new List<CShape>();
        foreach (CShape selectedShape in userCommandControl21.theglobal.SelectedShapeList)
        {
            list2.Add(selectedShape.Copy());
            selectedShape.Color1 = (Color)barEditItem4.EditValue;
            list.Add(selectedShape);
        }
        userCommandControl21.theglobal.ForUndo(list, list2);
        userCommandControl21.theglobal.uc2.RefreshGraphics();
    }

    private void BarEditItem3_EditValueChanged(object sender, EventArgs e)
    {
        if (CEditEnvironmentGlobal.NotEditValue)
        {
            return;
        }
        List<CShape> list = new();
        List<CShape> list2 = new();
        foreach (CShape selectedShape in userCommandControl21.theglobal.SelectedShapeList)
        {
            list2.Add(selectedShape.Copy());
            selectedShape.PenColor = (Color)barEditItem3.EditValue;
            list.Add(selectedShape);
        }
        userCommandControl21.theglobal.ForUndo(list, list2);
        userCommandControl21.theglobal.uc2.RefreshGraphics();
    }

    private void BarEditItem5_EditValueChanged(object sender, EventArgs e)
    {
        if (CEditEnvironmentGlobal.NotEditValue)
            return;

        List<CShape> list = new();
        List<CShape> list2 = new();
        userCommandControl21.theglobal.OldShapes.Clear();
        foreach (CShape selectedShape in userCommandControl21.theglobal.SelectedShapeList)
        {
            list2.Add(selectedShape.Copy());
            selectedShape.PenWidth = Convert.ToSingle(barEditItem5.EditValue);
            list.Add(selectedShape.Copy());
        }
        userCommandControl21.theglobal.ForUndo(list, list2);
        userCommandControl21.theglobal.uc2.RefreshGraphics();
    }

    private void DockPanel_导航栏_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
        menubarCheckItem_导航栏.Checked = e.Visibility != DockVisibility.Hidden;
    }

    private void DockPanel_输出栏_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
        menubarCheckItem_输出栏.Checked = e.Visibility != DockVisibility.Hidden;
    }

    private void DockPanel_对象浏览器_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
        menubarCheckItem_对象浏览器.Checked = e.Visibility != DockVisibility.Hidden;
    }

    private void DockPanel_变量浏览器_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
        menubarCheckItem_变量浏览器.Checked = e.Visibility != DockVisibility.Hidden;
    }

    private void DockPanel_属性_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
        menubarCheckItem_属性.Checked = e.Visibility != DockVisibility.Hidden;
    }

    private void DockPanel_事件_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
        menubarCheckItem_事件.Checked = e.Visibility != DockVisibility.Hidden;
    }

    private void DockPanel_动画_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
        menubarCheckItem_动画.Checked = e.Visibility != DockVisibility.Hidden;
    }

    private void DockPanel_基本控件_SizeChanged(object sender, EventArgs e)
    {
        int num = 0;
        foreach (var group in navBarControl_基本控件.Groups.Cast<NavBarGroup>())
        {
            if (group.Visible)
            {
                num += 23;
            }
        }
        int num2 = ((num + Convert.ToInt32((double)dockPanel_基本控件.Height * 0.5) > dockPanel_基本控件.Height) ? (num + Convert.ToInt32((double)dockPanel_基本控件.Height * 0.5)) : dockPanel_基本控件.Height);
        navBarControl_基本控件.Height = num2;
    }

    private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.ColumnIndex == 2)
        {
            Rectangle cellDisplayRectangle = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, cutOverflow: false);
            cb.Items.Clear();
            cb.Items.Add("<删除动画连接>");
            cb.Items.Add(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            cb.Width = cellDisplayRectangle.Width;
            cb.Height = cellDisplayRectangle.Height;
            cb.Top = cellDisplayRectangle.Top;
            cb.Left = cellDisplayRectangle.Left;
            cb.DroppedDown = true;
            cb.Visible = true;
        }
        else
        {
            cb.Visible = false;
        }
    }

    private void FileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
    {
        try
        {
            Thread.Sleep(200);
            foreach (DataFile df in CEditEnvironmentGlobal.dfs)
            {
                CEditEnvironmentGlobal.dhp.dirtyPageAdd(df.name);
            }
        }
        catch(Exception ex)
        {
            LogUtil.Error("文件系统监视变化处理出现异常:" + ex);
        }
    }

    private void BarButtonItem_打开页面_ItemClick(object sender, ItemClickEventArgs e)
    {
        var form = new PageOpenerForm();
        form.OpenPageEvent += delegate (object s1, PageOpenerForm.OpenPageEventArgs e1)
        {
            OpenPage(e1.PageName);
        };
        form.ShowDialog();
    }

    public void BarButtonItem_保存工程_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (CEditEnvironmentGlobal.HMIPath == "" || CEditEnvironmentGlobal.ProjectHPFFilePath == "")
            return;

        SizeHelper.SetProjectRunSize();
        var projectfile = CEditEnvironmentGlobal.ProjectHPFFilePath;
        var dictionary = new Dictionary<string, string>();
        List<string> list = new();
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            Operation.BinarySaveFile(CEditEnvironmentGlobal.HMIPath + "\\" + df.name + ".hpg", df);
            try
            {
                dictionary.Add(df.name, df.name + ".hpg");
            }
            catch (ArgumentException)
            {
                CEditEnvironmentGlobal.OutputMessage.Say("页面脚本名称为==" + df.name + "==脚本名称重复！页面名称为" + df.pageName);
            }
            if (df.visable)
            {
                list.Add(df.name);
            }
        }
        CEditEnvironmentGlobal.dhp.PageGroup = treeView_工程导航.Nodes[0].Nodes[0].Tag as HmiPageGroup;
        CEditEnvironmentGlobal.dhp.pages = dictionary;
        CEditEnvironmentGlobal.dhp.startVisiblePages = list;
        Operation.BinarySaveProject(projectfile, CEditEnvironmentGlobal.dhp);
        CEditEnvironmentGlobal.OutputMessage.Say("成功保存到" + projectfile);
    }

    private void BarButtonItem70_ItemClick(object sender, ItemClickEventArgs e)
    {
        try
        {
            CControl cControl = new()
            {
                _dllfile = AppDomain.CurrentDomain.BaseDirectory + "HistoryLines.dll",
                _files = new string[1] { AppDomain.CurrentDomain.BaseDirectory + "HistoryLines.dll" },
                type = "HistoryLines.RealtimeWrapper"
            };
            cControl.AddPoint(PointF.Empty);
            cControl.initLocationErr = true;
            CEditEnvironmentGlobal.childform.theglobal.uc2.Controls.Add(cControl._c);
            cControl.initLocationErr = false;
            cControl._c.Enabled = false;
            UserCommandControl2.GiveName(cControl);
            CEditEnvironmentGlobal.childform.theglobal.g_ListAllShowCShape.Add(cControl);
            CEditEnvironmentGlobal.childform.theglobal.SelectedShapeList.Clear();
            CEditEnvironmentGlobal.childform.theglobal.SelectedShapeList.Add(cControl);
            objView_Page.OnFresh(cControl.ShapeID.ToString());
            CShape item = CEditEnvironmentGlobal.childform.theglobal.g_ListAllShowCShape[CEditEnvironmentGlobal.childform.theglobal.g_ListAllShowCShape.Count - 1].Copy();
            List<CShape> list = new()
            {
                item
            };
            userCommandControl21.theglobal.ForUndo(list, null);
            CEditEnvironmentGlobal.childform.theglobal.str_IMDoingWhat = "Select";
            CEditEnvironmentGlobal.childform.theglobal.uc2.RefreshGraphics();
            if (cControl._c is IDCCEControl)
            {
                ((IDCCEControl)cControl._c).GetVarTableEvent += CForDCCEControl.GetVarTableEvent;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void BarButtonItem71_ItemClick(object sender, ItemClickEventArgs e)
    {
        try
        {
            CControl cControl = new()
            {
                _dllfile = AppDomain.CurrentDomain.BaseDirectory + "HistoryLines.dll",
                _files = new string[3]
            {
                AppDomain.CurrentDomain.BaseDirectory + "HistoryLines.dll",
                AppDomain.CurrentDomain.BaseDirectory + "Microsoft.Office.Interop.Excel.dll",
                AppDomain.CurrentDomain.BaseDirectory + "Office.dll"
            },
                type = "HistoryLines.HistoryWrapper"
            };
            cControl.AddPoint(PointF.Empty);
            cControl.initLocationErr = true;
            CEditEnvironmentGlobal.childform.theglobal.uc2.Controls.Add(cControl._c);
            cControl.initLocationErr = false;
            cControl._c.Enabled = false;
            UserCommandControl2.GiveName(cControl);
            CEditEnvironmentGlobal.childform.theglobal.g_ListAllShowCShape.Add(cControl);
            CEditEnvironmentGlobal.childform.theglobal.SelectedShapeList.Clear();
            CEditEnvironmentGlobal.childform.theglobal.SelectedShapeList.Add(cControl);
            objView_Page.OnFresh(cControl.ShapeID.ToString());
            CShape item = CEditEnvironmentGlobal.childform.theglobal.g_ListAllShowCShape[CEditEnvironmentGlobal.childform.theglobal.g_ListAllShowCShape.Count - 1].Copy();
            List<CShape> list = new()
            {
                item
            };
            userCommandControl21.theglobal.ForUndo(list, null);
            CEditEnvironmentGlobal.childform.theglobal.str_IMDoingWhat = "Select";
            CEditEnvironmentGlobal.childform.theglobal.uc2.RefreshGraphics();
            if (cControl._c is IDCCEControl)
            {
                ((IDCCEControl)cControl._c).GetVarTableEvent += CForDCCEControl.GetVarTableEvent;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void BarButtonItem_退出_ItemClick(object sender, ItemClickEventArgs e)
    {
        Close();
    }

    private void BarButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button_撤消_Click(sender, e);
    }

    private void BarButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button_重复_Click(sender, e);
    }

    private void BarButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
    {
        CEditEnvironmentGlobal.CLS.Clear();
        List<CShape> list = new();
        CShape[] array = userCommandControl21.theglobal.SelectedShapeList.ToArray();
        foreach (CShape cShape in array)
        {
            List<string> list2 = CheckIOExists.ShapeInUse(userCommandControl21.theglobal.df.name + "." + cShape.Name);
            if (list2.Count != 0)
            {
                delPage delPage2 = new(list2, "该图形正在被引用,是否仍继续操作.");
                if (delPage2.ShowDialog() != DialogResult.Yes)
                {
                    return;
                }
            }
            if (cShape is CControl)
            {
                userCommandControl21.theglobal.uc2.Controls.Remove(((CControl)cShape)._c);
            }
            userCommandControl21.theglobal.g_ListAllShowCShape.Remove(cShape);
            CEditEnvironmentGlobal.CLS.Add(cShape);
            list.Add(cShape);
            objView_Page.OnFresh(cShape.ShapeID.ToString());
        }
        userCommandControl21.theglobal.ForUndo(null, list);
        userCommandControl21.theglobal.SelectedShapeList.Clear();
        userCommandControl21.theglobal.uc2.RefreshGraphics();
    }

    private void BarButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
    {
        CEditEnvironmentGlobal.CLS.Clear();
        CShape[] array = userCommandControl21.theglobal.SelectedShapeList.ToArray();
        foreach (CShape cShape in array)
        {
            CShape item = cShape;
            CEditEnvironmentGlobal.CLS.Add(item);
        }
    }

    private void BarButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
    {
        List<CShape> list = new();
        userCommandControl21.theglobal.SelectedShapeList.Clear();
        CEditEnvironmentGlobal.CLS.Sort(CEditEnvironmentGlobal.CompareByLayer);
        CShape[] array = CEditEnvironmentGlobal.CLS.ToArray();
        foreach (CShape cShape in array)
        {
            CShape cShape2 = cShape.clone();
            cShape2.Location = new PointF(cShape2.Location.X + 25f, cShape2.Location.Y + 25f);
            if (cShape is CControl)
            {
                ((CControl)cShape2).initLocationErr = true;
                userCommandControl21.theglobal.uc2.Controls.Add(((CControl)cShape2)._c);
                ((CControl)cShape2).initLocationErr = false;
                ((CControl)cShape2)._c.Enabled = false;
                ((CControl)cShape2)._c.BringToFront();
                if (((CControl)cShape)._c is IDCCEControl)
                {
                    ((IDCCEControl)((CControl)cShape2)._c).GetVarTableEvent += CForDCCEControl.GetVarTableEvent;
                }
            }
            cShape2.ShapeID = Guid.NewGuid();
            UserCommandControl2.GiveName(cShape2);
            userCommandControl21.theglobal.g_ListAllShowCShape.Add(cShape2);
            userCommandControl21.theglobal.SelectedShapeList.Add(cShape2);
            list.Add(cShape2);
            objView_Page.OnFresh(cShape2.ShapeID.ToString());
        }
        userCommandControl21.theglobal.ForUndo(list, null);
        userCommandControl21.theglobal.uc2.RefreshGraphics();
    }

    private void BarButtonItem16_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.theglobal.SelectedShapeList.Clear();
        CShape[] array = userCommandControl21.theglobal.g_ListAllShowCShape.ToArray();
        foreach (CShape item in array)
        {
            userCommandControl21.theglobal.SelectedShapeList.Add(item);
        }
        userCommandControl21.theglobal.uc2.RefreshGraphics();
    }

    private void BarButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
    {
        var list = new List<CShape>();
        var array = userCommandControl21.theglobal.SelectedShapeList.ToArray();
        foreach (CShape cShape in array)
        {
            var list2 = CheckIOExists.ShapeInUse(userCommandControl21.theglobal.df.name + "." + cShape.Name);
            if (list2.Count != 0)
            {
                var delPage2 = new delPage(list2, "该图形正在被引用,是否仍继续操作.");
                if (delPage2.ShowDialog() != DialogResult.Yes)
                    return;
            }
            if (cShape is CControl)
            {
                userCommandControl21.theglobal.uc2.Controls.Remove(((CControl)cShape)._c);
            }
            userCommandControl21.theglobal.g_ListAllShowCShape.Remove(cShape);
            list.Add(cShape);
            objView_Page.OnFresh(cShape.ShapeID.ToString());
        }
        userCommandControl21.theglobal.ForUndo(null, list);
        userCommandControl21.theglobal.SelectedShapeList.Clear();
        userCommandControl21.theglobal.uc2.RefreshGraphics();
    }

    private void BarButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
    {
        var form = new TextFindForm();
        form.FindResultHandler += delegate (object s1, FindResultEventArgs e1)
        {
            if (e1.Result != null)
            {
                CEditEnvironmentGlobal.OutputMessage.Say("找到匹配项: \"" + e1.Result.PageDisplayName + "\"[" + e1.Result.ShapeName + "]");
                OpenPage(e1.Result.PageName);
                CGlobal theglobal = CEditEnvironmentGlobal.childform.theglobal;
                theglobal.SelectedShapeList.Clear();
                foreach (CShape item in theglobal.g_ListAllShowCShape)
                {
                    if (item.Name.Equals(e1.Result.ShapeName))
                    {
                        theglobal.SelectedShapeList.Add(item);
                        CEditEnvironmentGlobal.mdiparent.PropertyGrid_Attrubute.SelectedObject = item;
                    }
                }
                CEditEnvironmentGlobal.mdiparent.objView_Page.FreshSelect(theglobal);
                theglobal.uc2.RefreshGraphics();
            }
        };
        form.ShowDialog(this);
    }

    private void BarButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
    {
        var form = new yytjForm(CEditEnvironmentGlobal.dhp, CEditEnvironmentGlobal.dfs, CEditEnvironmentGlobal.ioitemroot, CEditEnvironmentGlobal.dhp.ProjectIOs);
        form.Show();
    }

    private void BarButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button9_Click(sender, e);
    }

    private void BarButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button10_Click(sender, e);
    }

    private void BarButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button19_Click(sender, e);
    }

    private void BarButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button20_Click(sender, e);
    }

    private void BarButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button7_Click(sender, e);
    }

    private void BarButtonItem28_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button8_Click(sender, e);
    }

    private void BarButtonItem48_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button16_Click(sender, e);
    }

    private void BarButtonItem49_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button15_Click(sender, e);
    }

    private void BarButtonItem50_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button14_Click(sender, e);
    }

    private void BarButtonItem51_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button11_Click(sender, e);
    }

    private void BarButtonItem52_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button12_Click(sender, e);
    }

    private void BarButtonItem53_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button13_Click(sender, e);
    }

    private void BarButtonItem54_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button27_Click(sender, e);
    }

    private void BarButtonItem55_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.SameWidth();
    }

    private void BarButtonItem56_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.SameHeight();
    }

    private void BarButtonItem29_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button17_Click(sender, e);
    }

    private void BarButtonItem30_ItemClick(object sender, ItemClickEventArgs e)
    {
        userCommandControl21.button18_Click(sender, e);
    }

    private void BarCheckItem10_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        if (barCheckItem10.Tag == null || (bool)barCheckItem10.Tag)
        {
            userCommandControl21.LockShape();
        }
        barCheckItem_LockElements.Checked = barCheckItem10.Checked;
    }

    private void Cb_DropDownClosed(object sender, EventArgs e)
    {
        if (userCommandControl21.theglobal.SelectedShapeList.Count == 0 || dataGridView1.SelectedCells.Count == 0 || cb.SelectedItem == null)
            return;

        switch (cb.SelectedItem.ToString())
        {
            case "模拟量输入":
                {
                    dataGridView1.SelectedCells[0].Value = "模拟量输入";
                    userCommandControl21.theglobal.SelectedShapeList[0].ai = true;
                    aiForm aiForm2 = new(userCommandControl21.theglobal);
                    aiForm2.ShowDialog();
                    break;
                }
            case "数字量输入":
                {
                    dataGridView1.SelectedCells[0].Value = "数字量输入";
                    userCommandControl21.theglobal.SelectedShapeList[0].di = true;
                    diForm diForm2 = new(userCommandControl21.theglobal);
                    diForm2.ShowDialog();
                    break;
                }
            case "字符串输入":
                {
                    dataGridView1.SelectedCells[0].Value = "字符串输入";
                    userCommandControl21.theglobal.SelectedShapeList[0].zfcsr = true;
                    zfcsrForm zfcsrForm2 = new(userCommandControl21.theglobal);
                    zfcsrForm2.ShowDialog();
                    break;
                }
            case "模拟量输出":
                {
                    dataGridView1.SelectedCells[0].Value = "模拟量输出";
                    userCommandControl21.theglobal.SelectedShapeList[0].ao = true;
                    aoForm aoForm2 = new(userCommandControl21.theglobal);
                    aoForm2.ShowDialog();
                    break;
                }
            case "数字量输出":
                {
                    dataGridView1.SelectedCells[0].Value = "数字量输出";
                    userCommandControl21.theglobal.SelectedShapeList[0].doo = true;
                    doForm doForm2 = new(userCommandControl21.theglobal);
                    doForm2.ShowDialog();
                    break;
                }
            case "字符串输出":
                {
                    dataGridView1.SelectedCells[0].Value = "字符串输出";
                    userCommandControl21.theglobal.SelectedShapeList[0].zfcsc = true;
                    zfcscForm zfcscForm2 = new(userCommandControl21.theglobal);
                    zfcscForm2.ShowDialog();
                    break;
                }
            case "边线":
                {
                    dataGridView1.SelectedCells[0].Value = "边线";
                    userCommandControl21.theglobal.SelectedShapeList[0].bxysbh = true;
                    ysForm ysForm4 = new(userCommandControl21.theglobal, 1);
                    ysForm4.ShowDialog();
                    break;
                }
            case "填充色1":
                {
                    dataGridView1.SelectedCells[0].Value = "填充色1";
                    userCommandControl21.theglobal.SelectedShapeList[0].tcs1ysbh = true;
                    ysForm ysForm3 = new(userCommandControl21.theglobal, 2);
                    ysForm3.ShowDialog();
                    break;
                }
            case "填充色2":
                {
                    dataGridView1.SelectedCells[0].Value = "填充色2";
                    userCommandControl21.theglobal.SelectedShapeList[0].tcs2ysbh = true;
                    ysForm ysForm2 = new(userCommandControl21.theglobal, 3);
                    ysForm2.ShowDialog();
                    break;
                }
            case "垂直填充":
                {
                    dataGridView1.SelectedCells[0].Value = "垂直填充";
                    userCommandControl21.theglobal.SelectedShapeList[0].czbfb = true;
                    czbfbForm czbfbForm2 = new(userCommandControl21.theglobal);
                    czbfbForm2.ShowDialog();
                    break;
                }
            case "水平填充":
                {
                    dataGridView1.SelectedCells[0].Value = "水平填充";
                    userCommandControl21.theglobal.SelectedShapeList[0].spbfb = true;
                    spbfbForm spbfbForm2 = new(userCommandControl21.theglobal);
                    spbfbForm2.ShowDialog();
                    break;
                }
            case "垂直移动":
                {
                    dataGridView1.SelectedCells[0].Value = "垂直移动";
                    userCommandControl21.theglobal.SelectedShapeList[0].czyd = true;
                    czydForm czydForm2 = new(userCommandControl21.theglobal);
                    czydForm2.ShowDialog();
                    break;
                }
            case "水平移动":
                {
                    dataGridView1.SelectedCells[0].Value = "水平移动";
                    userCommandControl21.theglobal.SelectedShapeList[0].spyd = true;
                    spydForm spydForm2 = new(userCommandControl21.theglobal);
                    spydForm2.ShowDialog();
                    break;
                }
            case "旋转":
                {
                    dataGridView1.SelectedCells[0].Value = "旋转";
                    userCommandControl21.theglobal.SelectedShapeList[0].mbxz = true;
                    xzForm xzForm2 = new(userCommandControl21.theglobal);
                    xzForm2.ShowDialog();
                    break;
                }
            case "宽度变化":
                {
                    dataGridView1.SelectedCells[0].Value = "宽度变化";
                    userCommandControl21.theglobal.SelectedShapeList[0].kdbh = true;
                    kdbhForm kdbhForm2 = new(userCommandControl21.theglobal);
                    kdbhForm2.ShowDialog();
                    break;
                }
            case "高度变化":
                {
                    dataGridView1.SelectedCells[0].Value = "高度变化";
                    userCommandControl21.theglobal.SelectedShapeList[0].gdbh = true;
                    gdbhForm gdbhForm2 = new(userCommandControl21.theglobal);
                    gdbhForm2.ShowDialog();
                    break;
                }
            case "隐藏":
                {
                    dataGridView1.SelectedCells[0].Value = "隐藏";
                    userCommandControl21.theglobal.SelectedShapeList[0].txyc = true;
                    txycForm txycForm2 = new(userCommandControl21.theglobal);
                    txycForm2.ShowDialog();
                    break;
                }
            case "页面切换":
                {
                    dataGridView1.SelectedCells[0].Value = "页面切换";
                    userCommandControl21.theglobal.SelectedShapeList[0].ymqh = true;
                    pageForm pageForm2 = new(userCommandControl21.theglobal, CEditEnvironmentGlobal.dfs);
                    pageForm2.ShowDialog();
                    break;
                }
            case "水平拖拽":
                {
                    dataGridView1.SelectedCells[0].Value = "水平拖拽";
                    userCommandControl21.theglobal.SelectedShapeList[0].sptz = true;
                    sptzForm sptzForm2 = new(userCommandControl21.theglobal);
                    sptzForm2.ShowDialog();
                    break;
                }
            case "垂直拖拽":
                {
                    dataGridView1.SelectedCells[0].Value = "垂直拖拽";
                    userCommandControl21.theglobal.SelectedShapeList[0].cztz = true;
                    cztzForm cztzForm2 = new(userCommandControl21.theglobal);
                    cztzForm2.ShowDialog();
                    break;
                }
            case "鼠标事件":
                dataGridView1.SelectedCells[0].Value = "鼠标事件";
                userCommandControl21.theglobal.SelectedShapeList[0].sbsj = true;
                CEditEnvironmentGlobal.scriptUnitForm.Init();
                CEditEnvironmentGlobal.scriptUnitForm.Show();
                CEditEnvironmentGlobal.scriptUnitForm.BringToFront();
                CEditEnvironmentGlobal.scriptUnitForm.SelectScript(string.Concat("工程相关>页面相关>", userCommandControl21.theglobal.df.pageName, ">", userCommandControl21.theglobal.SelectedShapeList[0], ">鼠标左键按下"));
                break;
            case "<删除动画连接>":
                switch (dataGridView1.SelectedCells[0].OwningRow.Cells[0].Value.ToString())
                {
                    case "模拟量输入":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].ai = false;
                        break;
                    case "数字量输入":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].di = false;
                        break;
                    case "字符串输入":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].zfcsr = false;
                        break;
                    case "模拟量输出":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].ao = false;
                        break;
                    case "数字量输出":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].doo = false;
                        break;
                    case "字符串输出":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].zfcsc = false;
                        break;
                    case "边线":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].bxysbh = false;
                        break;
                    case "填充色1":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].tcs1ysbh = false;
                        break;
                    case "填充色2":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].tcs2ysbh = false;
                        break;
                    case "垂直填充":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].czbfb = false;
                        break;
                    case "水平填充":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].spbfb = false;
                        break;
                    case "垂直移动":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].czyd = false;
                        break;
                    case "水平移动":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].spyd = false;
                        break;
                    case "旋转":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].mbxz = false;
                        break;
                    case "宽度变化":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].kdbh = false;
                        break;
                    case "高度变化":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].gdbh = false;
                        break;
                    case "隐藏":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].txyc = false;
                        break;
                    case "页面切换":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].ymqh = false;
                        break;
                    case "水平拖拽":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].sptz = false;
                        break;
                    case "垂直拖拽":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].cztz = false;
                        break;
                    case "鼠标事件":
                        dataGridView1.SelectedCells[0].Value = "";
                        userCommandControl21.theglobal.SelectedShapeList[0].sbsj = false;
                        break;
                }
                break;
        }
        cb.SelectedItem = null;
        cb.Visible = false;
    }

    private void MDIParent1_MdiChildActivate(object sender, EventArgs e)
    {
        if (base.ActiveMdiChild != null && base.ActiveMdiChild.GetType() == typeof(ChildForm))
        {
            varExplore.AddVarExplore();
            ((ChildForm)base.ActiveMdiChild).theglobal.uc1 = userCommandControl21;
            ((ChildForm)base.ActiveMdiChild).theglobal.dataGridView = dataGridView1;
            ((ChildForm)base.ActiveMdiChild).theglobal.listView2 = listView_事件;
            ((ChildForm)base.ActiveMdiChild).theglobal.pg = PropertyGrid_Attrubute;
            CEditEnvironmentGlobal.childform = (ChildForm)base.ActiveMdiChild;
            userCommandControl21.theglobal = ((ChildForm)base.ActiveMdiChild).theglobal;
            userCommandControl21.ReFreshEnable();

            PropertyGrid_Attrubute.SelectedObject = ((ChildForm)base.ActiveMdiChild).theglobal.pageProp;

            barButtonItem10.Enabled = userCommandControl21.button_撤消.Enabled;
            barButtonItem_Undo.Enabled = userCommandControl21.button_撤消.Enabled;
            barButtonItem11.Enabled = userCommandControl21.button_重复.Enabled;
            barButtonItem_Redo.Enabled = userCommandControl21.button_重复.Enabled;
            if (!((ChildForm)base.ActiveMdiChild).m_IsClosing)
            {
                objView_Page.OnClear();
                objView_Page.m_ObjGbl = userCommandControl21.theglobal;
                objView_Page.OnShow();
            }
        }
    }

    private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (CEditEnvironmentGlobal.HMIPath != "")
        {
            switch (MessageBox.Show("HMI将关闭,是否将更改保存到工程中?", "警告", MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    return;
                case DialogResult.Yes:
                    BarButtonItem_保存工程_ItemClick(sender, null);
                    break;
            }
            SaveBarLayout();
            userCommandControl21.theglobal = nullcglobal;
            SaveCommonMetaFileConfig();
        }
        try
        {
            if (CEditEnvironmentGlobal.HMIRunProcess != null)
            {
                CEditEnvironmentGlobal.HMIRunProcess.Kill();
                CEditEnvironmentGlobal.HMIRunProcess = null;
            }
        }
        catch (Exception)
        {
        }
    }

    private void DockPanel_ActiveX控件_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            popupMenu1.ShowPopup(Cursor.Position);
        }
    }

    private void BarButtonItem_绘制直线_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarStaticItem_Status.Caption = "开始绘制直线";
        userCommandControl21.button_绘制直线_Click(sender, e);
    }

    private void BarButtonItem_绘制折线_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarStaticItem_Status.Caption = "开始绘制折线";
        userCommandControl21.button_绘制折线_Click(sender, e);
    }

    private void BarButtonItem_绘制椭圆_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarStaticItem_Status.Caption = "开始绘制椭圆";
        userCommandControl21.button_绘制椭圆_Click(sender, e);
    }

    private void BarButtonItem_绘制矩形_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarStaticItem_Status.Caption = "开始绘制矩形";
        userCommandControl21.button_绘制矩形_Click(sender, e);
    }

    private void BarButtonItem_绘制多边形_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarStaticItem_Status.Caption = "开始绘制多边形";
        userCommandControl21.button_绘制多边形_Click(sender, e);
    }

    private void BarButtonItem_绘制圆角矩形_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarStaticItem_Status.Caption = "开始绘制圆角矩形";
        userCommandControl21.button_绘制圆角矩形_Click(sender, e);
    }

    private void BarButtonItem_绘制文字_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarStaticItem_Status.Caption = "开始绘制文字";
        userCommandControl21.button_绘制文字_Click(sender, e);
    }

    private void BarButtonItem_导入图片_ItemClick(object sender, ItemClickEventArgs e)
    {
        BarStaticItem_Status.Caption = "开始导入图片";
        userCommandControl21.button_导入图片_Click(sender, e);
    }

    private void MenubarCheckItem_Windows控件_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        dockPanel_基本控件.Visibility = ((!menubarCheckItem_基本控件.Checked) ? DockVisibility.Hidden : DockVisibility.Visible);
    }

    private void DockPanel_Windows控件_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
        menubarCheckItem_基本控件.Checked = e.Visibility != DockVisibility.Hidden;
    }

    private void MenubarCheckItem_DCCE工控组件_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        dockPanel_DCCE工控组件.Visibility = ((!menubarCheckItem_DCCE工控组件.Checked) ? DockVisibility.Hidden : DockVisibility.Visible);
    }

    private void DockPanel_DCCE工控组件_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
        menubarCheckItem_DCCE工控组件.Checked = e.Visibility != DockVisibility.Hidden;
    }

    private void MenubarCheckItem_ActiveX控件_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        dockPanel_ActiveX控件.Visibility = ((!menubarCheckItem_ActiveX控件.Checked) ? DockVisibility.Hidden : DockVisibility.Visible);
    }

    private void DockPanel_ActiveX控件_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
        menubarCheckItem_ActiveX控件.Checked = e.Visibility != DockVisibility.Hidden;
    }

    private void MenubarCheckItem_精灵控件_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        dockPanel_精灵控件.Visibility = ((!menubarCheckItem_精灵控件.Checked) ? DockVisibility.Hidden : DockVisibility.Visible);
    }

    private void DockPanel_精灵控件_VisibilityChanged(object sender, VisibilityChangedEventArgs e)
    {
        menubarCheckItem_精灵控件.Checked = e.Visibility != DockVisibility.Hidden;
    }

    private void RepositoryItemZoomTrackBar1_EditValueChanging(object sender, ChangingEventArgs e)
    {
        try
        {
            if (Convert.ToSingle(e.NewValue) < 50f)
            {
                Convert.ToSingle(e.NewValue);
            }
            else if (Convert.ToSingle(e.NewValue) != 50f && Convert.ToSingle(e.NewValue) > 50f)
            {
                Convert.ToSingle(e.NewValue);
            }
            Form[] mdiChildren = base.MdiChildren;
            foreach (Form form in mdiChildren)
            {
                ChildForm childForm = (ChildForm)form;
                foreach (CShape item in childForm.theglobal.g_ListAllShowCShape)
                {
                    if (item is CControl)
                    {
                        ((CControl)item).UpdateControl();
                    }
                }
                childForm.Location = new Point(0, 0);
                childForm.Size = new Size(Convert.ToInt32(childForm.theglobal.df.size.Width), Convert.ToInt32(childForm.theglobal.df.size.Height));
                childForm.theglobal.uc2.RefreshGraphics();
            }
        }
        catch
        {
            MessageBox.Show("页面缩放出现异常！", "提示");
        }
    }

    private void UserCommandControl21_OnButtonEnableChanged(object sender, EventArgs e)
    {
        barButtonItem10.Enabled = userCommandControl21.button_撤消.Enabled;
        barButtonItem_Undo.Enabled = userCommandControl21.button_撤消.Enabled;
        barButtonItem11.Enabled = userCommandControl21.button_重复.Enabled;
        barButtonItem_Redo.Enabled = userCommandControl21.button_重复.Enabled;
    }

    private void TreeView_工程导航_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
    {
        if (e.Label != null && !e.Label.Equals(e.Node.Text))
        {
            if (e.Label.Length > 0)
            {
                if (e.Label.IndexOfAny(new char[4] { '@', '.', ',', '!' }) == -1)
                {
                    if (e.Node.Tag is HmiPageGroup)
                    {
                        if (页面组_重命名(e.Node.Name, e.Label))
                        {
                            (e.Node.Tag as HmiPageGroup).Text = e.Label;
                            e.Node.EndEdit(cancel: false);
                            treeView_工程导航.LabelEdit = false;
                        }
                        else
                        {
                            e.CancelEdit = true;
                            MessageBox.Show("无效的树形节点名称。\n节点名称已存在", "节点名称编辑");
                            e.Node.BeginEdit();
                        }
                    }
                    else if (e.Node.Tag is HmiPage)
                    {
                        if (RenamePage(e.Node.Name, e.Label))
                        {
                            (e.Node.Tag as HmiPage).Text = e.Label;
                            e.Node.EndEdit(cancel: false);
                            treeView_工程导航.LabelEdit = false;
                        }
                        else
                        {
                            e.CancelEdit = true;
                            MessageBox.Show("无效的树形节点名称。\n节点名称已存在", "节点名称编辑");
                            e.Node.BeginEdit();
                        }
                    }
                }
                else
                {
                    e.CancelEdit = true;
                    MessageBox.Show("无效的树形节点名称。\n节点名称包含非法字符", "节点名称编辑");
                    e.Node.BeginEdit();
                }
            }
            else
            {
                e.CancelEdit = true;
                MessageBox.Show("无效的树形节点名称。\n节点名称不可以为空", "节点名称编辑");
                e.Node.BeginEdit();
            }
        }
        else
        {
            treeView_工程导航.LabelEdit = false;
        }
    }

    private void TreeView_工程导航_AfterCollapse(object sender, TreeViewEventArgs e)
    {
        if (e.Node.Tag is HmiPageGroup && (e.Node.Tag as HmiPageGroup).Parent != null)
        {
            TreeNode node = e.Node;
            string imageKey = (e.Node.SelectedImageKey = "PageGroupClosed.png");
            node.ImageKey = imageKey;
        }
    }

    private void TreeView_工程导航_AfterExpand(object sender, TreeViewEventArgs e)
    {
        if (e.Node.Tag is HmiPageGroup && (e.Node.Tag as HmiPageGroup).Parent != null)
        {
            TreeNode node = e.Node;
            string imageKey = (e.Node.SelectedImageKey = "PageGroupOpen.png");
            node.ImageKey = imageKey;
        }
    }

    private void TreeView_工程导航_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
    {
        e.Cancel = m_MouseClicks > 1;
    }

    private void TreeView_工程导航_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
        e.Cancel = m_MouseClicks > 1;
    }

    private void TreeView_工程导航_MouseDown(object sender, MouseEventArgs e)
    {
        m_MouseClicks = e.Clicks;
        treeView_工程导航.SelectedNode = treeView_工程导航.GetNodeAt(e.X, e.Y);
    }

    private void TreeView_工程导航_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        if (e == null || e.Node == null || e.Button != MouseButtons.Left || e.Node.Nodes.Count > 0 || e.Node.Tag is HmiPageGroup)
            return;

        if (e.Node.Tag is HmiPage)
        {
            OpenPage(e.Node.Name);
            return;
        }

        if (e.Node.Name == "IO")
        {
            IOForm ioForm = CEditEnvironmentGlobal.ioForm;
            ioForm.Edit = true;
            ioForm.ShowDialog();
            ioForm.BringToFront();
            varExplore.AddVarExplore();
        }
        else if (e.Node.Name == "TagManager")
        {
            var form = new TagManagerForm();
            form.ShowDialog();
        }
        else if (e.Node.Name == "DeviceManager")
        {
            //var isExisted = MdiChildren.Contains(deviceManagerForm);
            //if (!isExisted)
            //{
            //    deviceManagerForm = new DeviceManagerForm();
            //    deviceManagerForm.MdiParent = this;
            //    deviceManagerForm.Show();
            //    ActivateMdiChild(deviceManagerForm);
            //    deviceManagerForm.BringToFront();
            //}
            //else
            //{
            //    deviceManagerForm.BringToFront();
            //}

            var deviceManagerForm = new DeviceManagerForm();
            deviceManagerForm.ShowDialog();
        }
        else if (e.Node.Name == "DBConn")
        {
            DBConnConfigForm dBConnConfigForm = new()
            {
                DbType = CEditEnvironmentGlobal.dhp.dbCfgPara.DBType,
                DbConnStr = CEditEnvironmentGlobal.dhp.dbCfgPara.DBConnStr
            };
            DBConnConfigForm dBConnConfigForm2 = dBConnConfigForm;
            if (dBConnConfigForm2.ShowDialog() == DialogResult.OK)
            {
                CEditEnvironmentGlobal.dhp.dbCfgPara.DBType = dBConnConfigForm2.DbType;
                CEditEnvironmentGlobal.dhp.dbCfgPara.DBConnStr = dBConnConfigForm2.DbConnStr;
                try
                {
                    DBOperationGlobal.Refresh(CEditEnvironmentGlobal.dhp.dbCfgPara.DBType, CEditEnvironmentGlobal.dhp.dbCfgPara.DBConnStr);
                }
                catch
                {
                    MessageBox.Show("数据库连接失败，请检查！" + Environment.NewLine + "（如果为MySQL数据库连接，请安装MySQL数据库连接驱动）");
                }
            }
        }
        else if (e.Node.Name == "Authority")
        {
            //AuthoritySettingForm authoritySettingForm = new();
            //authoritySettingForm.ShowDialog();
            var form = new UserForm();
            form.ShowDialog();
        }
        else if (e.Node.Name == "Setting")
        {
            setForm setForm2 = new(CEditEnvironmentGlobal.dhp);
            setForm2.ShowDialog();
        }
        else
        {
            _ = e.Node.Name == "Alarm_CrossPlatform";
        }
    }

    private void TreeView_工程导航_ItemDrag(object sender, ItemDragEventArgs e)
    {
        if (e.Button != MouseButtons.Left || e.Item is not TreeNode)
        {
            return;
        }
        if (((TreeNode)e.Item).Tag is HmiPageGroup)
        {
            if ((((TreeNode)e.Item).Tag as HmiPageGroup).Parent != null)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }
        else if (((TreeNode)e.Item).Tag is HmiPage)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }
    }

    private void TreeView_工程导航_DragOver(object sender, DragEventArgs e)
    {
        e.Effect = DragDropEffects.None;
        if (e.Data.GetDataPresent(typeof(TreeNode)) && e.Data.GetData(typeof(TreeNode)) is TreeNode treeNode)
        {
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            TreeNode nodeAt = ((TreeView)sender).GetNodeAt(pt);
            if (nodeAt != null && nodeAt.Tag is HmiPageGroup && treeNode != nodeAt && treeNode.Parent != nodeAt)
            {
                e.Effect = DragDropEffects.Move;
            }
        }
    }

    private void TreeView_工程导航_DragDrop(object sender, DragEventArgs e)
    {
        if (!e.Data.GetDataPresent(typeof(TreeNode)) || e.Data.GetData(typeof(TreeNode)) is not TreeNode treeNode)
        {
            return;
        }
        Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
        TreeNode nodeAt = ((TreeView)sender).GetNodeAt(pt);
        if (nodeAt != null && nodeAt.Tag is HmiPageGroup)
        {
            if (treeNode.Tag is HmiPageGroup)
            {
                CEditEnvironmentGlobal.PageManager.MovePageGroupToPageGroup(treeNode, nodeAt);
                ((HmiPageGroup)treeNode.Tag).Parent.Children.Remove((HmiPageGroup)treeNode.Tag);
                treeNode.Parent.Nodes.Remove(treeNode);
                ((HmiPageGroup)treeNode.Tag).Parent = (HmiPageGroup)nodeAt.Tag;
                ((HmiPageGroup)nodeAt.Tag).Children.Add((HmiPageGroup)treeNode.Tag);
                nodeAt.Nodes.Add(treeNode);
                nodeAt.ExpandAll();
            }
            else if (treeNode.Tag is HmiPage)
            {
                CEditEnvironmentGlobal.PageManager.MovePageToPageGroup(treeNode, nodeAt);
                ((HmiPage)treeNode.Tag).Parent.Children.Remove((HmiPage)treeNode.Tag);
                treeNode.Parent.Nodes.Remove(treeNode);
                ((HmiPage)treeNode.Tag).Parent = (HmiPageGroup)nodeAt.Tag;
                ((HmiPageGroup)nodeAt.Tag).Children.Add((HmiPage)treeNode.Tag);
                nodeAt.Nodes.Add(treeNode);
                nodeAt.ExpandAll();
            }
        }
    }

    private void ToolStripMenuItem_页面根_关闭所有_Click(object sender, EventArgs e)
    {
        页面组_关闭所有();
    }

    private void ToolStripMenuItem_页面组_新建页面组_Click(object sender, EventArgs e)
    {
        NewPageGroup();
    }

    private void ToolStripMenuItem_页面组_重命名_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            treeView_工程导航.LabelEdit = true;
            if (!selectedNode.IsEditing)
            {
                selectedNode.BeginEdit();
            }
        }
    }

    private void ToolStripMenuItem_页面组_删除_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            页面组_删除(selectedNode.Name);
        }
    }

    private void ToolStripMenuItem_页面组_新建页面_Click(object sender, EventArgs e)
    {
        CreateNewPage();
    }

    private void ToolStripMenuItem_页面组_导入页面_Click(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new()
        {
            Filter = "图形文件(*.hpg)|*.hpg"
        };
        OpenFileDialog openFileDialog2 = openFileDialog;
        if (openFileDialog2.ShowDialog(this) == DialogResult.OK)
        {
            ImportPage(openFileDialog2.FileName);
        }
    }

    private void ToolStripMenuItem_页面组_粘贴页面_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            PastePage();
        }
    }

    private void ToolStripMenuItem_页面组_上移_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            TreeNode prevNode = selectedNode.PrevNode;
            if (prevNode != null)
            {
                CEditEnvironmentGlobal.PageManager.MovePageGroupBeforeNode(selectedNode, prevNode);
                页面树节点_上移(selectedNode, prevNode);
            }
        }
    }

    private void ToolStripMenuItem_页面组_下移_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            TreeNode nextNode = selectedNode.NextNode;
            if (nextNode != null)
            {
                CEditEnvironmentGlobal.PageManager.MovePageGroupAfterNode(selectedNode, nextNode);
                页面树节点_下移(selectedNode, nextNode);
            }
        }
    }

    private void ToolStripMenuItem_页面组_移至顶层_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            TreeNode firstNode = selectedNode.Parent.FirstNode;
            if (selectedNode != firstNode)
            {
                CEditEnvironmentGlobal.PageManager.MovePageGroupBeforeNode(selectedNode, firstNode);
                页面树节点_移至顶层(selectedNode, firstNode);
            }
        }
    }

    private void ToolStripMenuItem_页面组_移至底层_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            TreeNode lastNode = selectedNode.Parent.LastNode;
            if (selectedNode != lastNode)
            {
                CEditEnvironmentGlobal.PageManager.MovePageGroupAfterNode(selectedNode, lastNode);
                页面树节点_移至底层(selectedNode, lastNode);
            }
        }
    }

    private void ToolStripMenuItem_页面组_关闭所有_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            页面组_关闭所有(selectedNode.Name);
        }
    }

    private void ToolStripMenuItem_页面_打开_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            OpenPage(selectedNode.Name);
        }
    }

    private void ToolStripMenuItem_页面_关闭_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            ClosePage(selectedNode.Name);
        }
    }

    private void ToolStripMenuItem_页面_保存_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            SavePage(selectedNode.Name);
        }
    }

    private void ToolStripMenuItem_页面_删除_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            DeletePage(selectedNode.Name);
        }
    }

    private void ToolStripMenuItem_页面_上移_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            TreeNode prevNode = selectedNode.PrevNode;
            if (prevNode != null)
            {
                CEditEnvironmentGlobal.PageManager.MovePageBeforeNode(selectedNode, prevNode);
                页面树节点_上移(selectedNode, prevNode);
            }
        }
    }

    private void ToolStripMenuItem_页面_下移_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            TreeNode nextNode = selectedNode.NextNode;
            if (nextNode != null)
            {
                CEditEnvironmentGlobal.PageManager.MovePageAfterNode(selectedNode, nextNode);
                页面树节点_下移(selectedNode, nextNode);
            }
        }
    }

    private void ToolStripMenuItem_页面_移至顶层_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            TreeNode firstNode = selectedNode.Parent.FirstNode;
            if (selectedNode != firstNode)
            {
                CEditEnvironmentGlobal.PageManager.MovePageBeforeNode(selectedNode, firstNode);
                页面树节点_移至顶层(selectedNode, firstNode);
            }
        }
    }

    private void ToolStripMenuItem_页面_移至底层_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            TreeNode lastNode = selectedNode.Parent.LastNode;
            if (selectedNode != lastNode)
            {
                CEditEnvironmentGlobal.PageManager.MovePageAfterNode(selectedNode, lastNode);
                页面树节点_移至底层(selectedNode, lastNode);
            }
        }
    }

    private void ToolStripMenuItem_页面_复制_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            CopyPage(selectedNode.Name);
        }
    }

    private void ToolStripMenuItem_页面_导出_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode == null)
        {
            return;
        }
        SaveFileDialog saveFileDialog = new()
        {
            Filter = "图形文件(*.hpg)|*.hpg",
            Title = "导出页面“" + selectedNode.Text + "”...",
            FileName = selectedNode.Name
        };
        SaveFileDialog saveFileDialog2 = saveFileDialog;
        if (saveFileDialog2.ShowDialog(this) == DialogResult.OK)
        {
            DataFile dataFile = CEditEnvironmentGlobal.PageManager.DFM.Find(selectedNode.Name);
            if (dataFile != null)
            {
                dataFile.PIO = CEditEnvironmentGlobal.dhp.ProjectIOs.ToArray();
                Operation.BinarySaveFile(saveFileDialog2.FileName, dataFile);
                dataFile.PIO = null;
            }
        }
    }

    private void ToolStripMenuItem_页面_属性_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = treeView_工程导航.SelectedNode;
        if (selectedNode != null)
        {
            PropertyPage(selectedNode.Name);
        }
    }

    private void InitBasicWindowsControl()
    {
        foreach (ListViewItem item in listView_基本图元.Items)
        {
            item.Name = item.Text;
        }
        foreach (ListViewItem item2 in listView_Windows控件.Items)
        {
            item2.Name = item2.Text;
        }
    }

    private void InitCommonMetaFileControl()
    {
        try
        {
            string text = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\CommonMetafile.config";
            CommonMetaDataInfo.ReadXml(text, XmlReadMode.ReadSchema);
            foreach (DataRow row in CommonMetaDataInfo.Tables["Meta"].Rows)
            {
                ListView listView = null;
                int num = (int)row["CategorySN"];
                string name = row["GroupName"].ToString();
                switch (num)
                {
                    case 0:
                        listView = listView_基本图元;
                        break;
                    case 2:
                        listView = listView_Windows控件;
                        break;
                    case 3:
                        listView = navBarControl_DCCE工控组件.Groups[name].ControlContainer.Controls[0] as ListView;
                        break;
                    case 4:
                        listView = listView_ActiveX控件;
                        break;
                    case 5:
                        listView = navBarControl_精灵控件.Groups[name].ControlContainer.Controls[0] as ListView;
                        break;
                }

                if (listView != null)
                {
                    ListViewItem listViewItem = listView.Items[row["Name"].ToString()];
                    if (listViewItem != null)
                    {
                        AddCommonListView(listView, listViewItem, isLoad: true);
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }

    private void AddCommonListView(ListView lv, ListViewItem lvi, bool isLoad)
    {
        ListViewItem listViewItem2 = lvi.Clone() as ListViewItem;
        listViewItem2.Tag = GetSharpType(lv.Name);
        listViewItem2.Name = lvi.Name;
        if (lv.VirtualMode)
        {
            listViewItem2.ImageKey = lv.LargeImageList.Images.Keys[lvi.ImageIndex];
        }

        if (!isLoad)
        {
            string value = "";
            if (lv.Name == "listView_精灵控件" || lv.Name == "listView_向量图元")
            {
                NavBarGroupControlContainer navBarGroupControlContainer = lv.Parent as NavBarGroupControlContainer;
                NavBarGroup ownerGroup = navBarGroupControlContainer.OwnerGroup;
                value = ownerGroup.Name;
            }
            try
            {
                DataRow dataRow = CommonMetaDataInfo.Tables["Meta"].NewRow();
                dataRow["CategorySN"] = listViewItem2.Tag;
                dataRow["Name"] = listViewItem2.Name;
                dataRow["GroupName"] = value;
                CommonMetaDataInfo.Tables["Meta"].Rows.Add(dataRow);
            }
            catch (Exception)
            {
            }
        }
    }

    private void SaveCommonMetaFileConfig()
    {
        try
        {
            string text = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\DView\\Config\\";
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            CommonMetaDataInfo.WriteXml(text + "CommonMetafile.config", XmlWriteMode.WriteSchema);
        }
        catch (Exception)
        {
        }
    }

    private void ListView_图元_MouseDown(object sender, MouseEventArgs e)
    {
        ListView listView = sender as ListView;
        ListViewItem itemAt = listView.GetItemAt(e.X, e.Y);
        if (itemAt != null && e.Button == MouseButtons.Left)
        {
            int num = GetSharpType(listView.Name);
            if (num != -1)
            {
                listView.DoDragDrop($"AddShape:{num}:{itemAt.Name}", DragDropEffects.Copy);
            }
        }
    }

    private int GetSharpType(string sharpName)
    {
        int result = -1;
        switch (sharpName)
        {
            case "listView_基本图元":
                result = 0;
                break;
            case "listView_向量图元":
                result = 1;
                break;
            case "listView_Windows控件":
                result = 2;
                break;
            case "listView_DCCE工控组件":
                result = 3;
                break;
            case "listView_ActiveX控件":
                result = 4;
                break;
            case "listView_精灵控件":
                result = 5;
                break;
            case "listView_跨平台控件":
                result = 6;
                break;
            case "listView_常用图元":
                result = 9;
                break;
        }
        return result;
    }

    private void LoadBarLayout()
    {
        try
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + DockConfigFileName))
            {
                barManager1.RestoreLayoutFromXml(AppDomain.CurrentDomain.BaseDirectory + DockConfigFileName);
            }
        }
        catch
        {
            MessageBox.Show("获取窗体初始化布局方法出现异常！", "提示");
        }
    }

    public void InitPictureAndVar()
    {
        try
        {
            //initForm.say("正在加载变量及图片");
            VarTableUITypeEditor.GetVar = CForDCCEControl.GetVarTableEvent;
            VarTableImageManage.GetImage = CForDCCEControl.GetImage;
        }
        catch
        {
            MessageBox.Show("初始化加载图片和变量出现异常！", "提示");
        }
    }

    public void InitScript()
    {
        //try
        //{
        CEditEnvironmentGlobal.scriptUnitForm = new ScriptUnit();
        //}
        //catch
        //{
        //	MessageBox.Show("初始化脚本出现异常！", "提示");
        //}
    }

    public void InitDockAnimation()
    {
        try
        {
            cb = new System.Windows.Forms.ComboBox
            {
                Visible = false,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            dataGridView1.Controls.Add(cb);
        }
        catch
        {
            MessageBox.Show("初始化对象动画连接出现异常！", "提示");
        }
    }

    private void InitDockerLayout()
    {
        dockPanel_事件.Visibility = DockVisibility.Hidden;
        menubarCheckItem_事件.Visibility = BarItemVisibility.Never;
        dockPanel_动画.Visibility = DockVisibility.Hidden;
        menubarCheckItem_动画.Visibility = BarItemVisibility.Never;
        dockPanel_导航栏.Visibility = DockVisibility.Visible;
        menubarCheckItem_导航栏.Visibility = BarItemVisibility.Always;
        barSubItem_主菜单_脚本.Visibility = BarItemVisibility.Always;
        menubarCheckItem_精灵控件.Visibility = BarItemVisibility.Always;
        toolbarButtonItem_精灵控件.Visibility = BarItemVisibility.Always;
        dockPanel_精灵控件.Visibility = DockVisibility.AutoHide;
        menubarCheckItem_DCCE工控组件.Visibility = BarItemVisibility.Always;
        toolbarButtonItem_DCCE工控组件.Visibility = BarItemVisibility.Always;
        dockPanel_DCCE工控组件.Visibility = DockVisibility.AutoHide;
        menubarCheckItem_ActiveX控件.Visibility = BarItemVisibility.Always;
        toolbarButtonItem_ActiveX控件.Visibility = BarItemVisibility.Always;
        dockPanel_ActiveX控件.Visibility = DockVisibility.AutoHide;
        barButtonItem_Polygon.Visibility = BarItemVisibility.Always;
        barButtonItem_RoundedRectangle.Visibility = BarItemVisibility.Always;
    }

    private void InitNavigation()
    {
        try
        {
            //initForm.say("正在初始化工程导航栏");
            treeView_工程导航.Nodes[0].Text = "工程(" + CEditEnvironmentGlobal.dhp.ProjectName + ")";
            treeView_工程导航.ExpandAll();
        }
        catch
        {
            MessageBox.Show("初始化导航栏出现异常！", "提示");
        }
    }

    private void InitLibraryControl()
    {
        try
        {
            InitBasicWindowsControl();
            PixieLibraryControlDataSet.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\PixieControl.config");
            PixieLibraryControlImageList.ImageSize = new Size(50, 50);
            PixieLibraryControlImageList.ColorDepth = ColorDepth.Depth32Bit;
            InitPixieLibraryControl(PixieLibraryControlDataSet);
            DCCEControlLibraryControlDataSet.ReadXml(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\Pixie.config");
            DCCEControlLibraryControlImageList.ImageSize = new Size(50, 50);
            DCCEControlLibraryControlImageList.ColorDepth = ColorDepth.Depth32Bit;
            InitDCCEControlLibraryControl(DCCEControlLibraryControlDataSet);
            ActiveXControlImageList.ImageSize = new Size(50, 50);
            ActiveXControlImageList.ColorDepth = ColorDepth.Depth32Bit;
            listView_ActiveX控件.LargeImageList = ActiveXControlImageList;
            RefreshActiveXControl();
            InitCommonMetaFileControl();
        }
        catch
        {
            MessageBox.Show("初始化控件库出现异常！", "提示");
        }
    }

    public void InitEvent()
    {
        try
        {
            //initForm.say("正在初始化控件事件");
            repositoryItemZoomTrackBar1.EditValueChanging += RepositoryItemZoomTrackBar1_EditValueChanging;
            userCommandControl21.OnButtonEnableChanged += UserCommandControl21_OnButtonEnableChanged;
            cb.DropDownClosed += Cb_DropDownClosed;
        }
        catch
        {
            MessageBox.Show("初始化控件事件出现异常！", "提示");
        }
    }

    public void InitFormLook()
    {
        try
        {
            //initForm.say("正在初始化界面样式");

            SkinManager.EnableFormSkinsIfNotVista();
            UserLookAndFeel.Default.SetDefaultStyle();
            barManager1.ForceInitialize();
        }
        catch
        {
            MessageBox.Show("初始化界面外观出现异常！", "提示");
        }
    }

    public void InitAnimationConnect()
    {
        try
        {
            //initForm.say("正在初始化动画连接");

            dataGridView1.Rows.Add(21);
            DataGridViewComboBoxCell dataGridViewComboBoxCell = new();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("模拟量输入");
            dataGridView1.Rows[0].Cells[0].Value = "模拟量输入";
            dataGridView1.Rows[0].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[0].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("数字量输入");
            dataGridView1.Rows[1].Cells[0].Value = "数字量输入";
            dataGridView1.Rows[1].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[1].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("字符串输入");
            dataGridView1.Rows[2].Cells[0].Value = "字符串输入";
            dataGridView1.Rows[2].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[2].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("模拟量输出");
            dataGridView1.Rows[3].Cells[0].Value = "模拟量输出";
            dataGridView1.Rows[3].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[3].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("数字量输出");
            dataGridView1.Rows[4].Cells[0].Value = "数字量输出";
            dataGridView1.Rows[4].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[4].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("字符串输出");
            dataGridView1.Rows[5].Cells[0].Value = "字符串输出";
            dataGridView1.Rows[5].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[5].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("边线");
            dataGridView1.Rows[6].Cells[0].Value = "边线";
            dataGridView1.Rows[6].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[6].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("填充色1");
            dataGridView1.Rows[7].Cells[0].Value = "填充色1";
            dataGridView1.Rows[7].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[7].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("填充色2");
            dataGridView1.Rows[8].Cells[0].Value = "填充色2";
            dataGridView1.Rows[8].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[8].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("垂直填充");
            dataGridView1.Rows[9].Cells[0].Value = "垂直填充";
            dataGridView1.Rows[9].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[9].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("水平填充");
            dataGridView1.Rows[10].Cells[0].Value = "水平填充";
            dataGridView1.Rows[10].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[10].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("垂直移动");
            dataGridView1.Rows[11].Cells[0].Value = "垂直移动";
            dataGridView1.Rows[11].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[11].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("水平移动");
            dataGridView1.Rows[12].Cells[0].Value = "水平移动";
            dataGridView1.Rows[12].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[12].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("旋转");
            dataGridView1.Rows[13].Cells[0].Value = "旋转";
            dataGridView1.Rows[13].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[13].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("宽度变化");
            dataGridView1.Rows[14].Cells[0].Value = "宽度变化";
            dataGridView1.Rows[14].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[14].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("高度变化");
            dataGridView1.Rows[15].Cells[0].Value = "高度变化";
            dataGridView1.Rows[15].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[15].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("隐藏");
            dataGridView1.Rows[16].Cells[0].Value = "隐藏";
            dataGridView1.Rows[16].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[16].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("页面切换");
            dataGridView1.Rows[17].Cells[0].Value = "页面切换";
            dataGridView1.Rows[17].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[17].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("水平拖拽");
            dataGridView1.Rows[18].Cells[0].Value = "水平拖拽";
            dataGridView1.Rows[18].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[18].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("垂直拖拽");
            dataGridView1.Rows[19].Cells[0].Value = "垂直拖拽";
            dataGridView1.Rows[19].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[19].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
            dataGridViewComboBoxCell = new DataGridViewComboBoxCell();
            dataGridViewComboBoxCell.Items.Add("<无动画连接>");
            dataGridViewComboBoxCell.Items.Add("鼠标事件");
            dataGridView1.Rows[20].Cells[0].Value = "鼠标事件";
            dataGridView1.Rows[20].Cells[1] = dataGridViewComboBoxCell;
            dataGridView1.Rows[20].Cells[1].Value = dataGridViewComboBoxCell.Items[0];
        }
        catch
        {
            MessageBox.Show("初始化动画连接出现异常！", "提示");
        }
    }

    public void InitTheglobal()
    {
        try
        {
            userCommandControl21.theglobal = nullcglobal;
        }
        catch
        {
            MessageBox.Show("初始化全局变量出现异常！", "提示");
        }
    }

    public void InitVarBrowserShow()
    {
        try
        {
            varExplore.AddTreeNode();
        }
        catch
        {
            MessageBox.Show("初始化变量浏览显示出现异常！", "提示");
        }
    }

    public void PropertySelectJudge(PropertyValueChangedEventArgs e)
    {
        try
        {
            if (PropertyGrid_Attrubute.SelectedObject is CShape)
            {
                CEditEnvironmentGlobal.dhp.dirtyPageAdd(CEditEnvironmentGlobal.childform.theglobal.df.name);
            }
            else if (PropertyGrid_Attrubute.SelectedObject is CPageProperty && e.ChangedItem.Label != "脚本名称")
            {
                CEditEnvironmentGlobal.dhp.dirtyPageAdd(CEditEnvironmentGlobal.childform.theglobal.df.name);
            }
        }
        catch
        {
            MessageBox.Show("属性框选择时出现异常！", "提示");
        }
    }

    public void PropertyNameJudge(PropertyValueChangedEventArgs e)
    {
        try
        {
            if (!(e.ChangedItem.Label == "名称"))
            {
                return;
            }
            string text = (string)e.ChangedItem.Value;
            if (PropertyGrid_Attrubute.SelectedObject is CShape)
            {
                ((CShape)PropertyGrid_Attrubute.SelectedObject).Name = (string)e.OldValue;
            }
            else if (PropertyGrid_Attrubute.SelectedObject is IControlShape)
            {
                ((IControlShape)PropertyGrid_Attrubute.SelectedObject).ID = (string)e.OldValue;
            }
            List<string> list = CheckIOExists.ShapeInUse(CEditEnvironmentGlobal.childform.theglobal.df.name + "." + text);
            if (list.Count != 0)
            {
                delPage delPage2 = new(list, "该图形正在被引用,是否仍继续操作.");
                if (delPage2.ShowDialog() != DialogResult.Yes)
                {
                    userCommandControl21.theglobal.OldShapes.Clear();
                    return;
                }
            }
            Regex regex = new("^[a-zA-Z_][a-zA-Z0-9_]*$");
            if (!regex.IsMatch(text))
            {
                MessageBox.Show("名称中包含非法字符.", "错误");
                return;
            }
            foreach (CShape item in CEditEnvironmentGlobal.childform.theglobal.g_ListAllShowCShape)
            {
                if ((PropertyGrid_Attrubute.SelectedObject is not CShape || item != (CShape)PropertyGrid_Attrubute.SelectedObject) && (item is not CControl || ((CControl)item)._c != PropertyGrid_Attrubute.SelectedObject) && item.ShapeName == text)
                {
                    MessageBox.Show("名称重复", "错误");
                    PropertyGrid_Attrubute.Refresh();
                    userCommandControl21.theglobal.OldShapes.Clear();
                    return;
                }
            }
            string[] keystr = CheckScript.Keystr;
            foreach (string text2 in keystr)
            {
                if (text == text2)
                {
                    MessageBox.Show("名称中含有关键字将会导致错误。");
                    userCommandControl21.theglobal.OldShapes.Clear();
                    return;
                }
            }
            if (PropertyGrid_Attrubute.SelectedObject is CShape)
            {
                ((CShape)PropertyGrid_Attrubute.SelectedObject).Name = text;
                objView_Page.OnFresh(((CShape)PropertyGrid_Attrubute.SelectedObject).ShapeID.ToString());
            }
            else if (PropertyGrid_Attrubute.SelectedObject is IControlShape)
            {
                ((IControlShape)PropertyGrid_Attrubute.SelectedObject).ID = text;
            }
        }
        catch
        {
            MessageBox.Show("属性设置名称时出现异常", "提示");
        }
    }

    public void PropertyPageNameJudge(PropertyValueChangedEventArgs e)
    {
        try
        {
            if (!(e.ChangedItem.Label == "页面名称"))
            {
                return;
            }
            string text = string.Empty;
            string text2 = (string)e.ChangedItem.Value;

            ((CPageProperty)PropertyGrid_Attrubute.SelectedObject).ShowName = (string)e.OldValue;
            text = ((CPageProperty)PropertyGrid_Attrubute.SelectedObject).PageName;

            if (text2 == "")
            {
                MessageBox.Show("名称不能为空,请重新输入", "错误");
                userCommandControl21.theglobal.OldShapes.Clear();
                return;
            }
            foreach (DataFile df in CEditEnvironmentGlobal.dfs)
            {
                if (df.pageName == text2)
                {
                    MessageBox.Show("名称重复,请重新输入", "错误");
                    userCommandControl21.theglobal.OldShapes.Clear();
                    return;
                }
            }

            ((CPageProperty)PropertyGrid_Attrubute.SelectedObject).ShowName = text2;

            TreeNode[] array = treeView_工程导航.Nodes[0].Nodes[0].Nodes.Find(text, searchAllChildren: true);
            foreach (TreeNode treeNode in array)
            {
                treeNode.Text = e.ChangedItem.Value.ToString();
                if (treeNode.Tag is HmiPage hmiPage)
                {
                    hmiPage.Text = e.ChangedItem.Value.ToString();
                }
            }
            OpenPage(text);
        }
        catch
        {
            MessageBox.Show("设置页面名称出现异常！", "提示");
        }
    }

    public void PropertyScriptNameJudge(PropertyValueChangedEventArgs e)
    {
        try
        {
            if (!(e.ChangedItem.Label == "脚本名称"))
            {
                return;
            }
            string text = (string)e.ChangedItem.Value;

            ((CPageProperty)PropertyGrid_Attrubute.SelectedObject).PageName = (string)e.OldValue;

            List<string> list = CheckIOExists.PageInUse(CEditEnvironmentGlobal.childform.theglobal.df.name);
            if (list.Count != 0)
            {
                delPage delPage2 = new(list);
                if (delPage2.ShowDialog() != DialogResult.Yes)
                {
                    userCommandControl21.theglobal.OldShapes.Clear();
                    return;
                }
            }
            if (text == "")
            {
                MessageBox.Show("名称不能为空,请从新输入", "错误");
                userCommandControl21.theglobal.OldShapes.Clear();
                return;
            }
            foreach (DataFile df in CEditEnvironmentGlobal.dfs)
            {
                if (df.name == text)
                {
                    MessageBox.Show("名称重复,请从新输入", "错误");
                    userCommandControl21.theglobal.OldShapes.Clear();
                    return;
                }
            }
            var regex = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*$");
            if (!regex.IsMatch(text))
            {
                MessageBox.Show("变量名不合法,请从新输入", "错误");
                userCommandControl21.theglobal.OldShapes.Clear();
                return;
            }

            ((CPageProperty)PropertyGrid_Attrubute.SelectedObject).PageName = text;

            CEditEnvironmentGlobal.dhp.dirtyPageAdd(text);
            TreeNode[] array = treeView_工程导航.Nodes[0].Nodes[0].Nodes.Find(e.OldValue.ToString(), searchAllChildren: true);
            foreach (TreeNode treeNode in array)
            {
                treeNode.Name = e.ChangedItem.Value.ToString();
                if (treeNode.Tag is HmiPage hmiPage)
                {
                    hmiPage.Name = e.ChangedItem.Value.ToString();
                }
            }
        }
        catch
        {
            MessageBox.Show("脚本编辑名称出现异常！", "提示");
        }
    }

    public void PropertyShapeJudge()
    {
        try
        {
            var list = new List<CShape>();
            var list2 = new List<CShape>();
            if (PropertyGrid_Attrubute.SelectedObject is not CShape)
            {
                return;
            }
            foreach (CShape selectedShape in userCommandControl21.theglobal.SelectedShapeList)
            {
                foreach (CShape oldShape in userCommandControl21.theglobal.OldShapes)
                {
                    if (selectedShape.ShapeID == oldShape.ShapeID)
                    {
                        list.Add(selectedShape);
                        list2.Add(oldShape);
                    }
                }
                userCommandControl21.theglobal.ForUndo(list, list2);
            }
            userCommandControl21.theglobal.OldShapes.Clear();
        }
        catch
        {
            MessageBox.Show("属性图形判断出现异常！", "提示");
        }
    }

    private void InitPixieLibraryControl(DataSet ds)
    {
        DataRow[] array = ds.Tables["Category"].Select("", "Label");
        for (int i = 0; i < array.Length; i++)
        {
            var navBarGroup = new NavBarGroup();
            foreach (var group in navBarControl_精灵控件.Groups.Cast<NavBarGroup>())
            {
                if (group.Caption == (string)array[i]["Label"])
                {
                    navBarGroup = group;
                    break;
                }
            }
            if (navBarGroup == null)
            {
                continue;
            }
            //initForm.say("正在读取" + navBarGroup.Caption);
            if (navBarGroup.GroupStyle == NavBarGroupStyle.ControlContainer)
            {
                continue;
            }
            NavBarGroupControlContainer controlContainer = new();
            navBarGroup.ControlContainer = controlContainer;
            navBarGroup.GroupStyle = NavBarGroupStyle.ControlContainer;
            ListView listView = new()
            {
                Dock = DockStyle.Fill,
                Tag = (string)array[i]["Label"],
                LargeImageList = PixieLibraryControlImageList,
                Name = "listView_精灵控件"
            };
            listView.MouseDown += ListView_图元_MouseDown;
            navBarGroup.ControlContainer.Controls.Add(listView);
            DataRow[] array2 = ds.Tables["PixieControl"].Select("CategorySN='" + array[i]["SN"].ToString() + "'", "Label");
            for (int j = 0; j < array2.Length; j++)
            {
                string text = (string)array2[j]["ControlAssembly"];
                string text2 = (string)array2[j]["NeedFiles"];
                string text3 = (string)array2[j]["Type"];
                string text4 = (string)array2[j]["Label"];
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + text))
                {
                    try
                    {
                        var assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + text);
                        var type = assembly.GetType(text3);
                        object obj = Activator.CreateInstance(type);
                        var cPixieControl = obj as CPixieControl;
                        cPixieControl.AddPoint(PointF.Empty);
                        cPixieControl.Size = new SizeF(50f, 50f);
                        var image = new Bitmap(50, 50);
                        var g = Graphics.FromImage(image);
                        cPixieControl.DrawMe(g);
                        PixieLibraryControlImageList.Images.Add((string)array[i]["Label"] + "|" + text4, image);
                        ListViewItem listViewItem = new()
                        {
                            Name = text3 + "|" + (int)array2[j]["Width"] + "|" + (int)array2[j]["Height"] + "|" + AppDomain.CurrentDomain.BaseDirectory + text + "|" + text2.Replace(",", "|"),
                            Text = text4,
                            ImageKey = (string)array[i]["Label"] + "|" + text4
                        };
                        listView.Items.Add(listViewItem);
                    }
                    catch (Exception value)
                    {
                        Console.WriteLine(value);
                    }
                }
            }
        }
    }

    private void InitDCCEControlLibraryControl(DataSet ds)
    {
        DataRow[] array = ds.Tables["Category"].Select("", "Label");
        for (int i = 0; i < array.Length; i++)
        {
            NavBarGroup navBarGroup = new();
            foreach (var group in navBarControl_DCCE工控组件.Groups.Cast<NavBarGroup>())
            {
                if (group.Caption == (string)array[i]["Label"])
                {
                    navBarGroup = group;
                    break;
                }
            }
            if (navBarGroup == null)
            {
                continue;
            }
            //initForm.say("正在读取" + navBarGroup.Caption);
            if (navBarGroup.GroupStyle == NavBarGroupStyle.ControlContainer)
            {
                continue;
            }
            NavBarGroupControlContainer controlContainer = new();
            navBarGroup.ControlContainer = controlContainer;
            navBarGroup.GroupStyle = NavBarGroupStyle.ControlContainer;
            ListView listView = new()
            {
                Dock = DockStyle.Fill,
                Tag = (string)array[i]["Label"],
                LargeImageList = PixieLibraryControlImageList,
                Name = "listView_DCCE工控组件"
            };
            listView.MouseDown += ListView_图元_MouseDown;
            navBarGroup.ControlContainer.Controls.Add(listView);
            DataRow[] array2 = ds.Tables["Pixie"].Select("CategorySN='" + array[i]["SN"].ToString() + "'", "Label");
            for (int j = 0; j < array2.Length; j++)
            {
                string text = (string)array2[j]["ControlAssembly"];
                string text2 = (string)array2[j]["NeedFiles"];
                string text3 = (string)array2[j]["Type"];
                string text4 = (string)array2[j]["Label"];
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + text))
                {
                    continue;
                }
                try
                {
                    Assembly assembly = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + text);
                    Type type = assembly.GetType(text3);
                    if (type == null)
                    {
                        continue;
                    }
                    MethodInfo method = type.GetMethod("GetLogoStatic", BindingFlags.Static | BindingFlags.Public);
                    if (method != null)
                    {
                        object obj = method.Invoke(null, new object[0]);
                        if (obj is Image)
                        {
                            Image image = obj as Image;
                            if (image == null)
                            {
                                image = new PictureBox().ErrorImage;
                            }
                            double num = ((Convert.ToDouble(DCCEControlLibraryControlImageList.ImageSize.Width) / Convert.ToDouble(image.Width) < Convert.ToDouble(DCCEControlLibraryControlImageList.ImageSize.Height) / Convert.ToDouble(image.Height)) ? (Convert.ToDouble(DCCEControlLibraryControlImageList.ImageSize.Width) / Convert.ToDouble(image.Width)) : (Convert.ToDouble(DCCEControlLibraryControlImageList.ImageSize.Height) / Convert.ToDouble(image.Height)));
                            Bitmap bitmap = new(DCCEControlLibraryControlImageList.ImageSize.Width, DCCEControlLibraryControlImageList.ImageSize.Height);
                            Graphics graphics = Graphics.FromImage(bitmap);
                            graphics.DrawImage(image, Convert.ToSingle((double)bitmap.Width / 2.0 - (double)image.Width * num / 2.0), Convert.ToSingle((double)bitmap.Height / 2.0 - (double)image.Height * num / 2.0), Convert.ToSingle((double)image.Width * num), Convert.ToSingle((double)image.Height * num));
                            string text5 = array[i]["Label"].ToString();
                            DCCEControlLibraryControlImageList.Images.Add(text5 + "|" + text4, image);
                            listView.LargeImageList = DCCEControlLibraryControlImageList;
                        }
                    }
                    ListViewItem listViewItem = new()
                    {
                        Name = text3 + "|" + AppDomain.CurrentDomain.BaseDirectory + text + "|" + text2.Replace(",", "|"),
                        Text = text4,
                        ImageKey = array[i]["Label"].ToString() + "|" + text4
                    };
                    listView.Items.Add(listViewItem);
                }
                catch (Exception value)
                {
                    Console.WriteLine(value);
                }
            }
        }
    }

    private void ActiveXFinderButton_Click(object sender, EventArgs e)
    {
        try
        {
            ActiveXFinderForm activeXFinderForm = new();
            activeXFinderForm.ShowDialog();
            RefreshActiveXControl();
        }
        catch (Exception)
        {
            MessageBox.Show("由于ActiveXFinder组件未注册，无法获取系统中注册的ActiveX列表。");
        }
    }

    private void RefreshActiveXControl()
    {
        listView_ActiveX控件.Items.Clear();
        DirectoryInfo directoryInfo = new(AppDomain.CurrentDomain.BaseDirectory + "UserControl\\");
        DirectoryInfo[] directories = directoryInfo.GetDirectories();
        foreach (DirectoryInfo directoryInfo2 in directories)
        {
            FileInfo[] files = directoryInfo2.GetFiles("*.img");
            foreach (FileInfo fileInfo in files)
            {
                Image image = Image.FromFile(fileInfo.FullName);
                ActiveXControlImageList.Images.Add(directoryInfo2.Name, image);
            }
            FileInfo[] files2 = directoryInfo2.GetFiles("*.dll");
            foreach (FileInfo fileInfo2 in files2)
            {
                if (!(fileInfo2.Name.Substring(0, 2) == "Ax"))
                {
                    continue;
                }
                try
                {
                    Assembly assembly = Assembly.LoadFrom(fileInfo2.FullName);
                    Type[] types = assembly.GetTypes();
                    Type[] array = types;
                    foreach (Type type in array)
                    {
                        if (type.IsSubclassOf(typeof(Control)))
                        {
                            //initForm.say("正在读取" + type.Name);
                            ListViewItem listViewItem = new()
                            {
                                Name = type.FullName + "|" + fileInfo2.FullName + "|"
                            };
                            string text = "";
                            FileInfo[] files3 = directoryInfo2.GetFiles();
                            foreach (FileInfo fileInfo3 in files3)
                            {
                                text = text + fileInfo3.FullName + "|";
                            }
                            listViewItem.Name += text;
                            listViewItem.Text = type.Name;
                            listViewItem.ImageKey = directoryInfo2.Name;
                            listView_ActiveX控件.Items.Add(listViewItem);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    private void RotateShape(float ang)
    {
        if (CEditEnvironmentGlobal.childform.theglobal.SelectedShapeList.Count == 0)
        {
            return;
        }
        List<CShape> list = new();
        List<CShape> list2 = new();
        foreach (CShape selectedShape in CEditEnvironmentGlobal.childform.theglobal.SelectedShapeList)
        {
            list2.Add(selectedShape.Copy());
            selectedShape.Angel += ang;
            list.Add(selectedShape);
        }
        CEditEnvironmentGlobal.childform.theglobal.ForUndo(list, list2);
        CEditEnvironmentGlobal.childform.theglobal.uc2.RefreshGraphics();
    }

    private void AddWindowsControl(string type)
    {
        try
        {
            CControl cControl = new()
            {
                type = type,
                _dllfile = AppDomain.CurrentDomain.BaseDirectory + "ShapeRuntime.dll"
            };
            cControl.AddPoint(PointF.Empty);
            cControl.initLocationErr = true;
            userCommandControl21.theglobal.uc2.Controls.Add(cControl._c);
            cControl.initLocationErr = false;
            cControl._c.Enabled = false;
            UserCommandControl2.GiveName(cControl);
            CEditEnvironmentGlobal.childform.theglobal.g_ListAllShowCShape.Add(cControl);
            CEditEnvironmentGlobal.childform.theglobal.SelectedShapeList.Clear();
            CEditEnvironmentGlobal.childform.theglobal.SelectedShapeList.Add(cControl);
            objView_Page.OnFresh(cControl.ShapeID.ToString());
            CShape item = CEditEnvironmentGlobal.childform.theglobal.g_ListAllShowCShape[CEditEnvironmentGlobal.childform.theglobal.g_ListAllShowCShape.Count - 1].Copy();
            List<CShape> list = new()
            {
                item
            };
            userCommandControl21.theglobal.ForUndo(list, null);
            userCommandControl21.theglobal.str_IMDoingWhat = "Select";
            userCommandControl21.theglobal.uc2.RefreshGraphics();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public void Activeform(Form f)
    {
        _ = Thread.CurrentThread;
        ActivateMdiChild(f);
    }

    private void ListView2_DoubleClick(object sender, EventArgs e)
    {
        if (listView_事件.SelectedItems.Count != 0 && userCommandControl21.theglobal.SelectedShapeList.Count != 0)
        {
            CEditEnvironmentGlobal.scriptUnitForm.Init();
            CEditEnvironmentGlobal.scriptUnitForm.Show();
            CEditEnvironmentGlobal.scriptUnitForm.BringToFront();
            CEditEnvironmentGlobal.scriptUnitForm.SelectScript(string.Concat("工程相关>页面相关>", userCommandControl21.theglobal.df.pageName, ">", userCommandControl21.theglobal.SelectedShapeList[0], ">", listView_事件.SelectedItems[0].Text));
        }
    }

    private TreeNode AddPageGroupToTreeView(string key, string text, TreeNode parentTreeNode = null)
    {
        parentTreeNode ??= treeView_工程导航.Nodes[0].Nodes[0];
        TreeNode treeNode = parentTreeNode.Nodes.Add(key, text);
        string imageKey = (treeNode.SelectedImageKey = "PageGroupClosed.png");
        treeNode.ImageKey = imageKey;
        treeNode.ContextMenuStrip = contextMenuStrip_页面组;
        HmiPageGroup hmiPageGroup = new()
        {
            Name = treeNode.Name,
            Text = treeNode.Text,
            Parent = parentTreeNode.Tag as HmiPageGroup
        };
        HmiPageGroup hmiPageGroup2 = hmiPageGroup;
        hmiPageGroup2.Parent.Children.Add(hmiPageGroup2);
        treeNode.Tag = hmiPageGroup2;
        CEditEnvironmentGlobal.OutputMessage.Say("新建页面组\"" + treeNode.Text + "\"[" + treeNode.Name + "]@\"" + treeNode.Parent.Text + "\"成功.");
        return treeNode;
    }

    private TreeNode AddPageToTreeView(string key, string text, TreeNode parentTreeNode = null)
    {
        parentTreeNode ??= treeView_工程导航.Nodes[0].Nodes[0];
        TreeNode treeNode = parentTreeNode.Nodes.Add(key, text);
        string imageKey = (treeNode.SelectedImageKey = "Form.png");
        treeNode.ImageKey = imageKey;
        treeNode.ContextMenuStrip = contextMenuStrip_本地页面;
        HmiPage hmiPage = new()
        {
            Name = treeNode.Name,
            Text = treeNode.Text,
            Parent = parentTreeNode.Tag as HmiPageGroup
        };
        HmiPage hmiPage2 = hmiPage;
        hmiPage2.Parent.Children.Add(hmiPage2);
        treeNode.Tag = hmiPage2;
        return treeNode;
    }

    private TreeNode AddPage(DataFile df, TreeNode parentTreeNode = null)
    {
        TreeNode treeNode = AddPageToTreeView(df.name, df.pageName, parentTreeNode);
        (treeNode.Tag as HmiPage).DataFile = df;
        CEditEnvironmentGlobal.PageManager.AddPageToPageGroup(treeNode, parentTreeNode);
        CEditEnvironmentGlobal.OutputMessage.Say("新建页面\"" + treeNode.Text + "\"[" + treeNode.Name + "]@\"" + treeNode.Parent.Text + "\"成功.");
        return treeNode;
    }

    private void BuildPageGroupTreeView(HmiPageGroup group, TreeNode node = null)
    {
        if (group == null)
        {
            return;
        }
        node ??= treeView_工程导航.Nodes[0].Nodes[0];
        group.Children.ForEach(delegate (HmiAbstractPage item)
        {
            if (item is HmiPageGroup)
            {
                TreeNode node2 = AddPageGroupToTreeView(item.Name, item.Text, node);
                BuildPageGroupTreeView(item as HmiPageGroup, node2);
            }
            else if (item is HmiPage)
            {
                AddPageToTreeView(item.Name, item.Text, node);
            }
        });
    }

    private static void SetCPixieControlEvent(DataFile df)
    {
        foreach (CShape item in df.ListAllShowCShape)
        {
            if (item is CPixieControl)
            {
                ((CPixieControl)item).OnGetVarTable += CForDCCEControl.GetVarTableEvent;
                ((CPixieControl)item).ValidateVar += CForDCCEControl.ValidateVarEvent;
            }
        }
    }

    private void CreateNewPage()
    {
        TreeNode parentTreeNode = PageGroup_GetSelectedNode();
        ChildForm childForm = new()
        {
            MdiParent = this
        };
        childForm.Show();
        ActivateMdiChild(childForm);

        childForm.theglobal.pageProp.PageSize = new Size(800, 600);

        string text = "Page_" + childFormNumber++;
        bool flag;
        do
        {
            flag = false;
            foreach (DataFile df2 in CEditEnvironmentGlobal.dfs)
            {
                if (df2.name == childForm.Text || df2.pageName == text.Replace("Page_", "页面_"))
                {
                    flag = true;
                    text = "Page_" + childFormNumber++;
                }
            }
        }
        while (flag);

        childForm.theglobal.pageProp.PageName = text;
        childForm.theglobal.pageProp.ShowName = text.Replace("Page_", "页面_");

        DataFile df = childForm.theglobal.df;
        df.ListAllShowCShape = childForm.theglobal.g_ListAllShowCShape;

        df.color = childForm.theglobal.pageProp.PageColor;
        df.pageimage = childForm.theglobal.pageProp.PageImage.img;
        df.pageImageLayout = childForm.theglobal.pageProp.PageImageLayout;
        df.location = childForm.theglobal.pageProp.PageLocation;
        df.name = childForm.theglobal.pageProp.PageName;
        df.size = childForm.theglobal.pageProp.PageSize;
        df.visable = childForm.theglobal.pageProp.PageVisible;

        childForm.Text = df.pageName;
        objView_Page.OnClear();
        objView_Page.m_ObjGbl = childForm.theglobal;
        objView_Page.OnShow();
        TreeNode treeNode = AddPage(df, parentTreeNode);
        treeView_工程导航.SelectedNode = treeNode;
        treeNode.EnsureVisible();
        CEditEnvironmentGlobal.dhp.dirtyPageAdd(df.name);
    }

    private bool RenamePage(string pageName, string pageText)
    {
        if (string.IsNullOrEmpty(pageName))
            return false;

        TreeNode[] array = treeView_工程导航.Nodes[0].Nodes[0].Nodes.Find(pageName, searchAllChildren: true);
        foreach (TreeNode treeNode in array)
        {
            foreach (TreeNode node in treeNode.Parent.Nodes)
            {
                if (!node.Equals(treeNode) && node.Text.Equals(pageText))
                {
                    return false;
                }
            }
        }
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            if (!df.name.Equals(pageName) && df.pageName.Equals(pageText))
            {
                return false;
            }
        }
        foreach (DataFile df2 in CEditEnvironmentGlobal.dfs)
        {
            if (df2.name.Equals(pageName))
            {
                df2.pageName = pageText;
                return true;
            }
        }
        return false;
    }

    private void DeletePage(string pageName, bool showConfirm = true)
    {
        if (string.IsNullOrEmpty(pageName))
            return;

        if (showConfirm)
        {
            List<string> list = CheckIOExists.PageInUse(pageName);
            if (list.Count > 0)
            {
                delPage delPage2 = new(list);
                if (delPage2.ShowDialog() != DialogResult.Yes)
                {
                    return;
                }
            }
            else if (MessageBox.Show("您确认将要删除该页面吗?", "删除警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }
        }
        ClosePage(pageName);
        TreeNode[] array = treeView_工程导航.Nodes[0].Nodes[0].Nodes.Find(pageName, searchAllChildren: true);
        foreach (TreeNode treeNode in array)
        {
            CEditEnvironmentGlobal.dfs.RemoveAll((DataFile df) => df.name.Equals(pageName));
            if (treeNode.Tag is HmiPage)
            {
                HmiPage hmiPage = treeNode.Tag as HmiPage;
                hmiPage.Parent.Children.Remove(hmiPage);
            }
            treeNode.Remove();
            CEditEnvironmentGlobal.OutputMessage.Say("删除页面\"" + treeNode.Text + "\"[" + treeNode.Name + "]成功.");
        }
    }

    private void OpenPage(string pageName)
    {
        if (string.IsNullOrEmpty(pageName))
            return;

        Form[] mdiChildren = base.MdiChildren;
        foreach (Form form in mdiChildren)
        {
            if (form is ChildForm childForm)
            {
                if (pageName.Equals(childForm.theglobal.pageProp.PageName))
                {
                    ActivateMdiChild(childForm);
                    childForm.Text = CEditEnvironmentGlobal.PageManager.DFM.Find(pageName).pageName;
                    childForm.BringToFront();
                    CEditEnvironmentGlobal.childform = childForm;
                    return;
                }
            }
        }
        DataFile dataFile = CEditEnvironmentGlobal.PageManager.DFM.Find(pageName);
        if (dataFile == null)
            return;

        ChildForm childForm2 = new()
        {
            MdiParent = this,
            Text = dataFile.pageName
        };
        childForm2.Show();
        ActivateMdiChild(childForm2);
        CGlobal theglobal = childForm2.theglobal;
        theglobal.g_ListAllShowCShape.Clear();
        theglobal.SelectedShapeList.Clear();

        theglobal.pageProp.PageSize = dataFile.size;
        theglobal.pageProp.PageName = dataFile.name;
        theglobal.pageProp.PageLocation = dataFile.location;
        theglobal.pageProp.PageColor = dataFile.color;
        theglobal.pageProp.PageImage = new BitMapForIM
        {
            img = dataFile.pageimage,
            ImgGUID = dataFile.pageImageNamef
        };
        theglobal.pageProp.PageImageLayout = dataFile.pageImageLayout;
        theglobal.pageProp.PageVisible = dataFile.visable;

        theglobal.g_ListAllShowCShape = dataFile.ListAllShowCShape;
        theglobal.df = dataFile;
        objView_Page.OnClear();
        objView_Page.m_ObjGbl = childForm2.theglobal;
        objView_Page.OnShow();
        foreach (CShape item in childForm2.theglobal.g_ListAllShowCShape)
        {
            if (item.GetType() != typeof(CControl))
            {
                continue;
            }
            ((CControl)item).ReLifeMe();
            try
            {
                if (((CControl)item)._c is AxHost)
                {
                    ((ISupportInitialize)((CControl)item)._c).BeginInit();
                    ((CControl)item).initLocationErr = true;
                    childForm2.theglobal.uc2.Controls.Add(((CControl)item)._c);
                    ((CControl)item).initLocationErr = false;
                    ((CControl)item)._c.Enabled = false;
                    ((ISupportInitialize)((CControl)item)._c).EndInit();
                }
                else
                {
                    ((CControl)item).initLocationErr = true;
                    childForm2.theglobal.uc2.Controls.Add(((CControl)item)._c);
                    ((CControl)item).initLocationErr = false;
                    ((CControl)item)._c.Enabled = false;
                }
                ((CControl)item)._c.BringToFront();
                if (((CControl)item)._c is IDCCEControl)
                {
                    ((IDCCEControl)((CControl)item)._c).GetVarTableEvent += CForDCCEControl.GetVarTableEvent;
                }
                if (item is CPixieControl)
                {
                    ((CPixieControl)item).OnGetVarTable += CForDCCEControl.GetVarTableEvent;
                    ((CPixieControl)item).ValidateVar += CForDCCEControl.ValidateVarEvent;
                }
            }
            catch (Exception)
            {
                childForm2.theglobal.uc2.Controls.Remove(((CControl)item)._c);
                childForm2.theglobal.g_ListAllShowCShape.Remove(item);
                objView_Page.OnFresh(item.ShapeID.ToString());
            }
        }
        theglobal.uc2.RefreshGraphics();
        CEditEnvironmentGlobal.childform = childForm2;
    }

    private void ClosePage(string pageName)
    {
        if (string.IsNullOrEmpty(pageName))
            return;

        foreach (Form form in MdiChildren)
        {
            if (form is not ChildForm)
                continue;

            var childForm = form as ChildForm;
            if (pageName.Equals(childForm.theglobal.pageProp.PageName))
            {
                if (objView_Page.m_ObjGbl == childForm.theglobal)
                {
                    objView_Page.OnClear();
                }
                childForm.Close();
            }
        }
    }

    private void SavePage(string pageName)
    {
        if (!string.IsNullOrEmpty(pageName))
        {
            OpenPage(pageName);
            if (CEditEnvironmentGlobal.childform != null)
            {
                SaveOnePage(CEditEnvironmentGlobal.childform);
                CEditEnvironmentGlobal.dhp.dirtyPageAdd(CEditEnvironmentGlobal.childform.theglobal.df.name);
            }
        }
    }

    private void CopyPage(string pageName)
    {
        if (string.IsNullOrEmpty(pageName))
            return;

        var dataFile = CEditEnvironmentGlobal.PageManager.DFM.Find(pageName);
        if (null == dataFile)
            return;

        dataFile.PIO = CEditEnvironmentGlobal.dhp.ProjectIOs.ToArray();
        foreach (CShape item in dataFile.ListAllShowCShape)
        {
            if (item is CPixieControl)
            {
                ((CPixieControl)item).backupEvent();
                item.BeforeSaveMe();
            }
            else
            {
                item.BeforeSaveMe();
            }
        }
        if (string.IsNullOrEmpty(dataFile.pageName))
        {
            dataFile.pageName = dataFile.name;
        }
        dataFile.tls = dataFile.ListAllShowCShape.ToArray();
        using (MemoryStream memoryStream = new())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, dataFile);
            Clipboard.Clear();
            Clipboard.SetData("PageDATA", memoryStream.ToArray());
        }
        foreach (CShape item2 in dataFile.ListAllShowCShape)
        {
            item2.AfterSaveMe();
            if (item2 is CPixieControl)
            {
                ((CPixieControl)item2).resumeevent();
            }
        }
        dataFile.PIO = null;
    }

    private void PropertyPage(string pageName)
    {
        if (string.IsNullOrEmpty(pageName))
            return;

        OpenPage(pageName);
        if (CEditEnvironmentGlobal.childform != null)
        {
            PagePropertyForm pagePropertyForm = new();
            pagePropertyForm.ShowDialog();
        }
    }

    private void ImportPage(string fileName)
    {
        string currentDirectory = Environment.CurrentDirectory;
        try
        {
            var df = Operation.BinaryLoadFile(fileName);
        }
        catch
        {
            MessageBox.Show("导入页面失败，请检查文件。", "导入错误");
        }
        Environment.CurrentDirectory = currentDirectory;
    }

    private void PastePage()
    {
        DataFile df = null;
        try
        {
            byte[] buffer = Clipboard.GetData("PageDATA") as byte[];
            using MemoryStream serializationStream = new(buffer);
            IFormatter formatter = new BinaryFormatter();
            df = (DataFile)formatter.Deserialize(serializationStream);
        }
        catch (Exception)
        {
            MessageBox.Show("无法从剪切板获取页面数据", "粘贴失败");
        }

        if (df == null)
            return;

        while (CEditEnvironmentGlobal.dfs.Exists((DataFile item) => item.name.Equals(df.name)))
        {
            df.name += "_";
        }
        while (CEditEnvironmentGlobal.dfs.Exists((DataFile item) => item.pageName.Equals(df.pageName)))
        {
            df.pageName += "_";
        }
        df.ListAllShowCShape = new List<CShape>(df.tls);
        if (string.IsNullOrEmpty(df.pageName))
        {
            df.pageName = df.name;
        }
        df.sizef = df.size;
        df.locationf = df.location;
        foreach (CShape item in df.ListAllShowCShape)
        {
            try
            {
                item.AfterLoadMe();
                if (item is CControl control && control._c == null)
                {
                    df.ListAllShowCShape.Remove(item);
                }
                if (item.Layer > CShape.SumLayer)
                {
                    CShape.SumLayer = item.Layer;
                }
            }
            catch (Exception)
            {
                df.ListAllShowCShape.Remove(item);
            }
        }
        if (df.PIO != null)
        {
            ProjectIO[] pIO = df.PIO;
            foreach (ProjectIO pio in pIO)
            {
                List<ProjectIO> projectIOs = CEditEnvironmentGlobal.dhp.ProjectIOs;
                Predicate<ProjectIO> match = (ProjectIO item) => item.name.Equals(pio.name);
                if (!projectIOs.Exists(match))
                {
                    pio.ID = ++ProjectIO.StaticID;
                    pio.GroupName = CEditEnvironmentGlobal.dhp.ProjectIOTreeRoot.Text;
                    CEditEnvironmentGlobal.dhp.ProjectIOs.Add(pio);
                }
            }
            df.PIO = null;
        }
        TreeNode treeNode = AddPage(df, PageGroup_GetSelectedNode());
        OpenPage(treeNode.Name);
        treeView_工程导航.SelectedNode = treeNode;
    }

    private TreeNode PageGroup_GetSelectedNode()
    {
        TreeNode treeNode = treeView_工程导航.Nodes[0].Nodes[0];
        TreeNode treeNode2 = treeView_工程导航.SelectedNode ?? treeNode;
        if (treeNode2 != treeNode)
        {
            if (treeNode2.Tag == null)
            {
                treeNode2 = treeNode;
            }
            else if (treeNode2.Tag is not HmiPageGroup && treeNode2.Tag is HmiPage)
            {
                treeNode2 = treeNode2.Parent;
            }
        }
        return treeNode2;
    }

    private void NewPageGroup()
    {
        TreeNode parentTreeNode = PageGroup_GetSelectedNode();
        string text = "PageGroup_" + pageGroupNumber++;

        bool flag;
        do
        {
            flag = false;
            TreeNode[] array = treeView_工程导航.Nodes[0].Nodes[0].Nodes.Find(text, searchAllChildren: true);
            if (array != null && array.Length > 0)
            {
                flag = true;
                text = "PageGroup_" + pageGroupNumber++;
            }
        }
        while (flag);

        string text2 = text.Replace("PageGroup_", "页面组_");
        TreeNode treeNode = AddPageGroupToTreeView(text, text2, parentTreeNode);
        treeView_工程导航.SelectedNode = treeNode;
        treeNode.EnsureVisible();
    }

    private bool 页面组_重命名(string pageGroupName, string pageGroupText)
    {
        TreeNode[] array = treeView_工程导航.Nodes[0].Nodes[0].Nodes.Find(pageGroupName, searchAllChildren: true);
        foreach (TreeNode treeNode in array)
        {
            foreach (TreeNode node in treeNode.Parent.Nodes)
            {
                if (!node.Equals(treeNode) && node.Text.Equals(pageGroupText))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void 页面组_删除(string pageGroupName, bool showConfim = true)
    {
        if (string.IsNullOrEmpty(pageGroupName) || (showConfim && MessageBox.Show("您确认将要删除该页面组吗?\n该组包含的所有页面和页面组将会被全部删除，请慎重选择!", "删除警告", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes))
            return;

        TreeNode[] array = treeView_工程导航.Nodes[0].Nodes[0].Nodes.Find(pageGroupName, searchAllChildren: true);
        foreach (TreeNode treeNode in array)
        {
            for (TreeNode firstNode = treeNode.FirstNode; firstNode != null; firstNode = treeNode.FirstNode)
            {
                if (firstNode.Tag is HmiPageGroup)
                {
                    页面组_删除(firstNode.Name, showConfim: false);
                }
                else if (firstNode.Tag is HmiPage)
                {
                    DeletePage(firstNode.Name, showConfirm: false);
                }
            }
            if (treeNode.Tag is HmiPageGroup)
            {
                HmiPageGroup hmiPageGroup = treeNode.Tag as HmiPageGroup;
                hmiPageGroup.Parent.Children.Remove(hmiPageGroup);
            }
            treeNode.Remove();
            CEditEnvironmentGlobal.OutputMessage.Say("删除页面组\"" + treeNode.Text + "\"[" + treeNode.Name + "]成功.");
        }
    }

    private void 页面组_关闭所有(string pageGroupName = null)
    {
        if (string.IsNullOrEmpty(pageGroupName))
        {
            Form[] mdiChildren = base.MdiChildren;
            foreach (Form form in mdiChildren)
            {
                if (form is ChildForm)
                {
                    form.Close();
                }
            }
            objView_Page.OnClear();
            return;
        }
        TreeNode[] array = treeView_工程导航.Nodes[0].Nodes[0].Nodes.Find(pageGroupName, searchAllChildren: true);
        foreach (TreeNode treeNode in array)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (node.Tag is HmiPageGroup)
                {
                    页面组_关闭所有(node.Name);
                }
                else if (node.Tag is HmiPage)
                {
                    ClosePage(node.Name);
                }
            }
        }
    }

    private void 页面树节点_上移(TreeNode srcNode, TreeNode dstNode)
    {
        int index = dstNode.Index;
        HmiAbstractPage item = srcNode.Tag as HmiAbstractPage;
        HmiPageGroup hmiPageGroup = dstNode.Parent.Tag as HmiPageGroup;
        hmiPageGroup.Children.Remove(item);
        hmiPageGroup.Children.Insert(index, item);
        dstNode.Parent.Nodes.Remove(srcNode);
        dstNode.Parent.Nodes.Insert(index, srcNode);
        treeView_工程导航.SelectedNode = srcNode;
    }

    private void 页面树节点_下移(TreeNode srcNode, TreeNode dstNode)
    {
        int index = dstNode.Index;
        HmiAbstractPage item = srcNode.Tag as HmiAbstractPage;
        HmiPageGroup hmiPageGroup = dstNode.Parent.Tag as HmiPageGroup;
        hmiPageGroup.Children.Remove(item);
        hmiPageGroup.Children.Insert(index, item);
        dstNode.Parent.Nodes.Remove(srcNode);
        dstNode.Parent.Nodes.Insert(index, srcNode);
        treeView_工程导航.SelectedNode = srcNode;
    }

    private void 页面树节点_移至顶层(TreeNode srcNode, TreeNode dstNode)
    {
        HmiAbstractPage item = srcNode.Tag as HmiAbstractPage;
        HmiPageGroup hmiPageGroup = dstNode.Parent.Tag as HmiPageGroup;
        hmiPageGroup.Children.Remove(item);
        hmiPageGroup.Children.Insert(0, item);
        dstNode.Parent.Nodes.Remove(srcNode);
        dstNode.Parent.Nodes.Insert(0, srcNode);
        treeView_工程导航.SelectedNode = srcNode;
    }

    private void 页面树节点_移至底层(TreeNode srcNode, TreeNode dstNode)
    {
        HmiAbstractPage item = srcNode.Tag as HmiAbstractPage;
        HmiPageGroup hmiPageGroup = dstNode.Parent.Tag as HmiPageGroup;
        hmiPageGroup.Children.Remove(item);
        hmiPageGroup.Children.Add(item);
        dstNode.Parent.Nodes.Remove(srcNode);
        dstNode.Parent.Nodes.Add(srcNode);
        treeView_工程导航.SelectedNode = srcNode;
    }

    private void EnableAllControl(bool enabled)
    {
        foreach (Control control in base.Controls)
        {
            control.Enabled = enabled;
        }
    }

    private void MDIParent1_Load(object sender, EventArgs e)
    {
        LogUtil.Init(); // 初始化日志控件

        CEditEnvironmentGlobal.mdiparent = this;

        InitFormLook();
        LoadBarLayout();
        EnableAllControl(false);
        barDockControlTop.Enabled = true;

        projectPathSave = new ProjectPathSaveHandler();
        var test = projectPathSave.FileList;
        projectPathSave.RecentProjectItems = BarListItem_RecentlyProjece;//指定最近文件的菜单值，方便动态创建文件菜单
        projectPathSave.UpdateMenu();

        if (null != projectPathSave.RecentProjectItems)
        {
            projectPathSave.RecentProjectItems.ListItemClick += RecentProjectItem_Click;
        }
    }

    void RecentProjectItem_Click(object sender, ListItemClickEventArgs e)
    {
        //点击最近打开菜单项要执行的动作。
        var path = (e.Item as BarListItem).Strings[e.Index];
        if (File.Exists(path))
        {
            OpenProject(path);
        }
        else
        {
            MessageBox.Show("工程不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void EnableControls()
    {
        barButtonItem_CreatePage.Enabled = true;
        barButtonItem_OpenPage.Enabled = true;
        barButtonItem_SavePage.Enabled = true;
        barButtonItem_SaveProject.Enabled = true;
        barButtonItem_Undo.Enabled = true;
        barButtonItem_Redo.Enabled = true;
        barButtonItem_Cut.Enabled = true;
        barButtonItem_Paste.Enabled = true;
        barButtonItem_Copy.Enabled = true;
        barButtonItem_Delete.Enabled = true;
        BarButtonItem_Debug.Enabled = true;
        barButtonItem_FindText.Enabled = true;

        toolbarButtonItem_基本控件.Enabled = true;
        toolbarButtonItem_DCCE工控组件.Enabled = true;
        toolbarButtonItem_精灵控件.Enabled = true;
        toolbarButtonItem_ActiveX控件.Enabled = true;

        barButtonItem_DrawLine.Enabled = true;
        barButtonItem_CurveLine.Enabled = true;
        barButtonItem_DrawEllipse.Enabled = true;
        barButtonItem_Rectangle.Enabled = true;
        barButtonItem_Polygon.Enabled = true;
        barButtonItem_RoundedRectangle.Enabled = true;
        barButtonItem_String.Enabled = true;
        barButtonItem_Picture.Enabled = true;
        barButtonItem_ElementTop.Enabled = true;
        barButtonItem_ElementBottom.Enabled = true;
        barButtonItem_ElementCombine.Enabled = true;
        barButtonItem_ElementSeparate.Enabled = true;

        barButtonItem_HorizontalEquidistance.Enabled = true;
        barButtonItem_VerticalEquidistance.Enabled = true;
        barButtonItem_TopAlign.Enabled = true;
        barButtonItem_VerticalAlign.Enabled = true;
        barButtonItem_BottomAlign.Enabled = true;
        barButtonItem_LeftAlign.Enabled = true;
        barButtonItem_HorizontalAlign.Enabled = true;
        barButtonItem_RightAlign.Enabled = true;

        barButtonItem_EqualWidth.Enabled = true;
        barButtonItem_EqualHeight.Enabled = true;
        barButtonItem_EqualAndOpposite.Enabled = true;
        barButtonItem_HorizontalRotate.Enabled = true;
        barButtonItem_VerticalRotate.Enabled = true;
        barButtonItem_RightRotate.Enabled = true;
        barButtonItem_LeftRotate.Enabled = true;
        barCheckItem_LockElements.Enabled = true;

        barButtonItem_新建页面.Enabled = true;
        barButtonItem_打开页面.Enabled = true;
        barButtonItem_导入页面.Enabled = true;
        barButtonItem_保存页面.Enabled = true;
        barButtonItem_保存工程.Enabled = true;
        BarButtonItem_ProjectProperty.Enabled = true;

        barSubItem_主菜单_窗口.Enabled = true;
        barSubItem_主菜单_工具.Enabled = true;
        barSubItem_主菜单_脚本.Enabled = true;
        barSubItem_主菜单_操作.Enabled = true;
        barSubItem_主菜单_视图.Enabled = true;
        barSubItem_主菜单_编辑.Enabled = true;
    }

    private void BarButtonItem_NewProject_ItemClick(object sender, ItemClickEventArgs e)
    {
        var newProject = new NewProject();
        var result = newProject.ShowDialog();
        if (DialogResult.OK != result)
            return;

        OpenProject(newProject.ProjeceFilePath);
    }

    private void BarButtonItem_OpenProject_ItemClick(object sender, ItemClickEventArgs e)
    {
        openFileDialog_OpenProject.Filter = $"工程文件|*{ConstantHelper.ProjectSuffixName}";
        openFileDialog_OpenProject.InitialDirectory = PathHelper.GetDefaultProjectPath();
        if (DialogResult.OK != openFileDialog_OpenProject.ShowDialog())
            return;

        OpenProject(openFileDialog_OpenProject.FileName);
    }

    private void OpenProject(string projeceFilePath)
    {
        EnableAllControl(true);
        EnableControls();
        InitPictureAndVar();
        InitScript();
        InitDockAnimation();
        InitDockerLayout();
        InitNavigation();
        InitLibraryControl();
        InitEvent();
        InitTheglobal();
        InitAnimationConnect();
        InitVarBrowserShow();
        projectPathSave.AddRecentFile(projeceFilePath);
        LoadProject(projeceFilePath);
    }

    private void LoadProject(string projeceFilePath)
    {
        try
        {
            var fileInfo = new FileInfo(projeceFilePath);
            if (CEditEnvironmentGlobal.ProjectPath == fileInfo.DirectoryName)
                return;

            if (!string.IsNullOrEmpty(CEditEnvironmentGlobal.ProjectPath))
            {
                CEditEnvironmentGlobal.dfs.Clear();
                CEditEnvironmentGlobal.dhp = new HMIProjectFile();
                CEditEnvironmentGlobal.ProjectHPFFilePath = "";
                页面组_关闭所有();
            }

            CEditEnvironmentGlobal.ProjectPath = fileInfo.DirectoryName;
            CEditEnvironmentGlobal.HMIPath = fileInfo.DirectoryName + "\\hmi";
            CEditEnvironmentGlobal.ProjectHPFFilePath = CEditEnvironmentGlobal.HMIPath + "\\HMI.dhp";
            DHMIImageManage.projectpath = CEditEnvironmentGlobal.HMIPath;
            fileSystemWatcher1.Path = fileInfo.DirectoryName;

            CEditEnvironmentGlobal.dhp.ProjectName = Path.GetFileNameWithoutExtension(fileInfo.Name);
            CEditEnvironmentGlobal.dhp.CreatTime = DateTime.Now;

            CEditEnvironmentGlobal.OutputMessage.Say("开始载入工程.");

            LoadHMI();

            CheckIOExists.IOTableOld = true;
            CEditEnvironmentGlobal.OutputMessage.Say("成功载入工程.");
        }
        catch (Exception ex)
        {
            CEditEnvironmentGlobal.OutputMessage.Say("加载工程失败.");
            CEditEnvironmentGlobal.OutputMessage.Say("原因:" + ex.Message);
        }
    }

    private void LoadHMI()
    {
        Directory.CreateDirectory(CEditEnvironmentGlobal.HMIPath);
        
        EnableAllControl(true);
        childFormNumber = (pageGroupNumber = 0);

        treeView_工程导航.Nodes[0].Nodes.RemoveByKey("Page");

        treeView_工程导航.Nodes[0].Text = "本地工程(" + CEditEnvironmentGlobal.dhp.ProjectName + ")";
        TreeNode treeNode2 = treeView_工程导航.Nodes[0].Nodes.Insert(0, "Page", $"{ConstantHelper.SoftwareName}页面", "NativeApp.png", "NativeApp.png");
        treeNode2.ContextMenuStrip = contextMenuStrip_本地页面根节点;
        treeNode2.Tag = new HmiPageGroup
        {
            Name = treeNode2.Name,
            Text = treeNode2.Text,
            Parent = null
        };

        if (File.Exists(CEditEnvironmentGlobal.ProjectHPFFilePath))
        {
            CEditEnvironmentGlobal.dhp = Operation.BinaryLoadProject(CEditEnvironmentGlobal.ProjectHPFFilePath);
            HmiPageGroup hmiPageGroup = Operation.LoadProjectGroups(CEditEnvironmentGlobal.ProjectHPFFilePath);
            BuildPageGroupTreeView(hmiPageGroup);
            foreach (string value in CEditEnvironmentGlobal.dhp.pages.Values)
            {
                try
                {
                    CEditEnvironmentGlobal.OutputMessage.Say("加载页面" + value + ".");
                    DataFile dataFile = Operation.BinaryLoadFile(value);
                    SetCPixieControlEvent(dataFile);
                    CEditEnvironmentGlobal.dfs.Add(dataFile);
                    if (hmiPageGroup == null)
                    {
                        AddPageToTreeView(dataFile.name, dataFile.pageName);
                    }
                    CEditEnvironmentGlobal.OutputMessage.Say("加载页面" + value + "成功.");
                    Refresh();
                }
                catch (Exception ex)
                {
                    CEditEnvironmentGlobal.OutputMessage.Say("加载页面" + value + "失败.");
                    CEditEnvironmentGlobal.OutputMessage.Say("失败原因:" + ex.ToString());
                    LogUtil.Error("失败原因:" + ex);
                }
            }
            if (CEditEnvironmentGlobal.dhp.dirtyPage == null)
            {
                CEditEnvironmentGlobal.dhp.dirtyPage = new List<string>();
                foreach (DataFile df in CEditEnvironmentGlobal.dfs)
                {
                    CEditEnvironmentGlobal.dhp.dirtyPageAdd(df.name);
                }
            }
            treeView_工程导航.ExpandAll();
        }

        if (0 == CEditEnvironmentGlobal.dfs.Count)
        {
            CreateNewPage();
        }
    }


    private void SaveBarLayout()
    {
        barManager1.SaveLayoutToXml(AppDomain.CurrentDomain.BaseDirectory + DockConfigFileName);
    }

    private void SaveOnePage(ChildForm form)
    {
        if (form == null)
        {
            CEditEnvironmentGlobal.OutputMessage.Say("请选择页面再保存！");
            return;
        }
        CEditEnvironmentGlobal.childform = base.ActiveMdiChild as ChildForm;
        string text = CEditEnvironmentGlobal.HMIPath + "\\" + CEditEnvironmentGlobal.childform.theglobal.df.name + ".hpg";
        try
        {
            Operation.BinarySaveFile(text, CEditEnvironmentGlobal.childform.theglobal.df);
            CEditEnvironmentGlobal.OutputMessage.Say("成功保存到" + text);
        }
        catch(Exception ex)
        {
            CEditEnvironmentGlobal.OutputMessage.Say("保存到" + text + "失败!");
            LogUtil.Error("保存到页面" + text + "失败! 失败原因：" + ex);
        }
    }

    private void BarButtonItem_ProjectProperty_ItemClick(object sender, ItemClickEventArgs e)
    {
        var form = new ProjectProperty(CEditEnvironmentGlobal.ProjectPath);
        form.ShowDialog();
    }
}

