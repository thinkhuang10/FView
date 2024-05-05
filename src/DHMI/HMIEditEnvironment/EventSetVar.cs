using ShapeRuntime;
using System;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class EventSetVar : Form
{
    private EventSetItem result;

    private Label label1;

    private Label label2;

    private TextBox textBox1;

    private TextBox textBox2;

    private Button button1;

    private Button button2;

    private Button button3;

    private Button button4;

    private Label label3;

    private TextBox textBox3;

    private Button button5;

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

    public EventSetVar()
    {
        InitializeComponent();
    }

    public EventSetVar(EventSetItem item)
    {
        InitializeComponent();
        result = item;
    }

    private void button3_Click(object sender, EventArgs e)
    {
        textBox1.Text = CForDCCEControl.GetVarTableEvent("");
    }

    private void button4_Click(object sender, EventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent != "")
        {
            textBox2.Text = "[" + varTableEvent + "]";
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (!(textBox2.Text == ""))
        {
            EventSetItem eventSetItem = new()
            {
                OperationType = "变量赋值",
                FromObject = textBox2.Text
            };
            KVPart<string, string> toObject = new(textBox1.Text, "System.Object");
            eventSetItem.ToObject = toObject;
            eventSetItem.Condition = textBox3.Text;
            result = eventSetItem;
            base.DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void EventSetVar_Load(object sender, EventArgs e)
    {
        try
        {
            if (result != null)
            {
                if (result.Condition == null)
                {
                    result.Condition = "true";
                }
                textBox3.Text = result.Condition;
                textBox1.Text = result.ToObject.Key;
                textBox2.Text = result.FromObject;
            }
        }
        catch
        {
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

    private void InitializeComponent()
    {
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        textBox1 = new System.Windows.Forms.TextBox();
        textBox2 = new System.Windows.Forms.TextBox();
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        button3 = new System.Windows.Forms.Button();
        button4 = new System.Windows.Forms.Button();
        label3 = new System.Windows.Forms.Label();
        textBox3 = new System.Windows.Forms.TextBox();
        button5 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(13, 42);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(29, 12);
        label1.TabIndex = 0;
        label1.Text = "变量";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(13, 69);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(17, 12);
        label2.TabIndex = 0;
        label2.Text = "值";
        textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        textBox1.Location = new System.Drawing.Point(48, 39);
        textBox1.Name = "textBox1";
        textBox1.ReadOnly = true;
        textBox1.Size = new System.Drawing.Size(350, 21);
        textBox1.TabIndex = 2;
        textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        textBox2.Location = new System.Drawing.Point(48, 66);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(350, 21);
        textBox2.TabIndex = 4;
        button1.Location = new System.Drawing.Point(100, 101);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 6;
        button1.Text = "保  存";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button2.Location = new System.Drawing.Point(271, 101);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 7;
        button2.Text = "关  闭";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button3.Location = new System.Drawing.Point(404, 39);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(30, 23);
        button3.TabIndex = 3;
        button3.Text = "...";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        button4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button4.Location = new System.Drawing.Point(404, 64);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(30, 23);
        button4.TabIndex = 5;
        button4.Text = "...";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(button4_Click);
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(13, 15);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(29, 12);
        label3.TabIndex = 0;
        label3.Text = "条件";
        textBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        textBox3.Location = new System.Drawing.Point(48, 12);
        textBox3.Name = "textBox3";
        textBox3.Size = new System.Drawing.Size(350, 21);
        textBox3.TabIndex = 0;
        textBox3.Text = "true";
        button5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button5.Location = new System.Drawing.Point(404, 12);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(30, 23);
        button5.TabIndex = 1;
        button5.Text = "...";
        button5.UseVisualStyleBackColor = true;
        button5.Click += new System.EventHandler(button5_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(446, 133);
        base.Controls.Add(button2);
        base.Controls.Add(button4);
        base.Controls.Add(button5);
        base.Controls.Add(button3);
        base.Controls.Add(button1);
        base.Controls.Add(textBox2);
        base.Controls.Add(textBox3);
        base.Controls.Add(textBox1);
        base.Controls.Add(label3);
        base.Controls.Add(label2);
        base.Controls.Add(label1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "EventSetVar";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "变量赋值";
        base.Load += new System.EventHandler(EventSetVar_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
