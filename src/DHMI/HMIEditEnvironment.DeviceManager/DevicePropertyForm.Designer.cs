namespace HMIEditEnvironment.DeviceManager
{
    partial class DevicePropertyForm
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
            this.Button_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.Button_OK = new DevExpress.XtraEditors.SimpleButton();
            this.PropertyGridControl = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.TablePanel = new DevExpress.Utils.Layout.TablePanel();
            this.PanelControl = new DevExpress.XtraEditors.PanelControl();
            this.TreeView_Device = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.PropertyGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablePanel)).BeginInit();
            this.TablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PanelControl)).BeginInit();
            this.PanelControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(288, 13);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 1;
            this.Button_Cancel.Text = "取消";
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_OK
            // 
            this.Button_OK.Location = new System.Drawing.Point(201, 13);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 0;
            this.Button_OK.Text = "确定";
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // PropertyGridControl
            // 
            this.TablePanel.SetColumn(this.PropertyGridControl, 1);
            this.PropertyGridControl.Cursor = System.Windows.Forms.Cursors.Default;
            this.PropertyGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyGridControl.Location = new System.Drawing.Point(203, 10);
            this.PropertyGridControl.Margin = new System.Windows.Forms.Padding(0);
            this.PropertyGridControl.Name = "PropertyGridControl";
            this.PropertyGridControl.OptionsView.AllowReadOnlyRowAppearance = DevExpress.Utils.DefaultBoolean.True;
            this.TablePanel.SetRow(this.PropertyGridControl, 0);
            this.PropertyGridControl.Size = new System.Drawing.Size(384, 298);
            this.PropertyGridControl.TabIndex = 1;
            this.PropertyGridControl.CustomPropertyDescriptors += new DevExpress.XtraVerticalGrid.Events.CustomPropertyDescriptorsEventHandler(this.PropertyGridControl_CustomPropertyDescriptors);
            // 
            // TablePanel
            // 
            this.TablePanel.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 5F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 10F)});
            this.TablePanel.Controls.Add(this.TreeView_Device);
            this.TablePanel.Controls.Add(this.PanelControl);
            this.TablePanel.Controls.Add(this.PropertyGridControl);
            this.TablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TablePanel.Location = new System.Drawing.Point(0, 0);
            this.TablePanel.Name = "TablePanel";
            this.TablePanel.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 298F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.TablePanel.Size = new System.Drawing.Size(598, 368);
            this.TablePanel.TabIndex = 2;
            this.TablePanel.UseSkinIndents = true;
            // 
            // PanelControl
            // 
            this.TablePanel.SetColumn(this.PanelControl, 1);
            this.PanelControl.Controls.Add(this.Button_OK);
            this.PanelControl.Controls.Add(this.Button_Cancel);
            this.PanelControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelControl.Location = new System.Drawing.Point(203, 308);
            this.PanelControl.Margin = new System.Windows.Forms.Padding(0);
            this.PanelControl.Name = "PanelControl";
            this.TablePanel.SetRow(this.PanelControl, 1);
            this.PanelControl.Size = new System.Drawing.Size(384, 49);
            this.PanelControl.TabIndex = 2;
            // 
            // TreeView_Device
            // 
            this.TablePanel.SetColumn(this.TreeView_Device, 0);
            this.TreeView_Device.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView_Device.Location = new System.Drawing.Point(11, 10);
            this.TreeView_Device.Margin = new System.Windows.Forms.Padding(0);
            this.TreeView_Device.Name = "TreeView_Device";
            this.TablePanel.SetRow(this.TreeView_Device, 0);
            this.TreeView_Device.Size = new System.Drawing.Size(192, 298);
            this.TreeView_Device.TabIndex = 3;
            this.TreeView_Device.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_Device_NodeMouseClick);
            // 
            // DevicePropertyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(598, 368);
            this.Controls.Add(this.TablePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DevicePropertyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设备属性";
            this.Load += new System.EventHandler(this.DeviceProperty_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PropertyGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablePanel)).EndInit();
            this.TablePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PanelControl)).EndInit();
            this.PanelControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton Button_Cancel;
        private DevExpress.XtraEditors.SimpleButton Button_OK;
        private DevExpress.XtraVerticalGrid.PropertyGridControl PropertyGridControl;
        private DevExpress.Utils.Layout.TablePanel TablePanel;
        private DevExpress.XtraEditors.PanelControl PanelControl;
        private System.Windows.Forms.TreeView TreeView_Device;
    }
}