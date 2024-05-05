namespace HMIRunForm
{
    partial class RunForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HMIRunForm.RunForm));
            TopPanel = new System.Windows.Forms.Panel();
            menuStrip = new System.Windows.Forms.MenuStrip();
            数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            数据库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            全屏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            标准ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            屏幕键盘ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            计算其ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            截图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            保存到文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            打印ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            menuStrip.SuspendLayout();
            base.SuspendLayout();
            TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            TopPanel.Location = new System.Drawing.Point(0, 0);
            TopPanel.Name = "TopPanel";
            TopPanel.Size = new System.Drawing.Size(792, 1);
            TopPanel.TabIndex = 1;
            TopPanel.Visible = false;
            TopPanel.Paint += new System.Windows.Forms.PaintEventHandler(TopPanel_Paint);
            TopPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(TopPanel_MouseClick);
            TopPanel.MouseEnter += new System.EventHandler(TopPanel_MouseEnter);
            TopPanel.MouseLeave += new System.EventHandler(TopPanel_MouseLeave);
            menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { 数据库ToolStripMenuItem, 视图ToolStripMenuItem, 工具ToolStripMenuItem, 截图ToolStripMenuItem });
            menuStrip.Location = new System.Drawing.Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new System.Drawing.Size(792, 25);
            menuStrip.TabIndex = 2;
            menuStrip.Text = "menuStrip";
            menuStrip.Visible = false;
            数据库ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { 数据库管理ToolStripMenuItem });
            数据库ToolStripMenuItem.Name = "数据库ToolStripMenuItem";
            数据库ToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            数据库ToolStripMenuItem.Text = "数据库";
            数据库管理ToolStripMenuItem.Name = "数据库管理ToolStripMenuItem";
            数据库管理ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            数据库管理ToolStripMenuItem.Text = "数据库管理";
            数据库管理ToolStripMenuItem.Click += new System.EventHandler(数据库管理ToolStripMenuItem_Click);
            视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { 全屏ToolStripMenuItem, 标准ToolStripMenuItem });
            视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            视图ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            视图ToolStripMenuItem.Text = "视图";
            全屏ToolStripMenuItem.Name = "全屏ToolStripMenuItem";
            全屏ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            全屏ToolStripMenuItem.Text = "全屏";
            全屏ToolStripMenuItem.Click += new System.EventHandler(全屏ToolStripMenuItem_Click);
            标准ToolStripMenuItem.Name = "标准ToolStripMenuItem";
            标准ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            标准ToolStripMenuItem.Text = "标准";
            标准ToolStripMenuItem.Click += new System.EventHandler(标准ToolStripMenuItem_Click);
            工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { 屏幕键盘ToolStripMenuItem, 计算其ToolStripMenuItem });
            工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            工具ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            工具ToolStripMenuItem.Text = "工具";
            屏幕键盘ToolStripMenuItem.Name = "屏幕键盘ToolStripMenuItem";
            屏幕键盘ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            屏幕键盘ToolStripMenuItem.Text = "屏幕键盘";
            屏幕键盘ToolStripMenuItem.Click += new System.EventHandler(屏幕键盘ToolStripMenuItem_Click);
            计算其ToolStripMenuItem.Name = "计算其ToolStripMenuItem";
            计算其ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            计算其ToolStripMenuItem.Text = "计算器";
            计算其ToolStripMenuItem.Click += new System.EventHandler(计算其ToolStripMenuItem_Click);
            截图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { 保存到文件ToolStripMenuItem, 打印ToolStripMenuItem });
            截图ToolStripMenuItem.Name = "截图ToolStripMenuItem";
            截图ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            截图ToolStripMenuItem.Text = "截图";
            截图ToolStripMenuItem.Visible = false;
            保存到文件ToolStripMenuItem.Name = "保存到文件ToolStripMenuItem";
            保存到文件ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            保存到文件ToolStripMenuItem.Text = "保存文件";
            保存到文件ToolStripMenuItem.Click += new System.EventHandler(保存到文件ToolStripMenuItem_Click);
            打印ToolStripMenuItem.Name = "打印ToolStripMenuItem";
            打印ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            打印ToolStripMenuItem.Text = "打印";
            打印ToolStripMenuItem.Click += new System.EventHandler(打印ToolStripMenuItem_Click);
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(792, 573);
            base.Controls.Add(TopPanel);
            base.Controls.Add(menuStrip);
            DoubleBuffered = true;
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            base.KeyPreview = true;
            base.MainMenuStrip = menuStrip;
            base.Name = "RunForm";
            base.ShowIcon = false;
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Form1_FormClosing);
            base.Load += new System.EventHandler(RunForm_Load);
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion
    }
}
