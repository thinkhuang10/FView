using System;
using System.Windows.Forms;

namespace HMIEditEnvironment.Animation;

public class DBOperationTypeSelect : Form
{
    public string OperationType = "";

    private GroupBox groupBox1;

    private RadioButton radioButton4;

    private RadioButton radioButton3;

    private RadioButton radioButton2;

    private RadioButton radioButton1;

    private Button button1;

    private Button button2;

    public DBOperationTypeSelect()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        foreach (Control control in groupBox1.Controls)
        {
            if (control is RadioButton && (control as RadioButton).Checked)
            {
                OperationType = control.Text;
                break;
            }
        }
        if (OperationType != "")
        {
            base.DialogResult = DialogResult.OK;
        }
        Close();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void InitializeComponent()
    {
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.radioButton4 = new System.Windows.Forms.RadioButton();
        this.radioButton3 = new System.Windows.Forms.RadioButton();
        this.radioButton2 = new System.Windows.Forms.RadioButton();
        this.radioButton1 = new System.Windows.Forms.RadioButton();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();

        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.groupBox1.Controls.Add(this.radioButton4);
        this.groupBox1.Controls.Add(this.radioButton3);
        this.groupBox1.Controls.Add(this.radioButton2);
        this.groupBox1.Controls.Add(this.radioButton1);
        this.groupBox1.Location = new System.Drawing.Point(12, 12);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(201, 76);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "操作方式";
        this.radioButton4.AutoSize = true;
        this.radioButton4.Location = new System.Drawing.Point(117, 44);
        this.radioButton4.Name = "radioButton4";
        this.radioButton4.Size = new System.Drawing.Size(71, 16);
        this.radioButton4.TabIndex = 3;
        this.radioButton4.TabStop = true;
        this.radioButton4.Text = "删除数据";
        this.radioButton4.UseVisualStyleBackColor = true;
        this.radioButton3.AutoSize = true;
        this.radioButton3.Location = new System.Drawing.Point(117, 21);
        this.radioButton3.Name = "radioButton3";
        this.radioButton3.Size = new System.Drawing.Size(71, 16);
        this.radioButton3.TabIndex = 1;
        this.radioButton3.TabStop = true;
        this.radioButton3.Text = "修改数据";
        this.radioButton3.UseVisualStyleBackColor = true;
        this.radioButton2.AutoSize = true;
        this.radioButton2.Location = new System.Drawing.Point(7, 44);
        this.radioButton2.Name = "radioButton2";
        this.radioButton2.Size = new System.Drawing.Size(71, 16);
        this.radioButton2.TabIndex = 2;
        this.radioButton2.TabStop = true;
        this.radioButton2.Text = "添加数据";
        this.radioButton2.UseVisualStyleBackColor = true;
        this.radioButton1.AutoSize = true;
        this.radioButton1.Location = new System.Drawing.Point(7, 21);
        this.radioButton1.Name = "radioButton1";
        this.radioButton1.Size = new System.Drawing.Size(71, 16);
        this.radioButton1.TabIndex = 0;
        this.radioButton1.TabStop = true;
        this.radioButton1.Text = "查询数据";
        this.radioButton1.UseVisualStyleBackColor = true;
        this.button1.Location = new System.Drawing.Point(22, 95);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 1;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.Location = new System.Drawing.Point(128, 95);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 2;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(225, 129);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "DBOperationTypeSelect";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "选择操作方式";
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
    }
}