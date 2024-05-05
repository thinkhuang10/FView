using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using ShapeRuntime;

namespace HMIEditEnvironment;

public class EventSetFunction : Form
{
    private EventSetItem result;

    private IContainer components;

    private Label label3;

    private Label label4;

    private Label label1;

    private ComboBox comboBox3;

    private ComboBox comboBox2;

    private ComboBox comboBox1;

    private Button button2;

    private Label label5;

    private TextBox textBox2;

    private Button button4;

    private Button button5;

    private DataGridView dataGridView1;

    private DataGridViewTextBoxColumn Column1;

    private DataGridViewTextBoxColumn Column2;

    private Label label2;

    private TextBox textBox1;

    private Button button1;

    private ContextMenuStrip contextMenuStrip1;

    private ToolStripMenuItem 配置控件常用属性PToolStripMenuItem;

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

    public EventSetFunction()
    {
        InitializeComponent();
    }

    public EventSetFunction(EventSetItem item)
    {
        InitializeComponent();
        result = item;
    }

    private void EventSetFunction_Load(object sender, EventArgs e)
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
            textBox1.Text = result.Condition;
            textBox2.Text = result.ToObject.Key;
            foreach (KVPart<string, DataFile> item in comboBox3.Items)
            {
                if (!(item.Value.name == result.FromObject.Split('.')[0]))
                {
                    continue;
                }
                comboBox3.SelectedItem = item;
                comboBox2.Text = result.FromObject.Split('.')[1];
                {
                    foreach (KVPart<string, MethodInfo> item2 in comboBox1.Items)
                    {
                        if (!(item2.Value.Name == result.FromObject.Split('.')[2]))
                        {
                            continue;
                        }
                        ParameterInfo[] parameters = item2.Value.GetParameters();
                        if (parameters.Length != result.Paras.Count)
                        {
                            continue;
                        }
                        bool flag = false;
                        for (int i = 0; i < parameters.Length; i++)
                        {
                            if (parameters[i].ParameterType.AssemblyQualifiedName != result.Paras[i].Value)
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            comboBox1.SelectedItem = item2;
                            for (int j = 0; j < dataGridView1.Rows.Count; j++)
                            {
                                dataGridView1.Rows[j].Cells[1].Value = result.Paras[j].Key;
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
        catch
        {
        }
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
        MethodInfo[] methods = type.GetMethods();
        List<string> list = new();
        DataTable dataTable = new();
        dataTable = ((value is not CControl) ? CEditEnvironmentGlobal.controlMembersSetting.Tables[type.Name] : CEditEnvironmentGlobal.controlMembersSetting.Tables[type.Name]);
        bool flag = true;
        if (dataTable == null)
        {
            flag = false;
        }
        else
        {
            DataRow[] array = dataTable.Select("Type='方法'");
            for (int i = 0; i < array.Length; i++)
            {
                if (Convert.ToBoolean(array[i]["Visible"]))
                {
                    list.Add(array[i]["Name"].ToString());
                }
            }
        }
        comboBox1.Items.Clear();
        MethodInfo[] array2 = methods;
        foreach (MethodInfo methodInfo in array2)
        {
            if ((!flag || list.Contains(methodInfo.Name)) && !methodInfo.Name.StartsWith("get_") && !methodInfo.Name.StartsWith("set_") && !methodInfo.Name.StartsWith("remove_") && !methodInfo.Name.StartsWith("add_"))
            {
                string text = "";
                ParameterInfo[] parameters = methodInfo.GetParameters();
                ParameterInfo[] array3 = parameters;
                foreach (ParameterInfo parameterInfo in array3)
                {
                    string text2 = text;
                    text = text2 + parameterInfo.ParameterType.Name + " " + parameterInfo.Name + ",";
                }
                if (text.EndsWith(","))
                {
                    text = text.Substring(0, text.Length - 1);
                }
                comboBox1.Items.Add(new KVPart<string, MethodInfo>(methodInfo.ReturnType.Name + " " + methodInfo.Name + "(" + text + ")", methodInfo));
            }
        }
        if (comboBox1.Items.Count > 0)
        {
            comboBox1.SelectedIndex = 0;
        }
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (comboBox1.SelectedItem != null)
        {
            KVPart<string, MethodInfo> kVPart = (KVPart<string, MethodInfo>)comboBox1.SelectedItem;
            MethodInfo value = kVPart.Value;
            ParameterInfo[] parameters = value.GetParameters();
            dataGridView1.Rows.Clear();
            ParameterInfo[] array = parameters;
            foreach (ParameterInfo parameterInfo in array)
            {
                int index = dataGridView1.Rows.Add(parameterInfo.ParameterType.Name + " " + parameterInfo.Name, "");
                dataGridView1.Rows[index].Tag = parameterInfo.ParameterType.AssemblyQualifiedName;
            }
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent == "")
        {
            textBox2.Text = "";
        }
        else
        {
            textBox2.Text = varTableEvent;
        }
    }

    private void button4_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
        {
            if (item2.Cells[1].Value.ToString() == "")
            {
                MessageBox.Show("请输入调用方法的参数.");
                return;
            }
        }
        EventSetItem eventSetItem = new()
        {
            OperationType = "方法调用",
            Paras = new List<KVPart<string, string>>()
        };
        foreach (DataGridViewRow item3 in (IEnumerable)dataGridView1.Rows)
        {
            KVPart<string, string> item = new(item3.Cells[1].Value.ToString(), item3.Tag.ToString());
            eventSetItem.Paras.Add(item);
        }
        string name = ((KVPart<string, DataFile>)comboBox3.SelectedItem).Value.name;
        string key = ((KVPart<string, CShape>)comboBox2.SelectedItem).Key;
        MethodInfo val = ((KVPart<string, MethodInfo>)comboBox1.SelectedItem).val;
        string name2 = val.Name;
        if (val.ReturnType == typeof(void))
        {
            eventSetItem.ToObject = new KVPart<string, string>("", "System.Object");
        }
        else
        {
            eventSetItem.ToObject = new KVPart<string, string>(textBox2.Text, "System.Object");
        }
        eventSetItem.FromObject = name + "." + key + "." + name2;
        eventSetItem.Condition = textBox1.Text;
        result = eventSetItem;
        base.DialogResult = DialogResult.OK;
        Close();
    }

    private void button5_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent != "")
        {
            dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "[" + varTableEvent + "]";
        }
    }

    private void button1_Click(object sender, EventArgs e)
    {
        string varTableEvent = CForDCCEControl.GetVarTableEvent("");
        if (varTableEvent == "")
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "true";
            }
        }
        else
        {
            textBox1.Text = "[" + varTableEvent + "]";
        }
    }

    private void 配置控件常用属性PToolStripMenuItem_Click(object sender, EventArgs e)
    {
        ControlInfoSetting controlInfoSetting = new();
        controlInfoSetting.ShowDialog();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        label3 = new System.Windows.Forms.Label();
        contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
        配置控件常用属性PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        label4 = new System.Windows.Forms.Label();
        label1 = new System.Windows.Forms.Label();
        comboBox3 = new System.Windows.Forms.ComboBox();
        comboBox2 = new System.Windows.Forms.ComboBox();
        comboBox1 = new System.Windows.Forms.ComboBox();
        button2 = new System.Windows.Forms.Button();
        label5 = new System.Windows.Forms.Label();
        textBox2 = new System.Windows.Forms.TextBox();
        button4 = new System.Windows.Forms.Button();
        button5 = new System.Windows.Forms.Button();
        dataGridView1 = new System.Windows.Forms.DataGridView();
        Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        label2 = new System.Windows.Forms.Label();
        textBox1 = new System.Windows.Forms.TextBox();
        button1 = new System.Windows.Forms.Button();
        contextMenuStrip1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        base.SuspendLayout();
        label3.AutoSize = true;
        label3.ContextMenuStrip = contextMenuStrip1;
        label3.Location = new System.Drawing.Point(12, 9);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(29, 12);
        label3.TabIndex = 7;
        label3.Text = "页面";
        contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { 配置控件常用属性PToolStripMenuItem });
        contextMenuStrip1.Name = "contextMenuStrip1";
        contextMenuStrip1.Size = new System.Drawing.Size(188, 26);
        配置控件常用属性PToolStripMenuItem.Name = "配置控件常用属性PToolStripMenuItem";
        配置控件常用属性PToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
        配置控件常用属性PToolStripMenuItem.Text = "配置控件常用属性(&P)";
        配置控件常用属性PToolStripMenuItem.Click += new System.EventHandler(配置控件常用属性PToolStripMenuItem_Click);
        label4.AutoSize = true;
        label4.ContextMenuStrip = contextMenuStrip1;
        label4.Location = new System.Drawing.Point(211, 9);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(29, 12);
        label4.TabIndex = 8;
        label4.Text = "控件";
        label1.AutoSize = true;
        label1.ContextMenuStrip = contextMenuStrip1;
        label1.Location = new System.Drawing.Point(12, 36);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(29, 12);
        label1.TabIndex = 9;
        label1.Text = "方法";
        comboBox3.ContextMenuStrip = contextMenuStrip1;
        comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox3.FormattingEnabled = true;
        comboBox3.Location = new System.Drawing.Point(59, 6);
        comboBox3.Name = "comboBox3";
        comboBox3.Size = new System.Drawing.Size(146, 20);
        comboBox3.TabIndex = 0;
        comboBox3.SelectedIndexChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
        comboBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        comboBox2.ContextMenuStrip = contextMenuStrip1;
        comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox2.FormattingEnabled = true;
        comboBox2.Location = new System.Drawing.Point(246, 6);
        comboBox2.Name = "comboBox2";
        comboBox2.Size = new System.Drawing.Size(247, 20);
        comboBox2.TabIndex = 1;
        comboBox2.SelectedIndexChanged += new System.EventHandler(comboBox2_SelectedIndexChanged);
        comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        comboBox1.ContextMenuStrip = contextMenuStrip1;
        comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Location = new System.Drawing.Point(59, 33);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(434, 20);
        comboBox1.TabIndex = 2;
        comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
        button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button2.ContextMenuStrip = contextMenuStrip1;
        button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button2.Location = new System.Drawing.Point(464, 82);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(27, 23);
        button2.TabIndex = 6;
        button2.Text = "...";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        label5.AutoSize = true;
        label5.ContextMenuStrip = contextMenuStrip1;
        label5.Location = new System.Drawing.Point(12, 87);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(41, 12);
        label5.TabIndex = 9;
        label5.Text = "返回值";
        textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        textBox2.ContextMenuStrip = contextMenuStrip1;
        textBox2.Location = new System.Drawing.Point(59, 84);
        textBox2.Name = "textBox2";
        textBox2.ReadOnly = true;
        textBox2.Size = new System.Drawing.Size(401, 21);
        textBox2.TabIndex = 5;
        button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        button4.ContextMenuStrip = contextMenuStrip1;
        button4.Location = new System.Drawing.Point(78, 406);
        button4.Name = "button4";
        button4.Size = new System.Drawing.Size(75, 23);
        button4.TabIndex = 8;
        button4.Text = "保  存";
        button4.UseVisualStyleBackColor = true;
        button4.Click += new System.EventHandler(button4_Click);
        button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        button5.ContextMenuStrip = contextMenuStrip1;
        button5.Location = new System.Drawing.Point(347, 406);
        button5.Name = "button5";
        button5.Size = new System.Drawing.Size(75, 23);
        button5.TabIndex = 9;
        button5.Text = "关  闭";
        button5.UseVisualStyleBackColor = true;
        button5.Click += new System.EventHandler(button5_Click);
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.AllowUserToResizeRows = false;
        dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        dataGridView1.BackgroundColor = System.Drawing.Color.White;
        dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Columns.AddRange(Column1, Column2);
        dataGridView1.ContextMenuStrip = contextMenuStrip1;
        dataGridView1.Location = new System.Drawing.Point(12, 111);
        dataGridView1.MultiSelect = false;
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersVisible = false;
        dataGridView1.RowTemplate.Height = 23;
        dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.Size = new System.Drawing.Size(479, 279);
        dataGridView1.TabIndex = 7;
        dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
        Column1.HeaderText = "参数";
        Column1.Name = "Column1";
        Column1.ReadOnly = true;
        Column2.HeaderText = "值";
        Column2.Name = "Column2";
        label2.AutoSize = true;
        label2.ContextMenuStrip = contextMenuStrip1;
        label2.Location = new System.Drawing.Point(12, 60);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(29, 12);
        label2.TabIndex = 9;
        label2.Text = "条件";
        textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        textBox1.ContextMenuStrip = contextMenuStrip1;
        textBox1.Location = new System.Drawing.Point(59, 57);
        textBox1.Name = "textBox1";
        textBox1.Size = new System.Drawing.Size(401, 21);
        textBox1.TabIndex = 3;
        textBox1.Text = "true";
        button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        button1.ContextMenuStrip = contextMenuStrip1;
        button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        button1.Location = new System.Drawing.Point(464, 55);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(27, 23);
        button1.TabIndex = 4;
        button1.Text = "...";
        button1.UseVisualStyleBackColor = true;
        button1.Click += new System.EventHandler(button1_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(507, 444);
        ContextMenuStrip = contextMenuStrip1;
        base.Controls.Add(dataGridView1);
        base.Controls.Add(button5);
        base.Controls.Add(button4);
        base.Controls.Add(textBox1);
        base.Controls.Add(textBox2);
        base.Controls.Add(button1);
        base.Controls.Add(button2);
        base.Controls.Add(label3);
        base.Controls.Add(label4);
        base.Controls.Add(label5);
        base.Controls.Add(label2);
        base.Controls.Add(label1);
        base.Controls.Add(comboBox3);
        base.Controls.Add(comboBox2);
        base.Controls.Add(comboBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "EventSetFunction";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "调用控件方法";
        base.Load += new System.EventHandler(EventSetFunction_Load);
        contextMenuStrip1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
