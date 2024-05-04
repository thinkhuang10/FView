using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class PagePropertyForm : XtraForm
{

    private GroupBox groupBox1;

    private GroupBox groupBox2;

    private CheckBox checkBox1;

    private Label label4;

    private CheckBox checkBox7;

    private Label label9;

    private Label label8;

    private Label label7;

    private Label label6;

    private Button btnOK;

    private Button btnCancel;

    private System.Windows.Forms.ComboBox comboBox1;

    private TextBox textBox6;

    private TextBox textBox5;

    private TextBox textBox4;

    private TextBox textBox3;

    private CheckBox checkBox8;

    private CheckBox checkBox2;

    public PagePropertyForm()
    {
        InitializeComponent();
        if (CEditEnvironmentGlobal.childform == null)
        {
            Close();
        }

        Text = CEditEnvironmentGlobal.childform.theglobal.pageProp.ShowName + " 属性";
        textBox3.Text = CEditEnvironmentGlobal.childform.theglobal.pageProp.PageLocation.X.ToString();
        textBox4.Text = CEditEnvironmentGlobal.childform.theglobal.pageProp.PageLocation.Y.ToString();
        textBox5.Text = CEditEnvironmentGlobal.childform.theglobal.pageProp.PageSize.Width.ToString();
        textBox6.Text = CEditEnvironmentGlobal.childform.theglobal.pageProp.PageSize.Height.ToString();
        checkBox8.Checked = CEditEnvironmentGlobal.childform.theglobal.pageProp.PageVisible;
        checkBox7.Checked = CEditEnvironmentGlobal.childform.theglobal.pageProp.CloseOnBestrow;
        comboBox1.SelectedIndex = (CEditEnvironmentGlobal.childform.theglobal.pageProp.IsWindow ? 1 : 0);
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            CEditEnvironmentGlobal.childform.theglobal.pageProp.PageLocation = new Point(Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text));
            CEditEnvironmentGlobal.childform.theglobal.pageProp.PageSize = new Size(Convert.ToInt32(textBox5.Text), Convert.ToInt32(textBox6.Text));
            CEditEnvironmentGlobal.childform.theglobal.pageProp.PageVisible = checkBox8.Checked;
            CEditEnvironmentGlobal.childform.theglobal.pageProp.CloseOnBestrow = checkBox7.Checked;
            CEditEnvironmentGlobal.childform.theglobal.pageProp.CloseOnPart = checkBox2.Checked;
            CEditEnvironmentGlobal.childform.theglobal.pageProp.IsWindow = comboBox1.SelectedIndex == 1;
        }
        catch (Exception)
        {
        }
    }

    private void InitializeComponent()
    {
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.checkBox8 = new System.Windows.Forms.CheckBox();
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.checkBox2 = new System.Windows.Forms.CheckBox();
        this.checkBox7 = new System.Windows.Forms.CheckBox();
        this.checkBox1 = new System.Windows.Forms.CheckBox();
        this.label4 = new System.Windows.Forms.Label();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.textBox6 = new System.Windows.Forms.TextBox();
        this.textBox5 = new System.Windows.Forms.TextBox();
        this.textBox4 = new System.Windows.Forms.TextBox();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.label9 = new System.Windows.Forms.Label();
        this.label8 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.btnOK = new System.Windows.Forms.Button();
        this.btnCancel = new System.Windows.Forms.Button();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        base.SuspendLayout();
        this.groupBox1.Controls.Add(this.checkBox8);
        this.groupBox1.Controls.Add(this.comboBox1);
        this.groupBox1.Controls.Add(this.checkBox2);
        this.groupBox1.Controls.Add(this.checkBox7);
        this.groupBox1.Controls.Add(this.checkBox1);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Location = new System.Drawing.Point(19, 16);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(302, 196);
        this.groupBox1.TabIndex = 3;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "窗口风格";
        this.checkBox8.AutoSize = true;
        this.checkBox8.Location = new System.Drawing.Point(28, 104);
        this.checkBox8.Name = "checkBox8";
        this.checkBox8.Size = new System.Drawing.Size(74, 18);
        this.checkBox8.TabIndex = 3;
        this.checkBox8.Text = "初始显示";
        this.checkBox8.UseVisualStyleBackColor = true;
        this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Items.AddRange(new object[2] { "页面", "窗口" });
        this.comboBox1.Location = new System.Drawing.Point(90, 34);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new System.Drawing.Size(135, 22);
        this.comboBox1.TabIndex = 0;
        this.checkBox2.AutoSize = true;
        this.checkBox2.Location = new System.Drawing.Point(148, 71);
        this.checkBox2.Name = "checkBox2";
        this.checkBox2.Size = new System.Drawing.Size(122, 18);
        this.checkBox2.TabIndex = 2;
        this.checkBox2.Text = "被部分覆盖则关闭";
        this.checkBox2.UseVisualStyleBackColor = true;
        this.checkBox7.AutoSize = true;
        this.checkBox7.Location = new System.Drawing.Point(148, 104);
        this.checkBox7.Name = "checkBox7";
        this.checkBox7.Size = new System.Drawing.Size(122, 18);
        this.checkBox7.TabIndex = 2;
        this.checkBox7.Text = "被完全覆盖则关闭";
        this.checkBox7.UseVisualStyleBackColor = true;
        this.checkBox1.AutoSize = true;
        this.checkBox1.Checked = true;
        this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
        this.checkBox1.Enabled = false;
        this.checkBox1.Location = new System.Drawing.Point(28, 71);
        this.checkBox1.Name = "checkBox1";
        this.checkBox1.Size = new System.Drawing.Size(50, 18);
        this.checkBox1.TabIndex = 1;
        this.checkBox1.Text = "标题";
        this.checkBox1.UseVisualStyleBackColor = true;
        this.label4.AutoSize = true;
        this.label4.Enabled = false;
        this.label4.Location = new System.Drawing.Point(26, 37);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(55, 14);
        this.label4.TabIndex = 5;
        this.label4.Text = "显示风格";
        this.groupBox2.Controls.Add(this.textBox6);
        this.groupBox2.Controls.Add(this.textBox5);
        this.groupBox2.Controls.Add(this.textBox4);
        this.groupBox2.Controls.Add(this.textBox3);
        this.groupBox2.Controls.Add(this.label9);
        this.groupBox2.Controls.Add(this.label8);
        this.groupBox2.Controls.Add(this.label7);
        this.groupBox2.Controls.Add(this.label6);
        this.groupBox2.Location = new System.Drawing.Point(338, 16);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(232, 196);
        this.groupBox2.TabIndex = 4;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "位置大小";
        this.textBox6.Location = new System.Drawing.Point(124, 139);
        this.textBox6.Name = "textBox6";
        this.textBox6.Size = new System.Drawing.Size(72, 22);
        this.textBox6.TabIndex = 3;
        this.textBox5.Location = new System.Drawing.Point(124, 100);
        this.textBox5.Name = "textBox5";
        this.textBox5.Size = new System.Drawing.Size(72, 22);
        this.textBox5.TabIndex = 2;
        this.textBox4.Location = new System.Drawing.Point(124, 65);
        this.textBox4.Name = "textBox4";
        this.textBox4.Size = new System.Drawing.Size(72, 22);
        this.textBox4.TabIndex = 1;
        this.textBox3.Location = new System.Drawing.Point(124, 34);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(72, 22);
        this.textBox3.TabIndex = 0;
        this.label9.AutoSize = true;
        this.label9.Location = new System.Drawing.Point(83, 142);
        this.label9.Name = "label9";
        this.label9.Size = new System.Drawing.Size(31, 14);
        this.label9.TabIndex = 17;
        this.label9.Text = "高度";
        this.label8.AutoSize = true;
        this.label8.Location = new System.Drawing.Point(83, 104);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(31, 14);
        this.label8.TabIndex = 16;
        this.label8.Text = "宽度";
        this.label7.AutoSize = true;
        this.label7.Location = new System.Drawing.Point(36, 69);
        this.label7.Name = "label7";
        this.label7.Size = new System.Drawing.Size(75, 14);
        this.label7.TabIndex = 15;
        this.label7.Text = "左上角Y坐标";
        this.label6.AutoSize = true;
        this.label6.Location = new System.Drawing.Point(36, 37);
        this.label6.Name = "label6";
        this.label6.Size = new System.Drawing.Size(74, 14);
        this.label6.TabIndex = 14;
        this.label6.Text = "左上角X坐标";
        this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.btnOK.Location = new System.Drawing.Point(357, 218);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new System.Drawing.Size(87, 27);
        this.btnOK.TabIndex = 5;
        this.btnOK.Text = "确认";
        this.btnOK.UseVisualStyleBackColor = true;
        this.btnOK.Click += new System.EventHandler(btnOK_Click);
        this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.btnCancel.Location = new System.Drawing.Point(483, 218);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(87, 27);
        this.btnCancel.TabIndex = 6;
        this.btnCancel.Text = "取消";
        this.btnCancel.UseVisualStyleBackColor = true;
        base.AcceptButton = this.btnOK;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = this.btnCancel;
        base.ClientSize = new System.Drawing.Size(589, 256);
        base.Controls.Add(this.btnCancel);
        base.Controls.Add(this.btnOK);
        base.Controls.Add(this.groupBox2);
        base.Controls.Add(this.groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "PagePropertyForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "页面属性";
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        base.ResumeLayout(false);
    }
}
