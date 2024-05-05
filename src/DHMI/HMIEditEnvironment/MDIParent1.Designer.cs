using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraNavBar;
using System.IO;
using System.Windows.Forms;

namespace HMIEditEnvironment
{
    partial class MDIParent1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("FView页面");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("设备管理");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("设备变量");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("内部变量");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("数据库连接");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("报警服务器");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("数据库管理", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("用户管理");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("工程设置");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("发布", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode9});
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("工程", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode7,
            treeNode10});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIParent1));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("按钮", "Button.png");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("多选框", "CheckBox.png");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("标签", "Label.png");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("文本框", "TextBox.png");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("下拉列表", "ListBox.png");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("日历控件", "Calendar.png");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("图片框", "Image.png");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("分组框", "StockFrame.png");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("数据视图", "GridView.png");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("列表框", "ListView.png");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("定时器", "Timer.png");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("直线", "line.png");
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("椭圆", "ell.png");
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("矩形", "rect.png");
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("圆角矩形", "circleRect.png");
            System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem("贝塞尔曲线", "bezier.png");
            System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem("三角形", "san.png");
            System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem("文字", "str.png");
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.myPropertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.listView_事件 = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.treeView_工程导航 = new System.Windows.Forms.TreeView();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.contextMenuStrip_本地页面 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_本地页面_打开 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_本地页面_关闭 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_本地页面_保存 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_本地页面_删除 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_本地页面_上移 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_本地页面_下移 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_本地页面_移至顶层 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_本地页面_移至底层 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_本地页面_复制 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_本地页面_导出 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator29 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_本地页面_属性 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.panel_横向标尺 = new System.Windows.Forms.Panel();
            this.panel_纵向标尺 = new System.Windows.Forms.Panel();
            this.contextMenuStrip_本地页面根节点 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_本地根节点_新建页面组 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_本地根节点_新建页面 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_本地根节点_导入页面 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_本地根节点_粘贴页面 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator34 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_本地根节点_关闭所有 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton27 = new System.Windows.Forms.ToolStripButton();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.toolbar_标准 = new DevExpress.XtraBars.Bar();
            this.barButtonItem_CreatePage = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_OpenPage = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_SavePage = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_SaveProject = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Undo = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Redo = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Cut = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Copy = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Paste = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Delete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_FindText = new DevExpress.XtraBars.BarButtonItem();
            this.BarButtonItem_Debug = new DevExpress.XtraBars.BarButtonItem();
            this.menubar_菜单栏 = new DevExpress.XtraBars.Bar();
            this.barSubItem_主菜单_文件 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem_NewProject = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_OpenProject = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_新建页面 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_打开页面 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_导入页面 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_保存页面 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_保存工程 = new DevExpress.XtraBars.BarButtonItem();
            this.BarButtonItem_ProjectProperty = new DevExpress.XtraBars.BarButtonItem();
            this.BarSubItem_RecentlyProject = new DevExpress.XtraBars.BarSubItem();
            this.BarListItem_RecentlyProjece = new DevExpress.XtraBars.BarListItem();
            this.barButtonItem_退出 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem_主菜单_编辑 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem11 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem13 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem14 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem15 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem17 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem16 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem18 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem19 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem_主菜单_视图 = new DevExpress.XtraBars.BarSubItem();
            this.menubarCheckItem_工具栏 = new DevExpress.XtraBars.BarCheckItem();
            this.menubarCheckItem_状态栏 = new DevExpress.XtraBars.BarCheckItem();
            this.menubarCheckItem_导航栏 = new DevExpress.XtraBars.BarCheckItem();
            this.menubarCheckItem_输出栏 = new DevExpress.XtraBars.BarCheckItem();
            this.menubarCheckItem_对象浏览器 = new DevExpress.XtraBars.BarCheckItem();
            this.menubarCheckItem_变量浏览器 = new DevExpress.XtraBars.BarCheckItem();
            this.menubarCheckItem_属性 = new DevExpress.XtraBars.BarCheckItem();
            this.menubarCheckItem_动画 = new DevExpress.XtraBars.BarCheckItem();
            this.menubarSubItem_资源库 = new DevExpress.XtraBars.BarSubItem();
            this.menubarCheckItem_基本控件 = new DevExpress.XtraBars.BarCheckItem();
            this.menubarCheckItem_DCCE工控组件 = new DevExpress.XtraBars.BarCheckItem();
            this.menubarCheckItem_精灵控件 = new DevExpress.XtraBars.BarCheckItem();
            this.menubarCheckItem_ActiveX控件 = new DevExpress.XtraBars.BarCheckItem();
            this.barSubItem_主菜单_操作 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem23 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem24 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem25 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem26 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem27 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem28 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem12 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem48 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem52 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem50 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem51 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem49 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem53 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem13 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem55 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem56 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem54 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem29 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem30 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem9 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem12 = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckItem10 = new DevExpress.XtraBars.BarCheckItem();
            this.barSubItem_主菜单_脚本 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem137 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem_主菜单_工具 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem62 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem_主菜单_窗口 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem37 = new DevExpress.XtraBars.BarButtonItem();
            this.barMdiChildrenListItem1 = new DevExpress.XtraBars.BarMdiChildrenListItem();
            this.barSubItem_主菜单_帮助 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem_about = new DevExpress.XtraBars.BarButtonItem();
            this.statusbar_状态栏 = new DevExpress.XtraBars.Bar();
            this.BarStaticItem_Status = new DevExpress.XtraBars.BarStaticItem();
            this.toolbar_资源 = new DevExpress.XtraBars.Bar();
            this.toolbarButtonItem_基本控件 = new DevExpress.XtraBars.BarButtonItem();
            this.toolbarButtonItem_DCCE工控组件 = new DevExpress.XtraBars.BarButtonItem();
            this.toolbarButtonItem_精灵控件 = new DevExpress.XtraBars.BarButtonItem();
            this.toolbarButtonItem_ActiveX控件 = new DevExpress.XtraBars.BarButtonItem();
            this.toolbar_操作 = new DevExpress.XtraBars.Bar();
            this.barButtonItem_ElementTop = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_ElementBottom = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_ElementCombine = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_ElementSeparate = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_HorizontalEquidistance = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_VerticalEquidistance = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_TopAlign = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_VerticalAlign = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_BottomAlign = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_LeftAlign = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_HorizontalAlign = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_RightAlign = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_EqualWidth = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_EqualHeight = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_EqualAndOpposite = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_HorizontalRotate = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_VerticalRotate = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_RightRotate = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_LeftRotate = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckItem_LockElements = new DevExpress.XtraBars.BarCheckItem();
            this.toolbar_绘制基本图元 = new DevExpress.XtraBars.Bar();
            this.barButtonItem_DrawLine = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_CurveLine = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_DrawEllipse = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Rectangle = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Polygon = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_RoundedRectangle = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_String = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Picture = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.hideContainerRight = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.dockPanel_基本控件 = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer_基本控件 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.xtraScrollableControl_基本控件 = new DevExpress.XtraEditors.XtraScrollableControl();
            this.navBarControl_基本控件 = new DevExpress.XtraNavBar.NavBarControl();
            this.Windows控件 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupControlContainer_Windows控件 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.listView_Windows控件 = new System.Windows.Forms.ListView();
            this.imageList_Windows控件 = new System.Windows.Forms.ImageList(this.components);
            this.navBarGroupControlContainer_基本图元 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.listView_基本图元 = new System.Windows.Forms.ListView();
            this.imageList_图库_基本图元 = new System.Windows.Forms.ImageList(this.components);
            this.基本图元 = new DevExpress.XtraNavBar.NavBarGroup();
            this.dockPanel_DCCE工控组件 = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer_DCCE工控组件 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.navBarControl_DCCE工控组件 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup_DCCE开关 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_DCCE报警灯 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_变量操作 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_DCCE媒体控件 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_文件操作 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_图形操作 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_数控 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_数据管理 = new DevExpress.XtraNavBar.NavBarGroup();
            this.dockPanel_精灵控件 = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer_精灵控件 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.navBarControl_精灵控件 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup_仪表 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_开关 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_报警灯 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_按钮 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_时钟 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_泵 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_游标 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_电机 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_管道 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_罐 = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroup_阀门 = new DevExpress.XtraNavBar.NavBarGroup();
            this.dockPanel_ActiveX控件 = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer_ActiveX控件 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ActiveXFinderButton = new System.Windows.Forms.Button();
            this.listView_ActiveX控件 = new System.Windows.Forms.ListView();
            this.dockPanelContainer_LeftBottom = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel_属性 = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer_属性 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanel_事件 = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer_事件 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanel_动画 = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer_动画 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel_导航栏 = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer_导航栏 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanel_对象浏览器 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanelContainer_对象浏览器 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.objView_Page = new HMIEditEnvironment.ObjExplorer();
            this.dockPanel_变量浏览器 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.varExplore = new HMIEditEnvironment.VarExplore();
            this.dockPanel_输出栏 = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer_输出栏 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.barSubItem6 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem_导入 = new DevExpress.XtraBars.BarSubItem();
            this.barCheckItem2 = new DevExpress.XtraBars.BarCheckItem();
            this.barCheckItem9 = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItem32 = new DevExpress.XtraBars.BarButtonItem();
            this.barSubItem14 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem15 = new DevExpress.XtraBars.BarSubItem();
            this.barSubItem16 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem34 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem35 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem38 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem39 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem40 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem41 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem63 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem64 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem65 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem66 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem67 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem68 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem69 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem70 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem71 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem72 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem73 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem74 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem75 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem76 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem77 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem78 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem79 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem80 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem81 = new DevExpress.XtraBars.BarButtonItem();
            this.barCheckItem13 = new DevExpress.XtraBars.BarCheckItem();
            this.menubarCheckItem_事件 = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItem119 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem120 = new DevExpress.XtraBars.BarButtonItem();
            this.menubarCheckItem_默认主题 = new DevExpress.XtraBars.BarCheckItem();
            this.barButtonItem130 = new DevExpress.XtraBars.BarButtonItem();
            this.colorEditControl1 = new HMIEditEnvironment.ColorEditControl();
            this.barEditItem4 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemColorEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.barStaticItem3 = new DevExpress.XtraBars.BarStaticItem();
            this.barEditItem3 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemColorEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemColorEdit();
            this.barButtonItem131 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem132 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem133 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem134 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem135 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem136 = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem5 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.barButtonItem139 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem58 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem59 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem140 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem36 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_CloseProject = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem20 = new DevExpress.XtraBars.BarButtonItem();
            this.BarButtonItem_RecentlyProject = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemZoomTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.repositoryItemComboBox2 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupMenu2 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.ToolStripMenuItem_页面组_新建页面组 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_页面组_重命名 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_页面组_删除 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_页面组_新建页面 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_页面组_导入页面 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_页面组_粘贴页面 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_页面组_上移 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_页面组_下移 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_页面组_移至顶层 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_页面组_移至底层 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_页面组_关闭所有 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_页面组 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFileDialog_OpenProject = new System.Windows.Forms.OpenFileDialog();
            this.ImageCollection_ProjectNavigation = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip_本地页面.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.contextMenuStrip_本地页面根节点.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.hideContainerRight.SuspendLayout();
            this.dockPanel_基本控件.SuspendLayout();
            this.controlContainer_基本控件.SuspendLayout();
            this.xtraScrollableControl_基本控件.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl_基本控件)).BeginInit();
            this.navBarControl_基本控件.SuspendLayout();
            this.navBarGroupControlContainer_Windows控件.SuspendLayout();
            this.navBarGroupControlContainer_基本图元.SuspendLayout();
            this.dockPanel_DCCE工控组件.SuspendLayout();
            this.controlContainer_DCCE工控组件.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl_DCCE工控组件)).BeginInit();
            this.dockPanel_精灵控件.SuspendLayout();
            this.controlContainer_精灵控件.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl_精灵控件)).BeginInit();
            this.dockPanel_ActiveX控件.SuspendLayout();
            this.controlContainer_ActiveX控件.SuspendLayout();
            this.dockPanelContainer_LeftBottom.SuspendLayout();
            this.dockPanel_属性.SuspendLayout();
            this.controlContainer_属性.SuspendLayout();
            this.dockPanel_事件.SuspendLayout();
            this.controlContainer_事件.SuspendLayout();
            this.dockPanel_动画.SuspendLayout();
            this.controlContainer_动画.SuspendLayout();
            this.panelContainer1.SuspendLayout();
            this.dockPanel_导航栏.SuspendLayout();
            this.controlContainer_导航栏.SuspendLayout();
            this.dockPanel_对象浏览器.SuspendLayout();
            this.dockPanelContainer_对象浏览器.SuspendLayout();
            this.dockPanel_变量浏览器.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            this.dockPanel_输出栏.SuspendLayout();
            this.controlContainer_输出栏.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorEditControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).BeginInit();
            this.contextMenuStrip_页面组.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImageCollection_ProjectNavigation)).BeginInit();
            this.SuspendLayout();
            // 
            // myPropertyGrid1
            // 
            this.myPropertyGrid1.BackColor = System.Drawing.SystemColors.Control;
            this.myPropertyGrid1.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.myPropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myPropertyGrid1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.myPropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.myPropertyGrid1.Name = "myPropertyGrid1";
            this.myPropertyGrid1.Size = new System.Drawing.Size(198, 587);
            this.myPropertyGrid1.TabIndex = 0;
            this.myPropertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.myPropertyGrid1_PropertyValueChanged);
            this.myPropertyGrid1.Enter += new System.EventHandler(this.myPropertyGrid1_Enter);
            // 
            // listView_事件
            // 
            this.listView_事件.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.listView_事件.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_事件.HideSelection = false;
            this.listView_事件.Location = new System.Drawing.Point(0, 0);
            this.listView_事件.Name = "listView_事件";
            this.listView_事件.Size = new System.Drawing.Size(198, 587);
            this.listView_事件.TabIndex = 7;
            this.listView_事件.UseCompatibleStateImageBehavior = false;
            this.listView_事件.View = System.Windows.Forms.View.Details;
            this.listView_事件.DoubleClick += new System.EventHandler(this.listView2_DoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "事件";
            this.columnHeader2.Width = 500;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.Size = new System.Drawing.Size(198, 587);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "动画类型";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 93;
            // 
            // Column2
            // 
            this.Column2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.Column2.DropDownWidth = 100;
            this.Column2.HeaderText = "使能";
            this.Column2.MaxDropDownItems = 2;
            this.Column2.Name = "Column2";
            this.Column2.Visible = false;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "使能";
            this.Column3.Name = "Column3";
            // 
            // treeView_工程导航
            // 
            this.treeView_工程导航.AllowDrop = true;
            this.treeView_工程导航.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView_工程导航.HideSelection = false;
            this.treeView_工程导航.Location = new System.Drawing.Point(0, 0);
            this.treeView_工程导航.Name = "treeView_工程导航";
            treeNode1.ImageKey = "(默认值)";
            treeNode1.Name = "Page";
            treeNode1.SelectedImageKey = "NativeApp.png";
            treeNode1.Text = "FView页面";
            treeNode2.Name = "DeviceManager";
            treeNode2.Text = "设备管理";
            treeNode3.ImageKey = "Generic_Device.png";
            treeNode3.Name = "TagManager";
            treeNode3.SelectedImageKey = "Generic_Device.png";
            treeNode3.Text = "设备变量";
            treeNode4.ImageKey = "InnerVars.png";
            treeNode4.Name = "IO";
            treeNode4.SelectedImageKey = "InnerVars.png";
            treeNode4.Text = "内部变量";
            treeNode5.ImageKey = "DatabaseCopyDatabaseFile.png";
            treeNode5.Name = "DBConn";
            treeNode5.SelectedImageKey = "DatabaseCopyDatabaseFile.png";
            treeNode5.Text = "数据库连接";
            treeNode6.ImageIndex = 9;
            treeNode6.Name = "Alarm_CrossPlatform";
            treeNode6.SelectedImageKey = "DatabaseCopyDatabaseFile.png";
            treeNode6.Text = "报警服务器";
            treeNode7.ImageKey = "DatabaseCopyDatabaseFile.png";
            treeNode7.Name = "DB";
            treeNode7.SelectedImageKey = "DatabaseCopyDatabaseFile.png";
            treeNode7.Text = "数据库管理";
            treeNode8.ImageKey = "Settings.png";
            treeNode8.Name = "Authority";
            treeNode8.SelectedImageKey = "Settings.png";
            treeNode8.Text = "用户管理";
            treeNode9.ImageKey = "ActiveXRadioButton.png";
            treeNode9.Name = "Setting";
            treeNode9.SelectedImageKey = "ActiveXRadioButton.png";
            treeNode9.Text = "工程设置";
            treeNode10.ImageKey = "autoplay.ico";
            treeNode10.Name = "Publish";
            treeNode10.SelectedImageKey = "autoplay.ico";
            treeNode10.Text = "发布";
            treeNode11.ImageKey = "(默认值)";
            treeNode11.Name = "Project";
            treeNode11.SelectedImageKey = "(默认值)";
            treeNode11.Text = "工程";
            this.treeView_工程导航.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode11});
            this.treeView_工程导航.Size = new System.Drawing.Size(227, 587);
            this.treeView_工程导航.TabIndex = 11;
            this.treeView_工程导航.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_工程导航_AfterLabelEdit);
            this.treeView_工程导航.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_工程导航_BeforeCollapse);
            this.treeView_工程导航.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView_工程导航_AfterCollapse);
            this.treeView_工程导航.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView_工程导航_BeforeExpand);
            this.treeView_工程导航.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView_工程导航_AfterExpand);
            this.treeView_工程导航.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_工程导航_ItemDrag);
            this.treeView_工程导航.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_工程导航_AfterSelect);
            this.treeView_工程导航.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_工程导航_NodeMouseDoubleClick);
            this.treeView_工程导航.DragDrop += new System.Windows.Forms.DragEventHandler(this.TreeView_工程导航_DragDrop);
            this.treeView_工程导航.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_工程导航_DragOver);
            this.treeView_工程导航.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView_工程导航_KeyDown);
            this.treeView_工程导航.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeView_工程导航_MouseDown);
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(149, 6);
            // 
            // contextMenuStrip_本地页面
            // 
            this.contextMenuStrip_本地页面.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_本地页面_打开,
            this.ToolStripMenuItem_本地页面_关闭,
            this.ToolStripMenuItem_本地页面_保存,
            this.toolStripSeparator7,
            this.ToolStripMenuItem_本地页面_删除,
            this.toolStripSeparator1,
            this.ToolStripMenuItem_本地页面_上移,
            this.ToolStripMenuItem_本地页面_下移,
            this.toolStripMenuItem_本地页面_移至顶层,
            this.toolStripMenuItem_本地页面_移至底层,
            this.toolStripSeparator3,
            this.ToolStripMenuItem_本地页面_复制,
            this.ToolStripMenuItem_本地页面_导出,
            this.toolStripSeparator29,
            this.ToolStripMenuItem_本地页面_属性});
            this.contextMenuStrip_本地页面.Name = "contextMenuStrip_本地页面";
            this.contextMenuStrip_本地页面.Size = new System.Drawing.Size(178, 270);
            // 
            // ToolStripMenuItem_本地页面_打开
            // 
            this.ToolStripMenuItem_本地页面_打开.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_本地页面_打开.Image")));
            this.ToolStripMenuItem_本地页面_打开.Name = "ToolStripMenuItem_本地页面_打开";
            this.ToolStripMenuItem_本地页面_打开.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItem_本地页面_打开.Text = "打开(&O)";
            this.ToolStripMenuItem_本地页面_打开.Click += new System.EventHandler(this.ToolStripMenuItem_页面_打开_Click);
            // 
            // ToolStripMenuItem_本地页面_关闭
            // 
            this.ToolStripMenuItem_本地页面_关闭.Name = "ToolStripMenuItem_本地页面_关闭";
            this.ToolStripMenuItem_本地页面_关闭.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItem_本地页面_关闭.Text = "关闭(&C)";
            this.ToolStripMenuItem_本地页面_关闭.Click += new System.EventHandler(this.ToolStripMenuItem_页面_关闭_Click);
            // 
            // ToolStripMenuItem_本地页面_保存
            // 
            this.ToolStripMenuItem_本地页面_保存.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_本地页面_保存.Image")));
            this.ToolStripMenuItem_本地页面_保存.Name = "ToolStripMenuItem_本地页面_保存";
            this.ToolStripMenuItem_本地页面_保存.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItem_本地页面_保存.Text = "保存(&S)";
            this.ToolStripMenuItem_本地页面_保存.Click += new System.EventHandler(this.ToolStripMenuItem_页面_保存_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(174, 6);
            // 
            // ToolStripMenuItem_本地页面_删除
            // 
            this.ToolStripMenuItem_本地页面_删除.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_本地页面_删除.Image")));
            this.ToolStripMenuItem_本地页面_删除.Name = "ToolStripMenuItem_本地页面_删除";
            this.ToolStripMenuItem_本地页面_删除.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ToolStripMenuItem_本地页面_删除.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItem_本地页面_删除.Text = "删除(&D)";
            this.ToolStripMenuItem_本地页面_删除.Click += new System.EventHandler(this.ToolStripMenuItem_页面_删除_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(174, 6);
            // 
            // ToolStripMenuItem_本地页面_上移
            // 
            this.ToolStripMenuItem_本地页面_上移.Name = "ToolStripMenuItem_本地页面_上移";
            this.ToolStripMenuItem_本地页面_上移.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItem_本地页面_上移.Text = "上移(&U)";
            this.ToolStripMenuItem_本地页面_上移.Click += new System.EventHandler(this.ToolStripMenuItem_页面_上移_Click);
            // 
            // ToolStripMenuItem_本地页面_下移
            // 
            this.ToolStripMenuItem_本地页面_下移.Name = "ToolStripMenuItem_本地页面_下移";
            this.ToolStripMenuItem_本地页面_下移.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItem_本地页面_下移.Text = "下移(&D)";
            this.ToolStripMenuItem_本地页面_下移.Click += new System.EventHandler(this.ToolStripMenuItem_页面_下移_Click);
            // 
            // toolStripMenuItem_本地页面_移至顶层
            // 
            this.toolStripMenuItem_本地页面_移至顶层.Name = "toolStripMenuItem_本地页面_移至顶层";
            this.toolStripMenuItem_本地页面_移至顶层.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItem_本地页面_移至顶层.Text = "移至顶层(&T)";
            this.toolStripMenuItem_本地页面_移至顶层.Click += new System.EventHandler(this.ToolStripMenuItem_页面_移至顶层_Click);
            // 
            // toolStripMenuItem_本地页面_移至底层
            // 
            this.toolStripMenuItem_本地页面_移至底层.Name = "toolStripMenuItem_本地页面_移至底层";
            this.toolStripMenuItem_本地页面_移至底层.Size = new System.Drawing.Size(177, 22);
            this.toolStripMenuItem_本地页面_移至底层.Text = "移至底层(&B)";
            this.toolStripMenuItem_本地页面_移至底层.Click += new System.EventHandler(this.ToolStripMenuItem_页面_移至底层_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(174, 6);
            // 
            // ToolStripMenuItem_本地页面_复制
            // 
            this.ToolStripMenuItem_本地页面_复制.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_本地页面_复制.Image")));
            this.ToolStripMenuItem_本地页面_复制.Name = "ToolStripMenuItem_本地页面_复制";
            this.ToolStripMenuItem_本地页面_复制.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItem_本地页面_复制.Text = "复制(&C)";
            this.ToolStripMenuItem_本地页面_复制.Click += new System.EventHandler(this.ToolStripMenuItem_页面_复制_Click);
            // 
            // ToolStripMenuItem_本地页面_导出
            // 
            this.ToolStripMenuItem_本地页面_导出.Name = "ToolStripMenuItem_本地页面_导出";
            this.ToolStripMenuItem_本地页面_导出.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItem_本地页面_导出.Text = "导出(&E)";
            this.ToolStripMenuItem_本地页面_导出.Click += new System.EventHandler(this.ToolStripMenuItem_页面_导出_Click);
            // 
            // toolStripSeparator29
            // 
            this.toolStripSeparator29.Name = "toolStripSeparator29";
            this.toolStripSeparator29.Size = new System.Drawing.Size(174, 6);
            // 
            // ToolStripMenuItem_本地页面_属性
            // 
            this.ToolStripMenuItem_本地页面_属性.Name = "ToolStripMenuItem_本地页面_属性";
            this.ToolStripMenuItem_本地页面_属性.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Return)));
            this.ToolStripMenuItem_本地页面_属性.Size = new System.Drawing.Size(177, 22);
            this.ToolStripMenuItem_本地页面_属性.Text = "属性(&P)";
            this.ToolStripMenuItem_本地页面_属性.Click += new System.EventHandler(this.ToolStripMenuItem_页面_属性_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(177, 6);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            // 
            // panel_横向标尺
            // 
            this.panel_横向标尺.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_横向标尺.Location = new System.Drawing.Point(234, 75);
            this.panel_横向标尺.Name = "panel_横向标尺";
            this.panel_横向标尺.Size = new System.Drawing.Size(803, 27);
            this.panel_横向标尺.TabIndex = 25;
            this.panel_横向标尺.Visible = false;
            this.panel_横向标尺.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_横向标尺_Paint);
            // 
            // panel_纵向标尺
            // 
            this.panel_纵向标尺.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_纵向标尺.Location = new System.Drawing.Point(234, 102);
            this.panel_纵向标尺.Name = "panel_纵向标尺";
            this.panel_纵向标尺.Size = new System.Drawing.Size(27, 506);
            this.panel_纵向标尺.TabIndex = 26;
            this.panel_纵向标尺.Visible = false;
            this.panel_纵向标尺.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_纵向标尺_Paint);
            // 
            // contextMenuStrip_本地页面根节点
            // 
            this.contextMenuStrip_本地页面根节点.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_本地根节点_新建页面组,
            this.toolStripSeparator9,
            this.ToolStripMenuItem_本地根节点_新建页面,
            this.ToolStripMenuItem_本地根节点_导入页面,
            this.ToolStripMenuItem_本地根节点_粘贴页面,
            this.toolStripSeparator34,
            this.ToolStripMenuItem_本地根节点_关闭所有});
            this.contextMenuStrip_本地页面根节点.Name = "contextMenuStrip_本地页面根节点";
            this.contextMenuStrip_本地页面根节点.Size = new System.Drawing.Size(154, 126);
            // 
            // ToolStripMenuItem_本地根节点_新建页面组
            // 
            this.ToolStripMenuItem_本地根节点_新建页面组.Name = "ToolStripMenuItem_本地根节点_新建页面组";
            this.ToolStripMenuItem_本地根节点_新建页面组.Size = new System.Drawing.Size(153, 22);
            this.ToolStripMenuItem_本地根节点_新建页面组.Text = "新建页面组(&G)";
            this.ToolStripMenuItem_本地根节点_新建页面组.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_新建页面组_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(150, 6);
            // 
            // ToolStripMenuItem_本地根节点_新建页面
            // 
            this.ToolStripMenuItem_本地根节点_新建页面.Name = "ToolStripMenuItem_本地根节点_新建页面";
            this.ToolStripMenuItem_本地根节点_新建页面.Size = new System.Drawing.Size(153, 22);
            this.ToolStripMenuItem_本地根节点_新建页面.Text = "新建页面(&P)";
            this.ToolStripMenuItem_本地根节点_新建页面.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_新建页面_Click);
            // 
            // ToolStripMenuItem_本地根节点_导入页面
            // 
            this.ToolStripMenuItem_本地根节点_导入页面.Name = "ToolStripMenuItem_本地根节点_导入页面";
            this.ToolStripMenuItem_本地根节点_导入页面.Size = new System.Drawing.Size(153, 22);
            this.ToolStripMenuItem_本地根节点_导入页面.Text = "导入页面(&I)";
            this.ToolStripMenuItem_本地根节点_导入页面.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_导入页面_Click);
            // 
            // ToolStripMenuItem_本地根节点_粘贴页面
            // 
            this.ToolStripMenuItem_本地根节点_粘贴页面.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_本地根节点_粘贴页面.Image")));
            this.ToolStripMenuItem_本地根节点_粘贴页面.Name = "ToolStripMenuItem_本地根节点_粘贴页面";
            this.ToolStripMenuItem_本地根节点_粘贴页面.Size = new System.Drawing.Size(153, 22);
            this.ToolStripMenuItem_本地根节点_粘贴页面.Text = "粘贴页面(&P)";
            this.ToolStripMenuItem_本地根节点_粘贴页面.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_粘贴页面_Click);
            // 
            // toolStripSeparator34
            // 
            this.toolStripSeparator34.Name = "toolStripSeparator34";
            this.toolStripSeparator34.Size = new System.Drawing.Size(150, 6);
            // 
            // ToolStripMenuItem_本地根节点_关闭所有
            // 
            this.ToolStripMenuItem_本地根节点_关闭所有.Name = "ToolStripMenuItem_本地根节点_关闭所有";
            this.ToolStripMenuItem_本地根节点_关闭所有.Size = new System.Drawing.Size(153, 22);
            this.ToolStripMenuItem_本地根节点_关闭所有.Text = "关闭所有(&C)";
            this.ToolStripMenuItem_本地根节点_关闭所有.Click += new System.EventHandler(this.ToolStripMenuItem_页面根_关闭所有_Click);
            // 
            // toolStripButton27
            // 
            this.toolStripButton27.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton27.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton27.Name = "toolStripButton27";
            this.toolStripButton27.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton27.Text = "toolStripButton27";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(797, 80);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // barManager1
            // 
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.toolbar_标准,
            this.menubar_菜单栏,
            this.statusbar_状态栏,
            this.toolbar_资源,
            this.toolbar_操作,
            this.toolbar_绘制基本图元});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.DockManager = this.dockManager1;
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.BarStaticItem_Status,
            this.barSubItem_主菜单_文件,
            this.barSubItem_主菜单_编辑,
            this.barSubItem_主菜单_视图,
            this.barSubItem_主菜单_操作,
            this.barSubItem_主菜单_脚本,
            this.barSubItem6,
            this.barSubItem_主菜单_窗口,
            this.barButtonItem_新建页面,
            this.barButtonItem_打开页面,
            this.barSubItem_导入,
            this.barButtonItem_保存工程,
            this.barButtonItem_退出,
            this.barButtonItem10,
            this.barButtonItem11,
            this.barButtonItem13,
            this.barButtonItem14,
            this.barButtonItem15,
            this.barButtonItem16,
            this.barButtonItem17,
            this.barButtonItem18,
            this.barButtonItem19,
            this.menubarCheckItem_导航栏,
            this.barCheckItem2,
            this.menubarCheckItem_属性,
            this.menubarCheckItem_工具栏,
            this.menubarCheckItem_状态栏,
            this.menubarCheckItem_输出栏,
            this.barCheckItem9,
            this.barButtonItem23,
            this.barButtonItem24,
            this.barButtonItem25,
            this.barButtonItem26,
            this.barButtonItem27,
            this.barButtonItem28,
            this.barSubItem12,
            this.barSubItem13,
            this.barButtonItem29,
            this.barButtonItem30,
            this.barButtonItem32,
            this.barSubItem14,
            this.barSubItem15,
            this.barSubItem16,
            this.barButtonItem34,
            this.barButtonItem35,
            this.barButtonItem37,
            this.barMdiChildrenListItem1,
            this.barButtonItem38,
            this.barButtonItem39,
            this.barButtonItem40,
            this.barButtonItem41,
            this.barButtonItem_导入页面,
            this.barButtonItem48,
            this.barButtonItem49,
            this.barButtonItem50,
            this.barButtonItem51,
            this.barButtonItem52,
            this.barButtonItem53,
            this.barButtonItem54,
            this.barButtonItem55,
            this.barButtonItem56,
            this.barCheckItem10,
            this.barButtonItem63,
            this.barButtonItem64,
            this.barButtonItem65,
            this.barButtonItem66,
            this.barButtonItem67,
            this.barButtonItem68,
            this.barButtonItem69,
            this.barButtonItem70,
            this.barButtonItem71,
            this.barButtonItem72,
            this.barButtonItem73,
            this.barButtonItem74,
            this.barButtonItem75,
            this.barButtonItem76,
            this.barButtonItem77,
            this.barButtonItem78,
            this.barButtonItem79,
            this.barButtonItem80,
            this.barButtonItem81,
            this.barButtonItem_CreatePage,
            this.barButtonItem_OpenPage,
            this.barButtonItem_SaveProject,
            this.barButtonItem_Copy,
            this.barButtonItem_Cut,
            this.barButtonItem_Paste,
            this.barButtonItem_Delete,
            this.barButtonItem_Undo,
            this.barButtonItem_Redo,
            this.BarButtonItem_Debug,
            this.toolbarButtonItem_基本控件,
            this.toolbarButtonItem_DCCE工控组件,
            this.toolbarButtonItem_ActiveX控件,
            this.toolbarButtonItem_精灵控件,
            this.barCheckItem13,
            this.barButtonItem_ElementCombine,
            this.barButtonItem_ElementSeparate,
            this.barButtonItem_ElementTop,
            this.barButtonItem_ElementBottom,
            this.barButtonItem_HorizontalRotate,
            this.barButtonItem_VerticalRotate,
            this.barButtonItem_TopAlign,
            this.barButtonItem_HorizontalAlign,
            this.barButtonItem_BottomAlign,
            this.barButtonItem_LeftAlign,
            this.barButtonItem_VerticalAlign,
            this.barButtonItem_RightAlign,
            this.barButtonItem_EqualAndOpposite,
            this.barButtonItem_EqualWidth,
            this.barButtonItem_EqualHeight,
            this.barButtonItem_RightRotate,
            this.barButtonItem_LeftRotate,
            this.barCheckItem_LockElements,
            this.barButtonItem_HorizontalEquidistance,
            this.barButtonItem_VerticalEquidistance,
            this.menubarCheckItem_事件,
            this.barButtonItem119,
            this.barButtonItem120,
            this.menubarCheckItem_动画,
            this.menubarCheckItem_默认主题,
            this.barButtonItem130,
            this.barEditItem4,
            this.barStaticItem3,
            this.barEditItem3,
            this.barButtonItem131,
            this.menubarSubItem_资源库,
            this.barButtonItem132,
            this.barButtonItem133,
            this.barButtonItem134,
            this.barButtonItem135,
            this.barButtonItem136,
            this.menubarCheckItem_基本控件,
            this.menubarCheckItem_DCCE工控组件,
            this.menubarCheckItem_ActiveX控件,
            this.menubarCheckItem_精灵控件,
            this.barEditItem5,
            this.barButtonItem137,
            this.barButtonItem139,
            this.barButtonItem58,
            this.barButtonItem59,
            this.barButtonItem_SavePage,
            this.barSubItem_主菜单_工具,
            this.barButtonItem62,
            this.barButtonItem140,
            this.barButtonItem_DrawLine,
            this.barButtonItem_CurveLine,
            this.barButtonItem_DrawEllipse,
            this.barButtonItem_Rectangle,
            this.barButtonItem_Polygon,
            this.barButtonItem_RoundedRectangle,
            this.barButtonItem_Picture,
            this.barButtonItem_String,
            this.menubarCheckItem_对象浏览器,
            this.barButtonItem9,
            this.barButtonItem12,
            this.barSubItem1,
            this.barSubItem_主菜单_帮助,
            this.barButtonItem36,
            this.barButtonItem_about,
            this.barButtonItem_FindText,
            this.barButtonItem_保存页面,
            this.menubarCheckItem_变量浏览器,
            this.barButtonItem_NewProject,
            this.barButtonItem_OpenProject,
            this.barButtonItem_CloseProject,
            this.barButtonItem20,
            this.BarButtonItem_ProjectProperty,
            this.BarButtonItem_RecentlyProject,
            this.BarSubItem_RecentlyProject,
            this.BarListItem_RecentlyProjece});
            this.barManager1.MainMenu = this.menubar_菜单栏;
            this.barManager1.MaxItemId = 292;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemZoomTrackBar1,
            this.repositoryItemComboBox2,
            this.repositoryItemColorEdit1,
            this.repositoryItemColorEdit2,
            this.repositoryItemSpinEdit1});
            this.barManager1.StatusBar = this.statusbar_状态栏;
            // 
            // toolbar_标准
            // 
            this.toolbar_标准.BarName = "标准";
            this.toolbar_标准.DockCol = 0;
            this.toolbar_标准.DockRow = 1;
            this.toolbar_标准.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.toolbar_标准.FloatLocation = new System.Drawing.Point(1642, 128);
            this.toolbar_标准.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem_CreatePage, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem_OpenPage, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem_SavePage, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem_SaveProject, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Undo, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Redo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Cut, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Copy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Paste),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Delete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_FindText, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.BarButtonItem_Debug, true)});
            this.toolbar_标准.OptionsBar.AllowQuickCustomization = false;
            this.toolbar_标准.Text = "标准";
            // 
            // barButtonItem_CreatePage
            // 
            this.barButtonItem_CreatePage.Caption = "新建";
            this.barButtonItem_CreatePage.Enabled = false;
            this.barButtonItem_CreatePage.Hint = "新建";
            this.barButtonItem_CreatePage.Id = 116;
            this.barButtonItem_CreatePage.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_CreatePage.ImageOptions.Image")));
            this.barButtonItem_CreatePage.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
            this.barButtonItem_CreatePage.Name = "barButtonItem_CreatePage";
            this.barButtonItem_CreatePage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ToolStripMenuItem_页面组_新建页面_Click);
            // 
            // barButtonItem_OpenPage
            // 
            this.barButtonItem_OpenPage.Caption = "打开";
            this.barButtonItem_OpenPage.Enabled = false;
            this.barButtonItem_OpenPage.Hint = "打开";
            this.barButtonItem_OpenPage.Id = 117;
            this.barButtonItem_OpenPage.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_OpenPage.ImageOptions.Image")));
            this.barButtonItem_OpenPage.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O));
            this.barButtonItem_OpenPage.Name = "barButtonItem_OpenPage";
            this.barButtonItem_OpenPage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_打开页面_ItemClick);
            // 
            // barButtonItem_SavePage
            // 
            this.barButtonItem_SavePage.Caption = "保存页面";
            this.barButtonItem_SavePage.Enabled = false;
            this.barButtonItem_SavePage.Hint = "保存页面";
            this.barButtonItem_SavePage.Id = 235;
            this.barButtonItem_SavePage.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_SavePage.ImageOptions.Image")));
            this.barButtonItem_SavePage.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_SavePage.ImageOptions.LargeImage")));
            this.barButtonItem_SavePage.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.barButtonItem_SavePage.Name = "barButtonItem_SavePage";
            this.barButtonItem_SavePage.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_保存页面_ItemClick);
            // 
            // barButtonItem_SaveProject
            // 
            this.barButtonItem_SaveProject.Caption = "保存工程";
            this.barButtonItem_SaveProject.Enabled = false;
            this.barButtonItem_SaveProject.Hint = "保存工程";
            this.barButtonItem_SaveProject.Id = 118;
            this.barButtonItem_SaveProject.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_SaveProject.ImageOptions.Image")));
            this.barButtonItem_SaveProject.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_SaveProject.ImageOptions.LargeImage")));
            this.barButtonItem_SaveProject.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.S));
            this.barButtonItem_SaveProject.Name = "barButtonItem_SaveProject";
            this.barButtonItem_SaveProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_保存工程_ItemClick);
            // 
            // barButtonItem_Undo
            // 
            this.barButtonItem_Undo.Caption = "撤销";
            this.barButtonItem_Undo.Enabled = false;
            this.barButtonItem_Undo.Hint = "撤销";
            this.barButtonItem_Undo.Id = 123;
            this.barButtonItem_Undo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Undo.ImageOptions.Image")));
            this.barButtonItem_Undo.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Undo.ImageOptions.LargeImage")));
            this.barButtonItem_Undo.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z));
            this.barButtonItem_Undo.Name = "barButtonItem_Undo";
            this.barButtonItem_Undo.ShortcutKeyDisplayString = "Ctrl+Z";
            this.barButtonItem_Undo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem89_ItemClick);
            // 
            // barButtonItem_Redo
            // 
            this.barButtonItem_Redo.Caption = "重复";
            this.barButtonItem_Redo.Enabled = false;
            this.barButtonItem_Redo.Hint = "重复";
            this.barButtonItem_Redo.Id = 124;
            this.barButtonItem_Redo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Redo.ImageOptions.Image")));
            this.barButtonItem_Redo.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Redo.ImageOptions.LargeImage")));
            this.barButtonItem_Redo.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y));
            this.barButtonItem_Redo.Name = "barButtonItem_Redo";
            this.barButtonItem_Redo.ShortcutKeyDisplayString = "Ctrl+Y";
            this.barButtonItem_Redo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem90_ItemClick);
            // 
            // barButtonItem_Cut
            // 
            this.barButtonItem_Cut.Caption = "剪切";
            this.barButtonItem_Cut.Enabled = false;
            this.barButtonItem_Cut.Hint = "剪切";
            this.barButtonItem_Cut.Id = 120;
            this.barButtonItem_Cut.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Cut.ImageOptions.Image")));
            this.barButtonItem_Cut.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Cut.ImageOptions.LargeImage")));
            this.barButtonItem_Cut.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X));
            this.barButtonItem_Cut.Name = "barButtonItem_Cut";
            this.barButtonItem_Cut.ShortcutKeyDisplayString = "Ctrl+X";
            this.barButtonItem_Cut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem86_ItemClick);
            // 
            // barButtonItem_Copy
            // 
            this.barButtonItem_Copy.Caption = "复制";
            this.barButtonItem_Copy.Enabled = false;
            this.barButtonItem_Copy.Hint = "复制";
            this.barButtonItem_Copy.Id = 119;
            this.barButtonItem_Copy.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Copy.ImageOptions.Image")));
            this.barButtonItem_Copy.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Copy.ImageOptions.LargeImage")));
            this.barButtonItem_Copy.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C));
            this.barButtonItem_Copy.Name = "barButtonItem_Copy";
            this.barButtonItem_Copy.ShortcutKeyDisplayString = "Ctrl+C";
            this.barButtonItem_Copy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem85_ItemClick);
            // 
            // barButtonItem_Paste
            // 
            this.barButtonItem_Paste.Caption = "粘贴";
            this.barButtonItem_Paste.Enabled = false;
            this.barButtonItem_Paste.Hint = "粘贴";
            this.barButtonItem_Paste.Id = 121;
            this.barButtonItem_Paste.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Paste.ImageOptions.Image")));
            this.barButtonItem_Paste.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Paste.ImageOptions.LargeImage")));
            this.barButtonItem_Paste.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V));
            this.barButtonItem_Paste.Name = "barButtonItem_Paste";
            this.barButtonItem_Paste.ShortcutKeyDisplayString = "Ctrl+V";
            this.barButtonItem_Paste.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem87_ItemClick);
            // 
            // barButtonItem_Delete
            // 
            this.barButtonItem_Delete.Caption = "删除";
            this.barButtonItem_Delete.Enabled = false;
            this.barButtonItem_Delete.Hint = "删除";
            this.barButtonItem_Delete.Id = 122;
            this.barButtonItem_Delete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Delete.ImageOptions.Image")));
            this.barButtonItem_Delete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Delete.ImageOptions.LargeImage")));
            this.barButtonItem_Delete.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Delete);
            this.barButtonItem_Delete.Name = "barButtonItem_Delete";
            this.barButtonItem_Delete.ShortcutKeyDisplayString = "Delete";
            this.barButtonItem_Delete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem88_ItemClick);
            // 
            // barButtonItem_FindText
            // 
            this.barButtonItem_FindText.Caption = "文本查找";
            this.barButtonItem_FindText.Enabled = false;
            this.barButtonItem_FindText.Hint = "在页面中查找文本";
            this.barButtonItem_FindText.Id = 270;
            this.barButtonItem_FindText.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F));
            this.barButtonItem_FindText.Name = "barButtonItem_FindText";
            this.barButtonItem_FindText.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem18_ItemClick);
            // 
            // BarButtonItem_Debug
            // 
            this.BarButtonItem_Debug.Caption = "调试";
            this.BarButtonItem_Debug.Enabled = false;
            this.BarButtonItem_Debug.Hint = "启动调试当前工程";
            this.BarButtonItem_Debug.Id = 125;
            this.BarButtonItem_Debug.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BarButtonItem_Debug.ImageOptions.Image")));
            this.BarButtonItem_Debug.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BarButtonItem_Debug.ImageOptions.LargeImage")));
            this.BarButtonItem_Debug.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.F5);
            this.BarButtonItem_Debug.Name = "BarButtonItem_Debug";
            this.BarButtonItem_Debug.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem91_ItemClick);
            // 
            // menubar_菜单栏
            // 
            this.menubar_菜单栏.BarName = "菜单栏";
            this.menubar_菜单栏.DockCol = 0;
            this.menubar_菜单栏.DockRow = 0;
            this.menubar_菜单栏.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.menubar_菜单栏.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem_主菜单_文件),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem_主菜单_编辑),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem_主菜单_视图),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem_主菜单_操作),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem_主菜单_脚本),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem_主菜单_工具),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem_主菜单_窗口),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem_主菜单_帮助)});
            this.menubar_菜单栏.OptionsBar.AllowQuickCustomization = false;
            this.menubar_菜单栏.OptionsBar.DrawDragBorder = false;
            this.menubar_菜单栏.OptionsBar.MultiLine = true;
            this.menubar_菜单栏.OptionsBar.UseWholeRow = true;
            this.menubar_菜单栏.Text = "菜单栏";
            // 
            // barSubItem_主菜单_文件
            // 
            this.barSubItem_主菜单_文件.Caption = "文件(&F)";
            this.barSubItem_主菜单_文件.Id = 2;
            this.barSubItem_主菜单_文件.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_NewProject, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_OpenProject),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_新建页面, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_打开页面),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_导入页面),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_保存页面, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_保存工程),
            new DevExpress.XtraBars.LinkPersistInfo(this.BarButtonItem_ProjectProperty, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.BarSubItem_RecentlyProject),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_退出, true)});
            this.barSubItem_主菜单_文件.Name = "barSubItem_主菜单_文件";
            // 
            // barButtonItem_NewProject
            // 
            this.barButtonItem_NewProject.Caption = "新建工程";
            this.barButtonItem_NewProject.Id = 282;
            this.barButtonItem_NewProject.Name = "barButtonItem_NewProject";
            this.barButtonItem_NewProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_NewProject_ItemClick);
            // 
            // barButtonItem_OpenProject
            // 
            this.barButtonItem_OpenProject.Caption = "打开工程";
            this.barButtonItem_OpenProject.Id = 283;
            this.barButtonItem_OpenProject.Name = "barButtonItem_OpenProject";
            this.barButtonItem_OpenProject.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_OpenProject_ItemClick);
            // 
            // barButtonItem_新建页面
            // 
            this.barButtonItem_新建页面.Caption = "新建页面(&N)";
            this.barButtonItem_新建页面.Enabled = false;
            this.barButtonItem_新建页面.Id = 13;
            this.barButtonItem_新建页面.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_新建页面.ImageOptions.Image")));
            this.barButtonItem_新建页面.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N));
            this.barButtonItem_新建页面.Name = "barButtonItem_新建页面";
            this.barButtonItem_新建页面.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ToolStripMenuItem_页面组_新建页面_Click);
            // 
            // barButtonItem_打开页面
            // 
            this.barButtonItem_打开页面.Caption = "打开页面(&O)";
            this.barButtonItem_打开页面.Enabled = false;
            this.barButtonItem_打开页面.Id = 14;
            this.barButtonItem_打开页面.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_打开页面.ImageOptions.Image")));
            this.barButtonItem_打开页面.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O));
            this.barButtonItem_打开页面.Name = "barButtonItem_打开页面";
            this.barButtonItem_打开页面.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_打开页面_ItemClick);
            // 
            // barButtonItem_导入页面
            // 
            this.barButtonItem_导入页面.Caption = "导入页面(&F)...";
            this.barButtonItem_导入页面.Enabled = false;
            this.barButtonItem_导入页面.Id = 75;
            this.barButtonItem_导入页面.Name = "barButtonItem_导入页面";
            this.barButtonItem_导入页面.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ToolStripMenuItem_页面组_导入页面_Click);
            // 
            // barButtonItem_保存页面
            // 
            this.barButtonItem_保存页面.Caption = "保存页面";
            this.barButtonItem_保存页面.Enabled = false;
            this.barButtonItem_保存页面.Hint = "保存页面";
            this.barButtonItem_保存页面.Id = 271;
            this.barButtonItem_保存页面.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_保存页面.ImageOptions.Image")));
            this.barButtonItem_保存页面.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S));
            this.barButtonItem_保存页面.Name = "barButtonItem_保存页面";
            this.barButtonItem_保存页面.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_保存页面_ItemClick);
            // 
            // barButtonItem_保存工程
            // 
            this.barButtonItem_保存工程.Caption = "保存工程(&S)";
            this.barButtonItem_保存工程.Enabled = false;
            this.barButtonItem_保存工程.Id = 18;
            this.barButtonItem_保存工程.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_保存工程.ImageOptions.Image")));
            this.barButtonItem_保存工程.ItemShortcut = new DevExpress.XtraBars.BarShortcut(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                | System.Windows.Forms.Keys.S));
            this.barButtonItem_保存工程.Name = "barButtonItem_保存工程";
            this.barButtonItem_保存工程.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_保存工程_ItemClick);
            // 
            // BarButtonItem_ProjectProperty
            // 
            this.BarButtonItem_ProjectProperty.Caption = "工程属性";
            this.BarButtonItem_ProjectProperty.Enabled = false;
            this.BarButtonItem_ProjectProperty.Id = 288;
            this.BarButtonItem_ProjectProperty.Name = "BarButtonItem_ProjectProperty";
            this.BarButtonItem_ProjectProperty.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_ProjectProperty_ItemClick);
            // 
            // BarSubItem_RecentlyProject
            // 
            this.BarSubItem_RecentlyProject.Caption = "最近打开的工程";
            this.BarSubItem_RecentlyProject.Id = 290;
            this.BarSubItem_RecentlyProject.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.BarListItem_RecentlyProjece)});
            this.BarSubItem_RecentlyProject.Name = "BarSubItem_RecentlyProject";
            // 
            // BarListItem_RecentlyProjece
            // 
            this.BarListItem_RecentlyProjece.Id = 291;
            this.BarListItem_RecentlyProjece.Name = "BarListItem_RecentlyProjece";
            // 
            // barButtonItem_退出
            // 
            this.barButtonItem_退出.Caption = "退出(&X)";
            this.barButtonItem_退出.Id = 22;
            this.barButtonItem_退出.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q));
            this.barButtonItem_退出.Name = "barButtonItem_退出";
            this.barButtonItem_退出.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_退出_ItemClick);
            // 
            // barSubItem_主菜单_编辑
            // 
            this.barSubItem_主菜单_编辑.Caption = "编辑(&E)";
            this.barSubItem_主菜单_编辑.Enabled = false;
            this.barSubItem_主菜单_编辑.Id = 3;
            this.barSubItem_主菜单_编辑.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem10, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem11),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem13, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem14),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem15),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem17),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem16, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem18, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem19)});
            this.barSubItem_主菜单_编辑.Name = "barSubItem_主菜单_编辑";
            // 
            // barButtonItem10
            // 
            this.barButtonItem10.Caption = "撤销(&U)";
            this.barButtonItem10.Id = 23;
            this.barButtonItem10.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem10.ImageOptions.Image")));
            this.barButtonItem10.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z));
            this.barButtonItem10.Name = "barButtonItem10";
            this.barButtonItem10.ShortcutKeyDisplayString = "Ctrl+Z";
            this.barButtonItem10.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem10_ItemClick);
            // 
            // barButtonItem11
            // 
            this.barButtonItem11.Caption = "重复(&R)";
            this.barButtonItem11.Id = 24;
            this.barButtonItem11.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem11.ImageOptions.Image")));
            this.barButtonItem11.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y));
            this.barButtonItem11.Name = "barButtonItem11";
            this.barButtonItem11.ShortcutKeyDisplayString = "Ctrl+Y";
            this.barButtonItem11.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem11_ItemClick);
            // 
            // barButtonItem13
            // 
            this.barButtonItem13.Caption = "剪切(&T)";
            this.barButtonItem13.Id = 26;
            this.barButtonItem13.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem13.ImageOptions.Image")));
            this.barButtonItem13.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X));
            this.barButtonItem13.Name = "barButtonItem13";
            this.barButtonItem13.ShortcutKeyDisplayString = "Ctrl+X";
            this.barButtonItem13.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem13_ItemClick);
            // 
            // barButtonItem14
            // 
            this.barButtonItem14.Caption = "复制(&C)";
            this.barButtonItem14.Id = 27;
            this.barButtonItem14.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem14.ImageOptions.Image")));
            this.barButtonItem14.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C));
            this.barButtonItem14.Name = "barButtonItem14";
            this.barButtonItem14.ShortcutKeyDisplayString = "Ctrl+C";
            this.barButtonItem14.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem14_ItemClick);
            // 
            // barButtonItem15
            // 
            this.barButtonItem15.Caption = "粘贴(&P)";
            this.barButtonItem15.Id = 28;
            this.barButtonItem15.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem15.ImageOptions.Image")));
            this.barButtonItem15.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V));
            this.barButtonItem15.Name = "barButtonItem15";
            this.barButtonItem15.ShortcutKeyDisplayString = "Ctrl+V";
            this.barButtonItem15.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem15_ItemClick);
            // 
            // barButtonItem17
            // 
            this.barButtonItem17.Caption = "删除(&D)";
            this.barButtonItem17.Id = 30;
            this.barButtonItem17.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem17.ImageOptions.Image")));
            this.barButtonItem17.ItemShortcut = new DevExpress.XtraBars.BarShortcut(System.Windows.Forms.Keys.Delete);
            this.barButtonItem17.Name = "barButtonItem17";
            this.barButtonItem17.ShortcutKeyDisplayString = "Delete";
            this.barButtonItem17.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem17_ItemClick);
            // 
            // barButtonItem16
            // 
            this.barButtonItem16.Caption = "全选(&A)";
            this.barButtonItem16.Id = 29;
            this.barButtonItem16.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A));
            this.barButtonItem16.Name = "barButtonItem16";
            this.barButtonItem16.ShortcutKeyDisplayString = "Ctrl+A";
            this.barButtonItem16.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem16_ItemClick);
            // 
            // barButtonItem18
            // 
            this.barButtonItem18.Caption = "文本查找(&F)";
            this.barButtonItem18.Id = 31;
            this.barButtonItem18.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem18.ImageOptions.Image")));
            this.barButtonItem18.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F));
            this.barButtonItem18.Name = "barButtonItem18";
            this.barButtonItem18.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem18_ItemClick);
            // 
            // barButtonItem19
            // 
            this.barButtonItem19.Caption = "引用统计(&R)";
            this.barButtonItem19.Id = 32;
            this.barButtonItem19.Name = "barButtonItem19";
            this.barButtonItem19.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem19_ItemClick);
            // 
            // barSubItem_主菜单_视图
            // 
            this.barSubItem_主菜单_视图.Caption = "视图(&V)";
            this.barSubItem_主菜单_视图.Enabled = false;
            this.barSubItem_主菜单_视图.Id = 4;
            this.barSubItem_主菜单_视图.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menubarCheckItem_工具栏, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.menubarCheckItem_状态栏),
            new DevExpress.XtraBars.LinkPersistInfo(this.menubarCheckItem_导航栏, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.menubarCheckItem_输出栏),
            new DevExpress.XtraBars.LinkPersistInfo(this.menubarCheckItem_对象浏览器),
            new DevExpress.XtraBars.LinkPersistInfo(this.menubarCheckItem_变量浏览器),
            new DevExpress.XtraBars.LinkPersistInfo(this.menubarCheckItem_属性, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.menubarCheckItem_动画),
            new DevExpress.XtraBars.LinkPersistInfo(this.menubarSubItem_资源库, true)});
            this.barSubItem_主菜单_视图.Name = "barSubItem_主菜单_视图";
            // 
            // menubarCheckItem_工具栏
            // 
            this.menubarCheckItem_工具栏.BindableChecked = true;
            this.menubarCheckItem_工具栏.Caption = "工具栏(&T)";
            this.menubarCheckItem_工具栏.Checked = true;
            this.menubarCheckItem_工具栏.Id = 39;
            this.menubarCheckItem_工具栏.Name = "menubarCheckItem_工具栏";
            this.menubarCheckItem_工具栏.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.menubarCheckItem_工具栏_CheckedChanged);
            // 
            // menubarCheckItem_状态栏
            // 
            this.menubarCheckItem_状态栏.BindableChecked = true;
            this.menubarCheckItem_状态栏.Caption = "状态栏(&S)";
            this.menubarCheckItem_状态栏.Checked = true;
            this.menubarCheckItem_状态栏.Id = 40;
            this.menubarCheckItem_状态栏.Name = "menubarCheckItem_状态栏";
            this.menubarCheckItem_状态栏.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.menubarCheckItem_状态栏_CheckedChanged);
            // 
            // menubarCheckItem_导航栏
            // 
            this.menubarCheckItem_导航栏.BindableChecked = true;
            this.menubarCheckItem_导航栏.Caption = "导航栏(&N)";
            this.menubarCheckItem_导航栏.Checked = true;
            this.menubarCheckItem_导航栏.Id = 36;
            this.menubarCheckItem_导航栏.Name = "menubarCheckItem_导航栏";
            this.menubarCheckItem_导航栏.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.menubarCheckItem_导航栏_CheckedChanged);
            // 
            // menubarCheckItem_输出栏
            // 
            this.menubarCheckItem_输出栏.BindableChecked = true;
            this.menubarCheckItem_输出栏.Caption = "输出栏(&O)";
            this.menubarCheckItem_输出栏.Checked = true;
            this.menubarCheckItem_输出栏.Id = 41;
            this.menubarCheckItem_输出栏.Name = "menubarCheckItem_输出栏";
            this.menubarCheckItem_输出栏.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.MenubarCheckItem_输出栏_CheckedChanged);
            // 
            // menubarCheckItem_对象浏览器
            // 
            this.menubarCheckItem_对象浏览器.BindableChecked = true;
            this.menubarCheckItem_对象浏览器.Caption = "对象浏览器(&B)";
            this.menubarCheckItem_对象浏览器.Checked = true;
            this.menubarCheckItem_对象浏览器.Id = 257;
            this.menubarCheckItem_对象浏览器.Name = "menubarCheckItem_对象浏览器";
            this.menubarCheckItem_对象浏览器.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.MenubarCheckItem_对象浏览器_CheckedChanged);
            // 
            // menubarCheckItem_变量浏览器
            // 
            this.menubarCheckItem_变量浏览器.BindableChecked = true;
            this.menubarCheckItem_变量浏览器.Caption = "变量浏览器";
            this.menubarCheckItem_变量浏览器.Checked = true;
            this.menubarCheckItem_变量浏览器.Id = 272;
            this.menubarCheckItem_变量浏览器.Name = "menubarCheckItem_变量浏览器";
            this.menubarCheckItem_变量浏览器.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.MenubarCheckItem_变量浏览器_CheckedChanged);
            // 
            // menubarCheckItem_属性
            // 
            this.menubarCheckItem_属性.BindableChecked = true;
            this.menubarCheckItem_属性.Caption = "属性框(&P)";
            this.menubarCheckItem_属性.Checked = true;
            this.menubarCheckItem_属性.Id = 38;
            this.menubarCheckItem_属性.Name = "menubarCheckItem_属性";
            this.menubarCheckItem_属性.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.menubarCheckItem_属性_CheckedChanged);
            // 
            // menubarCheckItem_动画
            // 
            this.menubarCheckItem_动画.BindableChecked = true;
            this.menubarCheckItem_动画.Caption = "动画框(&A)";
            this.menubarCheckItem_动画.Checked = true;
            this.menubarCheckItem_动画.Id = 175;
            this.menubarCheckItem_动画.Name = "menubarCheckItem_动画";
            this.menubarCheckItem_动画.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.menubarCheckItem_动画_CheckedChanged);
            // 
            // menubarSubItem_资源库
            // 
            this.menubarSubItem_资源库.Caption = "资源库";
            this.menubarSubItem_资源库.Id = 198;
            this.menubarSubItem_资源库.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.menubarCheckItem_基本控件),
            new DevExpress.XtraBars.LinkPersistInfo(this.menubarCheckItem_DCCE工控组件),
            new DevExpress.XtraBars.LinkPersistInfo(this.menubarCheckItem_精灵控件),
            new DevExpress.XtraBars.LinkPersistInfo(this.menubarCheckItem_ActiveX控件)});
            this.menubarSubItem_资源库.Name = "menubarSubItem_资源库";
            // 
            // menubarCheckItem_基本控件
            // 
            this.menubarCheckItem_基本控件.BindableChecked = true;
            this.menubarCheckItem_基本控件.Caption = "基本控件";
            this.menubarCheckItem_基本控件.Checked = true;
            this.menubarCheckItem_基本控件.Id = 205;
            this.menubarCheckItem_基本控件.Name = "menubarCheckItem_基本控件";
            this.menubarCheckItem_基本控件.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.MenubarCheckItem_Windows控件_CheckedChanged);
            // 
            // menubarCheckItem_DCCE工控组件
            // 
            this.menubarCheckItem_DCCE工控组件.BindableChecked = true;
            this.menubarCheckItem_DCCE工控组件.Caption = "DCCE工控组件";
            this.menubarCheckItem_DCCE工控组件.Checked = true;
            this.menubarCheckItem_DCCE工控组件.Id = 206;
            this.menubarCheckItem_DCCE工控组件.Name = "menubarCheckItem_DCCE工控组件";
            this.menubarCheckItem_DCCE工控组件.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.MenubarCheckItem_DCCE工控组件_CheckedChanged);
            // 
            // menubarCheckItem_精灵控件
            // 
            this.menubarCheckItem_精灵控件.BindableChecked = true;
            this.menubarCheckItem_精灵控件.Caption = "精灵控件";
            this.menubarCheckItem_精灵控件.Checked = true;
            this.menubarCheckItem_精灵控件.Id = 208;
            this.menubarCheckItem_精灵控件.Name = "menubarCheckItem_精灵控件";
            this.menubarCheckItem_精灵控件.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.MenubarCheckItem_精灵控件_CheckedChanged);
            // 
            // menubarCheckItem_ActiveX控件
            // 
            this.menubarCheckItem_ActiveX控件.BindableChecked = true;
            this.menubarCheckItem_ActiveX控件.Caption = "ActiveX控件";
            this.menubarCheckItem_ActiveX控件.Checked = true;
            this.menubarCheckItem_ActiveX控件.Id = 207;
            this.menubarCheckItem_ActiveX控件.Name = "menubarCheckItem_ActiveX控件";
            this.menubarCheckItem_ActiveX控件.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.MenubarCheckItem_ActiveX控件_CheckedChanged);
            // 
            // barSubItem_主菜单_操作
            // 
            this.barSubItem_主菜单_操作.Caption = "操作(&O)";
            this.barSubItem_主菜单_操作.Enabled = false;
            this.barSubItem_主菜单_操作.Id = 5;
            this.barSubItem_主菜单_操作.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem23, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem24),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem25, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem26),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem27, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem28),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem12, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem13),
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItem10)});
            this.barSubItem_主菜单_操作.Name = "barSubItem_主菜单_操作";
            // 
            // barButtonItem23
            // 
            this.barButtonItem23.Caption = "元素置顶(&B)";
            this.barButtonItem23.Id = 45;
            this.barButtonItem23.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem23.ImageOptions.Image")));
            this.barButtonItem23.Name = "barButtonItem23";
            this.barButtonItem23.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem23_ItemClick);
            // 
            // barButtonItem24
            // 
            this.barButtonItem24.Caption = "元素置底(&S)";
            this.barButtonItem24.Id = 46;
            this.barButtonItem24.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem24.ImageOptions.Image")));
            this.barButtonItem24.Name = "barButtonItem24";
            this.barButtonItem24.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem24_ItemClick);
            // 
            // barButtonItem25
            // 
            this.barButtonItem25.Caption = "组合(&G)";
            this.barButtonItem25.Id = 47;
            this.barButtonItem25.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem25.ImageOptions.Image")));
            this.barButtonItem25.Name = "barButtonItem25";
            this.barButtonItem25.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem25_ItemClick);
            // 
            // barButtonItem26
            // 
            this.barButtonItem26.Caption = "拆解(&U)";
            this.barButtonItem26.Id = 48;
            this.barButtonItem26.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem26.ImageOptions.Image")));
            this.barButtonItem26.Name = "barButtonItem26";
            this.barButtonItem26.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem26_ItemClick);
            // 
            // barButtonItem27
            // 
            this.barButtonItem27.Caption = "水平等间距(&H)";
            this.barButtonItem27.Id = 50;
            this.barButtonItem27.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem27.ImageOptions.Image")));
            this.barButtonItem27.Name = "barButtonItem27";
            this.barButtonItem27.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem27_ItemClick);
            // 
            // barButtonItem28
            // 
            this.barButtonItem28.Caption = "垂直等间距(&V)";
            this.barButtonItem28.Id = 51;
            this.barButtonItem28.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem28.ImageOptions.Image")));
            this.barButtonItem28.Name = "barButtonItem28";
            this.barButtonItem28.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem28_ItemClick);
            // 
            // barSubItem12
            // 
            this.barSubItem12.Caption = "对象对齐(&A)";
            this.barSubItem12.Id = 53;
            this.barSubItem12.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem48),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem52),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem50),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem51),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem49),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem53)});
            this.barSubItem12.Name = "barSubItem12";
            // 
            // barButtonItem48
            // 
            this.barButtonItem48.Caption = "上对齐";
            this.barButtonItem48.Id = 79;
            this.barButtonItem48.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem48.ImageOptions.Image")));
            this.barButtonItem48.Name = "barButtonItem48";
            this.barButtonItem48.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem48_ItemClick);
            // 
            // barButtonItem52
            // 
            this.barButtonItem52.Caption = "垂直对齐";
            this.barButtonItem52.Id = 83;
            this.barButtonItem52.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem52.ImageOptions.Image")));
            this.barButtonItem52.Name = "barButtonItem52";
            this.barButtonItem52.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem52_ItemClick);
            // 
            // barButtonItem50
            // 
            this.barButtonItem50.Caption = "下对齐";
            this.barButtonItem50.Id = 81;
            this.barButtonItem50.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem50.ImageOptions.Image")));
            this.barButtonItem50.Name = "barButtonItem50";
            this.barButtonItem50.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem50_ItemClick);
            // 
            // barButtonItem51
            // 
            this.barButtonItem51.Caption = "左对齐";
            this.barButtonItem51.Id = 82;
            this.barButtonItem51.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem51.ImageOptions.Image")));
            this.barButtonItem51.Name = "barButtonItem51";
            this.barButtonItem51.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem51_ItemClick);
            // 
            // barButtonItem49
            // 
            this.barButtonItem49.Caption = "水平对齐";
            this.barButtonItem49.Id = 80;
            this.barButtonItem49.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem49.ImageOptions.Image")));
            this.barButtonItem49.Name = "barButtonItem49";
            this.barButtonItem49.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem49_ItemClick);
            // 
            // barButtonItem53
            // 
            this.barButtonItem53.Caption = "右对齐";
            this.barButtonItem53.Id = 84;
            this.barButtonItem53.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem53.ImageOptions.Image")));
            this.barButtonItem53.Name = "barButtonItem53";
            this.barButtonItem53.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem53_ItemClick);
            // 
            // barSubItem13
            // 
            this.barSubItem13.Caption = "对象尺寸(&S)";
            this.barSubItem13.Id = 55;
            this.barSubItem13.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem55),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem56),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem54)});
            this.barSubItem13.Name = "barSubItem13";
            // 
            // barButtonItem55
            // 
            this.barButtonItem55.Caption = "相同宽度";
            this.barButtonItem55.Id = 86;
            this.barButtonItem55.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem55.ImageOptions.Image")));
            this.barButtonItem55.Name = "barButtonItem55";
            this.barButtonItem55.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem55_ItemClick);
            // 
            // barButtonItem56
            // 
            this.barButtonItem56.Caption = "相同高度";
            this.barButtonItem56.Id = 87;
            this.barButtonItem56.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem56.ImageOptions.Image")));
            this.barButtonItem56.Name = "barButtonItem56";
            this.barButtonItem56.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem56_ItemClick);
            // 
            // barButtonItem54
            // 
            this.barButtonItem54.Caption = "相同大小";
            this.barButtonItem54.Id = 85;
            this.barButtonItem54.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem54.ImageOptions.Image")));
            this.barButtonItem54.Name = "barButtonItem54";
            this.barButtonItem54.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem54_ItemClick);
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "对象旋转(&R)";
            this.barSubItem1.Id = 261;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem29),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem30),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem9),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem12)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItem29
            // 
            this.barButtonItem29.Caption = "水平翻转";
            this.barButtonItem29.Id = 56;
            this.barButtonItem29.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem29.ImageOptions.Image")));
            this.barButtonItem29.Name = "barButtonItem29";
            this.barButtonItem29.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem29_ItemClick);
            // 
            // barButtonItem30
            // 
            this.barButtonItem30.Caption = "垂直翻转";
            this.barButtonItem30.Id = 57;
            this.barButtonItem30.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem30.ImageOptions.Image")));
            this.barButtonItem30.Name = "barButtonItem30";
            this.barButtonItem30.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem30_ItemClick);
            // 
            // barButtonItem9
            // 
            this.barButtonItem9.Caption = "向右旋转";
            this.barButtonItem9.Hint = "向右旋转";
            this.barButtonItem9.Id = 259;
            this.barButtonItem9.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem9.ImageOptions.Image")));
            this.barButtonItem9.Name = "barButtonItem9";
            this.barButtonItem9.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem115_ItemClick);
            // 
            // barButtonItem12
            // 
            this.barButtonItem12.Caption = "向左旋转";
            this.barButtonItem12.Hint = "向左旋转";
            this.barButtonItem12.Id = 260;
            this.barButtonItem12.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem12.ImageOptions.Image")));
            this.barButtonItem12.Name = "barButtonItem12";
            this.barButtonItem12.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem116_ItemClick);
            // 
            // barCheckItem10
            // 
            this.barCheckItem10.Caption = "锁定图形(&L)";
            this.barCheckItem10.Id = 88;
            this.barCheckItem10.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barCheckItem10.ImageOptions.Image")));
            this.barCheckItem10.Name = "barCheckItem10";
            this.barCheckItem10.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.barCheckItem10_CheckedChanged);
            // 
            // barSubItem_主菜单_脚本
            // 
            this.barSubItem_主菜单_脚本.Caption = "脚本(&S)";
            this.barSubItem_主菜单_脚本.Enabled = false;
            this.barSubItem_主菜单_脚本.Id = 6;
            this.barSubItem_主菜单_脚本.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem137)});
            this.barSubItem_主菜单_脚本.Name = "barSubItem_主菜单_脚本";
            // 
            // barButtonItem137
            // 
            this.barButtonItem137.Caption = "打开脚本管理器(&O)";
            this.barButtonItem137.Id = 210;
            this.barButtonItem137.Name = "barButtonItem137";
            this.barButtonItem137.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem137_ItemClick);
            // 
            // barSubItem_主菜单_工具
            // 
            this.barSubItem_主菜单_工具.Caption = "工具(&T)";
            this.barSubItem_主菜单_工具.Enabled = false;
            this.barSubItem_主菜单_工具.Id = 237;
            this.barSubItem_主菜单_工具.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem62)});
            this.barSubItem_主菜单_工具.Name = "barSubItem_主菜单_工具";
            // 
            // barButtonItem62
            // 
            this.barButtonItem62.Caption = "控件设置(&C)";
            this.barButtonItem62.Id = 238;
            this.barButtonItem62.Name = "barButtonItem62";
            this.barButtonItem62.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem62_ItemClick);
            // 
            // barSubItem_主菜单_窗口
            // 
            this.barSubItem_主菜单_窗口.Caption = "窗口(&W)";
            this.barSubItem_主菜单_窗口.Enabled = false;
            this.barSubItem_主菜单_窗口.Id = 8;
            this.barSubItem_主菜单_窗口.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem37, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barMdiChildrenListItem1, true)});
            this.barSubItem_主菜单_窗口.Name = "barSubItem_主菜单_窗口";
            // 
            // barButtonItem37
            // 
            this.barButtonItem37.Caption = "全部关闭(&C)";
            this.barButtonItem37.Id = 67;
            this.barButtonItem37.Name = "barButtonItem37";
            this.barButtonItem37.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ToolStripMenuItem_页面根_关闭所有_Click);
            // 
            // barMdiChildrenListItem1
            // 
            this.barMdiChildrenListItem1.Caption = "子窗口列表";
            this.barMdiChildrenListItem1.Id = 68;
            this.barMdiChildrenListItem1.Name = "barMdiChildrenListItem1";
            this.barMdiChildrenListItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInCustomizing;
            // 
            // barSubItem_主菜单_帮助
            // 
            this.barSubItem_主菜单_帮助.Caption = "帮助(&H)";
            this.barSubItem_主菜单_帮助.Id = 266;
            this.barSubItem_主菜单_帮助.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_about, true)});
            this.barSubItem_主菜单_帮助.Name = "barSubItem_主菜单_帮助";
            // 
            // barButtonItem_about
            // 
            this.barButtonItem_about.Caption = "关于 FView (&A)";
            this.barButtonItem_about.Id = 268;
            this.barButtonItem_about.Name = "barButtonItem_about";
            this.barButtonItem_about.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_about_ItemClick);
            // 
            // statusbar_状态栏
            // 
            this.statusbar_状态栏.BarName = "状态栏";
            this.statusbar_状态栏.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.statusbar_状态栏.DockCol = 0;
            this.statusbar_状态栏.DockRow = 0;
            this.statusbar_状态栏.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.statusbar_状态栏.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.BarStaticItem_Status)});
            this.statusbar_状态栏.OptionsBar.AllowQuickCustomization = false;
            this.statusbar_状态栏.OptionsBar.DisableClose = true;
            this.statusbar_状态栏.OptionsBar.DisableCustomization = true;
            this.statusbar_状态栏.OptionsBar.DrawDragBorder = false;
            this.statusbar_状态栏.OptionsBar.UseWholeRow = true;
            this.statusbar_状态栏.Text = "状态栏";
            // 
            // BarStaticItem_Status
            // 
            this.BarStaticItem_Status.Caption = "状态";
            this.BarStaticItem_Status.Id = 0;
            this.BarStaticItem_Status.Name = "BarStaticItem_Status";
            // 
            // toolbar_资源
            // 
            this.toolbar_资源.BarName = "资源";
            this.toolbar_资源.DockCol = 1;
            this.toolbar_资源.DockRow = 1;
            this.toolbar_资源.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.toolbar_资源.FloatLocation = new System.Drawing.Point(391, 213);
            this.toolbar_资源.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolbarButtonItem_基本控件, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolbarButtonItem_DCCE工控组件, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolbarButtonItem_精灵控件, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolbarButtonItem_ActiveX控件, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.toolbar_资源.Offset = 378;
            this.toolbar_资源.OptionsBar.AllowQuickCustomization = false;
            this.toolbar_资源.Text = "资源";
            // 
            // toolbarButtonItem_基本控件
            // 
            this.toolbarButtonItem_基本控件.Caption = "基本控件";
            this.toolbarButtonItem_基本控件.Enabled = false;
            this.toolbarButtonItem_基本控件.Hint = "基本控件";
            this.toolbarButtonItem_基本控件.Id = 130;
            this.toolbarButtonItem_基本控件.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolbarButtonItem_基本控件.ImageOptions.Image")));
            this.toolbarButtonItem_基本控件.Name = "toolbarButtonItem_基本控件";
            this.toolbarButtonItem_基本控件.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolbarButtonItem_Windows控件_ItemClick);
            // 
            // toolbarButtonItem_DCCE工控组件
            // 
            this.toolbarButtonItem_DCCE工控组件.Caption = "DCCE工控组件";
            this.toolbarButtonItem_DCCE工控组件.Enabled = false;
            this.toolbarButtonItem_DCCE工控组件.Hint = "DCCE工控组件";
            this.toolbarButtonItem_DCCE工控组件.Id = 131;
            this.toolbarButtonItem_DCCE工控组件.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolbarButtonItem_DCCE工控组件.ImageOptions.Image")));
            this.toolbarButtonItem_DCCE工控组件.Name = "toolbarButtonItem_DCCE工控组件";
            this.toolbarButtonItem_DCCE工控组件.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ToolbarButtonItem_DCCE工控组件_ItemClick);
            // 
            // toolbarButtonItem_精灵控件
            // 
            this.toolbarButtonItem_精灵控件.Caption = "精灵控件";
            this.toolbarButtonItem_精灵控件.Enabled = false;
            this.toolbarButtonItem_精灵控件.Hint = "精灵控件";
            this.toolbarButtonItem_精灵控件.Id = 133;
            this.toolbarButtonItem_精灵控件.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolbarButtonItem_精灵控件.ImageOptions.Image")));
            this.toolbarButtonItem_精灵控件.Name = "toolbarButtonItem_精灵控件";
            this.toolbarButtonItem_精灵控件.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.toolbarButtonItem_精灵控件_ItemClick);
            // 
            // toolbarButtonItem_ActiveX控件
            // 
            this.toolbarButtonItem_ActiveX控件.Caption = "ActiveX控件";
            this.toolbarButtonItem_ActiveX控件.Enabled = false;
            this.toolbarButtonItem_ActiveX控件.Hint = "ActiveX控件";
            this.toolbarButtonItem_ActiveX控件.Id = 132;
            this.toolbarButtonItem_ActiveX控件.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("toolbarButtonItem_ActiveX控件.ImageOptions.Image")));
            this.toolbarButtonItem_ActiveX控件.Name = "toolbarButtonItem_ActiveX控件";
            this.toolbarButtonItem_ActiveX控件.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ToolbarButtonItem_ActiveX控件_ItemClick);
            // 
            // toolbar_操作
            // 
            this.toolbar_操作.BarName = "操作";
            this.toolbar_操作.DockCol = 1;
            this.toolbar_操作.DockRow = 2;
            this.toolbar_操作.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.toolbar_操作.FloatLocation = new System.Drawing.Point(403, 208);
            this.toolbar_操作.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_ElementTop, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_ElementBottom),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_ElementCombine, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_ElementSeparate),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_HorizontalEquidistance, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_VerticalEquidistance),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_TopAlign, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_VerticalAlign),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_BottomAlign),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_LeftAlign),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_HorizontalAlign),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_RightAlign),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_EqualWidth, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_EqualHeight),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_EqualAndOpposite),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_HorizontalRotate, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_VerticalRotate),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_RightRotate),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_LeftRotate),
            new DevExpress.XtraBars.LinkPersistInfo(this.barCheckItem_LockElements)});
            this.toolbar_操作.Offset = 243;
            this.toolbar_操作.OptionsBar.AllowQuickCustomization = false;
            this.toolbar_操作.Text = "操作";
            // 
            // barButtonItem_ElementTop
            // 
            this.barButtonItem_ElementTop.Caption = "元素置顶";
            this.barButtonItem_ElementTop.Enabled = false;
            this.barButtonItem_ElementTop.Hint = "元素置顶";
            this.barButtonItem_ElementTop.Id = 150;
            this.barButtonItem_ElementTop.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_ElementTop.ImageOptions.Image")));
            this.barButtonItem_ElementTop.Name = "barButtonItem_ElementTop";
            this.barButtonItem_ElementTop.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem102_ItemClick);
            // 
            // barButtonItem_ElementBottom
            // 
            this.barButtonItem_ElementBottom.Caption = "元素置底";
            this.barButtonItem_ElementBottom.Enabled = false;
            this.barButtonItem_ElementBottom.Hint = "元素置底";
            this.barButtonItem_ElementBottom.Id = 151;
            this.barButtonItem_ElementBottom.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_ElementBottom.ImageOptions.Image")));
            this.barButtonItem_ElementBottom.Name = "barButtonItem_ElementBottom";
            this.barButtonItem_ElementBottom.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem103_ItemClick);
            // 
            // barButtonItem_ElementCombine
            // 
            this.barButtonItem_ElementCombine.Caption = "组合";
            this.barButtonItem_ElementCombine.Enabled = false;
            this.barButtonItem_ElementCombine.Hint = "组合";
            this.barButtonItem_ElementCombine.Id = 148;
            this.barButtonItem_ElementCombine.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_ElementCombine.ImageOptions.Image")));
            this.barButtonItem_ElementCombine.Name = "barButtonItem_ElementCombine";
            this.barButtonItem_ElementCombine.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem100_ItemClick);
            // 
            // barButtonItem_ElementSeparate
            // 
            this.barButtonItem_ElementSeparate.Caption = "拆解";
            this.barButtonItem_ElementSeparate.Enabled = false;
            this.barButtonItem_ElementSeparate.Hint = "拆解";
            this.barButtonItem_ElementSeparate.Id = 149;
            this.barButtonItem_ElementSeparate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_ElementSeparate.ImageOptions.Image")));
            this.barButtonItem_ElementSeparate.Name = "barButtonItem_ElementSeparate";
            this.barButtonItem_ElementSeparate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem101_ItemClick);
            // 
            // barButtonItem_HorizontalEquidistance
            // 
            this.barButtonItem_HorizontalEquidistance.Caption = "水平等间距";
            this.barButtonItem_HorizontalEquidistance.Enabled = false;
            this.barButtonItem_HorizontalEquidistance.Hint = "水平等间距";
            this.barButtonItem_HorizontalEquidistance.Id = 170;
            this.barButtonItem_HorizontalEquidistance.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_HorizontalEquidistance.ImageOptions.Image")));
            this.barButtonItem_HorizontalEquidistance.Name = "barButtonItem_HorizontalEquidistance";
            this.barButtonItem_HorizontalEquidistance.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem117_ItemClick);
            // 
            // barButtonItem_VerticalEquidistance
            // 
            this.barButtonItem_VerticalEquidistance.Caption = "垂直等间距";
            this.barButtonItem_VerticalEquidistance.Enabled = false;
            this.barButtonItem_VerticalEquidistance.Hint = "垂直等间距";
            this.barButtonItem_VerticalEquidistance.Id = 171;
            this.barButtonItem_VerticalEquidistance.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_VerticalEquidistance.ImageOptions.Image")));
            this.barButtonItem_VerticalEquidistance.Name = "barButtonItem_VerticalEquidistance";
            this.barButtonItem_VerticalEquidistance.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem118_ItemClick);
            // 
            // barButtonItem_TopAlign
            // 
            this.barButtonItem_TopAlign.Caption = "上对齐";
            this.barButtonItem_TopAlign.Enabled = false;
            this.barButtonItem_TopAlign.Hint = "上对齐";
            this.barButtonItem_TopAlign.Id = 154;
            this.barButtonItem_TopAlign.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_TopAlign.ImageOptions.Image")));
            this.barButtonItem_TopAlign.Name = "barButtonItem_TopAlign";
            this.barButtonItem_TopAlign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem106_ItemClick);
            // 
            // barButtonItem_VerticalAlign
            // 
            this.barButtonItem_VerticalAlign.Caption = "垂直对齐";
            this.barButtonItem_VerticalAlign.Enabled = false;
            this.barButtonItem_VerticalAlign.Hint = "垂直对齐";
            this.barButtonItem_VerticalAlign.Id = 158;
            this.barButtonItem_VerticalAlign.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_VerticalAlign.ImageOptions.Image")));
            this.barButtonItem_VerticalAlign.Name = "barButtonItem_VerticalAlign";
            this.barButtonItem_VerticalAlign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem110_ItemClick);
            // 
            // barButtonItem_BottomAlign
            // 
            this.barButtonItem_BottomAlign.Caption = "下对齐";
            this.barButtonItem_BottomAlign.Enabled = false;
            this.barButtonItem_BottomAlign.Hint = "下对齐";
            this.barButtonItem_BottomAlign.Id = 156;
            this.barButtonItem_BottomAlign.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_BottomAlign.ImageOptions.Image")));
            this.barButtonItem_BottomAlign.Name = "barButtonItem_BottomAlign";
            this.barButtonItem_BottomAlign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem108_ItemClick);
            // 
            // barButtonItem_LeftAlign
            // 
            this.barButtonItem_LeftAlign.Caption = "左对齐";
            this.barButtonItem_LeftAlign.Enabled = false;
            this.barButtonItem_LeftAlign.Hint = "左对齐";
            this.barButtonItem_LeftAlign.Id = 157;
            this.barButtonItem_LeftAlign.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_LeftAlign.ImageOptions.Image")));
            this.barButtonItem_LeftAlign.Name = "barButtonItem_LeftAlign";
            this.barButtonItem_LeftAlign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem109_ItemClick);
            // 
            // barButtonItem_HorizontalAlign
            // 
            this.barButtonItem_HorizontalAlign.Caption = "水平对齐";
            this.barButtonItem_HorizontalAlign.Enabled = false;
            this.barButtonItem_HorizontalAlign.Hint = "水平对齐";
            this.barButtonItem_HorizontalAlign.Id = 155;
            this.barButtonItem_HorizontalAlign.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_HorizontalAlign.ImageOptions.Image")));
            this.barButtonItem_HorizontalAlign.Name = "barButtonItem_HorizontalAlign";
            this.barButtonItem_HorizontalAlign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem107_ItemClick);
            // 
            // barButtonItem_RightAlign
            // 
            this.barButtonItem_RightAlign.Caption = "右对齐";
            this.barButtonItem_RightAlign.Enabled = false;
            this.barButtonItem_RightAlign.Hint = "右对齐";
            this.barButtonItem_RightAlign.Id = 159;
            this.barButtonItem_RightAlign.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_RightAlign.ImageOptions.Image")));
            this.barButtonItem_RightAlign.Name = "barButtonItem_RightAlign";
            this.barButtonItem_RightAlign.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem111_ItemClick);
            // 
            // barButtonItem_EqualWidth
            // 
            this.barButtonItem_EqualWidth.Caption = "宽度相等";
            this.barButtonItem_EqualWidth.Enabled = false;
            this.barButtonItem_EqualWidth.Hint = "宽度相等";
            this.barButtonItem_EqualWidth.Id = 161;
            this.barButtonItem_EqualWidth.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_EqualWidth.ImageOptions.Image")));
            this.barButtonItem_EqualWidth.Name = "barButtonItem_EqualWidth";
            this.barButtonItem_EqualWidth.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem113_ItemClick);
            // 
            // barButtonItem_EqualHeight
            // 
            this.barButtonItem_EqualHeight.Caption = "高度相等";
            this.barButtonItem_EqualHeight.Enabled = false;
            this.barButtonItem_EqualHeight.Hint = "高度相等";
            this.barButtonItem_EqualHeight.Id = 162;
            this.barButtonItem_EqualHeight.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_EqualHeight.ImageOptions.Image")));
            this.barButtonItem_EqualHeight.Name = "barButtonItem_EqualHeight";
            this.barButtonItem_EqualHeight.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem114_ItemClick);
            // 
            // barButtonItem_EqualAndOpposite
            // 
            this.barButtonItem_EqualAndOpposite.Caption = "大小相等";
            this.barButtonItem_EqualAndOpposite.Enabled = false;
            this.barButtonItem_EqualAndOpposite.Hint = "大小相等";
            this.barButtonItem_EqualAndOpposite.Id = 160;
            this.barButtonItem_EqualAndOpposite.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_EqualAndOpposite.ImageOptions.Image")));
            this.barButtonItem_EqualAndOpposite.Name = "barButtonItem_EqualAndOpposite";
            this.barButtonItem_EqualAndOpposite.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem112_ItemClick);
            // 
            // barButtonItem_HorizontalRotate
            // 
            this.barButtonItem_HorizontalRotate.Caption = "水平翻转";
            this.barButtonItem_HorizontalRotate.Enabled = false;
            this.barButtonItem_HorizontalRotate.Hint = "水平翻转";
            this.barButtonItem_HorizontalRotate.Id = 152;
            this.barButtonItem_HorizontalRotate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_HorizontalRotate.ImageOptions.Image")));
            this.barButtonItem_HorizontalRotate.Name = "barButtonItem_HorizontalRotate";
            this.barButtonItem_HorizontalRotate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem104_ItemClick);
            // 
            // barButtonItem_VerticalRotate
            // 
            this.barButtonItem_VerticalRotate.Caption = "垂直翻转";
            this.barButtonItem_VerticalRotate.Enabled = false;
            this.barButtonItem_VerticalRotate.Hint = "垂直翻转";
            this.barButtonItem_VerticalRotate.Id = 153;
            this.barButtonItem_VerticalRotate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_VerticalRotate.ImageOptions.Image")));
            this.barButtonItem_VerticalRotate.Name = "barButtonItem_VerticalRotate";
            this.barButtonItem_VerticalRotate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem105_ItemClick);
            // 
            // barButtonItem_RightRotate
            // 
            this.barButtonItem_RightRotate.Caption = "向右旋转";
            this.barButtonItem_RightRotate.Enabled = false;
            this.barButtonItem_RightRotate.Hint = "向右旋转";
            this.barButtonItem_RightRotate.Id = 163;
            this.barButtonItem_RightRotate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_RightRotate.ImageOptions.Image")));
            this.barButtonItem_RightRotate.Name = "barButtonItem_RightRotate";
            this.barButtonItem_RightRotate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem115_ItemClick);
            // 
            // barButtonItem_LeftRotate
            // 
            this.barButtonItem_LeftRotate.Caption = "向左旋转";
            this.barButtonItem_LeftRotate.Enabled = false;
            this.barButtonItem_LeftRotate.Hint = "向左旋转";
            this.barButtonItem_LeftRotate.Id = 164;
            this.barButtonItem_LeftRotate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_LeftRotate.ImageOptions.Image")));
            this.barButtonItem_LeftRotate.Name = "barButtonItem_LeftRotate";
            this.barButtonItem_LeftRotate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem116_ItemClick);
            // 
            // barCheckItem_LockElements
            // 
            this.barCheckItem_LockElements.Caption = "元素锁定";
            this.barCheckItem_LockElements.Enabled = false;
            this.barCheckItem_LockElements.Hint = "元素锁定";
            this.barCheckItem_LockElements.Id = 168;
            this.barCheckItem_LockElements.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barCheckItem_LockElements.ImageOptions.Image")));
            this.barCheckItem_LockElements.Name = "barCheckItem_LockElements";
            this.barCheckItem_LockElements.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.BarCheckItem17_CheckedChanged);
            // 
            // toolbar_绘制基本图元
            // 
            this.toolbar_绘制基本图元.BarName = "绘图";
            this.toolbar_绘制基本图元.DockCol = 0;
            this.toolbar_绘制基本图元.DockRow = 2;
            this.toolbar_绘制基本图元.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.toolbar_绘制基本图元.FloatLocation = new System.Drawing.Point(1483, 213);
            this.toolbar_绘制基本图元.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_DrawLine),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_CurveLine),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_DrawEllipse),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Rectangle),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Polygon),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_RoundedRectangle),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_String),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Picture, true)});
            this.toolbar_绘制基本图元.Text = "绘图";
            // 
            // barButtonItem_DrawLine
            // 
            this.barButtonItem_DrawLine.Caption = "直线";
            this.barButtonItem_DrawLine.Enabled = false;
            this.barButtonItem_DrawLine.Hint = "直线";
            this.barButtonItem_DrawLine.Id = 249;
            this.barButtonItem_DrawLine.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_DrawLine.ImageOptions.Image")));
            this.barButtonItem_DrawLine.Name = "barButtonItem_DrawLine";
            this.barButtonItem_DrawLine.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_绘制直线_ItemClick);
            // 
            // barButtonItem_CurveLine
            // 
            this.barButtonItem_CurveLine.Caption = "折线";
            this.barButtonItem_CurveLine.Enabled = false;
            this.barButtonItem_CurveLine.Hint = "折线";
            this.barButtonItem_CurveLine.Id = 250;
            this.barButtonItem_CurveLine.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_CurveLine.ImageOptions.Image")));
            this.barButtonItem_CurveLine.Name = "barButtonItem_CurveLine";
            this.barButtonItem_CurveLine.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_绘制折线_ItemClick);
            // 
            // barButtonItem_DrawEllipse
            // 
            this.barButtonItem_DrawEllipse.Caption = "椭圆";
            this.barButtonItem_DrawEllipse.Enabled = false;
            this.barButtonItem_DrawEllipse.Hint = "椭圆";
            this.barButtonItem_DrawEllipse.Id = 251;
            this.barButtonItem_DrawEllipse.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_DrawEllipse.ImageOptions.Image")));
            this.barButtonItem_DrawEllipse.Name = "barButtonItem_DrawEllipse";
            this.barButtonItem_DrawEllipse.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_绘制椭圆_ItemClick);
            // 
            // barButtonItem_Rectangle
            // 
            this.barButtonItem_Rectangle.Caption = "矩形";
            this.barButtonItem_Rectangle.Enabled = false;
            this.barButtonItem_Rectangle.Hint = "矩形";
            this.barButtonItem_Rectangle.Id = 252;
            this.barButtonItem_Rectangle.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Rectangle.ImageOptions.Image")));
            this.barButtonItem_Rectangle.Name = "barButtonItem_Rectangle";
            this.barButtonItem_Rectangle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_绘制矩形_ItemClick);
            // 
            // barButtonItem_Polygon
            // 
            this.barButtonItem_Polygon.Caption = "多边形";
            this.barButtonItem_Polygon.Enabled = false;
            this.barButtonItem_Polygon.Hint = "多边形";
            this.barButtonItem_Polygon.Id = 253;
            this.barButtonItem_Polygon.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Polygon.ImageOptions.Image")));
            this.barButtonItem_Polygon.Name = "barButtonItem_Polygon";
            this.barButtonItem_Polygon.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_绘制多边形_ItemClick);
            // 
            // barButtonItem_RoundedRectangle
            // 
            this.barButtonItem_RoundedRectangle.Caption = "圆角矩形";
            this.barButtonItem_RoundedRectangle.Enabled = false;
            this.barButtonItem_RoundedRectangle.Hint = "圆角矩形";
            this.barButtonItem_RoundedRectangle.Id = 254;
            this.barButtonItem_RoundedRectangle.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_RoundedRectangle.ImageOptions.Image")));
            this.barButtonItem_RoundedRectangle.Name = "barButtonItem_RoundedRectangle";
            this.barButtonItem_RoundedRectangle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_绘制圆角矩形_ItemClick);
            // 
            // barButtonItem_String
            // 
            this.barButtonItem_String.Caption = "文字";
            this.barButtonItem_String.Enabled = false;
            this.barButtonItem_String.Hint = "文字";
            this.barButtonItem_String.Id = 256;
            this.barButtonItem_String.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_String.ImageOptions.Image")));
            this.barButtonItem_String.Name = "barButtonItem_String";
            this.barButtonItem_String.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_绘制文字_ItemClick);
            // 
            // barButtonItem_Picture
            // 
            this.barButtonItem_Picture.Caption = "图片";
            this.barButtonItem_Picture.Enabled = false;
            this.barButtonItem_Picture.Hint = "图片";
            this.barButtonItem_Picture.Id = 255;
            this.barButtonItem_Picture.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Picture.ImageOptions.Image")));
            this.barButtonItem_Picture.Name = "barButtonItem_Picture";
            this.barButtonItem_Picture.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.barButtonItem_Picture.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BarButtonItem_导入图片_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1264, 75);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 718);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1264, 24);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 75);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 643);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1264, 75);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 643);
            // 
            // dockManager1
            // 
            this.dockManager1.AutoHideContainers.AddRange(new DevExpress.XtraBars.Docking.AutoHideContainer[] {
            this.hideContainerRight});
            this.dockManager1.Form = this;
            this.dockManager1.HiddenPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel_ActiveX控件});
            this.dockManager1.MenuManager = this.barManager1;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelContainer_LeftBottom,
            this.panelContainer1,
            this.dockPanel_输出栏});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl"});
            // 
            // hideContainerRight
            // 
            this.hideContainerRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.hideContainerRight.Controls.Add(this.dockPanel_基本控件);
            this.hideContainerRight.Controls.Add(this.dockPanel_DCCE工控组件);
            this.hideContainerRight.Controls.Add(this.dockPanel_精灵控件);
            this.hideContainerRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.hideContainerRight.Location = new System.Drawing.Point(1242, 75);
            this.hideContainerRight.Name = "hideContainerRight";
            this.hideContainerRight.Size = new System.Drawing.Size(22, 643);
            // 
            // dockPanel_基本控件
            // 
            this.dockPanel_基本控件.Appearance.Options.UseTextOptions = true;
            this.dockPanel_基本控件.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.dockPanel_基本控件.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.dockPanel_基本控件.Controls.Add(this.controlContainer_基本控件);
            this.dockPanel_基本控件.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel_基本控件.FloatSize = new System.Drawing.Size(250, 200);
            this.dockPanel_基本控件.FloatVertical = true;
            this.dockPanel_基本控件.ID = new System.Guid("3fce40c8-fef0-4115-b790-dbb2b87182cd");
            this.dockPanel_基本控件.Location = new System.Drawing.Point(0, 0);
            this.dockPanel_基本控件.Name = "dockPanel_基本控件";
            this.dockPanel_基本控件.OriginalSize = new System.Drawing.Size(236, 496);
            this.dockPanel_基本控件.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel_基本控件.SavedIndex = 0;
            this.dockPanel_基本控件.Size = new System.Drawing.Size(236, 643);
            this.dockPanel_基本控件.Text = "基本控件";
            this.dockPanel_基本控件.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            this.dockPanel_基本控件.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler(this.DockPanel_Windows控件_VisibilityChanged);
            this.dockPanel_基本控件.SizeChanged += new System.EventHandler(this.dockPanel_基本控件_SizeChanged);
            // 
            // controlContainer_基本控件
            // 
            this.controlContainer_基本控件.Controls.Add(this.xtraScrollableControl_基本控件);
            this.controlContainer_基本控件.Location = new System.Drawing.Point(4, 26);
            this.controlContainer_基本控件.Name = "controlContainer_基本控件";
            this.controlContainer_基本控件.Size = new System.Drawing.Size(229, 614);
            this.controlContainer_基本控件.TabIndex = 0;
            // 
            // xtraScrollableControl_基本控件
            // 
            this.xtraScrollableControl_基本控件.AlwaysScrollActiveControlIntoView = false;
            this.xtraScrollableControl_基本控件.Controls.Add(this.navBarControl_基本控件);
            this.xtraScrollableControl_基本控件.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraScrollableControl_基本控件.Location = new System.Drawing.Point(0, 0);
            this.xtraScrollableControl_基本控件.Name = "xtraScrollableControl_基本控件";
            this.xtraScrollableControl_基本控件.Size = new System.Drawing.Size(229, 614);
            this.xtraScrollableControl_基本控件.TabIndex = 1;
            // 
            // navBarControl_基本控件
            // 
            this.navBarControl_基本控件.ActiveGroup = this.Windows控件;
            this.navBarControl_基本控件.Controls.Add(this.navBarGroupControlContainer_Windows控件);
            this.navBarControl_基本控件.Controls.Add(this.navBarGroupControlContainer_基本图元);
            this.navBarControl_基本控件.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl_基本控件.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.Windows控件,
            this.基本图元});
            this.navBarControl_基本控件.Location = new System.Drawing.Point(0, 0);
            this.navBarControl_基本控件.Name = "navBarControl_基本控件";
            this.navBarControl_基本控件.OptionsNavPane.ExpandedWidth = 229;
            this.navBarControl_基本控件.Size = new System.Drawing.Size(229, 614);
            this.navBarControl_基本控件.TabIndex = 0;
            this.navBarControl_基本控件.Text = "navBarControl_基本控件";
            this.navBarControl_基本控件.View = new DevExpress.XtraNavBar.ViewInfo.XP2ViewInfoRegistrator();
            // 
            // Windows控件
            // 
            this.Windows控件.Caption = "Windows控件";
            this.Windows控件.ControlContainer = this.navBarGroupControlContainer_Windows控件;
            this.Windows控件.Expanded = true;
            this.Windows控件.GroupClientHeight = 20;
            this.Windows控件.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.Windows控件.Name = "Windows控件";
            // 
            // navBarGroupControlContainer_Windows控件
            // 
            this.navBarGroupControlContainer_Windows控件.Controls.Add(this.listView_Windows控件);
            this.navBarGroupControlContainer_Windows控件.Name = "navBarGroupControlContainer_Windows控件";
            this.navBarGroupControlContainer_Windows控件.Size = new System.Drawing.Size(229, 571);
            this.navBarGroupControlContainer_Windows控件.TabIndex = 0;
            // 
            // listView_Windows控件
            // 
            this.listView_Windows控件.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_Windows控件.HideSelection = false;
            this.listView_Windows控件.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11});
            this.listView_Windows控件.LargeImageList = this.imageList_Windows控件;
            this.listView_Windows控件.Location = new System.Drawing.Point(0, 0);
            this.listView_Windows控件.Name = "listView_Windows控件";
            this.listView_Windows控件.Size = new System.Drawing.Size(229, 571);
            this.listView_Windows控件.TabIndex = 0;
            this.listView_Windows控件.UseCompatibleStateImageBehavior = false;
            this.listView_Windows控件.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_图元_MouseDown);
            // 
            // imageList_Windows控件
            // 
            this.imageList_Windows控件.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_Windows控件.ImageStream")));
            this.imageList_Windows控件.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_Windows控件.Images.SetKeyName(0, "Label.png");
            this.imageList_Windows控件.Images.SetKeyName(1, "CheckBox.png");
            this.imageList_Windows控件.Images.SetKeyName(2, "Button.png");
            this.imageList_Windows控件.Images.SetKeyName(3, "ListBox.png");
            this.imageList_Windows控件.Images.SetKeyName(4, "TextBox.png");
            this.imageList_Windows控件.Images.SetKeyName(5, "Image.png");
            this.imageList_Windows控件.Images.SetKeyName(6, "Calendar.png");
            this.imageList_Windows控件.Images.SetKeyName(7, "Timer.png");
            this.imageList_Windows控件.Images.SetKeyName(8, "ListView.png");
            this.imageList_Windows控件.Images.SetKeyName(9, "GridView.png");
            this.imageList_Windows控件.Images.SetKeyName(10, "StockFrame.png");
            this.imageList_Windows控件.Images.SetKeyName(11, "Image.png");
            // 
            // navBarGroupControlContainer_基本图元
            // 
            this.navBarGroupControlContainer_基本图元.Controls.Add(this.listView_基本图元);
            this.navBarGroupControlContainer_基本图元.Name = "navBarGroupControlContainer_基本图元";
            this.navBarGroupControlContainer_基本图元.Size = new System.Drawing.Size(230, 534);
            this.navBarGroupControlContainer_基本图元.TabIndex = 0;
            // 
            // listView_基本图元
            // 
            this.listView_基本图元.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_基本图元.HideSelection = false;
            this.listView_基本图元.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18});
            this.listView_基本图元.LargeImageList = this.imageList_图库_基本图元;
            this.listView_基本图元.Location = new System.Drawing.Point(0, 0);
            this.listView_基本图元.Name = "listView_基本图元";
            this.listView_基本图元.Size = new System.Drawing.Size(230, 534);
            this.listView_基本图元.TabIndex = 1;
            this.listView_基本图元.UseCompatibleStateImageBehavior = false;
            this.listView_基本图元.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_图元_MouseDown);
            // 
            // imageList_图库_基本图元
            // 
            this.imageList_图库_基本图元.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList_图库_基本图元.ImageStream")));
            this.imageList_图库_基本图元.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList_图库_基本图元.Images.SetKeyName(0, "line.png");
            this.imageList_图库_基本图元.Images.SetKeyName(1, "ell.png");
            this.imageList_图库_基本图元.Images.SetKeyName(2, "rect.png");
            this.imageList_图库_基本图元.Images.SetKeyName(3, "circleRect.png");
            this.imageList_图库_基本图元.Images.SetKeyName(4, "str.png");
            this.imageList_图库_基本图元.Images.SetKeyName(5, "san.png");
            this.imageList_图库_基本图元.Images.SetKeyName(6, "bezier.png");
            // 
            // 基本图元
            // 
            this.基本图元.Caption = "基本图元";
            this.基本图元.ControlContainer = this.navBarGroupControlContainer_基本图元;
            this.基本图元.GroupClientHeight = 20;
            this.基本图元.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.基本图元.Name = "基本图元";
            // 
            // dockPanel_DCCE工控组件
            // 
            this.dockPanel_DCCE工控组件.Appearance.Options.UseTextOptions = true;
            this.dockPanel_DCCE工控组件.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.dockPanel_DCCE工控组件.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.dockPanel_DCCE工控组件.Controls.Add(this.controlContainer_DCCE工控组件);
            this.dockPanel_DCCE工控组件.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel_DCCE工控组件.FloatSize = new System.Drawing.Size(250, 200);
            this.dockPanel_DCCE工控组件.FloatVertical = true;
            this.dockPanel_DCCE工控组件.ID = new System.Guid("7a88aeb1-b170-47a2-bd5b-32317bb61cb2");
            this.dockPanel_DCCE工控组件.Location = new System.Drawing.Point(0, 0);
            this.dockPanel_DCCE工控组件.Name = "dockPanel_DCCE工控组件";
            this.dockPanel_DCCE工控组件.OriginalSize = new System.Drawing.Size(234, 496);
            this.dockPanel_DCCE工控组件.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel_DCCE工控组件.SavedIndex = 1;
            this.dockPanel_DCCE工控组件.Size = new System.Drawing.Size(234, 643);
            this.dockPanel_DCCE工控组件.Text = "DCCE工控组件";
            this.dockPanel_DCCE工控组件.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            this.dockPanel_DCCE工控组件.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler(this.DockPanel_DCCE工控组件_VisibilityChanged);
            // 
            // controlContainer_DCCE工控组件
            // 
            this.controlContainer_DCCE工控组件.Controls.Add(this.navBarControl_DCCE工控组件);
            this.controlContainer_DCCE工控组件.Location = new System.Drawing.Point(4, 26);
            this.controlContainer_DCCE工控组件.Name = "controlContainer_DCCE工控组件";
            this.controlContainer_DCCE工控组件.Size = new System.Drawing.Size(227, 614);
            this.controlContainer_DCCE工控组件.TabIndex = 0;
            // 
            // navBarControl_DCCE工控组件
            // 
            this.navBarControl_DCCE工控组件.ActiveGroup = this.navBarGroup_DCCE开关;
            this.navBarControl_DCCE工控组件.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl_DCCE工控组件.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup_DCCE开关,
            this.navBarGroup_DCCE报警灯,
            this.navBarGroup_变量操作,
            this.navBarGroup_DCCE媒体控件,
            this.navBarGroup_文件操作,
            this.navBarGroup_图形操作,
            this.navBarGroup_数控,
            this.navBarGroup_数据管理});
            this.navBarControl_DCCE工控组件.Location = new System.Drawing.Point(0, 0);
            this.navBarControl_DCCE工控组件.Name = "navBarControl_DCCE工控组件";
            this.navBarControl_DCCE工控组件.OptionsNavPane.ExpandedWidth = 227;
            this.navBarControl_DCCE工控组件.Size = new System.Drawing.Size(227, 614);
            this.navBarControl_DCCE工控组件.TabIndex = 2;
            this.navBarControl_DCCE工控组件.Text = "navBarControl_DCCE工控组件";
            this.navBarControl_DCCE工控组件.View = new DevExpress.XtraNavBar.ViewInfo.XP2ViewInfoRegistrator();
            // 
            // navBarGroup_DCCE开关
            // 
            this.navBarGroup_DCCE开关.Caption = "开关";
            this.navBarGroup_DCCE开关.Expanded = true;
            this.navBarGroup_DCCE开关.Name = "navBarGroup_DCCE开关";
            // 
            // navBarGroup_DCCE报警灯
            // 
            this.navBarGroup_DCCE报警灯.Caption = "报警灯";
            this.navBarGroup_DCCE报警灯.Name = "navBarGroup_DCCE报警灯";
            // 
            // navBarGroup_变量操作
            // 
            this.navBarGroup_变量操作.Caption = "变量操作";
            this.navBarGroup_变量操作.Name = "navBarGroup_变量操作";
            // 
            // navBarGroup_DCCE媒体控件
            // 
            this.navBarGroup_DCCE媒体控件.Caption = "媒体控件";
            this.navBarGroup_DCCE媒体控件.Name = "navBarGroup_DCCE媒体控件";
            // 
            // navBarGroup_文件操作
            // 
            this.navBarGroup_文件操作.Caption = "文件操作";
            this.navBarGroup_文件操作.Name = "navBarGroup_文件操作";
            // 
            // navBarGroup_图形操作
            // 
            this.navBarGroup_图形操作.Caption = "图形操作";
            this.navBarGroup_图形操作.Name = "navBarGroup_图形操作";
            // 
            // navBarGroup_数控
            // 
            this.navBarGroup_数控.Caption = "数控";
            this.navBarGroup_数控.Name = "navBarGroup_数控";
            // 
            // navBarGroup_数据管理
            // 
            this.navBarGroup_数据管理.Caption = "数据管理";
            this.navBarGroup_数据管理.Name = "navBarGroup_数据管理";
            // 
            // dockPanel_精灵控件
            // 
            this.dockPanel_精灵控件.Appearance.Options.UseTextOptions = true;
            this.dockPanel_精灵控件.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.dockPanel_精灵控件.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.dockPanel_精灵控件.Controls.Add(this.controlContainer_精灵控件);
            this.dockPanel_精灵控件.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel_精灵控件.FloatSize = new System.Drawing.Size(250, 200);
            this.dockPanel_精灵控件.FloatVertical = true;
            this.dockPanel_精灵控件.ID = new System.Guid("7c33b350-aba4-42ef-aa3f-05a4a31819cf");
            this.dockPanel_精灵控件.Location = new System.Drawing.Point(0, 0);
            this.dockPanel_精灵控件.Name = "dockPanel_精灵控件";
            this.dockPanel_精灵控件.OriginalSize = new System.Drawing.Size(237, 496);
            this.dockPanel_精灵控件.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel_精灵控件.SavedIndex = 0;
            this.dockPanel_精灵控件.Size = new System.Drawing.Size(237, 605);
            this.dockPanel_精灵控件.Text = "精灵控件";
            this.dockPanel_精灵控件.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            this.dockPanel_精灵控件.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler(this.DockPanel_精灵控件_VisibilityChanged);
            // 
            // controlContainer_精灵控件
            // 
            this.controlContainer_精灵控件.Controls.Add(this.navBarControl_精灵控件);
            this.controlContainer_精灵控件.Location = new System.Drawing.Point(3, 25);
            this.controlContainer_精灵控件.Name = "controlContainer_精灵控件";
            this.controlContainer_精灵控件.Size = new System.Drawing.Size(231, 577);
            this.controlContainer_精灵控件.TabIndex = 0;
            // 
            // navBarControl_精灵控件
            // 
            this.navBarControl_精灵控件.ActiveGroup = this.navBarGroup_仪表;
            this.navBarControl_精灵控件.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl_精灵控件.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup_仪表,
            this.navBarGroup_开关,
            this.navBarGroup_报警灯,
            this.navBarGroup_按钮,
            this.navBarGroup_时钟,
            this.navBarGroup_泵,
            this.navBarGroup_游标,
            this.navBarGroup_电机,
            this.navBarGroup_管道,
            this.navBarGroup_罐,
            this.navBarGroup_阀门});
            this.navBarControl_精灵控件.Location = new System.Drawing.Point(0, 0);
            this.navBarControl_精灵控件.Name = "navBarControl_精灵控件";
            this.navBarControl_精灵控件.OptionsNavPane.ExpandedWidth = 244;
            this.navBarControl_精灵控件.Size = new System.Drawing.Size(231, 577);
            this.navBarControl_精灵控件.TabIndex = 1;
            this.navBarControl_精灵控件.Text = "navBarControl_精灵控件";
            this.navBarControl_精灵控件.View = new DevExpress.XtraNavBar.ViewInfo.XP2ViewInfoRegistrator();
            // 
            // navBarGroup_仪表
            // 
            this.navBarGroup_仪表.Caption = "仪表";
            this.navBarGroup_仪表.Expanded = true;
            this.navBarGroup_仪表.Name = "navBarGroup_仪表";
            // 
            // navBarGroup_开关
            // 
            this.navBarGroup_开关.Caption = "开关";
            this.navBarGroup_开关.Name = "navBarGroup_开关";
            // 
            // navBarGroup_报警灯
            // 
            this.navBarGroup_报警灯.Caption = "报警灯";
            this.navBarGroup_报警灯.Name = "navBarGroup_报警灯";
            // 
            // navBarGroup_按钮
            // 
            this.navBarGroup_按钮.Caption = "按钮";
            this.navBarGroup_按钮.Name = "navBarGroup_按钮";
            // 
            // navBarGroup_时钟
            // 
            this.navBarGroup_时钟.Caption = "时钟";
            this.navBarGroup_时钟.Name = "navBarGroup_时钟";
            // 
            // navBarGroup_泵
            // 
            this.navBarGroup_泵.Caption = "泵";
            this.navBarGroup_泵.Name = "navBarGroup_泵";
            // 
            // navBarGroup_游标
            // 
            this.navBarGroup_游标.Caption = "游标";
            this.navBarGroup_游标.Name = "navBarGroup_游标";
            // 
            // navBarGroup_电机
            // 
            this.navBarGroup_电机.Caption = "电机";
            this.navBarGroup_电机.Name = "navBarGroup_电机";
            // 
            // navBarGroup_管道
            // 
            this.navBarGroup_管道.Caption = "管道";
            this.navBarGroup_管道.Name = "navBarGroup_管道";
            // 
            // navBarGroup_罐
            // 
            this.navBarGroup_罐.Caption = "罐";
            this.navBarGroup_罐.Name = "navBarGroup_罐";
            // 
            // navBarGroup_阀门
            // 
            this.navBarGroup_阀门.Caption = "阀门";
            this.navBarGroup_阀门.Name = "navBarGroup_阀门";
            // 
            // dockPanel_ActiveX控件
            // 
            this.dockPanel_ActiveX控件.Appearance.Options.UseTextOptions = true;
            this.dockPanel_ActiveX控件.Appearance.TextOptions.Trimming = DevExpress.Utils.Trimming.None;
            this.dockPanel_ActiveX控件.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.dockPanel_ActiveX控件.Controls.Add(this.controlContainer_ActiveX控件);
            this.dockPanel_ActiveX控件.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel_ActiveX控件.FloatSize = new System.Drawing.Size(250, 200);
            this.dockPanel_ActiveX控件.FloatVertical = true;
            this.dockPanel_ActiveX控件.ID = new System.Guid("a8d22793-fe34-4675-9f3b-1e2ce7854bf8");
            this.dockPanel_ActiveX控件.Location = new System.Drawing.Point(0, 0);
            this.dockPanel_ActiveX控件.Name = "dockPanel_ActiveX控件";
            this.dockPanel_ActiveX控件.OriginalSize = new System.Drawing.Size(234, 496);
            this.dockPanel_ActiveX控件.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanel_ActiveX控件.SavedIndex = 0;
            this.dockPanel_ActiveX控件.Size = new System.Drawing.Size(234, 605);
            this.dockPanel_ActiveX控件.Text = "ActiveX";
            this.dockPanel_ActiveX控件.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            this.dockPanel_ActiveX控件.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler(this.DockPanel_ActiveX控件_VisibilityChanged);
            this.dockPanel_ActiveX控件.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dockPanel_ActiveX控件_MouseClick);
            // 
            // controlContainer_ActiveX控件
            // 
            this.controlContainer_ActiveX控件.Controls.Add(this.ActiveXFinderButton);
            this.controlContainer_ActiveX控件.Controls.Add(this.listView_ActiveX控件);
            this.controlContainer_ActiveX控件.Location = new System.Drawing.Point(3, 25);
            this.controlContainer_ActiveX控件.Name = "controlContainer_ActiveX控件";
            this.controlContainer_ActiveX控件.Size = new System.Drawing.Size(228, 577);
            this.controlContainer_ActiveX控件.TabIndex = 0;
            // 
            // ActiveXFinderButton
            // 
            this.ActiveXFinderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ActiveXFinderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ActiveXFinderButton.Location = new System.Drawing.Point(204, 0);
            this.ActiveXFinderButton.Name = "ActiveXFinderButton";
            this.ActiveXFinderButton.Size = new System.Drawing.Size(21, 21);
            this.ActiveXFinderButton.TabIndex = 0;
            this.ActiveXFinderButton.Text = "+";
            this.ActiveXFinderButton.UseVisualStyleBackColor = true;
            this.ActiveXFinderButton.Click += new System.EventHandler(this.ActiveXFinderButton_Click);
            // 
            // listView_ActiveX控件
            // 
            this.listView_ActiveX控件.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_ActiveX控件.HideSelection = false;
            this.listView_ActiveX控件.Location = new System.Drawing.Point(0, 0);
            this.listView_ActiveX控件.Name = "listView_ActiveX控件";
            this.listView_ActiveX控件.Size = new System.Drawing.Size(228, 577);
            this.listView_ActiveX控件.TabIndex = 1;
            this.listView_ActiveX控件.UseCompatibleStateImageBehavior = false;
            this.listView_ActiveX控件.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listView_图元_MouseDown);
            // 
            // dockPanelContainer_LeftBottom
            // 
            this.dockPanelContainer_LeftBottom.ActiveChild = this.dockPanel_属性;
            this.dockPanelContainer_LeftBottom.Controls.Add(this.dockPanel_属性);
            this.dockPanelContainer_LeftBottom.Controls.Add(this.dockPanel_事件);
            this.dockPanelContainer_LeftBottom.Controls.Add(this.dockPanel_动画);
            this.dockPanelContainer_LeftBottom.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanelContainer_LeftBottom.ID = new System.Guid("1fa20855-f54f-4735-acd4-3c3f8ee400fc");
            this.dockPanelContainer_LeftBottom.Location = new System.Drawing.Point(1037, 75);
            this.dockPanelContainer_LeftBottom.Name = "dockPanelContainer_LeftBottom";
            this.dockPanelContainer_LeftBottom.OriginalSize = new System.Drawing.Size(205, 264);
            this.dockPanelContainer_LeftBottom.Size = new System.Drawing.Size(205, 643);
            this.dockPanelContainer_LeftBottom.Tabbed = true;
            this.dockPanelContainer_LeftBottom.Text = "dockPanelContainer_LeftBottom";
            // 
            // dockPanel_属性
            // 
            this.dockPanel_属性.Controls.Add(this.controlContainer_属性);
            this.dockPanel_属性.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel_属性.ID = new System.Guid("00206914-4add-4981-9486-087c4939f841");
            this.dockPanel_属性.Location = new System.Drawing.Point(4, 26);
            this.dockPanel_属性.Name = "dockPanel_属性";
            this.dockPanel_属性.OriginalSize = new System.Drawing.Size(198, 587);
            this.dockPanel_属性.Size = new System.Drawing.Size(198, 587);
            this.dockPanel_属性.Text = "属性";
            this.dockPanel_属性.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler(this.dockPanel_属性_VisibilityChanged);
            // 
            // controlContainer_属性
            // 
            this.controlContainer_属性.Controls.Add(this.myPropertyGrid1);
            this.controlContainer_属性.Location = new System.Drawing.Point(0, 0);
            this.controlContainer_属性.Name = "controlContainer_属性";
            this.controlContainer_属性.Size = new System.Drawing.Size(198, 587);
            this.controlContainer_属性.TabIndex = 0;
            // 
            // dockPanel_事件
            // 
            this.dockPanel_事件.Controls.Add(this.controlContainer_事件);
            this.dockPanel_事件.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel_事件.FloatVertical = true;
            this.dockPanel_事件.ID = new System.Guid("edbb18d3-d7be-4343-b0e8-b13c7aa999e3");
            this.dockPanel_事件.Location = new System.Drawing.Point(4, 26);
            this.dockPanel_事件.Name = "dockPanel_事件";
            this.dockPanel_事件.OriginalSize = new System.Drawing.Size(198, 587);
            this.dockPanel_事件.Size = new System.Drawing.Size(198, 587);
            this.dockPanel_事件.Text = "事件";
            this.dockPanel_事件.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler(this.dockPanel_事件_VisibilityChanged);
            // 
            // controlContainer_事件
            // 
            this.controlContainer_事件.Controls.Add(this.listView_事件);
            this.controlContainer_事件.Location = new System.Drawing.Point(0, 0);
            this.controlContainer_事件.Name = "controlContainer_事件";
            this.controlContainer_事件.Size = new System.Drawing.Size(198, 587);
            this.controlContainer_事件.TabIndex = 0;
            // 
            // dockPanel_动画
            // 
            this.dockPanel_动画.Controls.Add(this.controlContainer_动画);
            this.dockPanel_动画.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel_动画.ID = new System.Guid("3bd6cd47-e5f6-4685-8dfc-5139f5b7d271");
            this.dockPanel_动画.Location = new System.Drawing.Point(4, 26);
            this.dockPanel_动画.Name = "dockPanel_动画";
            this.dockPanel_动画.OriginalSize = new System.Drawing.Size(198, 587);
            this.dockPanel_动画.Size = new System.Drawing.Size(198, 587);
            this.dockPanel_动画.Text = "动画";
            this.dockPanel_动画.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler(this.dockPanel_动画_VisibilityChanged);
            // 
            // controlContainer_动画
            // 
            this.controlContainer_动画.Controls.Add(this.dataGridView1);
            this.controlContainer_动画.Location = new System.Drawing.Point(0, 0);
            this.controlContainer_动画.Name = "controlContainer_动画";
            this.controlContainer_动画.Size = new System.Drawing.Size(198, 587);
            this.controlContainer_动画.TabIndex = 0;
            // 
            // panelContainer1
            // 
            this.panelContainer1.ActiveChild = this.dockPanel_导航栏;
            this.panelContainer1.Controls.Add(this.dockPanel_导航栏);
            this.panelContainer1.Controls.Add(this.dockPanel_对象浏览器);
            this.panelContainer1.Controls.Add(this.dockPanel_变量浏览器);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.panelContainer1.FloatVertical = true;
            this.panelContainer1.ID = new System.Guid("c138eeb8-e571-4153-be56-36563be3e8a7");
            this.panelContainer1.Location = new System.Drawing.Point(0, 75);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(234, 486);
            this.panelContainer1.Size = new System.Drawing.Size(234, 643);
            this.panelContainer1.Tabbed = true;
            this.panelContainer1.Text = "panelContainer1";
            // 
            // dockPanel_导航栏
            // 
            this.dockPanel_导航栏.Controls.Add(this.controlContainer_导航栏);
            this.dockPanel_导航栏.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel_导航栏.FloatVertical = true;
            this.dockPanel_导航栏.ID = new System.Guid("da19e6ce-b03c-4ce2-a9ec-942236eae40b");
            this.dockPanel_导航栏.Location = new System.Drawing.Point(3, 26);
            this.dockPanel_导航栏.Name = "dockPanel_导航栏";
            this.dockPanel_导航栏.OriginalSize = new System.Drawing.Size(227, 587);
            this.dockPanel_导航栏.Size = new System.Drawing.Size(227, 587);
            this.dockPanel_导航栏.Text = "导航栏";
            this.dockPanel_导航栏.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler(this.dockPanel_导航栏_VisibilityChanged);
            // 
            // controlContainer_导航栏
            // 
            this.controlContainer_导航栏.Controls.Add(this.treeView_工程导航);
            this.controlContainer_导航栏.Location = new System.Drawing.Point(0, 0);
            this.controlContainer_导航栏.Name = "controlContainer_导航栏";
            this.controlContainer_导航栏.Size = new System.Drawing.Size(227, 587);
            this.controlContainer_导航栏.TabIndex = 0;
            // 
            // dockPanel_对象浏览器
            // 
            this.dockPanel_对象浏览器.Controls.Add(this.dockPanelContainer_对象浏览器);
            this.dockPanel_对象浏览器.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel_对象浏览器.FloatVertical = true;
            this.dockPanel_对象浏览器.ID = new System.Guid("5928126a-a76e-442c-8a50-260e7b872a6d");
            this.dockPanel_对象浏览器.Location = new System.Drawing.Point(3, 26);
            this.dockPanel_对象浏览器.Name = "dockPanel_对象浏览器";
            this.dockPanel_对象浏览器.OriginalSize = new System.Drawing.Size(227, 587);
            this.dockPanel_对象浏览器.Size = new System.Drawing.Size(227, 587);
            this.dockPanel_对象浏览器.Text = "对象浏览器";
            this.dockPanel_对象浏览器.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler(this.dockPanel_对象浏览器_VisibilityChanged);
            // 
            // dockPanelContainer_对象浏览器
            // 
            this.dockPanelContainer_对象浏览器.Controls.Add(this.objView_Page);
            this.dockPanelContainer_对象浏览器.Location = new System.Drawing.Point(0, 0);
            this.dockPanelContainer_对象浏览器.Name = "dockPanelContainer_对象浏览器";
            this.dockPanelContainer_对象浏览器.Size = new System.Drawing.Size(227, 587);
            this.dockPanelContainer_对象浏览器.TabIndex = 0;
            // 
            // objView_Page
            // 
            this.objView_Page.AutoScroll = true;
            this.objView_Page.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objView_Page.Location = new System.Drawing.Point(0, 0);
            this.objView_Page.Name = "objView_Page";
            this.objView_Page.Size = new System.Drawing.Size(227, 587);
            this.objView_Page.TabIndex = 12;
            // 
            // dockPanel_变量浏览器
            // 
            this.dockPanel_变量浏览器.Controls.Add(this.dockPanel1_Container);
            this.dockPanel_变量浏览器.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanel_变量浏览器.ID = new System.Guid("cc0525de-1b85-42f4-9330-41027deda3d0");
            this.dockPanel_变量浏览器.Location = new System.Drawing.Point(3, 26);
            this.dockPanel_变量浏览器.Name = "dockPanel_变量浏览器";
            this.dockPanel_变量浏览器.OriginalSize = new System.Drawing.Size(227, 587);
            this.dockPanel_变量浏览器.Size = new System.Drawing.Size(227, 587);
            this.dockPanel_变量浏览器.Text = "变量浏览器";
            this.dockPanel_变量浏览器.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler(this.dockPanel_变量浏览器_VisibilityChanged);
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.varExplore);
            this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(227, 587);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // varExplore
            // 
            this.varExplore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.varExplore.Location = new System.Drawing.Point(0, 0);
            this.varExplore.Name = "varExplore";
            this.varExplore.Size = new System.Drawing.Size(227, 587);
            this.varExplore.TabIndex = 0;
            // 
            // dockPanel_输出栏
            // 
            this.dockPanel_输出栏.Controls.Add(this.controlContainer_输出栏);
            this.dockPanel_输出栏.Dock = DevExpress.XtraBars.Docking.DockingStyle.Bottom;
            this.dockPanel_输出栏.FloatSize = new System.Drawing.Size(496, 200);
            this.dockPanel_输出栏.FloatVertical = true;
            this.dockPanel_输出栏.ID = new System.Guid("431ea039-70bb-4a98-9b08-c84c31e1f79e");
            this.dockPanel_输出栏.Location = new System.Drawing.Point(234, 608);
            this.dockPanel_输出栏.Name = "dockPanel_输出栏";
            this.dockPanel_输出栏.OriginalSize = new System.Drawing.Size(791, 110);
            this.dockPanel_输出栏.Size = new System.Drawing.Size(803, 110);
            this.dockPanel_输出栏.Text = "输出栏";
            this.dockPanel_输出栏.VisibilityChanged += new DevExpress.XtraBars.Docking.VisibilityChangedEventHandler(this.dockPanel_输出栏_VisibilityChanged);
            // 
            // controlContainer_输出栏
            // 
            this.controlContainer_输出栏.Controls.Add(this.richTextBox1);
            this.controlContainer_输出栏.Location = new System.Drawing.Point(3, 27);
            this.controlContainer_输出栏.Name = "controlContainer_输出栏";
            this.controlContainer_输出栏.Size = new System.Drawing.Size(797, 80);
            this.controlContainer_输出栏.TabIndex = 0;
            // 
            // barSubItem6
            // 
            this.barSubItem6.Id = 215;
            this.barSubItem6.Name = "barSubItem6";
            // 
            // barSubItem_导入
            // 
            this.barSubItem_导入.Id = 277;
            this.barSubItem_导入.Name = "barSubItem_导入";
            // 
            // barCheckItem2
            // 
            this.barCheckItem2.BindableChecked = true;
            this.barCheckItem2.Caption = "工具箱";
            this.barCheckItem2.Checked = true;
            this.barCheckItem2.Id = 37;
            this.barCheckItem2.Name = "barCheckItem2";
            // 
            // barCheckItem9
            // 
            this.barCheckItem9.Caption = "全屏";
            this.barCheckItem9.Id = 44;
            this.barCheckItem9.Name = "barCheckItem9";
            // 
            // barButtonItem32
            // 
            this.barButtonItem32.Caption = "编辑布局";
            this.barButtonItem32.Id = 59;
            this.barButtonItem32.Name = "barButtonItem32";
            // 
            // barSubItem14
            // 
            this.barSubItem14.Id = 216;
            this.barSubItem14.Name = "barSubItem14";
            // 
            // barSubItem15
            // 
            this.barSubItem15.Id = 217;
            this.barSubItem15.Name = "barSubItem15";
            // 
            // barSubItem16
            // 
            this.barSubItem16.Id = 218;
            this.barSubItem16.Name = "barSubItem16";
            // 
            // barButtonItem34
            // 
            this.barButtonItem34.Id = 219;
            this.barButtonItem34.Name = "barButtonItem34";
            // 
            // barButtonItem35
            // 
            this.barButtonItem35.Id = 220;
            this.barButtonItem35.Name = "barButtonItem35";
            // 
            // barButtonItem38
            // 
            this.barButtonItem38.Id = 241;
            this.barButtonItem38.Name = "barButtonItem38";
            // 
            // barButtonItem39
            // 
            this.barButtonItem39.Id = 242;
            this.barButtonItem39.Name = "barButtonItem39";
            // 
            // barButtonItem40
            // 
            this.barButtonItem40.Id = 243;
            this.barButtonItem40.Name = "barButtonItem40";
            // 
            // barButtonItem41
            // 
            this.barButtonItem41.Id = 244;
            this.barButtonItem41.Name = "barButtonItem41";
            // 
            // barButtonItem63
            // 
            this.barButtonItem63.Id = 221;
            this.barButtonItem63.Name = "barButtonItem63";
            // 
            // barButtonItem64
            // 
            this.barButtonItem64.Id = 222;
            this.barButtonItem64.Name = "barButtonItem64";
            // 
            // barButtonItem65
            // 
            this.barButtonItem65.Id = 223;
            this.barButtonItem65.Name = "barButtonItem65";
            // 
            // barButtonItem66
            // 
            this.barButtonItem66.Id = 224;
            this.barButtonItem66.Name = "barButtonItem66";
            // 
            // barButtonItem67
            // 
            this.barButtonItem67.Id = 225;
            this.barButtonItem67.Name = "barButtonItem67";
            // 
            // barButtonItem68
            // 
            this.barButtonItem68.Id = 226;
            this.barButtonItem68.Name = "barButtonItem68";
            // 
            // barButtonItem69
            // 
            this.barButtonItem69.Id = 227;
            this.barButtonItem69.Name = "barButtonItem69";
            // 
            // barButtonItem70
            // 
            this.barButtonItem70.Caption = "实时曲线";
            this.barButtonItem70.Id = 104;
            this.barButtonItem70.Name = "barButtonItem70";
            this.barButtonItem70.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem70_ItemClick);
            // 
            // barButtonItem71
            // 
            this.barButtonItem71.Caption = "历史曲线";
            this.barButtonItem71.Id = 105;
            this.barButtonItem71.Name = "barButtonItem71";
            this.barButtonItem71.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem71_ItemClick);
            // 
            // barButtonItem72
            // 
            this.barButtonItem72.Caption = "按钮";
            this.barButtonItem72.Id = 106;
            this.barButtonItem72.Name = "barButtonItem72";
            this.barButtonItem72.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem72_ItemClick);
            // 
            // barButtonItem73
            // 
            this.barButtonItem73.Caption = "多选框";
            this.barButtonItem73.Id = 107;
            this.barButtonItem73.Name = "barButtonItem73";
            this.barButtonItem73.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem73_ItemClick);
            // 
            // barButtonItem74
            // 
            this.barButtonItem74.Caption = "标签";
            this.barButtonItem74.Id = 108;
            this.barButtonItem74.Name = "barButtonItem74";
            this.barButtonItem74.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem74_ItemClick);
            // 
            // barButtonItem75
            // 
            this.barButtonItem75.Caption = "文本框";
            this.barButtonItem75.Id = 109;
            this.barButtonItem75.Name = "barButtonItem75";
            this.barButtonItem75.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem75_ItemClick);
            // 
            // barButtonItem76
            // 
            this.barButtonItem76.Caption = "下拉列表";
            this.barButtonItem76.Id = 110;
            this.barButtonItem76.Name = "barButtonItem76";
            this.barButtonItem76.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem76_ItemClick);
            // 
            // barButtonItem77
            // 
            this.barButtonItem77.Caption = "日历控件";
            this.barButtonItem77.Id = 111;
            this.barButtonItem77.Name = "barButtonItem77";
            this.barButtonItem77.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem77_ItemClick);
            // 
            // barButtonItem78
            // 
            this.barButtonItem78.Caption = "图片框";
            this.barButtonItem78.Id = 112;
            this.barButtonItem78.Name = "barButtonItem78";
            this.barButtonItem78.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem78_ItemClick);
            // 
            // barButtonItem79
            // 
            this.barButtonItem79.Caption = "分组框";
            this.barButtonItem79.Id = 113;
            this.barButtonItem79.Name = "barButtonItem79";
            this.barButtonItem79.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem79_ItemClick);
            // 
            // barButtonItem80
            // 
            this.barButtonItem80.Caption = "数据视图";
            this.barButtonItem80.Id = 114;
            this.barButtonItem80.Name = "barButtonItem80";
            this.barButtonItem80.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem80_ItemClick);
            // 
            // barButtonItem81
            // 
            this.barButtonItem81.Id = 228;
            this.barButtonItem81.Name = "barButtonItem81";
            // 
            // barCheckItem13
            // 
            this.barCheckItem13.BindableChecked = true;
            this.barCheckItem13.Caption = "工具箱";
            this.barCheckItem13.Checked = true;
            this.barCheckItem13.Id = 143;
            this.barCheckItem13.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barCheckItem13.ImageOptions.Image")));
            this.barCheckItem13.Name = "barCheckItem13";
            // 
            // menubarCheckItem_事件
            // 
            this.menubarCheckItem_事件.BindableChecked = true;
            this.menubarCheckItem_事件.Caption = "事件框(&E)";
            this.menubarCheckItem_事件.Checked = true;
            this.menubarCheckItem_事件.Id = 172;
            this.menubarCheckItem_事件.Name = "menubarCheckItem_事件";
            // 
            // barButtonItem119
            // 
            this.barButtonItem119.Caption = "动画框";
            this.barButtonItem119.Id = 173;
            this.barButtonItem119.Name = "barButtonItem119";
            // 
            // barButtonItem120
            // 
            this.barButtonItem120.Caption = "动画框";
            this.barButtonItem120.Id = 174;
            this.barButtonItem120.Name = "barButtonItem120";
            // 
            // menubarCheckItem_默认主题
            // 
            this.menubarCheckItem_默认主题.Id = 281;
            this.menubarCheckItem_默认主题.Name = "menubarCheckItem_默认主题";
            // 
            // barButtonItem130
            // 
            this.barButtonItem130.ActAsDropDown = true;
            this.barButtonItem130.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barButtonItem130.Caption = "填充配置";
            this.barButtonItem130.DropDownControl = this.colorEditControl1;
            this.barButtonItem130.Hint = "填充配置";
            this.barButtonItem130.Id = 189;
            this.barButtonItem130.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem130.ImageOptions.Image")));
            this.barButtonItem130.Name = "barButtonItem130";
            // 
            // colorEditControl1
            // 
            this.colorEditControl1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.colorEditControl1.Appearance.Options.UseBackColor = true;
            this.colorEditControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.colorEditControl1.Location = new System.Drawing.Point(475, 164);
            this.colorEditControl1.Manager = this.barManager1;
            this.colorEditControl1.Name = "colorEditControl1";
            this.colorEditControl1.Size = new System.Drawing.Size(150, 230);
            this.colorEditControl1.TabIndex = 35;
            this.colorEditControl1.Visible = false;
            // 
            // barEditItem4
            // 
            this.barEditItem4.Caption = "填充色";
            this.barEditItem4.Edit = this.repositoryItemColorEdit1;
            this.barEditItem4.Hint = "填充色";
            this.barEditItem4.Id = 191;
            this.barEditItem4.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barEditItem4.ImageOptions.Image")));
            this.barEditItem4.Name = "barEditItem4";
            this.barEditItem4.EditValueChanged += new System.EventHandler(this.BarEditItem4_EditValueChanged);
            // 
            // repositoryItemColorEdit1
            // 
            this.repositoryItemColorEdit1.AutoHeight = false;
            this.repositoryItemColorEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            this.repositoryItemColorEdit1.NullColor = System.Drawing.Color.Empty;
            // 
            // barStaticItem3
            // 
            this.barStaticItem3.Caption = "边线";
            this.barStaticItem3.Id = 192;
            this.barStaticItem3.Name = "barStaticItem3";
            // 
            // barEditItem3
            // 
            this.barEditItem3.Caption = "边线颜色";
            this.barEditItem3.Edit = this.repositoryItemColorEdit2;
            this.barEditItem3.Hint = "边线颜色";
            this.barEditItem3.Id = 193;
            this.barEditItem3.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barEditItem3.ImageOptions.Image")));
            this.barEditItem3.Name = "barEditItem3";
            this.barEditItem3.EditValueChanged += new System.EventHandler(this.barEditItem3_EditValueChanged);
            // 
            // repositoryItemColorEdit2
            // 
            this.repositoryItemColorEdit2.AutoHeight = false;
            this.repositoryItemColorEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemColorEdit2.Name = "repositoryItemColorEdit2";
            this.repositoryItemColorEdit2.NullColor = System.Drawing.Color.Empty;
            // 
            // barButtonItem131
            // 
            this.barButtonItem131.Caption = "添加ActiveX组件";
            this.barButtonItem131.Id = 194;
            this.barButtonItem131.Name = "barButtonItem131";
            // 
            // barButtonItem132
            // 
            this.barButtonItem132.Caption = "图库";
            this.barButtonItem132.Id = 199;
            this.barButtonItem132.Name = "barButtonItem132";
            // 
            // barButtonItem133
            // 
            this.barButtonItem133.Caption = "Windows控件";
            this.barButtonItem133.Id = 200;
            this.barButtonItem133.Name = "barButtonItem133";
            // 
            // barButtonItem134
            // 
            this.barButtonItem134.Caption = "DCCE工控组件";
            this.barButtonItem134.Id = 201;
            this.barButtonItem134.Name = "barButtonItem134";
            // 
            // barButtonItem135
            // 
            this.barButtonItem135.Caption = "ActiveX控件";
            this.barButtonItem135.Id = 202;
            this.barButtonItem135.Name = "barButtonItem135";
            // 
            // barButtonItem136
            // 
            this.barButtonItem136.Caption = "Windows控件";
            this.barButtonItem136.Id = 204;
            this.barButtonItem136.Name = "barButtonItem136";
            // 
            // barEditItem5
            // 
            this.barEditItem5.Caption = "边线宽度";
            this.barEditItem5.Edit = this.repositoryItemSpinEdit1;
            this.barEditItem5.Hint = "边线宽度";
            this.barEditItem5.Id = 209;
            this.barEditItem5.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barEditItem5.ImageOptions.Image")));
            this.barEditItem5.Name = "barEditItem5";
            this.barEditItem5.EditValueChanged += new System.EventHandler(this.barEditItem5_EditValueChanged);
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit1.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Default;
            this.repositoryItemSpinEdit1.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // barButtonItem139
            // 
            this.barButtonItem139.Id = 278;
            this.barButtonItem139.Name = "barButtonItem139";
            // 
            // barButtonItem58
            // 
            this.barButtonItem58.Id = 279;
            this.barButtonItem58.Name = "barButtonItem58";
            // 
            // barButtonItem59
            // 
            this.barButtonItem59.Id = 280;
            this.barButtonItem59.Name = "barButtonItem59";
            // 
            // barButtonItem140
            // 
            this.barButtonItem140.Id = 245;
            this.barButtonItem140.Name = "barButtonItem140";
            // 
            // barButtonItem36
            // 
            this.barButtonItem36.Id = 285;
            this.barButtonItem36.Name = "barButtonItem36";
            // 
            // barButtonItem_CloseProject
            // 
            this.barButtonItem_CloseProject.Id = 287;
            this.barButtonItem_CloseProject.Name = "barButtonItem_CloseProject";
            // 
            // barButtonItem20
            // 
            this.barButtonItem20.Caption = "barButtonItem20";
            this.barButtonItem20.Id = 286;
            this.barButtonItem20.Name = "barButtonItem20";
            // 
            // BarButtonItem_RecentlyProject
            // 
            this.BarButtonItem_RecentlyProject.Caption = "最近打开的工程";
            this.BarButtonItem_RecentlyProject.Id = 289;
            this.BarButtonItem_RecentlyProject.Name = "BarButtonItem_RecentlyProject";
            // 
            // repositoryItemZoomTrackBar1
            // 
            this.repositoryItemZoomTrackBar1.LargeChange = 100;
            this.repositoryItemZoomTrackBar1.Maximum = 100;
            this.repositoryItemZoomTrackBar1.Name = "repositoryItemZoomTrackBar1";
            this.repositoryItemZoomTrackBar1.SmallChange = 10;
            // 
            // repositoryItemComboBox2
            // 
            this.repositoryItemComboBox2.AutoHeight = false;
            this.repositoryItemComboBox2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox2.Name = "repositoryItemComboBox2";
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem131)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // popupMenu2
            // 
            this.popupMenu2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem139),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem58),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem59)});
            this.popupMenu2.Manager = this.barManager1;
            this.popupMenu2.Name = "popupMenu2";
            // 
            // ToolStripMenuItem_页面组_新建页面组
            // 
            this.ToolStripMenuItem_页面组_新建页面组.Name = "ToolStripMenuItem_页面组_新建页面组";
            this.ToolStripMenuItem_页面组_新建页面组.Size = new System.Drawing.Size(162, 22);
            this.ToolStripMenuItem_页面组_新建页面组.Text = "新建页面组(&G)";
            this.ToolStripMenuItem_页面组_新建页面组.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_新建页面组_Click);
            // 
            // ToolStripMenuItem_页面组_重命名
            // 
            this.ToolStripMenuItem_页面组_重命名.Name = "ToolStripMenuItem_页面组_重命名";
            this.ToolStripMenuItem_页面组_重命名.Size = new System.Drawing.Size(162, 22);
            this.ToolStripMenuItem_页面组_重命名.Text = "重命名(&R)";
            this.ToolStripMenuItem_页面组_重命名.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_重命名_Click);
            // 
            // ToolStripMenuItem_页面组_删除
            // 
            this.ToolStripMenuItem_页面组_删除.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_页面组_删除.Image")));
            this.ToolStripMenuItem_页面组_删除.Name = "ToolStripMenuItem_页面组_删除";
            this.ToolStripMenuItem_页面组_删除.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.ToolStripMenuItem_页面组_删除.Size = new System.Drawing.Size(162, 22);
            this.ToolStripMenuItem_页面组_删除.Text = "删除(&D)";
            this.ToolStripMenuItem_页面组_删除.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_删除_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(159, 6);
            // 
            // ToolStripMenuItem_页面组_新建页面
            // 
            this.ToolStripMenuItem_页面组_新建页面.Name = "ToolStripMenuItem_页面组_新建页面";
            this.ToolStripMenuItem_页面组_新建页面.Size = new System.Drawing.Size(162, 22);
            this.ToolStripMenuItem_页面组_新建页面.Text = "新建页面(&P)";
            this.ToolStripMenuItem_页面组_新建页面.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_新建页面_Click);
            // 
            // ToolStripMenuItem_页面组_导入页面
            // 
            this.ToolStripMenuItem_页面组_导入页面.Name = "ToolStripMenuItem_页面组_导入页面";
            this.ToolStripMenuItem_页面组_导入页面.Size = new System.Drawing.Size(162, 22);
            this.ToolStripMenuItem_页面组_导入页面.Text = "导入页面(&I)";
            this.ToolStripMenuItem_页面组_导入页面.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_导入页面_Click);
            // 
            // ToolStripMenuItem_页面组_粘贴页面
            // 
            this.ToolStripMenuItem_页面组_粘贴页面.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItem_页面组_粘贴页面.Image")));
            this.ToolStripMenuItem_页面组_粘贴页面.Name = "ToolStripMenuItem_页面组_粘贴页面";
            this.ToolStripMenuItem_页面组_粘贴页面.Size = new System.Drawing.Size(162, 22);
            this.ToolStripMenuItem_页面组_粘贴页面.Text = "粘贴页面(&P)";
            this.ToolStripMenuItem_页面组_粘贴页面.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_粘贴页面_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(159, 6);
            // 
            // ToolStripMenuItem_页面组_上移
            // 
            this.ToolStripMenuItem_页面组_上移.Name = "ToolStripMenuItem_页面组_上移";
            this.ToolStripMenuItem_页面组_上移.Size = new System.Drawing.Size(162, 22);
            this.ToolStripMenuItem_页面组_上移.Text = "上移(&U)";
            this.ToolStripMenuItem_页面组_上移.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_上移_Click);
            // 
            // ToolStripMenuItem_页面组_下移
            // 
            this.ToolStripMenuItem_页面组_下移.Name = "ToolStripMenuItem_页面组_下移";
            this.ToolStripMenuItem_页面组_下移.Size = new System.Drawing.Size(162, 22);
            this.ToolStripMenuItem_页面组_下移.Text = "下移(&D)";
            this.ToolStripMenuItem_页面组_下移.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_下移_Click);
            // 
            // ToolStripMenuItem_页面组_移至顶层
            // 
            this.ToolStripMenuItem_页面组_移至顶层.Name = "ToolStripMenuItem_页面组_移至顶层";
            this.ToolStripMenuItem_页面组_移至顶层.Size = new System.Drawing.Size(162, 22);
            this.ToolStripMenuItem_页面组_移至顶层.Text = "移至顶层(&T)";
            this.ToolStripMenuItem_页面组_移至顶层.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_移至顶层_Click);
            // 
            // ToolStripMenuItem_页面组_移至底层
            // 
            this.ToolStripMenuItem_页面组_移至底层.Name = "ToolStripMenuItem_页面组_移至底层";
            this.ToolStripMenuItem_页面组_移至底层.Size = new System.Drawing.Size(162, 22);
            this.ToolStripMenuItem_页面组_移至底层.Text = "移至底层(&B)";
            this.ToolStripMenuItem_页面组_移至底层.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_移至底层_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(159, 6);
            // 
            // ToolStripMenuItem_页面组_关闭所有
            // 
            this.ToolStripMenuItem_页面组_关闭所有.Name = "ToolStripMenuItem_页面组_关闭所有";
            this.ToolStripMenuItem_页面组_关闭所有.Size = new System.Drawing.Size(162, 22);
            this.ToolStripMenuItem_页面组_关闭所有.Text = "关闭所有(&C)";
            this.ToolStripMenuItem_页面组_关闭所有.Click += new System.EventHandler(this.ToolStripMenuItem_页面组_关闭所有_Click);
            // 
            // contextMenuStrip_页面组
            // 
            this.contextMenuStrip_页面组.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_页面组_新建页面组,
            this.ToolStripMenuItem_页面组_重命名,
            this.ToolStripMenuItem_页面组_删除,
            this.toolStripMenuItem5,
            this.ToolStripMenuItem_页面组_新建页面,
            this.ToolStripMenuItem_页面组_导入页面,
            this.ToolStripMenuItem_页面组_粘贴页面,
            this.toolStripMenuItem6,
            this.ToolStripMenuItem_页面组_上移,
            this.ToolStripMenuItem_页面组_下移,
            this.ToolStripMenuItem_页面组_移至顶层,
            this.ToolStripMenuItem_页面组_移至底层,
            this.toolStripSeparator11,
            this.ToolStripMenuItem_页面组_关闭所有});
            this.contextMenuStrip_页面组.Name = "contextMenuStrip3";
            this.contextMenuStrip_页面组.Size = new System.Drawing.Size(163, 264);
            // 
            // ImageCollection_ProjectNavigation
            // 
            this.ImageCollection_ProjectNavigation.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("ImageCollection_ProjectNavigation.ImageStream")));
            this.ImageCollection_ProjectNavigation.Images.SetKeyName(0, "Project");
            // 
            // MDIParent1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 742);
            this.Controls.Add(this.colorEditControl1);
            this.Controls.Add(this.panel_纵向标尺);
            this.Controls.Add(this.panel_横向标尺);
            this.Controls.Add(this.dockPanel_输出栏);
            this.Controls.Add(this.panelContainer1);
            this.Controls.Add(this.dockPanelContainer_LeftBottom);
            this.Controls.Add(this.hideContainerRight);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.Name = "MDIParent1";
            this.Text = "FView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MDIParent1_FormClosing);
            this.Load += new System.EventHandler(this.MDIParent1_Load);
            this.MdiChildActivate += new System.EventHandler(this.MDIParent1_MdiChildActivate);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip_本地页面.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.contextMenuStrip_本地页面根节点.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.hideContainerRight.ResumeLayout(false);
            this.dockPanel_基本控件.ResumeLayout(false);
            this.controlContainer_基本控件.ResumeLayout(false);
            this.xtraScrollableControl_基本控件.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl_基本控件)).EndInit();
            this.navBarControl_基本控件.ResumeLayout(false);
            this.navBarGroupControlContainer_Windows控件.ResumeLayout(false);
            this.navBarGroupControlContainer_基本图元.ResumeLayout(false);
            this.dockPanel_DCCE工控组件.ResumeLayout(false);
            this.controlContainer_DCCE工控组件.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl_DCCE工控组件)).EndInit();
            this.dockPanel_精灵控件.ResumeLayout(false);
            this.controlContainer_精灵控件.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl_精灵控件)).EndInit();
            this.dockPanel_ActiveX控件.ResumeLayout(false);
            this.controlContainer_ActiveX控件.ResumeLayout(false);
            this.dockPanelContainer_LeftBottom.ResumeLayout(false);
            this.dockPanel_属性.ResumeLayout(false);
            this.controlContainer_属性.ResumeLayout(false);
            this.dockPanel_事件.ResumeLayout(false);
            this.controlContainer_事件.ResumeLayout(false);
            this.dockPanel_动画.ResumeLayout(false);
            this.controlContainer_动画.ResumeLayout(false);
            this.panelContainer1.ResumeLayout(false);
            this.dockPanel_导航栏.ResumeLayout(false);
            this.controlContainer_导航栏.ResumeLayout(false);
            this.dockPanel_对象浏览器.ResumeLayout(false);
            this.dockPanelContainer_对象浏览器.ResumeLayout(false);
            this.dockPanel_变量浏览器.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            this.dockPanel_输出栏.ResumeLayout(false);
            this.controlContainer_输出栏.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.colorEditControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemColorEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).EndInit();
            this.contextMenuStrip_页面组.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImageCollection_ProjectNavigation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.ComboBox cb;
        private ToolTip toolTip;
        private ListView listView_事件;
        private ColumnHeader columnHeader2;
        private ToolStripButton toolStripButton27;
        private DataGridView dataGridView1;
        private ToolStripSeparator toolStripSeparator21;
        private ContextMenuStrip contextMenuStrip_本地页面;

        private ToolStripMenuItem ToolStripMenuItem_本地页面_删除;

        private ToolStripSeparator toolStripSeparator3;

        private ToolStripMenuItem ToolStripMenuItem_本地页面_属性;

        private ToolStripMenuItem ToolStripMenuItem_本地页面_打开;

        private ToolStripMenuItem ToolStripMenuItem_本地页面_关闭;

        private DataGridViewTextBoxColumn Column1;

        private DataGridViewComboBoxColumn Column2;

        private DataGridViewTextBoxColumn Column3;

        private FileSystemWatcher fileSystemWatcher1;

        private Panel panel_纵向标尺;

        private Panel panel_横向标尺;

        private ContextMenuStrip contextMenuStrip_本地页面根节点;

        private ToolStripMenuItem ToolStripMenuItem_本地根节点_新建页面;

        private ToolStripMenuItem ToolStripMenuItem_本地根节点_导入页面;

        private ToolStripMenuItem ToolStripMenuItem_本地根节点_关闭所有;

        public TreeView treeView_工程导航;

        public PropertyGrid myPropertyGrid1;

        private ToolStripMenuItem ToolStripMenuItem_本地页面_导出;

        private ToolStripSeparator toolStripSeparator29;

        public RichTextBox richTextBox1;

        private ToolStripMenuItem ToolStripMenuItem_本地页面_复制;

        private ToolStripMenuItem ToolStripMenuItem_本地根节点_粘贴页面;

        private ToolStripSeparator toolStripSeparator34;

        private BarDockControl barDockControlLeft;

        private BarDockControl barDockControlRight;

        private BarDockControl barDockControlBottom;

        private BarDockControl barDockControlTop;

        private BarManager barManager1;

        private Bar toolbar_标准;

        private Bar menubar_菜单栏;

        private Bar statusbar_状态栏;

        public BarStaticItem BarStaticItem_Status;

        private BarSubItem barSubItem_主菜单_文件;

        private BarSubItem barSubItem_主菜单_编辑;

        private BarSubItem barSubItem_主菜单_视图;

        private BarSubItem barSubItem_主菜单_操作;

        private BarSubItem barSubItem_主菜单_脚本;

        private BarSubItem barSubItem6;

        private BarSubItem barSubItem_主菜单_窗口;

        private BarButtonItem barButtonItem_新建页面;

        private BarButtonItem barButtonItem_打开页面;

        private BarSubItem barSubItem_导入;

        private BarButtonItem barButtonItem_保存工程;

        private BarButtonItem barButtonItem_退出;

        private BarButtonItem barButtonItem10;

        private BarButtonItem barButtonItem11;

        private BarButtonItem barButtonItem13;

        private BarButtonItem barButtonItem14;

        private BarButtonItem barButtonItem15;

        private BarButtonItem barButtonItem16;

        private BarButtonItem barButtonItem17;

        private BarButtonItem barButtonItem18;

        private BarButtonItem barButtonItem19;

        private BarCheckItem menubarCheckItem_导航栏;

        private BarCheckItem barCheckItem2;

        private BarCheckItem menubarCheckItem_属性;

        private BarCheckItem menubarCheckItem_工具栏;

        private BarCheckItem menubarCheckItem_状态栏;

        private BarCheckItem menubarCheckItem_输出栏;

        private BarCheckItem barCheckItem9;

        private BarButtonItem barButtonItem23;

        private BarButtonItem barButtonItem24;

        private BarButtonItem barButtonItem25;

        private BarButtonItem barButtonItem26;

        private BarButtonItem barButtonItem27;

        private BarButtonItem barButtonItem28;

        private BarSubItem barSubItem12;

        private BarSubItem barSubItem13;

        private BarButtonItem barButtonItem29;

        private BarButtonItem barButtonItem30;

        private BarButtonItem barButtonItem32;

        private BarSubItem barSubItem14;

        private BarSubItem barSubItem15;

        private BarSubItem barSubItem16;

        private BarButtonItem barButtonItem34;

        private BarButtonItem barButtonItem35;

        private BarButtonItem barButtonItem37;

        private BarMdiChildrenListItem barMdiChildrenListItem1;

        private BarButtonItem barButtonItem38;

        private BarButtonItem barButtonItem39;

        private BarButtonItem barButtonItem40;

        private BarButtonItem barButtonItem41;

        private BarButtonItem barButtonItem_导入页面;

        private BarButtonItem barButtonItem48;

        private BarButtonItem barButtonItem49;

        private BarButtonItem barButtonItem50;

        private BarButtonItem barButtonItem51;

        private BarButtonItem barButtonItem52;

        private BarButtonItem barButtonItem53;

        private BarButtonItem barButtonItem54;

        private BarButtonItem barButtonItem55;

        private BarButtonItem barButtonItem56;

        public BarCheckItem barCheckItem10;

        private BarButtonItem barButtonItem63;

        private BarButtonItem barButtonItem64;

        private BarButtonItem barButtonItem65;

        private BarButtonItem barButtonItem66;

        private BarButtonItem barButtonItem67;

        private BarButtonItem barButtonItem68;

        private BarButtonItem barButtonItem69;

        private BarButtonItem barButtonItem70;

        private BarButtonItem barButtonItem71;

        private BarButtonItem barButtonItem72;

        private BarButtonItem barButtonItem73;

        private BarButtonItem barButtonItem74;

        private BarButtonItem barButtonItem75;

        private BarButtonItem barButtonItem76;

        private BarButtonItem barButtonItem77;

        private BarButtonItem barButtonItem78;

        private BarButtonItem barButtonItem79;

        private BarButtonItem barButtonItem80;

        private BarButtonItem barButtonItem81;

        private BarButtonItem barButtonItem_CreatePage;

        private BarButtonItem barButtonItem_OpenPage;

        private BarButtonItem barButtonItem_SaveProject;

        private BarButtonItem barButtonItem_Copy;

        private BarButtonItem barButtonItem_Cut;

        private BarButtonItem barButtonItem_Paste;

        private BarButtonItem barButtonItem_Delete;

        private BarButtonItem barButtonItem_Undo;

        private BarButtonItem barButtonItem_Redo;

        private BarButtonItem BarButtonItem_Debug;

        private Bar toolbar_资源;

        private BarButtonItem toolbarButtonItem_基本控件;

        private BarButtonItem toolbarButtonItem_DCCE工控组件;

        private BarButtonItem toolbarButtonItem_ActiveX控件;

        private BarButtonItem toolbarButtonItem_精灵控件;

        private BarCheckItem barCheckItem13;

        private Bar toolbar_操作;

        private BarButtonItem barButtonItem_ElementCombine;

        private BarButtonItem barButtonItem_ElementSeparate;

        private BarButtonItem barButtonItem_ElementTop;

        private BarButtonItem barButtonItem_ElementBottom;

        private BarButtonItem barButtonItem_HorizontalRotate;

        private BarButtonItem barButtonItem_VerticalRotate;

        private BarButtonItem barButtonItem_TopAlign;

        private BarButtonItem barButtonItem_HorizontalAlign;

        private BarButtonItem barButtonItem_BottomAlign;

        private BarButtonItem barButtonItem_LeftAlign;

        private BarButtonItem barButtonItem_VerticalAlign;

        private BarButtonItem barButtonItem_RightAlign;

        private BarButtonItem barButtonItem_EqualAndOpposite;

        private BarButtonItem barButtonItem_EqualWidth;

        private BarButtonItem barButtonItem_EqualHeight;

        private BarButtonItem barButtonItem_RightRotate;

        private BarButtonItem barButtonItem_LeftRotate;

        private RepositoryItemZoomTrackBar repositoryItemZoomTrackBar1;

        private BarCheckItem barCheckItem_LockElements;

        private BarButtonItem barButtonItem_HorizontalEquidistance;

        private BarButtonItem barButtonItem_VerticalEquidistance;

        private DockManager dockManager1;

        private DockPanel dockPanel_导航栏;

        private ControlContainer controlContainer_导航栏;

        public DockPanel dockPanel_输出栏;

        private ControlContainer controlContainer_输出栏;

        private DockPanel dockPanel_属性;

        private ControlContainer controlContainer_属性;

        private DockPanel dockPanel_事件;

        private ControlContainer controlContainer_事件;

        private DockPanel dockPanel_动画;

        private ControlContainer controlContainer_动画;

        private DockPanel dockPanelContainer_LeftBottom;

        private BarCheckItem menubarCheckItem_事件;

        private BarButtonItem barButtonItem119;

        private BarCheckItem menubarCheckItem_动画;

        private BarButtonItem barButtonItem120;

        private BarCheckItem menubarCheckItem_默认主题;

        private DockPanel dockPanel_基本控件;

        private ControlContainer controlContainer_基本控件;

        private DockPanel dockPanel_DCCE工控组件;

        private ControlContainer controlContainer_DCCE工控组件;

        private DockPanel dockPanel_精灵控件;

        private ControlContainer controlContainer_精灵控件;

        private NavBarGroup 基本图元;

        private XtraScrollableControl xtraScrollableControl_基本控件;

        private NavBarControl navBarControl_基本控件;

        private NavBarGroup Windows控件;

        private BarButtonItem barButtonItem130;

        private BarEditItem barEditItem4;

        private RepositoryItemColorEdit repositoryItemColorEdit1;

        private RepositoryItemComboBox repositoryItemComboBox2;

        private BarStaticItem barStaticItem3;

        private BarEditItem barEditItem3;

        private RepositoryItemColorEdit repositoryItemColorEdit2;

        private DockPanel dockPanel_ActiveX控件;

        private ControlContainer controlContainer_ActiveX控件;

        private PopupMenu popupMenu1;

        private BarButtonItem barButtonItem131;

        private BarSubItem menubarSubItem_资源库;

        private BarCheckItem menubarCheckItem_基本控件;

        private BarButtonItem barButtonItem132;

        private BarButtonItem barButtonItem133;

        private BarButtonItem barButtonItem134;

        private BarButtonItem barButtonItem135;

        private BarButtonItem barButtonItem136;

        private BarCheckItem menubarCheckItem_DCCE工控组件;

        private BarCheckItem menubarCheckItem_ActiveX控件;

        private BarCheckItem menubarCheckItem_精灵控件;

        private NavBarGroupControlContainer navBarGroupControlContainer_基本图元;

        private ListView listView_基本图元;

        private ImageList imageList_图库_基本图元;

        private ListView listView_Windows控件;

        private NavBarGroupControlContainer navBarGroupControlContainer_Windows控件;

        private Button ActiveXFinderButton;

        private ListView listView_ActiveX控件;

        private BarEditItem barEditItem5;

        private RepositoryItemSpinEdit repositoryItemSpinEdit1;

        public ColorEditControl colorEditControl1;

        private BarButtonItem barButtonItem137;

        private BarButtonItem barButtonItem139;

        private PopupMenu popupMenu2;

        private NavBarControl navBarControl_精灵控件;

        private NavBarGroup navBarGroup_仪表;

        private NavBarGroup navBarGroup_开关;

        private NavBarGroup navBarGroup_报警灯;

        private NavBarGroup navBarGroup_按钮;

        private NavBarGroup navBarGroup_时钟;

        private NavBarGroup navBarGroup_泵;

        private NavBarGroup navBarGroup_游标;

        private NavBarGroup navBarGroup_电机;

        private NavBarGroup navBarGroup_管道;

        private NavBarGroup navBarGroup_罐;

        private NavBarGroup navBarGroup_阀门;

        private BarButtonItem barButtonItem58;

        private BarButtonItem barButtonItem59;

        private ToolStripMenuItem ToolStripMenuItem_本地页面_保存;

        private ToolStripSeparator toolStripSeparator1;

        private ToolStripMenuItem ToolStripMenuItem_本地页面_上移;

        private ToolStripMenuItem ToolStripMenuItem_本地页面_下移;

        private BarButtonItem barButtonItem_SavePage;

        private BarSubItem barSubItem_主菜单_工具;

        private BarButtonItem barButtonItem62;

        private ToolStripMenuItem toolStripMenuItem_本地页面_移至顶层;

        private ToolStripMenuItem toolStripMenuItem_本地页面_移至底层;

        private ToolStripSeparator toolStripMenuItem4;

        private BarButtonItem barButtonItem140;

        private ToolStripMenuItem ToolStripMenuItem_本地根节点_新建页面组;

        private ToolStripSeparator toolStripSeparator9;

        private DockPanel dockPanel_对象浏览器;

        private ControlContainer dockPanelContainer_对象浏览器;

        private ToolStripSeparator toolStripSeparator7;

        private ImageList imageList_Windows控件;

        public ObjExplorer objView_Page;

        private AutoHideContainer hideContainerRight;

        private Bar toolbar_绘制基本图元;

        private BarButtonItem barButtonItem_DrawLine;

        private BarButtonItem barButtonItem_CurveLine;

        private BarButtonItem barButtonItem_DrawEllipse;

        private BarButtonItem barButtonItem_Rectangle;

        private BarButtonItem barButtonItem_Polygon;

        private BarButtonItem barButtonItem_RoundedRectangle;

        private BarButtonItem barButtonItem_Picture;

        private BarButtonItem barButtonItem_String;

        private BarCheckItem menubarCheckItem_对象浏览器;

        private BarButtonItem barButtonItem9;

        private BarButtonItem barButtonItem12;

        private BarSubItem barSubItem1;

        private BarSubItem barSubItem_主菜单_帮助;

        private BarButtonItem barButtonItem36;

        private BarButtonItem barButtonItem_about;

        private BarButtonItem barButtonItem_FindText;

        private BarButtonItem barButtonItem_保存页面;

        private DockPanel dockPanel_变量浏览器;

        private ControlContainer dockPanel1_Container;

        private DockPanel panelContainer1;

        private BarCheckItem menubarCheckItem_变量浏览器;

        private VarExplore varExplore;

        private ToolStripMenuItem ToolStripMenuItem_页面组_新建页面组;

        private ToolStripMenuItem ToolStripMenuItem_页面组_重命名;

        private ToolStripMenuItem ToolStripMenuItem_页面组_删除;

        private ToolStripSeparator toolStripMenuItem5;

        private ToolStripMenuItem ToolStripMenuItem_页面组_新建页面;

        private ToolStripMenuItem ToolStripMenuItem_页面组_导入页面;

        private ToolStripMenuItem ToolStripMenuItem_页面组_粘贴页面;

        private ToolStripSeparator toolStripMenuItem6;

        private ToolStripMenuItem ToolStripMenuItem_页面组_上移;

        private ToolStripMenuItem ToolStripMenuItem_页面组_下移;

        private ToolStripMenuItem ToolStripMenuItem_页面组_移至顶层;

        private ToolStripMenuItem ToolStripMenuItem_页面组_移至底层;

        private ToolStripSeparator toolStripSeparator11;

        private ToolStripMenuItem ToolStripMenuItem_页面组_关闭所有;

        private ContextMenuStrip contextMenuStrip_页面组;

        private NavBarControl navBarControl_DCCE工控组件;

        private NavBarGroup navBarGroup_DCCE开关;

        private NavBarGroup navBarGroup_DCCE报警灯;

        private NavBarGroup navBarGroup_变量操作;

        private NavBarGroup navBarGroup_DCCE媒体控件;

        private NavBarGroup navBarGroup_文件操作;

        private NavBarGroup navBarGroup_图形操作;

        private NavBarGroup navBarGroup_数控;

        private NavBarGroup navBarGroup_数据管理;
        private BarButtonItem barButtonItem_NewProject;
        private BarButtonItem barButtonItem_OpenProject;
        private OpenFileDialog openFileDialog_OpenProject;
        private BarButtonItem barButtonItem_CloseProject;
        private BarButtonItem barButtonItem20;
        private BarButtonItem BarButtonItem_ProjectProperty;
        private BarButtonItem BarButtonItem_RecentlyProject;
        private BarSubItem BarSubItem_RecentlyProject;
        private BarListItem BarListItem_RecentlyProjece;
        private DevExpress.Utils.ImageCollection ImageCollection_ProjectNavigation;
    }
}
