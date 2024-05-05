using ShapeRuntime;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class EventSetGoto : Form
{
    private EventSetItem result;

    private Button button5;

    private TextBox textBox3;

    private Label label3;

    private Label label1;

    private ComboBox comboBox1;

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

    public EventSetGoto(List<string> items)
    {
        InitializeComponent();
        comboBox1.Items.AddRange(items.ToArray());
        if (comboBox1.Items.Count > 0)
        {
            comboBox1.SelectedIndex = 0;
        }
    }

    private void EventSetGoto_Load(object sender, EventArgs e)
    {
        if (result != null)
        {
            comboBox1.Text = result.FromObject;
            textBox3.Text = result.Condition;
        }
    }

    private void button5_Click(object sender, EventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent == "")
        {
            textBox3.Text = "true";
        }
        else
        {
            textBox3.Text = "[" + varTableEvent + "]";
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (!(textBox3.Text == "") && !(comboBox1.Text == ""))
        {
            EventSetItem eventSetItem = new()
            {
                OperationType = "跳转标签",
                FromObject = comboBox1.Text,
                Condition = textBox3.Text
            };
            result = eventSetItem;
            base.DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void InitializeComponent()
    {
        button5 = new System.Windows.Forms.Button();
        textBox3 = new System.Windows.Forms.TextBox();
        label3 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        comboBox1 = new System.Windows.Forms.ComboBox();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button5.Location = new System.Drawing.Point(242, 23);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(30, 23);
        button5.TabIndex = 1;
        button5.Text = "...";
        button5.UseVisualStyleBackColor = true;
        button5.Click += new System.EventHandler(button5_Click);
        textBox3.Location = new System.Drawing.Point(59, 23);
        textBox3.Name = "textBox3";
        textBox3.Size = new System.Drawing.Size(177, 21);
        textBox3.TabIndex = 0;
        textBox3.Text = "true";
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(24, 26);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(29, 12);
        label3.TabIndex = 3;
        label3.Text = "条件";
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(24, 53);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(29, 12);
        label1.TabIndex = 4;
        label1.Text = "标签";
        comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new System.Drawing.Point(59, 50);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(213, 20);
        comboBox1.TabIndex = 2;
        button1.Location = new System.Drawing.Point(43, 93);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 3;
        button1.Text = "保存";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.Location = new System.Drawing.Point(179, 93);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 4;
        button2.Text = "关闭";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(296, 131);
        base.Controls.Add(button2);
        base.Controls.Add(button1);
        base.Controls.Add(comboBox1);
        base.Controls.Add(button5);
        base.Controls.Add(textBox3);
        base.Controls.Add(label3);
        base.Controls.Add(label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "EventSetGoto";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "跳转标签";
        base.Load += new System.EventHandler(EventSetGoto_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
