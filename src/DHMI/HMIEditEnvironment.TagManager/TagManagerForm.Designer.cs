namespace HMIEditEnvironment.TagManager
{
    partial class TagManagerForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel = new System.Windows.Forms.Panel();
            this.Button_Monitor = new DevExpress.XtraEditors.SimpleButton();
            this.Button_Import = new DevExpress.XtraEditors.SimpleButton();
            this.Button_Export = new DevExpress.XtraEditors.SimpleButton();
            this.Button_Delete = new DevExpress.XtraEditors.SimpleButton();
            this.Button_Add = new DevExpress.XtraEditors.SimpleButton();
            this.GridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tableLayoutPanel.SuspendLayout();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.panel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.GridControl, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.450705F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.54929F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(798, 568);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // panel
            // 
            this.panel.Controls.Add(this.Button_Monitor);
            this.panel.Controls.Add(this.Button_Import);
            this.panel.Controls.Add(this.Button_Export);
            this.panel.Controls.Add(this.Button_Delete);
            this.panel.Controls.Add(this.Button_Add);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(792, 42);
            this.panel.TabIndex = 1;
            // 
            // Button_Monitor
            // 
            this.Button_Monitor.Location = new System.Drawing.Point(710, 9);
            this.Button_Monitor.Name = "Button_Monitor";
            this.Button_Monitor.Size = new System.Drawing.Size(75, 23);
            this.Button_Monitor.TabIndex = 4;
            this.Button_Monitor.Text = "监控";
            this.Button_Monitor.Click += new System.EventHandler(this.Button_Monitor_Click);
            // 
            // Button_Import
            // 
            this.Button_Import.Location = new System.Drawing.Point(629, 9);
            this.Button_Import.Name = "Button_Import";
            this.Button_Import.Size = new System.Drawing.Size(75, 23);
            this.Button_Import.TabIndex = 3;
            this.Button_Import.Text = "导入";
            this.Button_Import.Click += new System.EventHandler(this.Button_Import_Click);
            // 
            // Button_Export
            // 
            this.Button_Export.Location = new System.Drawing.Point(548, 9);
            this.Button_Export.Name = "Button_Export";
            this.Button_Export.Size = new System.Drawing.Size(75, 23);
            this.Button_Export.TabIndex = 2;
            this.Button_Export.Text = "导出";
            this.Button_Export.Click += new System.EventHandler(this.Button_Export_Click);
            // 
            // Button_Delete
            // 
            this.Button_Delete.Location = new System.Drawing.Point(90, 9);
            this.Button_Delete.Name = "Button_Delete";
            this.Button_Delete.Size = new System.Drawing.Size(75, 23);
            this.Button_Delete.TabIndex = 1;
            this.Button_Delete.Text = "删除";
            // 
            // Button_Add
            // 
            this.Button_Add.Location = new System.Drawing.Point(9, 9);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(75, 23);
            this.Button_Add.TabIndex = 0;
            this.Button_Add.Text = "添加";
            this.Button_Add.Click += new System.EventHandler(this.Button_Add_Click);
            // 
            // GridControl
            // 
            this.GridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl.Location = new System.Drawing.Point(3, 51);
            this.GridControl.MainView = this.gridView;
            this.GridControl.Name = "GridControl";
            this.GridControl.Size = new System.Drawing.Size(792, 514);
            this.GridControl.TabIndex = 2;
            this.GridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.GridControl;
            this.gridView.Name = "gridView";
            this.gridView.DoubleClick += new System.EventHandler(this.GridView_DoubleClick);
            // 
            // TagManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 568);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "TagManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "变量管理";
            this.Load += new System.EventHandler(this.TagManagerForm_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panel;
        private DevExpress.XtraEditors.SimpleButton Button_Delete;
        private DevExpress.XtraEditors.SimpleButton Button_Add;
        private DevExpress.XtraGrid.GridControl GridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.SimpleButton Button_Import;
        private DevExpress.XtraEditors.SimpleButton Button_Export;
        private DevExpress.XtraEditors.SimpleButton Button_Monitor;
    }
}