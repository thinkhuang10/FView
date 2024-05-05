using ShapeRuntime;
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class EventSetProperty : Form
{
    private EventSetItem result;

    private Button button1;

    private ComboBox comboBox1;

    private TextBox textBox1;

    private Button button2;

    private Label label1;

    private Label label2;

    private Button button3;

    private ComboBox comboBox2;

    private ComboBox comboBox3;

    private Label label3;

    private Label label4;

    private Label label5;

    private TextBox textBox2;

    private Button button4;

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

    public EventSetProperty()
    {
        InitializeComponent();
    }

    public EventSetProperty(EventSetItem item)
    {
        InitializeComponent();
        result = item;
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
    }

    private void EventSetProperty_Load(object sender, EventArgs e)
    {
        foreach (DataFile df in CEditEnvironmentGlobal.dfs)
        {
            comboBox3.Items.Add(new KVPart<string, DataFile>(df.pageName, df));
        }
        comboBox3.Text = CEditEnvironmentGlobal.childform.theglobal.df.pageName;
        if (comboBox3.SelectedIndex == -1 && comboBox3.Items.Count > 0)
        {
            comboBox3.SelectedIndex = 0;
        }
        try
        {
            if (result == null)
            {
                return;
            }
            if (result.Condition == null)
            {
                result.Condition = "true";
            }
            textBox2.Text = result.Condition;
            textBox1.Text = result.FromObject;
            foreach (KVPart<string, DataFile> item in comboBox3.Items)
            {
                if (item.Value.name == result.ToObject.Key.Split('.')[0])
                {
                    comboBox3.SelectedItem = item;
                    comboBox2.Text = result.ToObject.Key.Split('.')[1];
                    comboBox1.Text = result.ToObject.Key.Split('.')[2];
                }
            }
        }
        catch
        {
        }
    }

    private void button3_Click(object sender, EventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent != "")
        {
            textBox1.Text = "[" + varTableEvent + "]";
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (!(textBox1.Text == "") && comboBox1.SelectedItem != null)
        {
            EventSetItem eventSetItem = new()
            {
                OperationType = "属性赋值",
                FromObject = textBox1.Text,
                Condition = textBox2.Text
            };
            string name = ((KVPart<string, DataFile>)comboBox3.SelectedItem).Value.name;
            string key = ((KVPart<string, CShape>)comboBox2.SelectedItem).Key;
            string key2 = ((KVPart<string, string>)comboBox1.SelectedItem).Key;
            string value = ((KVPart<string, string>)comboBox1.SelectedItem).Value;
            KVPart<string, string> toObject = new(name + "." + key + "." + key2, value);
            eventSetItem.ToObject = toObject;
            result = eventSetItem;
            base.DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataFile value = ((KVPart<string, DataFile>)comboBox3.SelectedItem).Value;
        comboBox2.Items.Clear();
        foreach (CShape item in value.ListAllShowCShape)
        {
            comboBox2.Items.Add(new KVPart<string, CShape>(item.Name, item));
        }
        if (comboBox2.Items.Count > 0)
        {
            comboBox2.SelectedIndex = 0;
        }
    }

    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        CShape value = ((KVPart<string, CShape>)comboBox2.SelectedItem).Value;
        object obj = ((value is not CControl) ? ((object)value) : ((object)(value as CControl)._c));
        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();
        comboBox1.Items.Clear();
        if (value is CControl)
        {
            DataSet dataSet = CEditEnvironmentGlobal.controlMembersSetting;
            DataTable dataTable = dataSet.Tables[type.Name];
            if (dataTable == null)
            {
                PropertyInfo[] array = properties;
                foreach (PropertyInfo propertyInfo in array)
                {
                    comboBox1.Items.Add(new KVPart<string, string>(propertyInfo.Name, propertyInfo.PropertyType.AssemblyQualifiedName));
                }
            }
            else
            {
                DataRow[] array2 = dataTable.Select("Type='属性'");
                for (int j = 0; j < array2.Length; j++)
                {
                    PropertyInfo[] array3 = properties;
                    foreach (PropertyInfo propertyInfo2 in array3)
                    {
                        if (propertyInfo2.Name == array2[j]["Name"].ToString() && Convert.ToBoolean(array2[j]["Visible"]))
                        {
                            comboBox1.Items.Add(new KVPart<string, string>(propertyInfo2.Name, propertyInfo2.PropertyType.AssemblyQualifiedName));
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            PropertyInfo[] array4 = properties;
            foreach (PropertyInfo propertyInfo3 in array4)
            {
                comboBox1.Items.Add(new KVPart<string, string>(propertyInfo3.Name, propertyInfo3.PropertyType.AssemblyQualifiedName));
            }
        }
        if (comboBox1.Items.Count > 0)
        {
            comboBox1.SelectedIndex = 0;
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent == "")
        {
            textBox2.Text = "true";
        }
        else
        {
            textBox2.Text = "[" + varTableEvent + "]";
        }
    }

    private void InitializeComponent()
    {
        this.button1 = new System.Windows.Forms.Button();
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.button2 = new System.Windows.Forms.Button();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.button3 = new System.Windows.Forms.Button();
        this.comboBox2 = new System.Windows.Forms.ComboBox();
        this.comboBox3 = new System.Windows.Forms.ComboBox();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.button4 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        this.button1.Location = new System.Drawing.Point(130, 128);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(75, 23);
        this.button1.TabIndex = 7;
        this.button1.Text = "保  存";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Location = new System.Drawing.Point(45, 37);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new System.Drawing.Size(462, 20);
        this.comboBox1.Sorted = true;
        this.comboBox1.TabIndex = 2;
        this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.textBox1.Location = new System.Drawing.Point(45, 88);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(429, 21);
        this.textBox1.TabIndex = 5;
        this.textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
        this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button2.Location = new System.Drawing.Point(324, 128);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(75, 23);
        this.button2.TabIndex = 8;
        this.button2.Text = "关  闭";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(12, 40);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(29, 12);
        this.label1.TabIndex = 3;
        this.label1.Text = "属性";
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(12, 92);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(17, 12);
        this.label2.TabIndex = 3;
        this.label2.Text = "值";
        this.button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.button3.Location = new System.Drawing.Point(480, 86);
        this.button3.Name = "button3";
        this.button3.Size = new System.Drawing.Size(27, 23);
        this.button3.TabIndex = 6;
        this.button3.Text = "...";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new System.EventHandler(button3_Click);
        this.comboBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox2.FormattingEnabled = true;
        this.comboBox2.Location = new System.Drawing.Point(232, 10);
        this.comboBox2.Name = "comboBox2";
        this.comboBox2.Size = new System.Drawing.Size(275, 20);
        this.comboBox2.TabIndex = 1;
        this.comboBox2.SelectedIndexChanged += new System.EventHandler(comboBox2_SelectedIndexChanged);
        this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox3.FormattingEnabled = true;
        this.comboBox3.Location = new System.Drawing.Point(45, 10);
        this.comboBox3.Name = "comboBox3";
        this.comboBox3.Size = new System.Drawing.Size(146, 20);
        this.comboBox3.TabIndex = 0;
        this.comboBox3.SelectedIndexChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(12, 13);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(29, 12);
        this.label3.TabIndex = 3;
        this.label3.Text = "页面";
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(197, 13);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(29, 12);
        this.label4.TabIndex = 3;
        this.label4.Text = "控件";
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(12, 64);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(29, 12);
        this.label5.TabIndex = 3;
        this.label5.Text = "条件";
        this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.textBox2.Location = new System.Drawing.Point(45, 61);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new System.Drawing.Size(429, 21);
        this.textBox2.TabIndex = 3;
        this.textBox2.Text = "true";
        this.textBox2.TextChanged += new System.EventHandler(textBox1_TextChanged);
        this.button4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.button4.Location = new System.Drawing.Point(480, 59);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(27, 23);
        this.button4.TabIndex = 4;
        this.button4.Text = "...";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(button4_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(528, 170);
        base.Controls.Add(this.label5);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.textBox2);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.comboBox3);
        base.Controls.Add(this.comboBox2);
        base.Controls.Add(this.comboBox1);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button4);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "EventSetProperty";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "控件属性赋值";
        base.Load += new System.EventHandler(EventSetProperty_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
