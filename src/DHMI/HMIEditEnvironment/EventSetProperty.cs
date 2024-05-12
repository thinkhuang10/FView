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
        comboBox3.Text = CEditEnvironmentGlobal.ChildForm.theglobal.df.pageName;
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
        button1 = new System.Windows.Forms.Button();
        comboBox1 = new System.Windows.Forms.ComboBox();
        textBox1 = new System.Windows.Forms.TextBox();
        button2 = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        button3 = new System.Windows.Forms.Button();
        comboBox2 = new System.Windows.Forms.ComboBox();
        comboBox3 = new System.Windows.Forms.ComboBox();
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        textBox2 = new System.Windows.Forms.TextBox();
        button4 = new System.Windows.Forms.Button();
        base.SuspendLayout();
        button1.Location = new System.Drawing.Point(130, 128);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(75, 23);
        button1.TabIndex = 7;
        button1.Text = "保  存";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new System.Drawing.Point(45, 37);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(462, 20);
        comboBox1.Sorted = true;
        comboBox1.TabIndex = 2;
        textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        textBox1.Location = new System.Drawing.Point(45, 88);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(429, 21);
        textBox1.TabIndex = 5;
        textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
        button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button2.Location = new System.Drawing.Point(324, 128);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 8;
        button2.Text = "关  闭";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(12, 40);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(29, 12);
        label1.TabIndex = 3;
        label1.Text = "属性";
        label2.AutoSize = true;
        label2.Location = new System.Drawing.Point(12, 92);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(17, 12);
        label2.TabIndex = 3;
        label2.Text = "值";
        button3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button3.Location = new System.Drawing.Point(480, 86);
        button3.Name = "button3";
        button3.Size = new System.Drawing.Size(27, 23);
        button3.TabIndex = 6;
        button3.Text = "...";
        button3.UseVisualStyleBackColor = true;
        button3.Click += new System.EventHandler(button3_Click);
        comboBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox2.FormattingEnabled = true;
        comboBox2.Location = new System.Drawing.Point(232, 10);
        comboBox2.Name = "comboBox2";
        comboBox2.Size = new System.Drawing.Size(275, 20);
        comboBox2.TabIndex = 1;
        comboBox2.SelectedIndexChanged += new System.EventHandler(comboBox2_SelectedIndexChanged);
        comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox3.FormattingEnabled = true;
        comboBox3.Location = new System.Drawing.Point(45, 10);
        comboBox3.Name = "comboBox3";
        comboBox3.Size = new System.Drawing.Size(146, 20);
        comboBox3.TabIndex = 0;
        comboBox3.SelectedIndexChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
        label3.AutoSize = true;
        label3.Location = new System.Drawing.Point(12, 13);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(29, 12);
        label3.TabIndex = 3;
        label3.Text = "页面";
        label4.AutoSize = true;
        label4.Location = new System.Drawing.Point(197, 13);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(29, 12);
        label4.TabIndex = 3;
        label4.Text = "控件";
        label5.AutoSize = true;
        label5.Location = new System.Drawing.Point(12, 64);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(29, 12);
        label5.TabIndex = 3;
        label5.Text = "条件";
        textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        textBox2.Location = new System.Drawing.Point(45, 61);
        textBox2.Name = "textBox2";
        textBox2.Size = new System.Drawing.Size(429, 21);
        textBox2.TabIndex = 3;
        textBox2.Text = "true";
        textBox2.TextChanged += new System.EventHandler(textBox1_TextChanged);
        button4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button4.Location = new System.Drawing.Point(480, 59);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(27, 23);
        button4.TabIndex = 4;
        button4.Text = "...";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(button4_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(528, 170);
        base.Controls.Add(label5);
        base.Controls.Add(label2);
        base.Controls.Add(label3);
        base.Controls.Add(label4);
        base.Controls.Add(label1);
        base.Controls.Add(textBox2);
        base.Controls.Add(textBox1);
        base.Controls.Add(comboBox3);
        base.Controls.Add(comboBox2);
        base.Controls.Add(comboBox1);
        base.Controls.Add(button2);
        base.Controls.Add(button4);
        base.Controls.Add(button3);
        base.Controls.Add(button1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.MaximizeBox = false;
        base.Name = "EventSetProperty";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "控件属性赋值";
        base.Load += new System.EventHandler(EventSetProperty_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
