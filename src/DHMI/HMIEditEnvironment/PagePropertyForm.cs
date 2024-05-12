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
        if (CEditEnvironmentGlobal.ChildForm == null)
        {
            Close();
        }

        Text = CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.ShowName + " 属性";
        textBox3.Text = CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.PageLocation.X.ToString();
        textBox4.Text = CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.PageLocation.Y.ToString();
        textBox5.Text = CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.PageSize.Width.ToString();
        textBox6.Text = CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.PageSize.Height.ToString();
        checkBox8.Checked = CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.PageVisible;
        checkBox7.Checked = CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.CloseOnBestrow;
        comboBox1.SelectedIndex = (CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.IsWindow ? 1 : 0);
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.PageLocation = new Point(Convert.ToInt32(textBox3.Text), Convert.ToInt32(textBox4.Text));
            CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.PageSize = new Size(Convert.ToInt32(textBox5.Text), Convert.ToInt32(textBox6.Text));
            CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.PageVisible = checkBox8.Checked;
            CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.CloseOnBestrow = checkBox7.Checked;
            CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.CloseOnPart = checkBox2.Checked;
            CEditEnvironmentGlobal.ChildForm.theglobal.pageProp.IsWindow = comboBox1.SelectedIndex == 1;
        }
        catch (Exception)
        {
        }
    }

    private void InitializeComponent()
    {
        groupBox1 = new System.Windows.Forms.GroupBox();
        checkBox8 = new System.Windows.Forms.CheckBox();
        comboBox1 = new System.Windows.Forms.ComboBox();
        checkBox2 = new System.Windows.Forms.CheckBox();
        checkBox7 = new System.Windows.Forms.CheckBox();
        checkBox1 = new System.Windows.Forms.CheckBox();
        label4 = new System.Windows.Forms.Label();
        groupBox2 = new System.Windows.Forms.GroupBox();
        textBox6 = new System.Windows.Forms.TextBox();
        textBox5 = new System.Windows.Forms.TextBox();
        textBox4 = new System.Windows.Forms.TextBox();
        textBox3 = new System.Windows.Forms.TextBox();
        label9 = new System.Windows.Forms.Label();
        label8 = new System.Windows.Forms.Label();
        label7 = new System.Windows.Forms.Label();
        label6 = new System.Windows.Forms.Label();
        btnOK = new System.Windows.Forms.Button();
        btnCancel = new System.Windows.Forms.Button();
        groupBox1.SuspendLayout();
        groupBox2.SuspendLayout();
        base.SuspendLayout();
        groupBox1.Controls.Add(checkBox8);
        groupBox1.Controls.Add(comboBox1);
        groupBox1.Controls.Add(checkBox2);
        groupBox1.Controls.Add(checkBox7);
        groupBox1.Controls.Add(checkBox1);
        groupBox1.Controls.Add(label4);
        groupBox1.Location = new System.Drawing.Point(19, 16);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(302, 196);
        groupBox1.TabIndex = 3;
        groupBox1.TabStop = false;
        groupBox1.Text = "窗口风格";
        checkBox8.AutoSize = true;
        checkBox8.Location = new System.Drawing.Point(28, 104);
        checkBox8.Name = "checkBox8";
        checkBox8.Size = new System.Drawing.Size(74, 18);
        checkBox8.TabIndex = 3;
        checkBox8.Text = "初始显示";
        checkBox8.UseVisualStyleBackColor = true;
        comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Items.AddRange(new object[2] { "页面", "窗口" });
        comboBox1.Location = new System.Drawing.Point(90, 34);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(135, 22);
        comboBox1.TabIndex = 0;
        checkBox2.AutoSize = true;
        checkBox2.Location = new System.Drawing.Point(148, 71);
        checkBox2.Name = "checkBox2";
        checkBox2.Size = new System.Drawing.Size(122, 18);
        checkBox2.TabIndex = 2;
        checkBox2.Text = "被部分覆盖则关闭";
        checkBox2.UseVisualStyleBackColor = true;
        checkBox7.AutoSize = true;
        checkBox7.Location = new System.Drawing.Point(148, 104);
        checkBox7.Name = "checkBox7";
        checkBox7.Size = new System.Drawing.Size(122, 18);
        checkBox7.TabIndex = 2;
        checkBox7.Text = "被完全覆盖则关闭";
        checkBox7.UseVisualStyleBackColor = true;
        checkBox1.AutoSize = true;
        checkBox1.Checked = true;
        checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
        checkBox1.Enabled = false;
        checkBox1.Location = new System.Drawing.Point(28, 71);
        checkBox1.Name = "checkBox1";
        checkBox1.Size = new System.Drawing.Size(50, 18);
        checkBox1.TabIndex = 1;
        checkBox1.Text = "标题";
        checkBox1.UseVisualStyleBackColor = true;
        label4.AutoSize = true;
        label4.Enabled = false;
        label4.Location = new System.Drawing.Point(26, 37);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(55, 14);
        label4.TabIndex = 5;
        label4.Text = "显示风格";
        groupBox2.Controls.Add(textBox6);
        groupBox2.Controls.Add(textBox5);
        groupBox2.Controls.Add(textBox4);
        groupBox2.Controls.Add(textBox3);
        groupBox2.Controls.Add(label9);
        groupBox2.Controls.Add(label8);
        groupBox2.Controls.Add(label7);
        groupBox2.Controls.Add(label6);
        groupBox2.Location = new System.Drawing.Point(338, 16);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new System.Drawing.Size(232, 196);
        groupBox2.TabIndex = 4;
        groupBox2.TabStop = false;
        groupBox2.Text = "位置大小";
        textBox6.Location = new System.Drawing.Point(124, 139);
        textBox6.Name = "textBox6";
        textBox6.Size = new System.Drawing.Size(72, 22);
        textBox6.TabIndex = 3;
        textBox5.Location = new System.Drawing.Point(124, 100);
        textBox5.Name = "textBox5";
        textBox5.Size = new System.Drawing.Size(72, 22);
        textBox5.TabIndex = 2;
        textBox4.Location = new System.Drawing.Point(124, 65);
        textBox4.Name = "textBox4";
        textBox4.Size = new System.Drawing.Size(72, 22);
        textBox4.TabIndex = 1;
        textBox3.Location = new System.Drawing.Point(124, 34);
        textBox3.Name = "textBox3";
        textBox3.Size = new System.Drawing.Size(72, 22);
        textBox3.TabIndex = 0;
        label9.AutoSize = true;
        label9.Location = new System.Drawing.Point(83, 142);
        label9.Name = "label9";
        label9.Size = new System.Drawing.Size(31, 14);
        label9.TabIndex = 17;
        label9.Text = "高度";
        label8.AutoSize = true;
        label8.Location = new System.Drawing.Point(83, 104);
        label8.Name = "label8";
        label8.Size = new System.Drawing.Size(31, 14);
        label8.TabIndex = 16;
        label8.Text = "宽度";
        label7.AutoSize = true;
        label7.Location = new System.Drawing.Point(36, 69);
        label7.Name = "label7";
        label7.Size = new System.Drawing.Size(75, 14);
        label7.TabIndex = 15;
        label7.Text = "左上角Y坐标";
        label6.AutoSize = true;
        label6.Location = new System.Drawing.Point(36, 37);
        label6.Name = "label6";
        label6.Size = new System.Drawing.Size(74, 14);
        label6.TabIndex = 14;
        label6.Text = "左上角X坐标";
        btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
        btnOK.Location = new System.Drawing.Point(357, 218);
        btnOK.Name = "btnOK";
        btnOK.Size = new System.Drawing.Size(87, 27);
        btnOK.TabIndex = 5;
        btnOK.Text = "确认";
        btnOK.UseVisualStyleBackColor = true;
        btnOK.Click += new System.EventHandler(btnOK_Click);
        btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        btnCancel.Location = new System.Drawing.Point(483, 218);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new System.Drawing.Size(87, 27);
        btnCancel.TabIndex = 6;
        btnCancel.Text = "取消";
        btnCancel.UseVisualStyleBackColor = true;
        base.AcceptButton = btnOK;
        base.AutoScaleDimensions = new System.Drawing.SizeF(7f, 14f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.CancelButton = btnCancel;
        base.ClientSize = new System.Drawing.Size(589, 256);
        base.Controls.Add(btnCancel);
        base.Controls.Add(btnOK);
        base.Controls.Add(groupBox2);
        base.Controls.Add(groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "PagePropertyForm";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "页面属性";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        base.ResumeLayout(false);
    }
}
