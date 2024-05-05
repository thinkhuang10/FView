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
        groupBox1 = new System.Windows.Forms.GroupBox();
        radioButton4 = new System.Windows.Forms.RadioButton();
        radioButton3 = new System.Windows.Forms.RadioButton();
        radioButton2 = new System.Windows.Forms.RadioButton();
        radioButton1 = new System.Windows.Forms.RadioButton();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();

        groupBox1.SuspendLayout();
        base.SuspendLayout();
        groupBox1.Controls.Add(radioButton4);
        groupBox1.Controls.Add(radioButton3);
        groupBox1.Controls.Add(radioButton2);
        groupBox1.Controls.Add(radioButton1);
        groupBox1.Location = new System.Drawing.Point(12, 12);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new System.Drawing.Size(201, 76);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "操作方式";
        radioButton4.AutoSize = true;
        radioButton4.Location = new System.Drawing.Point(117, 44);
        radioButton4.Name = "radioButton4";
        radioButton4.Size = new System.Drawing.Size(71, 16);
        radioButton4.TabIndex = 3;
        radioButton4.TabStop = true;
        radioButton4.Text = "删除数据";
        radioButton4.UseVisualStyleBackColor = true;
        radioButton3.AutoSize = true;
        radioButton3.Location = new System.Drawing.Point(117, 21);
        radioButton3.Name = "radioButton3";
        radioButton3.Size = new System.Drawing.Size(71, 16);
        radioButton3.TabIndex = 1;
        radioButton3.TabStop = true;
        radioButton3.Text = "修改数据";
        radioButton3.UseVisualStyleBackColor = true;
        radioButton2.AutoSize = true;
        radioButton2.Location = new System.Drawing.Point(7, 44);
        radioButton2.Name = "radioButton2";
        radioButton2.Size = new System.Drawing.Size(71, 16);
        radioButton2.TabIndex = 2;
        radioButton2.TabStop = true;
        radioButton2.Text = "添加数据";
        radioButton2.UseVisualStyleBackColor = true;
        radioButton1.AutoSize = true;
        radioButton1.Location = new System.Drawing.Point(7, 21);
        radioButton1.Name = "radioButton1";
        radioButton1.Size = new System.Drawing.Size(71, 16);
        radioButton1.TabIndex = 0;
        radioButton1.TabStop = true;
        radioButton1.Text = "查询数据";
        radioButton1.UseVisualStyleBackColor = true;
        button1.Location = new System.Drawing.Point(22, 95);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 1;
        button1.Text = "确定";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.Location = new System.Drawing.Point(128, 95);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 2;
        button2.Text = "取消";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(225, 129);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.Controls.Add(groupBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "DBOperationTypeSelect";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "选择操作方式";
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        base.ResumeLayout(false);
    }
}