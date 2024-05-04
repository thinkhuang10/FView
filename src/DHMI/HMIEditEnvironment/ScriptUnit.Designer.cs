using DevExpress.XtraBars;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using ICSharpCode.TextEditor;
using System.Windows.Forms;

namespace HMIEditEnvironment
{
    partial class ScriptUnit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptUnit));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barSubItem1 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.editSubItem = new DevExpress.XtraBars.BarSubItem();
            this.undoBarBtn = new DevExpress.XtraBars.BarButtonItem();
            this.redoBarBtn = new DevExpress.XtraBars.BarButtonItem();
            this.cutBarBtn = new DevExpress.XtraBars.BarButtonItem();
            this.copyBarBtn = new DevExpress.XtraBars.BarButtonItem();
            this.pasteBarBtn = new DevExpress.XtraBars.BarButtonItem();
            this.delBarBtn = new DevExpress.XtraBars.BarButtonItem();
            this.findBarBtn = new DevExpress.XtraBars.BarButtonItem();
            this.repBarBtn = new DevExpress.XtraBars.BarButtonItem();
            this.moveBarBtn = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.barButtonItem_Copy = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Paste = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Cut = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Delete = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Undo = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Redo = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Find = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Replace = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Goto = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Save = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Compile = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem_Quit = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem13 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem14 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem15 = new DevExpress.XtraBars.BarButtonItem();
            this.addEventScriptBtn = new DevExpress.XtraBars.BarButtonItem();
            this.deleteBtn = new DevExpress.XtraBars.BarButtonItem();
            this.deleteEventBtnItem = new DevExpress.XtraBars.BarButtonItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.splitterControl2 = new DevExpress.XtraEditors.SplitterControl();
            this.splitterControl3 = new DevExpress.XtraEditors.SplitterControl();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.textEditorControl1 = new ICSharpCode.TextEditor.TextEditorControl();
            this.editContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.UndoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RedoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.CutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.addEventScriptPopMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.deleteEventPopMenu = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.editContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addEventScriptPopMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deleteEventPopMenu)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2,
            this.bar3,
            this.bar1});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barSubItem1,
            this.barButtonItem4,
            this.barButtonItem5,
            this.barStaticItem1,
            this.barButtonItem_Save,
            this.barButtonItem_Compile,
            this.barButtonItem_Quit,
            this.barButtonItem10,
            this.barButtonItem13,
            this.barButtonItem14,
            this.barButtonItem15,
            this.editSubItem,
            this.undoBarBtn,
            this.redoBarBtn,
            this.cutBarBtn,
            this.copyBarBtn,
            this.pasteBarBtn,
            this.delBarBtn,
            this.findBarBtn,
            this.repBarBtn,
            this.moveBarBtn,
            this.addEventScriptBtn,
            this.deleteBtn,
            this.deleteEventBtnItem,
            this.barButtonItem_Undo,
            this.barButtonItem_Redo,
            this.barButtonItem_Cut,
            this.barButtonItem_Copy,
            this.barButtonItem_Paste,
            this.barButtonItem_Delete,
            this.barButtonItem_Find,
            this.barButtonItem_Replace,
            this.barButtonItem_Goto});
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 47;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barSubItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.editSubItem)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barSubItem1
            // 
            this.barSubItem1.Caption = "文件";
            this.barSubItem1.Id = 1;
            this.barSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem10),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5, true)});
            this.barSubItem1.Name = "barSubItem1";
            // 
            // barButtonItem10
            // 
            this.barButtonItem10.Caption = "保存";
            this.barButtonItem10.Id = 12;
            this.barButtonItem10.Name = "barButtonItem10";
            this.barButtonItem10.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem10_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "脚本检查";
            this.barButtonItem4.Id = 4;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "退出";
            this.barButtonItem5.Id = 5;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // editSubItem
            // 
            this.editSubItem.Caption = "编辑(&E)";
            this.editSubItem.Id = 18;
            this.editSubItem.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.undoBarBtn),
            new DevExpress.XtraBars.LinkPersistInfo(this.redoBarBtn),
            new DevExpress.XtraBars.LinkPersistInfo(this.cutBarBtn, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.copyBarBtn),
            new DevExpress.XtraBars.LinkPersistInfo(this.pasteBarBtn),
            new DevExpress.XtraBars.LinkPersistInfo(this.delBarBtn),
            new DevExpress.XtraBars.LinkPersistInfo(this.findBarBtn, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.repBarBtn),
            new DevExpress.XtraBars.LinkPersistInfo(this.moveBarBtn)});
            this.editSubItem.Name = "editSubItem";
            // 
            // undoBarBtn
            // 
            this.undoBarBtn.Caption = "撤销(&U)";
            this.undoBarBtn.Id = 19;
            this.undoBarBtn.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z));
            this.undoBarBtn.Name = "undoBarBtn";
            this.undoBarBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.undoBarBtn_ItemClick);
            // 
            // redoBarBtn
            // 
            this.redoBarBtn.Caption = "恢复(&R)";
            this.redoBarBtn.Id = 20;
            this.redoBarBtn.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y));
            this.redoBarBtn.Name = "redoBarBtn";
            this.redoBarBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.redoBarBtn_ItemClick);
            // 
            // cutBarBtn
            // 
            this.cutBarBtn.Caption = "剪切(&T)";
            this.cutBarBtn.Id = 21;
            this.cutBarBtn.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X));
            this.cutBarBtn.Name = "cutBarBtn";
            this.cutBarBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.cutBarBtn_ItemClick);
            // 
            // copyBarBtn
            // 
            this.copyBarBtn.Caption = "复制(&C)";
            this.copyBarBtn.Id = 22;
            this.copyBarBtn.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C));
            this.copyBarBtn.Name = "copyBarBtn";
            this.copyBarBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.copyBarBtn_ItemClick);
            // 
            // pasteBarBtn
            // 
            this.pasteBarBtn.Caption = "粘贴(&P)";
            this.pasteBarBtn.Id = 23;
            this.pasteBarBtn.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V));
            this.pasteBarBtn.Name = "pasteBarBtn";
            this.pasteBarBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.pasteBarBtn_ItemClick);
            // 
            // delBarBtn
            // 
            this.delBarBtn.Caption = "删除(&D)";
            this.delBarBtn.Id = 24;
            this.delBarBtn.Name = "delBarBtn";
            this.delBarBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.delBarBtn_ItemClick);
            // 
            // findBarBtn
            // 
            this.findBarBtn.Caption = "查找(&F)";
            this.findBarBtn.Id = 25;
            this.findBarBtn.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F));
            this.findBarBtn.Name = "findBarBtn";
            this.findBarBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.findBarBtn_ItemClick);
            // 
            // repBarBtn
            // 
            this.repBarBtn.Caption = "替换(&R)";
            this.repBarBtn.Id = 26;
            this.repBarBtn.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H));
            this.repBarBtn.Name = "repBarBtn";
            this.repBarBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.repBarBtn_ItemClick);
            // 
            // moveBarBtn
            // 
            this.moveBarBtn.Caption = "转到(&G)";
            this.moveBarBtn.Id = 27;
            this.moveBarBtn.ItemShortcut = new DevExpress.XtraBars.BarShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G));
            this.moveBarBtn.Name = "moveBarBtn";
            this.moveBarBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.moveBarBtn_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Id = 6;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // bar1
            // 
            this.bar1.BarName = "工具栏";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.FloatLocation = new System.Drawing.Point(1400, 186);
            this.bar1.FloatSize = new System.Drawing.Size(362, 26);
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Copy),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Paste),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Cut),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Delete),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barButtonItem_Undo, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Redo),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Find, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Replace),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Goto),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Save, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Compile, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem_Quit, true)});
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.Text = "工具栏";
            // 
            // barButtonItem_Copy
            // 
            this.barButtonItem_Copy.Hint = "复制";
            this.barButtonItem_Copy.Id = 41;
            this.barButtonItem_Copy.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Copy.ImageOptions.Image")));
            this.barButtonItem_Copy.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Copy.ImageOptions.LargeImage")));
            this.barButtonItem_Copy.Name = "barButtonItem_Copy";
            this.barButtonItem_Copy.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_Copy_ItemClick);
            // 
            // barButtonItem_Paste
            // 
            this.barButtonItem_Paste.Hint = "粘贴";
            this.barButtonItem_Paste.Id = 42;
            this.barButtonItem_Paste.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Paste.ImageOptions.Image")));
            this.barButtonItem_Paste.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Paste.ImageOptions.LargeImage")));
            this.barButtonItem_Paste.Name = "barButtonItem_Paste";
            this.barButtonItem_Paste.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_Paste_ItemClick);
            // 
            // barButtonItem_Cut
            // 
            this.barButtonItem_Cut.Hint = "剪切";
            this.barButtonItem_Cut.Id = 40;
            this.barButtonItem_Cut.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Cut.ImageOptions.Image")));
            this.barButtonItem_Cut.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Cut.ImageOptions.LargeImage")));
            this.barButtonItem_Cut.Name = "barButtonItem_Cut";
            this.barButtonItem_Cut.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_Cut_ItemClick);
            // 
            // barButtonItem_Delete
            // 
            this.barButtonItem_Delete.Hint = "删除";
            this.barButtonItem_Delete.Id = 43;
            this.barButtonItem_Delete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Delete.ImageOptions.Image")));
            this.barButtonItem_Delete.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Delete.ImageOptions.LargeImage")));
            this.barButtonItem_Delete.Name = "barButtonItem_Delete";
            this.barButtonItem_Delete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_Delete_ItemClick);
            // 
            // barButtonItem_Undo
            // 
            this.barButtonItem_Undo.Hint = "撤销";
            this.barButtonItem_Undo.Id = 38;
            this.barButtonItem_Undo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Undo.ImageOptions.Image")));
            this.barButtonItem_Undo.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Undo.ImageOptions.LargeImage")));
            this.barButtonItem_Undo.Name = "barButtonItem_Undo";
            this.barButtonItem_Undo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_Undo_ItemClick);
            // 
            // barButtonItem_Redo
            // 
            this.barButtonItem_Redo.Hint = "恢复";
            this.barButtonItem_Redo.Id = 39;
            this.barButtonItem_Redo.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Redo.ImageOptions.Image")));
            this.barButtonItem_Redo.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Redo.ImageOptions.LargeImage")));
            this.barButtonItem_Redo.Name = "barButtonItem_Redo";
            this.barButtonItem_Redo.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_Redo_ItemClick);
            // 
            // barButtonItem_Find
            // 
            this.barButtonItem_Find.Hint = "查找";
            this.barButtonItem_Find.Id = 44;
            this.barButtonItem_Find.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Find.ImageOptions.Image")));
            this.barButtonItem_Find.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Find.ImageOptions.LargeImage")));
            this.barButtonItem_Find.Name = "barButtonItem_Find";
            this.barButtonItem_Find.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_Find_ItemClick);
            // 
            // barButtonItem_Replace
            // 
            this.barButtonItem_Replace.Hint = "替换";
            this.barButtonItem_Replace.Id = 45;
            this.barButtonItem_Replace.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Replace.ImageOptions.Image")));
            this.barButtonItem_Replace.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Replace.ImageOptions.LargeImage")));
            this.barButtonItem_Replace.Name = "barButtonItem_Replace";
            this.barButtonItem_Replace.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_Replace_ItemClick);
            // 
            // barButtonItem_Goto
            // 
            this.barButtonItem_Goto.Hint = "转到";
            this.barButtonItem_Goto.Id = 46;
            this.barButtonItem_Goto.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Goto.ImageOptions.Image")));
            this.barButtonItem_Goto.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Goto.ImageOptions.LargeImage")));
            this.barButtonItem_Goto.Name = "barButtonItem_Goto";
            this.barButtonItem_Goto.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem_Goto_ItemClick);
            // 
            // barButtonItem_Save
            // 
            this.barButtonItem_Save.Hint = "保存脚本";
            this.barButtonItem_Save.Id = 8;
            this.barButtonItem_Save.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Save.ImageOptions.Image")));
            this.barButtonItem_Save.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Save.ImageOptions.LargeImage")));
            this.barButtonItem_Save.Name = "barButtonItem_Save";
            this.barButtonItem_Save.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem10_ItemClick);
            // 
            // barButtonItem_Compile
            // 
            this.barButtonItem_Compile.Hint = "脚本检查";
            this.barButtonItem_Compile.Id = 9;
            this.barButtonItem_Compile.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Compile.ImageOptions.Image")));
            this.barButtonItem_Compile.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Compile.ImageOptions.LargeImage")));
            this.barButtonItem_Compile.Name = "barButtonItem_Compile";
            this.barButtonItem_Compile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barButtonItem_Quit
            // 
            this.barButtonItem_Quit.Hint = "退出";
            this.barButtonItem_Quit.Id = 10;
            this.barButtonItem_Quit.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Quit.ImageOptions.Image")));
            this.barButtonItem_Quit.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("barButtonItem_Quit.ImageOptions.LargeImage")));
            this.barButtonItem_Quit.Name = "barButtonItem_Quit";
            this.barButtonItem_Quit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(792, 46);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 543);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(792, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 46);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 497);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(792, 46);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 497);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "文件";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem13
            // 
            this.barButtonItem13.Id = 33;
            this.barButtonItem13.Name = "barButtonItem13";
            // 
            // barButtonItem14
            // 
            this.barButtonItem14.Id = 34;
            this.barButtonItem14.Name = "barButtonItem14";
            // 
            // barButtonItem15
            // 
            this.barButtonItem15.Id = 35;
            this.barButtonItem15.Name = "barButtonItem15";
            // 
            // addEventScriptBtn
            // 
            this.addEventScriptBtn.Caption = "添加控件事件脚本";
            this.addEventScriptBtn.Id = 29;
            this.addEventScriptBtn.Name = "addEventScriptBtn";
            this.addEventScriptBtn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.addEventScriptBtn_ItemClick);
            // 
            // deleteBtn
            // 
            this.deleteBtn.Id = 31;
            this.deleteBtn.Name = "deleteBtn";
            // 
            // deleteEventBtnItem
            // 
            this.deleteEventBtnItem.Caption = "删除事件";
            this.deleteEventBtnItem.Id = 32;
            this.deleteEventBtnItem.Name = "deleteEventBtnItem";
            this.deleteEventBtnItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.deleteEventBtnItem_ItemClick);
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 46);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(244, 497);
            this.treeView1.TabIndex = 5;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeView1_KeyDown);
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitterControl1
            // 
            this.splitterControl1.Location = new System.Drawing.Point(244, 46);
            this.splitterControl1.Name = "splitterControl1";
            this.splitterControl1.Size = new System.Drawing.Size(10, 497);
            this.splitterControl1.TabIndex = 6;
            this.splitterControl1.TabStop = false;
            // 
            // treeView2
            // 
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Right;
            this.treeView2.HideSelection = false;
            this.treeView2.ImageIndex = 0;
            this.treeView2.ImageList = this.imageList1;
            this.treeView2.Location = new System.Drawing.Point(593, 46);
            this.treeView2.Name = "treeView2";
            this.treeView2.SelectedImageIndex = 0;
            this.treeView2.Size = new System.Drawing.Size(199, 497);
            this.treeView2.TabIndex = 8;
            this.treeView2.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView2_AfterSelect);
            this.treeView2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.treeView2_MouseDoubleClick);
            // 
            // splitterControl2
            // 
            this.splitterControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterControl2.Location = new System.Drawing.Point(254, 86);
            this.splitterControl2.Name = "splitterControl2";
            this.splitterControl2.Size = new System.Drawing.Size(329, 10);
            this.splitterControl2.TabIndex = 9;
            this.splitterControl2.TabStop = false;
            // 
            // splitterControl3
            // 
            this.splitterControl3.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterControl3.Location = new System.Drawing.Point(583, 46);
            this.splitterControl3.Name = "splitterControl3";
            this.splitterControl3.Size = new System.Drawing.Size(10, 497);
            this.splitterControl3.TabIndex = 10;
            this.splitterControl3.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "VBS文件|*.vbs|所有文件|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "VBS文件|*.vbs|所有文件|*.*";
            // 
            // textEditorControl1
            // 
            this.textEditorControl1.ContextMenuStrip = this.editContextMenu;
            this.textEditorControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditorControl1.IsReadOnly = false;
            this.textEditorControl1.Location = new System.Drawing.Point(254, 96);
            this.textEditorControl1.Name = "textEditorControl1";
            this.textEditorControl1.Size = new System.Drawing.Size(329, 447);
            this.textEditorControl1.TabIndex = 15;
            this.textEditorControl1.TextChanged += new System.EventHandler(this.textEditorControl1_TextChanged);
            // 
            // editContextMenu
            // 
            this.editContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UndoMenuItem,
            this.RedoMenuItem,
            this.toolStripMenuItem1,
            this.CutMenuItem,
            this.CopyMenuItem,
            this.PasteMenuItem,
            this.DeleteMenuItem,
            this.toolStripMenuItem2});
            this.editContextMenu.Name = "editContextMenu";
            this.editContextMenu.Size = new System.Drawing.Size(118, 148);
            // 
            // UndoMenuItem
            // 
            this.UndoMenuItem.Name = "UndoMenuItem";
            this.UndoMenuItem.Size = new System.Drawing.Size(117, 22);
            this.UndoMenuItem.Text = "撤销(&U)";
            this.UndoMenuItem.Click += new System.EventHandler(this.undoBarBtn_ItemClick);
            // 
            // RedoMenuItem
            // 
            this.RedoMenuItem.Name = "RedoMenuItem";
            this.RedoMenuItem.Size = new System.Drawing.Size(117, 22);
            this.RedoMenuItem.Text = "恢复(&R)";
            this.RedoMenuItem.Click += new System.EventHandler(this.redoBarBtn_ItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(114, 6);
            // 
            // CutMenuItem
            // 
            this.CutMenuItem.Name = "CutMenuItem";
            this.CutMenuItem.Size = new System.Drawing.Size(117, 22);
            this.CutMenuItem.Text = "剪切(&T)";
            this.CutMenuItem.Click += new System.EventHandler(this.cutBarBtn_ItemClick);
            // 
            // CopyMenuItem
            // 
            this.CopyMenuItem.Name = "CopyMenuItem";
            this.CopyMenuItem.Size = new System.Drawing.Size(117, 22);
            this.CopyMenuItem.Text = "复制(&C)";
            this.CopyMenuItem.Click += new System.EventHandler(this.copyBarBtn_ItemClick);
            // 
            // PasteMenuItem
            // 
            this.PasteMenuItem.Name = "PasteMenuItem";
            this.PasteMenuItem.Size = new System.Drawing.Size(117, 22);
            this.PasteMenuItem.Text = "粘贴(&P)";
            this.PasteMenuItem.Click += new System.EventHandler(this.pasteBarBtn_ItemClick);
            // 
            // DeleteMenuItem
            // 
            this.DeleteMenuItem.Name = "DeleteMenuItem";
            this.DeleteMenuItem.Size = new System.Drawing.Size(117, 22);
            this.DeleteMenuItem.Text = "删除(&D)";
            this.DeleteMenuItem.Click += new System.EventHandler(this.delBarBtn_ItemClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(114, 6);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem13),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem14),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem15)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // addEventScriptPopMenu
            // 
            this.addEventScriptPopMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.addEventScriptBtn)});
            this.addEventScriptPopMenu.Manager = this.barManager1;
            this.addEventScriptPopMenu.Name = "addEventScriptPopMenu";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Controls.Add(this.spinEdit1);
            this.panelControl1.Controls.Add(this.checkEdit1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(254, 46);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(329, 40);
            this.panelControl1.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(5, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(101, 14);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "脚本执行周期(ms):";
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(112, 10);
            this.spinEdit1.MenuManager = this.barManager1;
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Properties.IsFloatValue = false;
            this.spinEdit1.Properties.Mask.EditMask = "N00";
            this.spinEdit1.Properties.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.spinEdit1.Properties.MinValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.spinEdit1.Size = new System.Drawing.Size(77, 20);
            this.spinEdit1.TabIndex = 0;
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(87, 12);
            this.checkEdit1.MenuManager = this.barManager1;
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "当鼠标在按下状态进入或移开图形时是否执行相应事件";
            this.checkEdit1.Size = new System.Drawing.Size(311, 20);
            this.checkEdit1.TabIndex = 3;
            this.checkEdit1.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // deleteEventPopMenu
            // 
            this.deleteEventPopMenu.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.deleteEventBtnItem)});
            this.deleteEventPopMenu.Manager = this.barManager1;
            this.deleteEventPopMenu.Name = "deleteEventPopMenu";
            // 
            // ScriptUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.textEditorControl1);
            this.Controls.Add(this.splitterControl2);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.splitterControl1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.splitterControl3);
            this.Controls.Add(this.treeView2);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.IconOptions.ShowIcon = false;
            this.Name = "ScriptUnit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "脚本管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScriptUnit_FormClosing);
            this.Load += new System.EventHandler(this.ScriptUnit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.editContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addEventScriptPopMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deleteEventPopMenu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private BarManager barManager1;

        private Bar bar2;

        private Bar bar3;

        private BarDockControl barDockControlTop;

        private BarDockControl barDockControlBottom;

        private BarDockControl barDockControlLeft;

        private BarDockControl barDockControlRight;

        private BarButtonItem barButtonItem1;

        private BarSubItem barSubItem1;

        private BarButtonItem barButtonItem4;

        private BarButtonItem barButtonItem5;

        private BarStaticItem barStaticItem1;

        public TreeView treeView2;

        private SplitterControl splitterControl1;

        private TreeView treeView1;

        private SplitterControl splitterControl2;

        private SplitterControl splitterControl3;

        public ImageList imageList1;

        private OpenFileDialog openFileDialog1;

        private SaveFileDialog saveFileDialog1;

        private TextEditorControl textEditorControl1;

        private Bar bar1;

        private BarButtonItem barButtonItem_Save;

        private BarButtonItem barButtonItem_Compile;

        private BarButtonItem barButtonItem_Quit;

        private BarButtonItem barButtonItem10;

        private BarButtonItem barButtonItem13;

        private PopupMenu popupMenu1;

        private BarButtonItem barButtonItem14;

        private BarButtonItem barButtonItem15;

        private BarSubItem editSubItem;

        private BarButtonItem undoBarBtn;

        private BarButtonItem redoBarBtn;

        private BarButtonItem cutBarBtn;

        private ContextMenuStrip editContextMenu;

        private ToolStripMenuItem UndoMenuItem;

        private ToolStripMenuItem RedoMenuItem;

        private ToolStripMenuItem CutMenuItem;

        private ToolStripSeparator toolStripMenuItem1;

        private BarButtonItem copyBarBtn;

        private BarButtonItem pasteBarBtn;

        private BarButtonItem delBarBtn;

        private BarButtonItem findBarBtn;

        private BarButtonItem repBarBtn;

        private BarButtonItem moveBarBtn;

        private ToolStripMenuItem CopyMenuItem;

        private ToolStripMenuItem PasteMenuItem;

        private ToolStripMenuItem DeleteMenuItem;

        private ToolStripSeparator toolStripMenuItem2;

        private PopupMenu addEventScriptPopMenu;

        private BarButtonItem addEventScriptBtn;

        private PanelControl panelControl1;

        private LabelControl labelControl1;

        private SpinEdit spinEdit1;

        private CheckEdit checkEdit1;

        private BarButtonItem deleteBtn;

        private BarButtonItem deleteEventBtnItem;

        private PopupMenu deleteEventPopMenu;

        private BarButtonItem barButtonItem_Undo;

        private BarButtonItem barButtonItem_Redo;

        private BarButtonItem barButtonItem_Cut;

        private RepositoryItemTextEdit repositoryItemTextEdit1;

        private BarButtonItem barButtonItem_Copy;

        private BarButtonItem barButtonItem_Paste;

        private BarButtonItem barButtonItem_Delete;

        private BarButtonItem barButtonItem_Find;

        private BarButtonItem barButtonItem_Replace;

        private BarButtonItem barButtonItem_Goto;
    }
}
