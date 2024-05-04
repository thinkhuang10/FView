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
        this.button5 = new System.Windows.Forms.Button();
        this.textBox3 = new System.Windows.Forms.TextBox();
        this.label3 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.button1 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.button5.Location = new System.Drawing.Point(242, 23);
        this.button5.Name = "button5";
        this.button5.Size = new System.Drawing.Size(30, 23);
        this.button5.TabIndex = 1;
        this.button5.Text = "...";
        this.button5.UseVisualStyleBackColor = true;
        this.button5.Click += new System.EventHandler(button5_Click);
        this.textBox3.Location = new System.Drawing.Point(59, 23);
        this.textBox3.Name = "textBox3";
        this.textBox3.Size = new System.Drawing.Size(177, 21);
        this.textBox3.TabIndex = 0;
        this.textBox3.Text = "true";
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(24, 26);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(29, 12);
        this.label3.TabIndex = 3;
        this.label3.Text = "条件";
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(24, 53);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(29, 12);
        this.label1.TabIndex = 4;
        this.label1.Text = "标签";
        this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Location = new System.Drawing.Point(59, 50);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new System.Drawing.Size(213, 20);
        this.comboBox1.TabIndex = 2;
        this.button1.Location = new System.Drawing.Point(43, 93);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 3;
        this.button1.Text = "保存";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.button2.Location = new System.Drawing.Point(179, 93);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 4;
        this.button2.Text = "关闭";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(296, 131);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.comboBox1);
        base.Controls.Add(this.button5);
        base.Controls.Add(this.textBox3);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "EventSetGoto";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "跳转标签";
        base.Load += new System.EventHandler(EventSetGoto_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
