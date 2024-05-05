using CommonSnappableTypes;
using ShapeRuntime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace HMIEditEnvironment;

public class ControlInfoSetting : Form
{
    private readonly DataSet systemSetting = CEditEnvironmentGlobal.systemControlMembersSetting;

    private readonly DataSet setting = CEditEnvironmentGlobal.controlMembersSetting;

    private Type t;

    private Button button2;

    private DataGridView dataGridView1;

    private ComboBox comboBox1;

    private Label label1;

    private DataGridViewCheckBoxColumn Column1;

    private DataGridViewTextBoxColumn Column2;

    private DataGridViewTextBoxColumn Column3;

    private DataGridViewTextBoxColumn Column4;

    public ControlInfoSetting()
    {
        InitializeComponent();
        comboBox1.SelectedIndex = 0;
    }

    public ControlInfoSetting(string ctrlTypeName)
    {
        InitializeComponent();

        if (ctrlTypeName == typeof(CButton).Name)
        {
            comboBox1.SelectedIndex = 0;
        }
        else if (ctrlTypeName == typeof(CCheckBox).Name)
        {
            comboBox1.SelectedIndex = 1;
        }
        else if (ctrlTypeName == typeof(CLabel).Name)
        {
            comboBox1.SelectedIndex = 2;
        }
        else if (ctrlTypeName == typeof(CTextBox).Name)
        {
            comboBox1.SelectedIndex = 3;
        }
        else if (ctrlTypeName == typeof(CComboBox).Name)
        {
            comboBox1.SelectedIndex = 4;
        }
        else if (ctrlTypeName == typeof(CDateTimePicker).Name)
        {
            comboBox1.SelectedIndex = 5;
        }
        else if (ctrlTypeName == typeof(CPictureBox).Name)
        {
            comboBox1.SelectedIndex = 6;
        }
        else if (ctrlTypeName == typeof(CGroupBox).Name)
        {
            comboBox1.SelectedIndex = 7;
        }
        else if (ctrlTypeName == typeof(CDataGridView).Name)
        {
            comboBox1.SelectedIndex = 8;
        }
        else if (ctrlTypeName == typeof(CListBox).Name)
        {
            comboBox1.SelectedIndex = 9;
        }
        else if (ctrlTypeName == typeof(CTimer).Name)
        {
            comboBox1.SelectedIndex = 10;
        }
        else
        {
            comboBox1.SelectedIndex = 0;
        }
    }

    private void ControlInfoSetting_Load(object sender, EventArgs e)
    {
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (t != null)
        {
            if (!setting.Tables.Contains(t.Name))
            {
                DataTable dataTable = new(t.Name);
                dataTable.Columns.Add("Name");
                dataTable.Columns.Add("Type");
                dataTable.Columns.Add("Visible");
                dataTable.Columns.Add("Description");
                setting.Tables.Add(dataTable);
            }
            if (setting.Tables.Contains(t.Name))
            {
                DataTable dataTable2 = setting.Tables[t.Name];
                dataTable2.Rows.Clear();
                if (!dataTable2.Columns.Contains("Description"))
                {
                    dataTable2.Columns.Add("Description");
                }
                foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
                {
                    DataRow dataRow = dataTable2.NewRow();
                    dataRow["Name"] = item.Cells[1].Value;
                    dataRow["Type"] = item.Cells[2].Value;
                    dataRow["Visible"] = item.Cells[0].Value;
                    dataRow["Description"] = item.Cells[3].Value;
                    dataTable2.Rows.Add(dataRow);
                }
            }
        }
        Close();
    }

    private void ControlInfoSetting_FormClosing(object sender, FormClosingEventArgs e)
    {
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (t != null)
        {
            if (!setting.Tables.Contains(t.Name))
            {
                DataTable dataTable = new(t.Name);
                dataTable.Columns.Add("Name");
                dataTable.Columns.Add("Type");
                dataTable.Columns.Add("Visible");
                dataTable.Columns.Add("Description");
                setting.Tables.Add(dataTable);
            }
            DataTable dataTable2 = setting.Tables[t.Name];
            dataTable2.Rows.Clear();
            if (!dataTable2.Columns.Contains("Description"))
            {
                dataTable2.Columns.Add("Description");
            }
            foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
            {
                DataRow dataRow = dataTable2.NewRow();
                dataRow["Name"] = item.Cells[1].Value;
                dataRow["Type"] = item.Cells[2].Value;
                dataRow["Visible"] = item.Cells[0].Value;
                dataRow["Description"] = item.Cells[3].Value;
                dataTable2.Rows.Add(dataRow);
            }
        }
        dataGridView1.Rows.Clear();
        if ((string)comboBox1.SelectedItem == "按钮")
        {
            t = typeof(CButton);
        }
        else if ((string)comboBox1.SelectedItem == "多选框")
        {
            t = typeof(CCheckBox);
        }
        else if ((string)comboBox1.SelectedItem == "标签")
        {
            t = typeof(CLabel);
        }
        else if ((string)comboBox1.SelectedItem == "文本框")
        {
            t = typeof(CTextBox);
        }
        else if ((string)comboBox1.SelectedItem == "下拉列表")
        {
            t = typeof(CComboBox);
        }
        else if ((string)comboBox1.SelectedItem == "日历控件")
        {
            t = typeof(CDateTimePicker);
        }
        else if ((string)comboBox1.SelectedItem == "图片框")
        {
            t = typeof(CPictureBox);
        }
        else if ((string)comboBox1.SelectedItem == "分组框")
        {
            t = typeof(CGroupBox);
        }
        else if ((string)comboBox1.SelectedItem == "数据视图")
        {
            t = typeof(CDataGridView);
        }
        else if ((string)comboBox1.SelectedItem == "列表框")
        {
            t = typeof(CListBox);
        }
        else if ((string)comboBox1.SelectedItem == "定时器")
        {
            t = typeof(CTimer);
        }
        DataTable dataTable3 = systemSetting.Tables[t.Name];
        if (dataTable3 != null)
        {
            dataTable3.PrimaryKey = new DataColumn[1] { dataTable3.Columns["Name"] };
        }
        List<string> list = new();
        if (dataTable3 == null)
        {
            return;
        }
        PropertyInfo[] properties = t.GetProperties();
        foreach (MemberInfo minfo in properties)
        {
            if (!Attribute.IsDefined(minfo, typeof(DHMIHidePropertyAttribute)) && (!Attribute.IsDefined(minfo, typeof(BrowsableAttribute)) || ((BrowsableAttribute)Attribute.GetCustomAttribute(minfo, typeof(BrowsableAttribute))).Browsable))
            {
                string text = "";
                if (Attribute.IsDefined(minfo, typeof(DescriptionAttribute)))
                {
                    text = ((DescriptionAttribute)Attribute.GetCustomAttributes(minfo, typeof(DescriptionAttribute))[0]).Description;
                }
                DataRow dataRow2 = dataTable3.Rows.Find(minfo.Name.ToString());
                if (dataRow2 != null && list.FindIndex((string item) => minfo.Name.Equals(item.Split('|')[0])) == -1 && Convert.ToBoolean(dataRow2["Visible"].ToString()))
                {
                    dataGridView1.Rows.Add(true, minfo.Name, "属性", text);
                    list.Add(minfo.Name + "|属性|" + text);
                }
            }
        }
        EventInfo[] array = t.GetEvents();
        foreach (MemberInfo memberInfo in array)
        {
            string text2 = "";
            if (Attribute.IsDefined(memberInfo, typeof(DescriptionAttribute)))
            {
                text2 = ((DescriptionAttribute)Attribute.GetCustomAttributes(memberInfo, typeof(DescriptionAttribute))[0]).Description;
            }
            DataRow dataRow2 = dataTable3.Rows.Find(memberInfo.Name.ToString());
            if (dataRow2 != null && !list.Contains(memberInfo.Name + "|事件|" + text2) && Convert.ToBoolean(dataRow2["Visible"].ToString()))
            {
                dataGridView1.Rows.Add(true, memberInfo.Name, "事件", text2);
                list.Add(memberInfo.Name + "|事件|" + text2);
            }
        }
        MethodInfo[] methods = t.GetMethods();
        foreach (MemberInfo memberInfo2 in methods)
        {
            if (memberInfo2.MemberType == MemberTypes.Method)
            {
                MethodInfo methodInfo = memberInfo2 as MethodInfo;
                if (!methodInfo.IsPublic || methodInfo.Name.StartsWith("get_") || methodInfo.Name.StartsWith("set_") || methodInfo.Name.StartsWith("add_") || methodInfo.Name.StartsWith("remove"))
                {
                    continue;
                }
            }
            string text3 = "";
            if (Attribute.IsDefined(memberInfo2, typeof(DescriptionAttribute)))
            {
                text3 = ((DescriptionAttribute)Attribute.GetCustomAttributes(memberInfo2, typeof(DescriptionAttribute))[0]).Description;
            }
            DataRow dataRow2 = dataTable3.Rows.Find(memberInfo2.Name.ToString());
            if (dataRow2 != null && !list.Contains(memberInfo2.Name + "|方法|" + text3) && Convert.ToBoolean(dataRow2["Visible"].ToString()))
            {
                dataGridView1.Rows.Add(true, memberInfo2.Name, "方法", text3);
                list.Add(memberInfo2.Name + "|方法|" + text3);
            }
        }
        if (!setting.Tables.Contains(t.Name))
        {
            return;
        }
        DataTable dataTable4 = setting.Tables[t.Name];
        if (!dataTable4.Columns.Contains("Description"))
        {
            dataTable4.Columns.Add("Description");
        }
        Dictionary<string, object> dictionary = new();
        Dictionary<string, object> dictionary2 = new();
        foreach (DataRow row in dataTable4.Rows)
        {
            if (!dictionary.ContainsKey(row["Name"].ToString() + "|" + row["Type"].ToString()))
            {
                dictionary.Add(row["Name"].ToString() + "|" + row["Type"].ToString(), row["Visible"]);
            }
            if (!dictionary2.ContainsKey(row["Name"].ToString() + "|" + row["Type"].ToString()))
            {
                dictionary2.Add(row["Name"].ToString() + "|" + row["Type"].ToString(), row["Description"]);
            }
        }
        foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
        {
            if (dictionary.ContainsKey(item2.Cells[1].Value.ToString() + "|" + item2.Cells[2].Value.ToString()))
            {
                item2.Cells[0].Value = Convert.ToBoolean(dictionary[item2.Cells[1].Value.ToString() + "|" + item2.Cells[2].Value.ToString()]);
                if (dictionary2[item2.Cells[1].Value.ToString() + "|" + item2.Cells[2].Value.ToString()] != null && dictionary2[item2.Cells[1].Value.ToString() + "|" + item2.Cells[2].Value.ToString()] != null && dictionary2[item2.Cells[1].Value.ToString() + "|" + item2.Cells[2].Value.ToString()] != DBNull.Value)
                {
                    item2.Cells[3].Value = dictionary2[item2.Cells[1].Value.ToString() + "|" + item2.Cells[2].Value.ToString()];
                }
            }
            else
            {
                item2.Cells[0].Value = true;
            }
        }
    }

    private void InitializeComponent()
    {
        button2 = new System.Windows.Forms.Button();
        dataGridView1 = new System.Windows.Forms.DataGridView();
        Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
        Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
        comboBox1 = new System.Windows.Forms.ComboBox();
        label1 = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        base.SuspendLayout();
        button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        button2.Location = new System.Drawing.Point(500, 344);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(75, 23);
        button2.TabIndex = 0;
        button2.Text = "关闭";
        button2.UseVisualStyleBackColor = true;
        button2.Click += new System.EventHandler(button2_Click);
        dataGridView1.AllowUserToAddRows = false;
        dataGridView1.AllowUserToDeleteRows = false;
        dataGridView1.AllowUserToOrderColumns = true;
        dataGridView1.AllowUserToResizeRows = false;
        dataGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
        dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Columns.AddRange(Column1, Column2, Column3, Column4);
        dataGridView1.Location = new System.Drawing.Point(13, 38);
        dataGridView1.MultiSelect = false;
        dataGridView1.Name = "dataGridView1";
        dataGridView1.RowHeadersVisible = false;
        dataGridView1.RowTemplate.Height = 23;
        dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        dataGridView1.Size = new System.Drawing.Size(562, 300);
        dataGridView1.TabIndex = 1;
        Column1.FillWeight = 46.57937f;
        Column1.HeaderText = "";
        Column1.Name = "Column1";
        Column1.Width = 5;
        Column2.FillWeight = 99.09089f;
        Column2.HeaderText = "名称";
        Column2.Name = "Column2";
        Column2.ReadOnly = true;
        Column2.Width = 54;
        Column3.FillWeight = 72.46218f;
        Column3.HeaderText = "类型";
        Column3.Name = "Column3";
        Column3.ReadOnly = true;
        Column3.Width = 54;
        Column4.FillWeight = 99.09089f;
        Column4.HeaderText = "说明";
        Column4.Name = "Column4";
        Column4.ReadOnly = true;
        Column4.Width = 54;
        comboBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
        comboBox1.DisplayMember = "按钮";
        comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        comboBox1.FormattingEnabled = true;
        comboBox1.Items.AddRange(new object[11]
        {
            "按钮", "多选框", "标签", "文本框", "下拉列表", "日历控件", "图片框", "分组框", "数据视图", "列表框",
            "定时器"
        });
        comboBox1.Location = new System.Drawing.Point(77, 12);
        comboBox1.Name = "comboBox1";
        comboBox1.Size = new System.Drawing.Size(498, 20);
        comboBox1.TabIndex = 2;
        comboBox1.SelectedIndexChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);
        label1.AutoSize = true;
        label1.Location = new System.Drawing.Point(12, 15);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(59, 12);
        label1.TabIndex = 3;
        label1.Text = "控件名称:";
        base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
        base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        base.ClientSize = new System.Drawing.Size(587, 373);
        base.Controls.Add(label1);
        base.Controls.Add(comboBox1);
        base.Controls.Add(dataGridView1);
        base.Controls.Add(button2);
        base.Name = "ControlInfoSetting";
        base.ShowIcon = false;
        Text = "控件常用属性事件方法设置";
        base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ControlInfoSetting_FormClosing);
        base.Load += new System.EventHandler(ControlInfoSetting_Load);
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}
