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
        object obj = ((!(value is CControl)) ? ((object)value) : ((object)(value as CControl)._c));
        Type type = obj.GetType();
        MethodInfo[] methods = type.GetMethods();
        List<string> list = new();
        DataTable dataTable = new();
        dataTable = ((!(value is CControl)) ? CEditEnvironmentGlobal.controlMembersSetting.Tables[type.Name] : CEditEnvironmentGlobal.controlMembersSetting.Tables[type.Name]);
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
        this.components = new System.ComponentModel.Container();
        this.label3 = new System.Windows.Forms.Label();
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.配置控件常用属性PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.label4 = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.comboBox3 = new System.Windows.Forms.ComboBox();
        this.comboBox2 = new System.Windows.Forms.ComboBox();
        this.comboBox1 = new System.Windows.Forms.ComboBox();
        this.button2 = new System.Windows.Forms.Button();
        this.label5 = new System.Windows.Forms.Label();
        this.textBox2 = new System.Windows.Forms.TextBox();
        this.button4 = new System.Windows.Forms.Button();
        this.button5 = new System.Windows.Forms.Button();
        this.dataGridView1 = new System.Windows.Forms.DataGridView();
        this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.label2 = new System.Windows.Forms.Label();
        this.textBox1 = new System.Windows.Forms.TextBox();
        this.button1 = new System.Windows.Forms.Button();
        this.contextMenuStrip1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
        base.SuspendLayout();
        this.label3.AutoSize = true;
        this.label3.ContextMenuStrip = this.contextMenuStrip1;
        this.label3.Location = new System.Drawing.Point(12, 9);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(29, 12);
        this.label3.TabIndex = 7;
        this.label3.Text = "页面";
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.配置控件常用属性PToolStripMenuItem });
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new System.Drawing.Size(188, 26);
        this.配置控件常用属性PToolStripMenuItem.Name = "配置控件常用属性PToolStripMenuItem";
        this.配置控件常用属性PToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
        this.配置控件常用属性PToolStripMenuItem.Text = "配置控件常用属性(&P)";
        this.配置控件常用属性PToolStripMenuItem.Click += new System.EventHandler(配置控件常用属性PToolStripMenuItem_Click);
        this.label4.AutoSize = true;
        this.label4.ContextMenuStrip = this.contextMenuStrip1;
        this.label4.Location = new System.Drawing.Point(211, 9);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(29, 12);
        this.label4.TabIndex = 8;
        this.label4.Text = "控件";
        this.label1.AutoSize = true;
        this.label1.ContextMenuStrip = this.contextMenuStrip1;
        this.label1.Location = new System.Drawing.Point(12, 36);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(29, 12);
        this.label1.TabIndex = 9;
        this.label1.Text = "方法";
        this.comboBox3.ContextMenuStrip = this.contextMenuStrip1;
        this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox3.FormattingEnabled = true;
        this.comboBox3.Location = new System.Drawing.Point(59, 6);
        this.comboBox3.Name = "comboBox3";
        this.comboBox3.Size = new System.Drawing.Size(146, 20);
        this.comboBox3.TabIndex = 0;
        this.comboBox3.SelectedIndexChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
        this.comboBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.comboBox2.ContextMenuStrip = this.contextMenuStrip1;
        this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox2.FormattingEnabled = true;
        this.comboBox2.Location = new System.Drawing.Point(246, 6);
        this.comboBox2.Name = "comboBox2";
        this.comboBox2.Size = new System.Drawing.Size(247, 20);
        this.comboBox2.TabIndex = 1;
        this.comboBox2.SelectedIndexChanged += new System.EventHandler(comboBox2_SelectedIndexChanged);
        this.comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.comboBox1.ContextMenuStrip = this.contextMenuStrip1;
        this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Location = new System.Drawing.Point(59, 33);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new System.Drawing.Size(434, 20);
        this.comboBox1.TabIndex = 2;
        this.comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
        this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button2.ContextMenuStrip = this.contextMenuStrip1;
        this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.button2.Location = new System.Drawing.Point(464, 82);
        this.button2.Name = "button2";
        this.button2.Size = new System.Drawing.Size(27, 23);
        this.button2.TabIndex = 6;
        this.button2.Text = "...";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new System.EventHandler(button2_Click);
        this.label5.AutoSize = true;
        this.label5.ContextMenuStrip = this.contextMenuStrip1;
        this.label5.Location = new System.Drawing.Point(12, 87);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(41, 12);
        this.label5.TabIndex = 9;
        this.label5.Text = "返回值";
        this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.textBox2.ContextMenuStrip = this.contextMenuStrip1;
        this.textBox2.Location = new System.Drawing.Point(59, 84);
        this.textBox2.Name = "textBox2";
        this.textBox2.ReadOnly = true;
        this.textBox2.Size = new System.Drawing.Size(401, 21);
        this.textBox2.TabIndex = 5;
        this.button4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
        this.button4.ContextMenuStrip = this.contextMenuStrip1;
        this.button4.Location = new System.Drawing.Point(78, 406);
        this.button4.Name = "button4";
        this.button4.Size = new System.Drawing.Size(75, 23);
        this.button4.TabIndex = 8;
        this.button4.Text = "保  存";
        this.button4.UseVisualStyleBackColor = true;
        this.button4.Click += new System.EventHandler(button4_Click);
        this.button5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.button5.ContextMenuStrip = this.contextMenuStrip1;
        this.button5.Location = new System.Drawing.Point(347, 406);
        this.button5.Name = "button5";
        this.button5.Size = new System.Drawing.Size(75, 23);
        this.button5.TabIndex = 9;
        this.button5.Text = "关  闭";
        this.button5.UseVisualStyleBackColor = true;
        this.button5.Click += new System.EventHandler(button5_Click);
        this.dataGridView1.AllowUserToAddRows = false;
        this.dataGridView1.AllowUserToDeleteRows = false;
        this.dataGridView1.AllowUserToResizeRows = false;
        this.dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
        this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
        this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dataGridView1.Columns.AddRange(this.Column1, this.Column2);
        this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
        this.dataGridView1.Location = new System.Drawing.Point(12, 111);
        this.dataGridView1.MultiSelect = false;
        this.dataGridView1.Name = "dataGridView1";
        this.dataGridView1.RowHeadersVisible = false;
        this.dataGridView1.RowTemplate.Height = 23;
        this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.dataGridView1.Size = new System.Drawing.Size(479, 279);
        this.dataGridView1.TabIndex = 7;
        this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
        this.Column1.HeaderText = "参数";
        this.Column1.Name = "Column1";
        this.Column1.ReadOnly = true;
        this.Column2.HeaderText = "值";
        this.Column2.Name = "Column2";
        this.label2.AutoSize = true;
        this.label2.ContextMenuStrip = this.contextMenuStrip1;
        this.label2.Location = new System.Drawing.Point(12, 60);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(29, 12);
        this.label2.TabIndex = 9;
        this.label2.Text = "条件";
        this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        this.textBox1.ContextMenuStrip = this.contextMenuStrip1;
        this.textBox1.Location = new System.Drawing.Point(59, 57);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new System.Drawing.Size(401, 21);
        this.textBox1.TabIndex = 3;
        this.textBox1.Text = "true";
        this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        this.button1.ContextMenuStrip = this.contextMenuStrip1;
        this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.button1.Location = new System.Drawing.Point(464, 55);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(27, 23);
        this.button1.TabIndex = 4;
        this.button1.Text = "...";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(button1_Click);
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(507, 444);
        this.ContextMenuStrip = this.contextMenuStrip1;
        base.Controls.Add(this.dataGridView1);
        base.Controls.Add(this.button5);
        base.Controls.Add(this.button4);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.textBox2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.label5);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.label1);
        base.Controls.Add(this.comboBox3);
        base.Controls.Add(this.comboBox2);
        base.Controls.Add(this.comboBox1);
        base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        base.Name = "EventSetFunction";
        base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "调用控件方法";
        base.Load += new System.EventHandler(EventSetFunction_Load);
        this.contextMenuStrip1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
