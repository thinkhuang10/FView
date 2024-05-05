using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class EventSetLabel : Form
{
    private EventSetItem result;

    private readonly List<string> inusestr = new();

    private Label label1;

    private TextBox textBox1;

    private Button button1;

    private Button button2;

    public EventSetItem Result
    {
        get
        {
            return result;
        }
        set
        {
            result = value;
        }
    }

    public EventSetLabel()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (textBox1.Text.Trim() == "")
        {
            return;
        }
        Regex regex = new("^[a-zA-Z_][a-zA-Z0-9_]*$");
        if (!regex.IsMatch(textBox1.Text))
        {
            MessageBox.Show("标签中仅可以由字母数字以及下划线组成，并且不能以数字开头。", "错误");
            return;
        }
        foreach (string item in inusestr)
        {
            if (textBox1.Text.Trim() == item)
            {
                MessageBox.Show("标签名称已经存在，请修改标签名称。", "错误");
                return;
            }
        }
        EventSetItem eventSetItem = new()
        {
            OperationType = "定义标签",
            FromObject = textBox1.Text.Trim()
        };
        result = eventSetItem;
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void EventSetLabel_Load(object sender, EventArgs e)
    {
        if (result != null)
        {
            textBox1.Text = result.FromObject;
            inusestr.Remove(result.FromObject);
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void InitializeComponent()
    {
        label1 = new System.Windows.Forms.Label();
        textBox1 = new System.Windows.Forms.TextBox();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(22, 26);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(53, 12);
        label1.TabIndex = 0;
        label1.Text = "标签名称";
        textBox1.Location = new System.Drawing.Point(81, 23);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(186, 21);
        textBox1.TabIndex = 0;
        button1.Location = new System.Drawing.Point(36, 72);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 1;
        button1.Text = "保存";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.Location = new System.Drawing.Point(177, 72);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 2;
        button2.Text = "关闭";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(289, 121);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.Controls.Add(textBox1);
        base.Controls.Add(label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "EventSetLabel";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "定义标签";
        base.Load += new System.EventHandler(EventSetLabel_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
